using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoteBlock.Frontend;
using NoteBlock.Src.Models;
using NoteBlock.Src.Services;

namespace NoteBlock.Src.Handles
{
    public class MainWindowHandle
    {

        public Note ActiveNode;
        public List<Identifier> NoteList;

        private readonly MainWindow Owner;
        private SqlBridge Bridge;


        public MainWindowHandle( MainWindow owner )
        {
            Owner = owner;
            Bridge = new SqlBridge("localhost", "NoteBlockDB", TrustedConnection.True );
            NoteList = new List<Identifier>();
        }

        public void OnMainWindowLoadEvent()
        {

            bool connectionResult = Bridge.ConnectToDatabase();

            if ( !connectionResult )
            {
                throw new Exception($"Could not connect to the database.{Environment.NewLine}This is currently a fatal error, but will later be fixed");
            }

            NoteList = Bridge.GetAllDatebaseEntryIdentifiers();
        }

    }
}
