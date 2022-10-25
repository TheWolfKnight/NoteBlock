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

        //
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
        /// <param name="connection"> Whether the connection is trusted or not </param>
        public SqlBridge( string targetIP, string targetDB, TrustedConnection connection=TrustedConnection.False )
        {
            TargetIP = targetIP;
            TargetDB = targetDB;
            TrustedConnection = connection;
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
        public List<string> GetAllDatebaseEntryNames()
        {
            // Creates a command string for the database querry, then creates an instance of the
            // SqlCommand class to be sent to the database as querry
            string command = "select name from Notes";
            SqlCommand sqlCommand = new SqlCommand(command, SqlConn);

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

            return r;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note GetDatabaseEntry( string id )
        {
            throw new Exception("TBD");
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="note"></param>
        public void WriteDatabaseEntry( Note note )
        {
        }

    }

    public enum TrustedConnection
    {
        True,
        False,
    }

}
