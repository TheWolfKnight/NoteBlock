
namespace NoteBlock.Frontend
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tv_Notes = new System.Windows.Forms.TreeView();
            this.lb_Notes = new System.Windows.Forms.Label();
            this.btn_NewNote = new System.Windows.Forms.Button();
            this.btn_DeleteNote = new System.Windows.Forms.Button();
            this.btn_SaveNote = new System.Windows.Forms.Button();
            this.tb_NameField = new System.Windows.Forms.TextBox();
            this.rtb_NoteBody = new System.Windows.Forms.RichTextBox();
            this.lb_NameCharCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_CreationDate = new System.Windows.Forms.Label();
            this.lb_LastChangeDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tv_Notes
            // 
            this.tv_Notes.Location = new System.Drawing.Point(17, 37);
            this.tv_Notes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tv_Notes.Name = "tv_Notes";
            this.tv_Notes.Size = new System.Drawing.Size(173, 501);
            this.tv_Notes.TabIndex = 0;
            this.tv_Notes.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewNode_MouseClick);
            // 
            // lb_Notes
            // 
            this.lb_Notes.AutoSize = true;
            this.lb_Notes.Location = new System.Drawing.Point(17, 14);
            this.lb_Notes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Notes.Name = "lb_Notes";
            this.lb_Notes.Size = new System.Drawing.Size(49, 17);
            this.lb_Notes.TabIndex = 1;
            this.lb_Notes.Text = "Notes:";
            // 
            // btn_NewNote
            // 
            this.btn_NewNote.Location = new System.Drawing.Point(205, 7);
            this.btn_NewNote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_NewNote.Name = "btn_NewNote";
            this.btn_NewNote.Size = new System.Drawing.Size(100, 28);
            this.btn_NewNote.TabIndex = 2;
            this.btn_NewNote.Text = "New Note";
            this.btn_NewNote.UseVisualStyleBackColor = true;
            this.btn_NewNote.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_DeleteNote
            // 
            this.btn_DeleteNote.Location = new System.Drawing.Point(421, 7);
            this.btn_DeleteNote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DeleteNote.Name = "btn_DeleteNote";
            this.btn_DeleteNote.Size = new System.Drawing.Size(100, 28);
            this.btn_DeleteNote.TabIndex = 3;
            this.btn_DeleteNote.Text = "Delete Note";
            this.btn_DeleteNote.UseVisualStyleBackColor = true;
            this.btn_DeleteNote.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_SaveNote
            // 
            this.btn_SaveNote.Location = new System.Drawing.Point(313, 7);
            this.btn_SaveNote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_SaveNote.Name = "btn_SaveNote";
            this.btn_SaveNote.Size = new System.Drawing.Size(100, 28);
            this.btn_SaveNote.TabIndex = 4;
            this.btn_SaveNote.Text = "Save Note";
            this.btn_SaveNote.UseVisualStyleBackColor = true;
            this.btn_SaveNote.Click += new System.EventHandler(this.Button_Click);
            // 
            // tb_NameField
            // 
            this.tb_NameField.Location = new System.Drawing.Point(205, 44);
            this.tb_NameField.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_NameField.Name = "tb_NameField";
            this.tb_NameField.Size = new System.Drawing.Size(315, 22);
            this.tb_NameField.TabIndex = 5;
            this.tb_NameField.Text = "Enter a note name";
            this.tb_NameField.TextChanged += new System.EventHandler(this.TextField_Change);
            this.tb_NameField.Enter += new System.EventHandler(this.TextField_Enter);
            this.tb_NameField.Leave += new System.EventHandler(this.TextField_Leave);
            // 
            // rtb_NoteBody
            // 
            this.rtb_NoteBody.Location = new System.Drawing.Point(205, 78);
            this.rtb_NoteBody.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtb_NoteBody.Name = "rtb_NoteBody";
            this.rtb_NoteBody.Size = new System.Drawing.Size(844, 461);
            this.rtb_NoteBody.TabIndex = 6;
            this.rtb_NoteBody.Text = "Enter a note";
            this.rtb_NoteBody.TextChanged += new System.EventHandler(this.TextField_Change);
            this.rtb_NoteBody.Enter += new System.EventHandler(this.TextField_Enter);
            this.rtb_NoteBody.Leave += new System.EventHandler(this.TextField_Leave);
            // 
            // lb_NameCharCount
            // 
            this.lb_NameCharCount.AutoSize = true;
            this.lb_NameCharCount.Location = new System.Drawing.Point(529, 48);
            this.lb_NameCharCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_NameCharCount.Name = "lb_NameCharCount";
            this.lb_NameCharCount.Size = new System.Drawing.Size(36, 17);
            this.lb_NameCharCount.TabIndex = 7;
            this.lb_NameCharCount.Text = "0/50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(697, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Created:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(697, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Last Change:";
            // 
            // lb_CreationDate
            // 
            this.lb_CreationDate.AutoSize = true;
            this.lb_CreationDate.Location = new System.Drawing.Point(795, 14);
            this.lb_CreationDate.Name = "lb_CreationDate";
            this.lb_CreationDate.Size = new System.Drawing.Size(0, 17);
            this.lb_CreationDate.TabIndex = 10;
            // 
            // lb_LastChangeDate
            // 
            this.lb_LastChangeDate.AutoSize = true;
            this.lb_LastChangeDate.Location = new System.Drawing.Point(795, 37);
            this.lb_LastChangeDate.Name = "lb_LastChangeDate";
            this.lb_LastChangeDate.Size = new System.Drawing.Size(0, 17);
            this.lb_LastChangeDate.TabIndex = 11;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.lb_LastChangeDate);
            this.Controls.Add(this.lb_CreationDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_NameCharCount);
            this.Controls.Add(this.rtb_NoteBody);
            this.Controls.Add(this.tb_NameField);
            this.Controls.Add(this.btn_SaveNote);
            this.Controls.Add(this.btn_DeleteNote);
            this.Controls.Add(this.btn_NewNote);
            this.Controls.Add(this.lb_Notes);
            this.Controls.Add(this.tv_Notes);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "Note Block";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lb_Notes;
        private System.Windows.Forms.Button btn_NewNote;
        private System.Windows.Forms.Button btn_DeleteNote;
        private System.Windows.Forms.Button btn_SaveNote;
        public System.Windows.Forms.TextBox tb_NameField;
        public System.Windows.Forms.RichTextBox rtb_NoteBody;
        public System.Windows.Forms.TreeView tv_Notes;
        public System.Windows.Forms.Label lb_NameCharCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lb_LastChangeDate;
        public System.Windows.Forms.Label lb_CreationDate;
    }
}

