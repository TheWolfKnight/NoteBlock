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
        public List<Identifier> GetAllDatebaseEntryIdentifiers()
        {
            // Creates a command string for the database querry, then creates an instance of the
            // SqlCommand class to be sent to the database as querry
            string command = "select name, copyCount from Notes";
            SqlCommand sqlCommand = new SqlCommand(command, SqlConn);

            // Attemts to execute the command to the database
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Creates a string for the result to be pushed to
            List<Identifier> r = new List<Identifier>();

            // Reades the result from the command execution
            // until there is no more to be read
            while (reader.Read())
            {
                r.Add(new Identifier(reader.GetString(0), reader.GetInt32(1)));
            }

            return r;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note GetDatabaseEntry( Identifier ident )
        {
            // Creates a command string and sends it to the database connection.
            string cmd = $"select * from Notes where Notes.name={ident.Name} and Notes.count={ident.Count}";
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
                    throw new DublicatedEntryException($"The entry {ident.Name} is a dublicated entry, the program could therefore not get your note");

                //
                bool couldParse = uint.TryParse(reader.GetInt32(0).ToString(), out uint id );
                couldParse = uint.TryParse(reader.GetInt32(2).ToString(), out uint copyCount) && couldParse;

                //
                if ( !couldParse )
                {
                    throw new InvalidDataException("Key data was corrupted, could therefor not load in the desired note");
                }

                //
                string content = GetDatabaseEntryContent(reader.GetInt32(3));
                r = new Note(id, reader.GetString(1), copyCount, content, reader.GetDateTime(4), reader.GetDateTime(5));
                
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
            throw new TBD();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentHead"></param>
        /// <returns></returns>
        public string GetDatabaseEntryContent( int contentHead )
        {
            //
            string cmd = $"select * from Content where Content.id={contentHead}";
            SqlCommand sqlCommand = new SqlCommand(cmd, SqlConn);

            //
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //
            ContentHelper head = new ContentHelper(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
            ContentHelper tail = head;

            //
            while ( tail.GetTail() != null )
            {
                cmd = $"select * from Content where Content.id={tail.GetTail()}";
                sqlCommand = new SqlCommand(cmd, SqlConn);
                reader = sqlCommand.ExecuteReader();
                ContentHelper tmp = new ContentHelper(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                tail.SetTail(tmp);
                tail = tmp;
            }

            return head.GetContent();
        }

    }

    public enum TrustedConnection
    {
        True,
        False,
    }

}
