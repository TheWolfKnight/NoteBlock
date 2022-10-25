
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lb_Notes = new System.Windows.Forms.Label();
            this.bt_NewNote = new System.Windows.Forms.Button();
            this.bt_DeleteNote = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 30);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(131, 408);
            this.treeView1.TabIndex = 0;
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
            // bt_NewNote
            // 
            this.bt_NewNote.Location = new System.Drawing.Point(154, 6);
            this.bt_NewNote.Name = "bt_NewNote";
            this.bt_NewNote.Size = new System.Drawing.Size(75, 23);
            this.bt_NewNote.TabIndex = 2;
            this.bt_NewNote.Text = "New Note";
            this.bt_NewNote.UseVisualStyleBackColor = true;
            // 
            // bt_DeleteNote
            // 
            this.bt_DeleteNote.Location = new System.Drawing.Point(235, 6);
            this.bt_DeleteNote.Name = "bt_DeleteNote";
            this.bt_DeleteNote.Size = new System.Drawing.Size(75, 23);
            this.bt_DeleteNote.TabIndex = 3;
            this.bt_DeleteNote.Text = "Delete Note";
            this.bt_DeleteNote.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bt_DeleteNote);
            this.Controls.Add(this.bt_NewNote);
            this.Controls.Add(this.lb_Notes);
            this.Controls.Add(this.treeView1);
            this.Name = "MainWindow";
            this.Text = "Note Block";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lb_Notes;
        private System.Windows.Forms.Button bt_NewNote;
        private System.Windows.Forms.Button bt_DeleteNote;
    }
}

