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
        string backupFolder = "/DATA/MODTOOLS/LEVEL_BACKUPS/";
        const string manifestFile = "/manifest.json";
        JObject manifest;

        public Form1()
        {
            InitializeComponent();

            backupFolder = SettingsManager.GetString("PATH_GameRoot") + backupFolder;
            string[] levels = Level.GetLevels(SettingsManager.GetString("PATH_GameRoot")).ToArray();
            for (int i = 0; i < levels.Length; i++)
            {
                Directory.CreateDirectory(backupFolder + levels[i]);
                levelList.Items.Add(levels[i]);
            }
            levelList.SelectedIndex = 0;

            for (int i = 0; i < 9; i++)
                saveList.Items.Add(i.ToString());

            // Backups are not complete copies of the level.
            // When you make a backup, it generates hashes for all files in the level.
            // Check to see if those hashes are the same as the vanilla base.
            // If they are not the same as the vanilla base, check to see if they are in any other backups.
            // If they are not in any other backups and differ from vanilla, we should save the file.
            // The backup then becomes a list of hashes that point to the saved modified files.
            // When a backup is deleted, we check that the hashes within the backup are not used by any other backups.
            // If they're not, we can delete the saved modified file.
        }

        /* Populate save list with saves from selected level */
        private void levelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string manifestPath = backupFolder + levelList.SelectedItem + manifestFile;
            manifest = File.Exists(manifestPath) ? JObject.Parse(File.ReadAllText(manifestPath)) : new JObject("{\"backups\":[]}");
            RefreshList();
        }
        private void RefreshList()
        {
            saveList.Items.Clear();
            for (int i = 0; i < ((JArray)manifest["backups"]).Count; i++)
                saveList.Items.Add(manifest["backups"][i]["backup_name"]);
        }

        /* Save a backup for the selected level */
        private void saveBackup_Click(object sender, EventArgs e)
        {

            SaveManifest();
        }
        private void SaveManifest()
        {
            JArray backups = new JArray();
            for (int i = 0; i < saveList.Items.Count; i++)
            {
                backups.Add("");
            }
            manifest["backups"] = backups;
            File.WriteAllText(backupFolder + levelList.SelectedItem + manifestFile, manifest.ToString(Newtonsoft.Json.Formatting.Indented));
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

        }
    }
}
