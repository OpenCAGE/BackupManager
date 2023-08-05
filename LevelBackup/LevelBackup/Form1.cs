using CathodeLib;
using Newtonsoft.Json.Linq;
using OpenCAGE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace LevelBackup
{
    public partial class Form1 : Form
    {
        AlienLevel level = null;

        public Form1()
        {
            InitializeComponent();

            Directory.CreateDirectory(SettingsManager.GetString("PATH_GameRoot") + "/DATA/MODTOOLS/BACKUPS/");

            levelList.Items.AddRange(Level.GetLevels(SettingsManager.GetString("PATH_GameRoot")).ToArray());
            levelList.SelectedIndex = 0;

            RefreshList();

            // Backups are not complete copies of the level.
            // When you make a backup, it generates hashes for all files in the level.
            // Check to see if those hashes are the same as the vanilla base.
            // If they are not the same as the vanilla base, check to see if they are in any other backups.
            // If they are not in any other backups and differ from vanilla, we should save the file.
            // The backup then becomes a list of hashes that point to the saved modified files.
            // When a backup is deleted, we check that the hashes within the backup are not used by any other backups.
            // If they're not, we can delete the saved modified file.
        }

        private void RefreshList()
        {
            saveList.Items.Clear();
            for (int i = 0; i < level.Backups.Count; i++)
                saveList.Items.Add(level.Backups[i].Name);
        }

        private void levelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = new AlienLevel(levelList.SelectedItem.ToString());
            RefreshList();
        }

        private void saveBackup_Click(object sender, EventArgs e)
        {
            level.CreateBackup(backupName.Text);
            RefreshList();
        }

        /* Select/de-select all items in the save list */
        private void selectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < saveList.Items.Count; i++)
                saveList.SetItemChecked(i, true);
        }
        private void deselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < saveList.Items.Count; i++)
                saveList.SetItemChecked(i, false);
        }

        /* Restore the selected backup for the selected level */
        private void restoreSelectedBackup(object sender, EventArgs e)
        {

        }

        /* Delete the selected backups for the selected level */
        private void deleteSelectedBackups_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < saveList.CheckedItems.Count; i++)
                level.DeleteBackup(saveList.CheckedItems[i].ToString());
            RefreshList();
        }
    }
}
