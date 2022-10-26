using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NoteBlock.Frontend;
using NoteBlock.Src.Models;
using NoteBlock.Src.Services;

namespace NoteBlock.Src.Handles
{
    public class MainWindowHandle
    {

        public Note ActiveNote;
        public List<Identifier> NoteList;

        private readonly MainWindow Owner;
        private SqlBridge Bridge;


        public MainWindowHandle( MainWindow owner )
        {
            Owner = owner;
            Bridge = new SqlBridge("localhost", "NoteBlockDB", TrustedConnection.True );
            NoteList = new List<Identifier>();
            ActiveNote = new Note();
        }

        public void OnMainWindowLoadEvent()
        {
            /*
            bool connectionResult = Bridge.ConnectToDatabase();

            if ( !connectionResult )
            {
                throw new Exception($"Could not connect to the database.{Environment.NewLine}This is currently a fatal error, but will later be fixed");
            }

            NoteList = Bridge.GetAllDatebaseEntryIdentifiers();
            */

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public void OnTextFieldEnterEvent( object sender )
        {
            if ( sender.ToString().Split(',')[0] == "System.Windows.Forms.TextBox")
            {
                HandleTextBoxOnEnterDelegate((TextBox)sender);
            } else if ( sender.ToString().Split(',')[0] == "System.Windows.Forms.RichTextBox")
            {
                HandleRichTextBoxOnEnterDelegate((RichTextBox)sender);
            } else
            {
                throw new ElementNotFoundException("Could not find either a TextBox or a RichTextBox");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void HandleTextBoxOnEnterDelegate( TextBox sender )
        {
            if (sender.Tag.ToString() == "unchanged")
            {
                sender.Text = "";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void HandleRichTextBoxOnEnterDelegate( RichTextBox sender )
        {
            if ( sender.Tag.ToString() == "unchanged")
            {
                sender.Text = "";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public void OnTextFieldLeaveEvent( object sender )
        {
            if (sender.ToString().Split(',')[0] == "System.Windows.Forms.TextBox")
            {
                HandleTextBoxOnLeaveDelegate((TextBox)sender);
            }
            else if (sender.ToString().Split(',')[0] == "System.Windows.Forms.RichTextBox")
            {
                HandleRichTextBoxOnLeaveDelegate((RichTextBox)sender);
            }
            else
            {
                throw new ElementNotFoundException("Could not find either a TextBox or a RichTextBox");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void HandleTextBoxOnLeaveDelegate(TextBox sender)
        {
            if ( sender.TextLength == 0 )
            {
                sender.Text = "Enter a note name";
                sender.Tag = "unchanged";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void HandleRichTextBoxOnLeaveDelegate(RichTextBox sender)
        {
            if ( sender.TextLength == 0 )
            {
                sender.Text = "Enter a note";
                sender.Tag = "unchanged";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public void OnTextFieldChangeEvent(object sender)
        {
            if (sender.ToString().Split(',')[0] == "System.Windows.Forms.TextBox")
            {
                HandleTextBoxOnChangeDelegate((TextBox)sender);
            }
            else if (sender.ToString().Split(',')[0] == "System.Windows.Forms.RichTextBox")
            {
                HandleRichTextBoxOnChangeDelegate((RichTextBox)sender);
            }
            else
            {
                throw new ElementNotFoundException("Could not find either a TextBox or a RichTextBox");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void HandleTextBoxOnChangeDelegate(TextBox sender)
        {
            Console.WriteLine("hit");
            sender.Tag = "changed";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void HandleRichTextBoxOnChangeDelegate(RichTextBox sender)
        {
            sender.Tag = "changed";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public void OnButtonClickEvent(object sender)
        {
            Button btn = (Button)sender;

            switch (btn.Name) {
                case "btn_NewNote":
                    NewNoteButtonDelegate();
                    break;
                case "btn_SaveNote":
                    SaveNoteButtonDelegate();
                    break;
                case "btn_DeleteNote":
                    DeleteNoteButtonDelegate();
                    break;
                default:
                    throw new UnrechableCode();
            }
        }


        private void NewNoteButtonDelegate() { }
        private void SaveNoteButtonDelegate() { }
        private void DeleteNoteButtonDelegate() { }
    }
}
