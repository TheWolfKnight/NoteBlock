using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using NoteBlock.Frontend;
using NoteBlock.Src.Models;
using NoteBlock.Src.Services;

namespace NoteBlock.Src.Handles
{
    public class MainWindowHandle
    {
        #nullable enable
        public Note? ActiveNote;
        public List<string> NoteList;

        private List<Note> TMPNoteHolder;
        private readonly MainWindow Owner;
        private SqlBridge Bridge;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public MainWindowHandle( MainWindow owner )
        {
            Owner = owner;
            Bridge = new SqlBridge("localhost", "NoteBlockDB", TrustedConnection.True );
            NoteList = new List<string>();
            TMPNoteHolder = new List<Note>();
            ActiveNote = new Note();
        }


        /// <summary>
        /// 
        /// </summary>
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

            Owner.tb_NameField.Tag = new TextItemTag(false, false);
            Owner.rtb_NoteBody.Tag = new TextItemTag(false, false);

            LoadNotesList();

        }


        /// <summary>
        /// Loads in all the note elements names from the NoteList field
        /// </summary>
        private void LoadNotesList()
        {
            TreeView notes = Owner.tv_Notes;
            Owner.tv_Notes.Nodes.Clear();

            NoteList.ForEach(name => {
                TreeNode node = new TreeNode(name);
                if (!notes.Nodes.Contains(node))
                    notes.Nodes.Add(node);
            });
        }


        /// <summary>
        /// Delegats the events for the text boxes in form
        /// </summary>
        /// <param name="sender"> An object representation of the caller </param>
        /// <param name="textBoxDelegate"> The function that should run if the caller is a TextBox instance </param>
        /// <param name="richTextBoxDelegate"> The function that should run if the caller is a RichTextBox instance </param>
        public void OnTextFieldEvent( object sender, Func<TextBox, int> textBoxDelegate, Func<RichTextBox, int> richTextBoxDelegate )
        {
            if ( sender.ToString().Split(',')[0] == "System.Windows.Forms.TextBox")
            {
                textBoxDelegate((TextBox)sender);
            } else if ( sender.ToString().Split(',')[0] == "System.Windows.Forms.RichTextBox")
            {
                richTextBoxDelegate((RichTextBox)sender);
            } else
            {
                throw new ElementNotFoundException("Could not find either a TextBox or a RichTextBox");
            }
        }


        /// <summary>
        /// Handles the event where a TextBox gets enterd
        /// </summary>
        /// <param name="sender"> The caller of the event delegate </param>
        public int HandleTextBoxOnEnterDelegate( TextBox sender )
        {
            TextItemTag tag = (TextItemTag)sender.Tag;

            if (!tag.IsChanged)
            {
                sender.Text = "";
            }
            return 0;
        }


        /// <summary>
        /// Handles the event where a RichTextBox gets enterd
        /// </summary>
        /// <param name="sender"> The caller of the event delegate </param>
        public int HandleRichTextBoxOnEnterDelegate( RichTextBox sender )
        {
            TextItemTag tag = (TextItemTag)sender.Tag;

            if (!tag.IsChanged)
            {
                sender.Text = "";
            }
            return 0;
        }


        /// <summary>
        /// Handles the event where a TextBox gets left
        /// </summary>
        /// <param name="sender"> The caller of the event delegate </param>
        public int HandleTextBoxOnLeaveDelegate(TextBox sender)
        {
            if ( sender.TextLength == 0 )
            {
                sender.Text = "\n";

                TextItemTag tmp = (TextItemTag)sender.Tag;
                tmp.IsChanged = false;

                sender.Tag = tmp;
            }
            return 0;
        }


        /// <summary>
        /// Handles the event where a RichTextBox gets left
        /// </summary>
        /// <param name="sender"> The caller of the event delegate </param>
        public int HandleRichTextBoxOnLeaveDelegate(RichTextBox sender)
        {
            if ( sender.TextLength == 0 )
            {
                sender.Text = "Enter a note";

                TextItemTag tmp = (TextItemTag)sender.Tag;
                tmp.IsChanged = false;

                sender.Tag = tmp;
            }
            return 0;
        }


