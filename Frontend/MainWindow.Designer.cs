
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
            this.SuspendLayout();
            // 
            // tv_Notes
            // 
            this.tv_Notes.Location = new System.Drawing.Point(13, 30);
            this.tv_Notes.Name = "tv_Notes";
            this.tv_Notes.Size = new System.Drawing.Size(131, 408);
            this.tv_Notes.TabIndex = 0;
            // 
            // lb_Notes
            // 
            this.lb_Notes.AutoSize = true;
            this.lb_Notes.Location = new System.Drawing.Point(13, 11);
            this.lb_Notes.Name = "lb_Notes";
            this.lb_Notes.Size = new System.Drawing.Size(38, 13);
            this.lb_Notes.TabIndex = 1;
            this.lb_Notes.Text = "Notes:";
            // 
            // btn_NewNote
            // 
            this.btn_NewNote.Location = new System.Drawing.Point(154, 6);
            this.btn_NewNote.Name = "btn_NewNote";
            this.btn_NewNote.Size = new System.Drawing.Size(75, 23);
            this.btn_NewNote.TabIndex = 2;
            this.btn_NewNote.Text = "New Note";
            this.btn_NewNote.UseVisualStyleBackColor = true;
            this.btn_NewNote.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_DeleteNote
            // 
            this.btn_DeleteNote.Location = new System.Drawing.Point(316, 6);
            this.btn_DeleteNote.Name = "btn_DeleteNote";
            this.btn_DeleteNote.Size = new System.Drawing.Size(75, 23);
            this.btn_DeleteNote.TabIndex = 3;
            this.btn_DeleteNote.Text = "Delete Note";
            this.btn_DeleteNote.UseVisualStyleBackColor = true;
            this.btn_DeleteNote.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_SaveNote
            // 
            this.btn_SaveNote.Location = new System.Drawing.Point(235, 6);
            this.btn_SaveNote.Name = "btn_SaveNote";
            this.btn_SaveNote.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveNote.TabIndex = 4;
            this.btn_SaveNote.Text = "Save Note";
            this.btn_SaveNote.UseVisualStyleBackColor = true;
            this.btn_SaveNote.Click += new System.EventHandler(this.Button_Click);
            // 
            // tb_NameField
            // 
            this.tb_NameField.Location = new System.Drawing.Point(154, 36);
            this.tb_NameField.Name = "tb_NameField";
            this.tb_NameField.Size = new System.Drawing.Size(237, 20);
            this.tb_NameField.TabIndex = 5;
            this.tb_NameField.Text = "Enter a note name";
            this.tb_NameField.TextChanged += new System.EventHandler(this.TextField_Change);
            this.tb_NameField.Enter += new System.EventHandler(this.TextField_Enter);
            this.tb_NameField.Leave += new System.EventHandler(this.TextField_Leave);
            // 
            // rtb_NoteBody
            // 
            this.rtb_NoteBody.Location = new System.Drawing.Point(154, 63);
            this.rtb_NoteBody.Name = "rtb_NoteBody";
            this.rtb_NoteBody.Size = new System.Drawing.Size(634, 375);
            this.rtb_NoteBody.TabIndex = 6;
            this.rtb_NoteBody.Text = "Enter a note";
            this.rtb_NoteBody.TextChanged += new System.EventHandler(this.TextField_Change);
            this.rtb_NoteBody.Enter += new System.EventHandler(this.TextField_Enter);
            this.rtb_NoteBody.Leave += new System.EventHandler(this.TextField_Leave);
            // 
            // lb_NameCharCount
            // 
            this.lb_NameCharCount.AutoSize = true;
            this.lb_NameCharCount.Location = new System.Drawing.Point(397, 39);
            this.lb_NameCharCount.Name = "lb_NameCharCount";
            this.lb_NameCharCount.Size = new System.Drawing.Size(30, 13);
            this.lb_NameCharCount.TabIndex = 7;
            this.lb_NameCharCount.Text = "0/50";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lb_NameCharCount);
            this.Controls.Add(this.rtb_NoteBody);
            this.Controls.Add(this.tb_NameField);
            this.Controls.Add(this.btn_SaveNote);
            this.Controls.Add(this.btn_DeleteNote);
            this.Controls.Add(this.btn_NewNote);
            this.Controls.Add(this.lb_Notes);
            this.Controls.Add(this.tv_Notes);
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
    }
}

