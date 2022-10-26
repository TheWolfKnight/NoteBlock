using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Handler.OnTextFieldEnterEvent(sender);
        }

        private void TextField_Leave(object sender, EventArgs e)
        {
            Handler.OnTextFieldLeaveEvent(sender);
        }


        private void TextField_Change(object sender, EventArgs e)
        {
            Handler.OnTextFieldChangeEvent(sender);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Handler.OnButtonClickEvent(sender);
        }
    }
}