        /// <summary>
        /// Handles the event where a TextBox gets changed
        /// </summary>
        /// <param name="sender"> The caller of the event delegate </param>
        public int HandleTextBoxOnChangeDelegate(TextBox sender)
        {

            TextItemTag tmp = (TextItemTag)sender.Tag;
            tmp.IsChanged = true;

            sender.Tag = tmp;

            if (Owner.tb_NameField.Text == "\n")
            {
                Owner.tb_NameField.Text = "Enter a note name";
                Owner.lb_NameCharCount.Text = "0/50";
                return 0;
            }
            string[] splitLb = Owner.lb_NameCharCount.Text.Split('/');
            splitLb[0] = Owner.tb_NameField.TextLength.ToString();
            Owner.lb_NameCharCount.Text = string.Join("/", splitLb);

            if (Owner.tb_NameField.TextLength > 50)
                Owner.lb_NameCharCount.ForeColor = Color.Red;
            else
                Owner.lb_NameCharCount.ForeColor = Color.Black;

            return 0;
        }


        /// <summary>
        /// Handles the event where a RichTextBox gets changed
        /// </summary>
        /// <param name="sender"> The caller of the event delegate </param>
        public int HandleRichTextBoxOnChangeDelegate(RichTextBox sender)
        {

            TextItemTag tmp = (TextItemTag)sender.Tag;
            tmp.IsChanged = true;

            sender.Tag = tmp;
            return 0;
        }


        /// <summary>
        /// Handles the event where a button gets clicked, then delegats the call to the right function
        /// </summary>
        /// <param name="sender"> The caller of the event </param>
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


        /// <summary>
        /// Handles the event where the new note button is clicked, throws a warning it the program
        /// is about to overwrite some unsaved text
        /// </summary>
        private void NewNoteButtonDelegate()
        {
            TextItemTag rtbTag = (TextItemTag)Owner.rtb_NoteBody.Tag;
            TextItemTag tbTag = (TextItemTag)Owner.tb_NameField.Tag;

            if ( !tbTag.IsSaved && tbTag.IsChanged || !rtbTag.IsSaved && rtbTag.IsChanged )
            {
                DialogResult r = MessageBox.Show($"You are about to delete an unsaved note.{Environment.NewLine}Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (r == DialogResult.No)
                    return;
            }

            Owner.tb_NameField.Text = "Enter a note name";
            Owner.rtb_NoteBody.Text = "Enter a note";

            tbTag.Reset();
            rtbTag.Reset();

            Owner.tb_NameField.Tag = tbTag;
            Owner.rtb_NoteBody.Tag = rtbTag;

            ActiveNote = null;

        }


        /// <summary>
        /// Handles the event where the save button is clicked
        /// </summary>
        private void SaveNoteButtonDelegate()
        {

            TextItemTag rtb = (TextItemTag)Owner.rtb_NoteBody.Tag;
            TextItemTag tb = (TextItemTag)Owner.tb_NameField.Tag;

            if ( !rtb.IsChanged || !tb.IsChanged )
                return;

            if (!ValidateName())
            {
                return;
            }

            string name = Owner.tb_NameField.Text;
            Note note = new Note(name, Owner.rtb_NoteBody.Text, DateTime.Now);

            if ( !NoteList.Contains(name) )
            {
                NoteList.Add(name);
                TMPNoteHolder.Add(note);
            } else
            {
                Note tmp = TMPNoteHolder.Where(notes => notes.Name == name).FirstOrDefault();
                if (tmp != null)
                    TMPNoteHolder.Remove(tmp);
            }

            rtb.IsSaved = true;
            tb.IsSaved = true;

            Owner.tb_NameField.Tag = tb;
            Owner.rtb_NoteBody.Tag = rtb;

            ActiveNote = note;

            LoadNotesList();
        }
        

        /// <summary>
        /// Makes sure the notes name is withing the acceptable limit
        /// </summary>
        /// <returns> Where the name is accepted or not </returns>
        private bool ValidateName()
        {
            string[] splitLb = Owner.lb_NameCharCount.Text.Split('/');
            int length = int.Parse(splitLb[0]);

            if (length > 50)
            {
                MessageBox.Show("Your note cannot have a name over 50 charactors", "Notice", MessageBoxButtons.OK);
                return false;
            }
            else if (length < 1)
            {
                MessageBox.Show("Your note cannot have a name less than 1 charactor", "Notice", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        
        /// <summary>
        /// Deletes the current active note, and removes it from the NoteList List and database
        /// </summary>
        private void DeleteNoteButtonDelegate()
        {
            // If the active note is null, the method returns
            if (ActiveNote == null)
                return;

            // get the first element from the database that has the same name, or null if nothing with that name exsists
            // if the name does not appear, the method returns
            Note tmp = TMPNoteHolder.Where(notes => notes.Name == ActiveNote.Name).FirstOrDefault();
            if (tmp == null)
                return;

            // Removes the item from all relevant areas, and then reloads the list
            TMPNoteHolder.Remove(tmp);
            NoteList.Remove(ActiveNote.Name);
            LoadNotesList();

        }
    }
}
