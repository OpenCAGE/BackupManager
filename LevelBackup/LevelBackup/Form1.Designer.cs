namespace LevelBackup
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.saveList = new System.Windows.Forms.CheckedListBox();
            this.deleteSelectedBackups = new System.Windows.Forms.Button();
            this.removeSelectedBackup = new System.Windows.Forms.Button();
            this.selectAll = new System.Windows.Forms.Button();
            this.deselectAll = new System.Windows.Forms.Button();
            this.levelList = new System.Windows.Forms.ComboBox();
            this.backupName = new System.Windows.Forms.TextBox();
            this.saveBackup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // saveList
            // 
            this.saveList.FormattingEnabled = true;
            this.saveList.Location = new System.Drawing.Point(12, 52);
            this.saveList.Name = "saveList";
            this.saveList.Size = new System.Drawing.Size(878, 424);
            this.saveList.TabIndex = 0;
            // 
            // deleteSelectedBackups
            // 
            this.deleteSelectedBackups.Location = new System.Drawing.Point(710, 482);
            this.deleteSelectedBackups.Name = "deleteSelectedBackups";
            this.deleteSelectedBackups.Size = new System.Drawing.Size(180, 23);
            this.deleteSelectedBackups.TabIndex = 1;
            this.deleteSelectedBackups.Text = "Delete Checked Backups";
            this.deleteSelectedBackups.UseVisualStyleBackColor = true;
            this.deleteSelectedBackups.Click += new System.EventHandler(this.deleteSelectedBackups_Click);
            // 
            // removeSelectedBackup
            // 
            this.removeSelectedBackup.Location = new System.Drawing.Point(524, 482);
            this.removeSelectedBackup.Name = "removeSelectedBackup";
            this.removeSelectedBackup.Size = new System.Drawing.Size(180, 23);
            this.removeSelectedBackup.TabIndex = 2;
            this.removeSelectedBackup.Text = "Restore Checked Backup";
            this.removeSelectedBackup.UseVisualStyleBackColor = true;
            this.removeSelectedBackup.Click += new System.EventHandler(this.restoreSelectedBackup);
            // 
            // selectAll
            // 
            this.selectAll.Location = new System.Drawing.Point(12, 482);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(83, 23);
            this.selectAll.TabIndex = 3;
            this.selectAll.Text = "Check All";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // deselectAll
            // 
            this.deselectAll.Location = new System.Drawing.Point(101, 482);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(83, 23);
            this.deselectAll.TabIndex = 4;
            this.deselectAll.Text = "Un-Check All";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // levelList
            // 
            this.levelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelList.FormattingEnabled = true;
            this.levelList.Location = new System.Drawing.Point(12, 25);
            this.levelList.Name = "levelList";
            this.levelList.Size = new System.Drawing.Size(342, 21);
            this.levelList.TabIndex = 5;
            this.levelList.SelectedIndexChanged += new System.EventHandler(this.levelList_SelectedIndexChanged);
            // 
            // backupName
            // 
            this.backupName.Location = new System.Drawing.Point(505, 25);
            this.backupName.Name = "backupName";
            this.backupName.Size = new System.Drawing.Size(269, 20);
            this.backupName.TabIndex = 6;
            // 
            // saveBackup
            // 
            this.saveBackup.Location = new System.Drawing.Point(780, 23);
            this.saveBackup.Name = "saveBackup";
            this.saveBackup.Size = new System.Drawing.Size(111, 23);
            this.saveBackup.TabIndex = 7;
            this.saveBackup.Text = "Backup Now";
            this.saveBackup.UseVisualStyleBackColor = true;
            this.saveBackup.Click += new System.EventHandler(this.saveBackup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(502, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Backup Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Level";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 514);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveBackup);
            this.Controls.Add(this.backupName);
            this.Controls.Add(this.levelList);
            this.Controls.Add(this.deselectAll);
            this.Controls.Add(this.selectAll);
            this.Controls.Add(this.removeSelectedBackup);
            this.Controls.Add(this.deleteSelectedBackups);
            this.Controls.Add(this.saveList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenCAGE Level Backup Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox saveList;
        private System.Windows.Forms.Button deleteSelectedBackups;
        private System.Windows.Forms.Button removeSelectedBackup;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Button deselectAll;
        private System.Windows.Forms.ComboBox levelList;
        private System.Windows.Forms.TextBox backupName;
        private System.Windows.Forms.Button saveBackup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

