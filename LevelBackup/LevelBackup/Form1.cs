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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LevelBackup
{
    public partial class Form1 : Form
    {
        AlienLevel level = null;

        public Form1()
        {
            InitializeComponent();

            levelList.Items.AddRange(Level.GetLevels(SettingsManager.GetString("PATH_GameRoot")).ToArray());
            levelList.SelectedIndex = 0;

            RefreshList();
        }

        private void RefreshList()
        {
            backupList.Items.Clear();
            for (int i = 0; i < level.Backups.Count; i++)
                backupList.Items.Add(new ListViewItem(new string[] { level.Backups[i].Name, level.Backups[i].Date }));
        }

        private void levelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = new AlienLevel(levelList.SelectedItem.ToString());
            RefreshList();
        }

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
    }
}
