using CathodeLib;
using OpenCAGE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LevelBackup
{
    public partial class LevelBackupManager : Form
    {
        AlienLevel level = null;

        public LevelBackupManager(string level = null)
        {
            InitializeComponent();

            if (!Directory.Exists(SettingsManager.GetString("PATH_GameRoot") + "/DATA/MODTOOLS/BACKUPS"))
            {
                MessageBox.Show("Welcome to the OpenCAGE Level Backup Manager! It is recommended to create a backup of all levels when they are in an unmodified state, to be able to revert back to later.", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            levelList.Items.AddRange(Level.GetLevels(SettingsManager.GetString("PATH_GameRoot")).ToArray());
            if (level == null)
                levelList.SelectedIndex = 0;
            else
                levelList.SelectedItem = level;

            RefreshList();
        }

        /* Populate the UI for all backups in the selected level */
        private void RefreshList()
        {
            backupList.Items.Clear();
            for (int i = 0; i < level.Backups.Count; i++)
            {
                int changeCount = i == 0 ? level.Backups[i].GUIDs.Count : level.CalculateDiff(level.Backups[i - 1], level.Backups[i]);
                backupList.Items.Add(new ListViewItem(new string[] { level.Backups[i].Name, level.Backups[i].Date, changeCount + " Files Modified" }));
            }
            backupLabel.Text = "Create Backup (" + level.CalculateDiff(level.Backups.Count == 0 ? null : level.Backups[level.Backups.Count - 1]) + " Changes)";
        }

        /* Select a new level */
        private void levelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = new AlienLevel(levelList.SelectedItem.ToString());
            RefreshList();
        }

        /* Create a backup of the currently selected level */
        private void saveBackup_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            level.CreateBackup(backupName.Text);
            RefreshList();
            this.Cursor = Cursors.Default;
        }

        /* Restore the selected backup for the selected level */
        private void restoreSelectedBackup(object sender, EventArgs e)
        {
            if (backupList.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select one backup from the list to restore.", "None or multiple selected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (MessageBox.Show("Is Alien: Isolation closed?\nAre all mod tools are closed?", "About to restore...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (level.RestoreBackup(level.Backups[backupList.SelectedItems[0].Index].ID))
                {
                    RefreshList();
                    MessageBox.Show("Backup successfully restored!", "Restored backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to restore backup!\nPlease close anything that may be using the files within the level, and try again.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }

        /* Delete the selected backups for the selected level */
        private void deleteSelectedBackups_Click(object sender, EventArgs e)
        {
            if (backupList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a backup from the list to delete.", "None selected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < backupList.SelectedItems.Count; i++)
                level.DeleteBackup(level.Backups[backupList.SelectedItems[i].Index].ID);
            RefreshList();
            this.Cursor = Cursors.Default;
        }

        /* Backup every level as they stand right now! */
        private void backupAllNow_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will take some time!\nPlease do not close the tool.", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Cursor = Cursors.WaitCursor;

            List<string> levels = Level.GetLevels(SettingsManager.GetString("PATH_GameRoot"));
            //Parallel.ForEach(levels, (levelName) =>
            //{
            //    AlienLevel lvl = new AlienLevel(levelName);
            //    lvl.CreateBackup(lvl.Backups.Count == 0 ? "First backup" : "Automated backup across all levels");
            //});
            //Doing this in parallel requires so much RAM I don't think it's really feasible for most modders that use these tools.
            foreach (string levelName in levels)
            {
                AlienLevel lvl = new AlienLevel(levelName);
                lvl.CreateBackup(lvl.Backups.Count == 0 ? "First backup" : "Automated backup across all levels");
            }

            level = new AlienLevel(levelList.SelectedItem.ToString());
            RefreshList();
            this.Cursor = Cursors.Default;
        }
    }
}
