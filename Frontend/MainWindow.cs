using System;
using System.Windows.Forms;


using NoteBlock.Src.Handles;


namespace NoteBlock.Frontend
{
    public partial class MainWindow : Form
    {

        private readonly MainWindowHandle Handler;

        public MainWindow()
        {
            InitializeComponent();
            Handler = new MainWindowHandle(this);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Handler.OnMainWindowLoadEvent();
        }

        private void TextField_Enter(object sender, EventArgs e)
        {
            Handler.OnTextFieldEvent(sender, Handler.HandleTextBoxOnEnterDelegate, Handler.HandleRichTextBoxOnEnterDelegate);
        }

        private void TextField_Leave(object sender, EventArgs e)
        {
            Handler.OnTextFieldEvent(sender, Handler.HandleTextBoxOnLeaveDelegate, Handler.HandleRichTextBoxOnLeaveDelegate);
        }

        private void TextField_Change(object sender, EventArgs e)
        {
            Handler.OnTextFieldEvent(sender, Handler.HandleTextBoxOnChangeDelegate, Handler.HandleRichTextBoxOnChangeDelegate);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Handler.OnButtonClickEvent(sender);
        }
    }
}
