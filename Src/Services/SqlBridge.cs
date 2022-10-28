using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using NoteBlock.Src.Models;
using NoteBlock.Src;

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
            get { return $"Server={TargetDB};Database={TargetDB};Trusted_Connection={TrustedConnection}"; }
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
            SqlCommand sqlCommand = new SqlCommand(command, SqlConn);

            // Attemts to execute the command to the database
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Creates a string for the result to be pushed to
            List<string> r = new List<string>();

            // Reades the result from the command execution
            // until there is no more to be read
            while (reader.Read())
            {
                r.Add(reader.GetString(1));
            }

            return r;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note GetDatabaseEntry( string name )
        {
            // Creates a command string and sends it to the database connection.
            string cmd = $"select * from Notes where Notes.noteName={name}";
            SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn);

            // Creates a reader that can get the data from the database
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Reads the data 
            Note r = null;
            ushort i = 0;
            while (reader.Read())
            {
                //
                if (i != 0)
                    throw new DublicatedEntryException($"The entry {name} is a dublicated entry, the program could therefore not get your note");

                //
                bool couldParse = uint.TryParse(reader.GetInt32(0).ToString(), out uint id );
                couldParse = uint.TryParse(reader.GetInt32(2).ToString(), out uint copyCount) && couldParse;

                //
                if ( !couldParse )
                {
                    throw new InvalidDataException("Key data was corrupted, could therefor not load in the desired note");
                }


                r = new Note(id, reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4));
                
                i++;
            }

            return r;
        }


        /// <summary>
        /// Writes an entry in the database for a new/edited note
        /// </summary>
        /// <param name="note"> The item to be writen to the database </param>
        public void WriteDatabaseEntry( Note note )
        {

            SqlCommand sqlCommand;

            string cmd = $"select count(1) from Notes where Notes.noteID={note.ID}";
            sqlCommand = new SqlCommand(cmd, SqlConn);

            var obj = sqlCommand.ExecuteScalar();
            int i = Convert.ToInt32(obj);

            if (i == 0) {
                cmd = $"insert into Notes(noteName, content, creationDate, lastChange) values({note.Name}, {note.Contents}, {note.CreationDate}, {note.LastChange});";
                sqlCommand = new SqlCommand(cmd, SqlConn);
            } else
            {
                cmd = $"update table Notes set noteName={note.Name}, content={note.Contents}, createionDate={note.CreationDate}, lastChange={note.LastChange} where Notes.noteID={note.ID};";
                sqlCommand = new SqlCommand(cmd, SqlConn);
            }

            sqlCommand.ExecuteNonQuery();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteID"></param>
        /// <returns></returns>
        public string GetDatabaseEntryContent( int noteID )
        {
            //
            string cmd = $"select content from Notes where Notes.noteID={noteID}";
            SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn);

            //
            object obj = sqlCommand.ExecuteScalar();
            Console.WriteLine(obj);
            return "";
            
        }

    }

    public enum TrustedConnection
    {
        True,
        False,
    }

}
