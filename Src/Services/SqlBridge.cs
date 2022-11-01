using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using NoteBlock.Src.Models;

namespace NoteBlock.Src.Services
{
    public class SqlBridge
    {
        public string TargetIP;
        public string TargetDB;
        public TrustedConnection TrustedConnection;

        private SqlConnection SqlConn;


        /// <summary>
        /// Returns the current connection string, used to connect to the database
        /// </summary>
        public string ConnectionString
        {
            get { return $"Server={TargetIP};Database={TargetDB};Trusted_Connection={TrustedConnection}"; }
            private set { }
        }


        /// <summary>
        /// Constructs an instance of the SqlBridge class, and sets the fields to the correct elements
        /// </summary>
        /// <param name="targetIP"> Target server of the database </param>
        /// <param name="targetDB"> Target database on the server </param>
        /// <param name="isTrusted"> Whether the connection is trusted or not </param>
        public SqlBridge( string targetIP, string targetDB, TrustedConnection isTrusted=TrustedConnection.False )
        {
            TargetIP = targetIP;
            TargetDB = targetDB;
            TrustedConnection = isTrusted;
        }


        /// <summary>
        /// Attemts a connection to the database
        /// </summary>
        /// <returns> Whether the connection attemt succeded or not </returns>
        public bool ConnectToDatabase()
        {
            SqlConn = new SqlConnection(ConnectionString);
            SqlConn.Open();


            if (SqlConn.State != ConnectionState.Open)
                return false;
            return true;
        }


        /// <summary>
        /// Gets all the database entrys names, used to load the current list of saved notes
        /// </summary>
        /// <returns> An array containg all the names </returns>
        public List<string> GetAllDatebaseEntryIdentifiers()
        {
            // Creates a command string for the database querry, then creates an instance of the
            // SqlCommand class to be sent to the database as querry
            string command = "select noteName from Notes";
            using (SqlCommand sqlCommand = new SqlCommand(command, SqlConn))
            {

                // Attemts to execute the command to the database
                SqlDataReader reader = sqlCommand.ExecuteReader();

                // Creates a string for the result to be pushed to
                List<string> r = new List<string>();

                // Reades the result from the command execution
                // until there is no more to be read
                while (reader.Read())
                {
                    r.Add(reader.GetString(0));
                }

                reader.Close();
                return r;
            }
        }


        /// <summary>
        /// Gets an entry from the database based on the name of the entry
        /// </summary>
        /// <param name="name"> The name of the note to be gotten </param>
        /// <returns></returns>
        public Note GetDatabaseEntry( string name )
        {
            // Creates a command string and sends it to the database connection.
            string cmd = $"select * from Notes where Notes.noteName=@name";
            using (SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn))
            {

                sqlCommand.Parameters.AddWithValue("@name", name);

                // Creates a reader that can get the data from the database
                SqlDataReader reader = sqlCommand.ExecuteReader();

                // Reads the data 
                Note r = null;
                ushort i = 0;
                while (reader.Read())
                {
                    // Checks if the database contains more than one of the
                    // given note name.
                    if (i != 0)
                        throw new DublicatedEntryException($"The entry {name} is a dublicated entry, the program could therefore not get your note");

                    // Parses the data from the current entry to make sure that noting is
                    // wrong.
                    bool couldParse = uint.TryParse(reader.GetInt32(0).ToString(), out uint id);

                    // If the program could not parse,
                    // an error is thrown
                    if (!couldParse)
                        throw new InvalidDataException("Key data was corrupted, could therefor not load in the desired note");

                    // Sets r to be the note found by the program
                    r = new Note(id, reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4));

                    // Incroments i
                    i++;
                }
                reader.Close();
                return r;
            }
        }


        /// <summary>
        /// Writes an entry in the database for a new/edited note
        /// </summary>
        /// <param name="note"> The item to be writen to the database </param>
        public void WriteDatabaseEntry( Note note )
        {

            string cmd = $"select count(1) from Notes where Notes.noteID={note.ID}";
            using (SqlCommand leader = new SqlCommand(cmd, SqlConn))
            {
                object obj = leader.ExecuteScalar();
                int i = Convert.ToInt32(obj);

                if (i == 0)
                {
                    cmd = $"insert into Notes values(@noteName, @content, @creationDate, @lastChange);";
                    using (SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn))
                    {
                        sqlCommand.Parameters.AddWithValue("@noteName", note.Name);
                        sqlCommand.Parameters.AddWithValue("@content", note.Contents);
                        sqlCommand.Parameters.AddWithValue("@creationDate", note.CreationDate.ToString("d"));
                        sqlCommand.Parameters.AddWithValue("@lastChange", note.LastChange.ToString("d"));

                        sqlCommand.ExecuteNonQuery();

                    }
                }
                else
                {
                    cmd = $"update Notes set noteName=@name, content=@content, creationDate=@creation, lastChange=@lastChange where Notes.noteID=@id;";
                    using (SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn))
                    {

                        sqlCommand.Parameters.AddWithValue("@name", note.Name);
                        sqlCommand.Parameters.AddWithValue("@content", note.Contents);
                        sqlCommand.Parameters.AddWithValue("@creation", note.CreationDate);
                        sqlCommand.Parameters.AddWithValue("@lastChange", note.LastChange);
                        sqlCommand.Parameters.AddWithValue("@id", int.Parse(note.ID.ToString()));

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }


        /// <summary>
        /// Deletes an entry from the database
        /// </summary>
        /// <param name="name"> The name for the entry to be deleted </param>
        public void RemoveDatabaseEntry( string name )
        {
            string cmd = "delete from Notes where Notes.noteName=@name";
            using (SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn) )
            {
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }


    public enum TrustedConnection
    {
        True,
        False,
    }

}
