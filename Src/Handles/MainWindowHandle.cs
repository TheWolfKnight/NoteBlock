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

        private readonly MainWindow Owner;
        private SqlBridge Bridge;


        /// <summary>
        /// Initialize the MainWindowHandler instance
        /// </summary>
        /// <param name="owner"></param>
        public MainWindowHandle( MainWindow owner )
        {
            Owner = owner;
            Bridge = new SqlBridge("localhost", "NoteBlockDB", TrustedConnection.True );
        }


        /// <summary>
        /// Initialize the main window
        /// </summary>
        public void OnMainWindowLoadEvent()
        {

            bool connectionResult = Bridge.ConnectToDatabase();

            if ( !connectionResult )
                throw new Exception($"Could not connect to the database.{Environment.NewLine}This is currently a fatal error, but will later be fixed");

            Owner.tb_NameField.Tag = new TextItemTag(false, false);
            Owner.rtb_NoteBody.Tag = new TextItemTag(false, false);

             LoadNotesList();

        }


        /// <summary>
        /// Loads in all the note elements names from the database
        /// </summary>
        private void LoadNotesList()
        {
            Bridge.GetAllDatebaseEntryIdentifiers().ForEach(name =>
            {
                Owner.tv_Notes.Nodes.Add(new TreeNode(name));
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
                Owner.ChangeName = true;
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

            if (!tag.IsChanged && !tag.IsSaved)
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

            if (!tag.IsChanged && !tag.IsSaved)
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
            // makes sure the change comes from the user
            if (!Owner.ChangeName || Owner.tb_NameField.TextLength < 0)
                return 1;

            // gets the tag for the TextBox and sets them correctly
            TextItemTag tmp = (TextItemTag)sender.Tag;
            tmp.IsChanged = true;
            tmp.IsSaved = false;

            // Sets the Tag field on the TextBox
            sender.Tag = tmp;

            // If the TextBox is empty, set the text to the original message
            if (Owner.tb_NameField.Text == "\n")
            {
                Owner.tb_NameField.Text = "Enter a note name";
                Owner.lb_NameCharCount.Text = "0/50";
                return 0;
            }

            // updates the char counter
            string[] splitLb = Owner.lb_NameCharCount.Text.Split('/');
            splitLb[0] = Owner.tb_NameField.TextLength.ToString();
            Owner.lb_NameCharCount.Text = string.Join("/", splitLb);

            // if the char amt exceds the limit of 50 chars, set the char counter to red text
            // if it goes under the max, set it to black
            if (Owner.tb_NameField.TextLength > 50)
                Owner.lb_NameCharCount.ForeColor = Color.Red;
            else
                Owner.lb_NameCharCount.ForeColor = Color.Black;

            // finish the interaction
            Owner.ChangeName = false;

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
            tmp.IsSaved = false;

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

            if ( !IsSafeOverwrite() )
            {
                DialogResult r = MessageBox.Show($"You are about to delete an unsaved note.{Environment.NewLine}Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (r == DialogResult.No)
                    return;
            }

            Owner.tb_NameField.Text = "\n";
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
            // Gets the tags for the differnt elements to be saved
            TextItemTag rtb = (TextItemTag)Owner.rtb_NoteBody.Tag;
            TextItemTag tb = (TextItemTag)Owner.tb_NameField.Tag;

            // makes sure there is some kind of changes
            if ( !rtb.IsChanged && !tb.IsChanged )
                return;

            // validates the name
            if (!ValidateName())
            {
                return;
            }

            // gets the name for the note, and makes a new note instance
            string name = Owner.tb_NameField.Text;
            Note? note = null;

            // Checks if the note already exists.
            // If not the program saves a new note,
            // else it asks if you want to overwrite your note.
            if ( !TreeViewContains(name) )
            {
                Note tmp = new Note(name, Owner.rtb_NoteBody.Text, DateTime.Now);
                Owner.tv_Notes.Nodes.Add(new TreeNode(name));
                Bridge.WriteDatabaseEntry(tmp);
                note = tmp;
            } else
            {

                DialogResult r = MessageBox.Show($"You are about to overwrite an existing note{Environment.NewLine}Are you sure?", "Warning", MessageBoxButtons.YesNo);

                if (r != DialogResult.Yes)
                    return;
                Note tmp = Bridge.GetDatabaseEntry(name);
                tmp.MakeChange(Owner.rtb_NoteBody.Text);
                Bridge.WriteDatabaseEntry(tmp);
                note = tmp;
            }

            if (note == null)
                throw new InvalidDataException("The program faild to create a note on save.");

            // Sets the relevant flags
            rtb.IsSaved = true;
            rtb.IsChanged = false;
            tb.IsSaved = true;
            tb.IsChanged = false;

            // Saves the updated tags
            Owner.tb_NameField.Tag = tb;
            Owner.rtb_NoteBody.Tag = rtb;

            // Sets the active note
            ActiveNote = note;
            ApplayActiveNote();
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
        /// Handels the event where a TreeNode is clicked
        /// </summary>
        /// <param name="name"> The name of the selected node </param>
        public void OnTreeViewNodeMouseClickEvent(string name)
        {
            Note note = Bridge.GetDatabaseEntry(name);

            if (note == null)
                throw new InvalidDataException($"Could not find a note by the name: {name}");

            if ( ActiveNote != null && !IsSafeOverwrite() )
            {
                DialogResult r = MessageBox.Show($"You are about to leave a unsaved note{Environment.NewLine}Do you want to save before leaving?", "Warning", MessageBoxButtons.YesNoCancel);

                if (r == DialogResult.Cancel || r == DialogResult.None)
                    return;
                if (r == DialogResult.Yes)
                    SaveNoteButtonDelegate();
                    
            }

            ActiveNote = note;
            ApplayActiveNote();
        }


        /// <summary>
        /// Checks if the TreeView contains a specific name
        /// </summary>
        /// <param name="name"> The name to be found in the TreeView </param>
        /// <returns> Returns true if the item is in the TreeView, otherwise false </returns>
        private bool TreeViewContains(string name)
        {
            foreach ( TreeNode node in Owner.tv_Notes.Nodes )
            {
                if (node.Text == name)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Attempts to remove an item from the TreeVeiw
        /// </summary>
        /// <param name="name"> The name of the Item to be removed </param>
        private void RemoveTreeViewNode()
        {
            if (ActiveNote == null)
                return;

            foreach (TreeNode node in Owner.tv_Notes.Nodes)
            {
                if (node.Text == ActiveNote.Name)
                {
                    Owner.tv_Notes.Nodes.Remove(node);
                    Owner.lb_CreationDate.Text = "";
                    Owner.lb_LastChangeDate.Text = "";
                    return;
                }
            }
            return;
        }


        /// <summary>
        /// Deletes the current active note, and removes it from the NoteList List and database
        /// </summary>
        private void DeleteNoteButtonDelegate()
        {
            // If the active note is null, the method returns
            if (ActiveNote == null)
                return;

            Bridge.RemoveDatabaseEntry(ActiveNote.Name);
            
            RemoveTreeViewNode();
        }


        /// <summary>
        /// Makes sure the current active note is safe to discard from the view
        /// </summary>
        /// <returns> Returns true if the data can be safely discarded, and returns false otherwise </returns>
        public bool IsSafeOverwrite()
        {
            // gets the input fields tags
            TextItemTag rtbTag = (TextItemTag)Owner.rtb_NoteBody.Tag;
            TextItemTag tbTag = (TextItemTag)Owner.tb_NameField.Tag;

            // checks if the data is saved
            bool tbSafe = tbTag.IsSaved && !tbTag.IsChanged;
            bool rtbSafe = rtbTag.IsSaved && !rtbTag.IsChanged;

            // returns the result
            return  tbSafe && rtbSafe;
        }


        /// <summary>
        /// Applys the data from the current active note
        /// </summary>
        private void ApplayActiveNote()
        {
            if (ActiveNote == null)
                return;

            Owner.tb_NameField.Text = ActiveNote.Name;
            Owner.rtb_NoteBody.Text = ActiveNote.Contents;

            TextItemTag rtbTag = (TextItemTag)Owner.rtb_NoteBody.Tag;
            TextItemTag tbTag = (TextItemTag)Owner.tb_NameField.Tag;

            rtbTag.IsSaved = true;
            rtbTag.IsChanged = false;
            tbTag.IsSaved = true;
            tbTag.IsChanged = false;

            Owner.lb_CreationDate.Text = ActiveNote.CreationDate.ToString("F");
            Owner.lb_LastChangeDate.Text = ActiveNote.LastChange.ToString("F");
            Owner.rtb_NoteBody.Tag = rtbTag;
            Owner.tb_NameField.Tag = tbTag;

        }

    }
}
