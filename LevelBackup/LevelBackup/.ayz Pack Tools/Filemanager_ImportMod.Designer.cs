namespace LevelBackup
{
    partial class Filemanager_ImportMod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filemanager_ImportMod));
            this.InstalledMods = new System.Windows.Forms.ListBox();
            this.SelectMod = new System.Windows.Forms.Button();
            this.Title1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SELECTED_MOD_TITLE = new System.Windows.Forms.Label();
            this.MOD_PREVIEW = new System.Windows.Forms.PictureBox();
            this.SELECTED_MOD_DESCRIPTION = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MOD_PREVIEW)).BeginInit();
            this.SuspendLayout();
            // 
            // InstalledMods
            // 
            this.InstalledMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstalledMods.FormattingEnabled = true;
            this.InstalledMods.ItemHeight = 25;
            this.InstalledMods.Location = new System.Drawing.Point(12, 59);
            this.InstalledMods.Name = "InstalledMods";
            this.InstalledMods.ScrollAlwaysVisible = true;
            this.InstalledMods.Size = new System.Drawing.Size(402, 304);
            this.InstalledMods.TabIndex = 26;
            this.InstalledMods.SelectedIndexChanged += new System.EventHandler(this.InstalledMods_SelectedIndexChanged);
            // 
            // SelectMod
            // 
            this.SelectMod.Enabled = false;
            this.SelectMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectMod.Location = new System.Drawing.Point(420, 290);
            this.SelectMod.Name = "SelectMod";
            this.SelectMod.Size = new System.Drawing.Size(289, 73);
            this.SelectMod.TabIndex = 27;
            this.SelectMod.Text = "Install Mod";
            this.SelectMod.UseVisualStyleBackColor = true;
            this.SelectMod.Click += new System.EventHandler(this.SelectMod_Click);
            // 
            // Title1
            // 
            this.Title1.AutoSize = true;
            this.Title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold);
            this.Title1.Location = new System.Drawing.Point(6, 6);
            this.Title1.Name = "Title1";
            this.Title1.Size = new System.Drawing.Size(238, 30);
            this.Title1.TabIndex = 28;
            this.Title1.Text = "MOD_LIST_TITLE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Expecting to see more? Make sure to follow the mod download instructions correctl" +
    "y!";
            // 
            // SELECTED_MOD_TITLE
            // 
            this.SELECTED_MOD_TITLE.AutoSize = true;
            this.SELECTED_MOD_TITLE.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.SELECTED_MOD_TITLE.Location = new System.Drawing.Point(416, 191);
            this.SELECTED_MOD_TITLE.Name = "SELECTED_MOD_TITLE";
            this.SELECTED_MOD_TITLE.Size = new System.Drawing.Size(235, 22);
            this.SELECTED_MOD_TITLE.TabIndex = 31;
            this.SELECTED_MOD_TITLE.Text = "SELECTED_MOD_TITLE";
            // 
            // MOD_PREVIEW
            // 
            this.MOD_PREVIEW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MOD_PREVIEW.Location = new System.Drawing.Point(420, 59);
            this.MOD_PREVIEW.Name = "MOD_PREVIEW";
            this.MOD_PREVIEW.Size = new System.Drawing.Size(289, 128);
            this.MOD_PREVIEW.TabIndex = 32;
            this.MOD_PREVIEW.TabStop = false;
            // 
            // SELECTED_MOD_DESCRIPTION
            // 
            this.SELECTED_MOD_DESCRIPTION.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SELECTED_MOD_DESCRIPTION.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.SELECTED_MOD_DESCRIPTION.Location = new System.Drawing.Point(420, 219);
            this.SELECTED_MOD_DESCRIPTION.Multiline = true;
            this.SELECTED_MOD_DESCRIPTION.Name = "SELECTED_MOD_DESCRIPTION";
            this.SELECTED_MOD_DESCRIPTION.ReadOnly = true;
            this.SELECTED_MOD_DESCRIPTION.Size = new System.Drawing.Size(289, 68);
            this.SELECTED_MOD_DESCRIPTION.TabIndex = 33;
            this.SELECTED_MOD_DESCRIPTION.Text = "SELECTED_MOD_DESCRIPTION";
            // 
            // Filemanager_ImportMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 373);
            this.Controls.Add(this.SELECTED_MOD_DESCRIPTION);
            this.Controls.Add(this.MOD_PREVIEW);
            this.Controls.Add(this.SELECTED_MOD_TITLE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Title1);
            this.Controls.Add(this.SelectMod);
            this.Controls.Add(this.InstalledMods);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Filemanager_ImportMod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenCAGE Load Saved Configurations";
            this.Load += new System.EventHandler(this.Filemanager_ImportMod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MOD_PREVIEW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox InstalledMods;
        private System.Windows.Forms.Button SelectMod;
        private System.Windows.Forms.Label Title1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SELECTED_MOD_TITLE;
        private System.Windows.Forms.PictureBox MOD_PREVIEW;
        private System.Windows.Forms.TextBox SELECTED_MOD_DESCRIPTION;
    }
}