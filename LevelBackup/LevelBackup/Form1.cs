using CathodeLib;
using OpenCAGE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBackup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            levelList.Items.AddRange(Level.GetLevels(SettingsManager.GetString("PATH_GameRoot")).ToArray());

            // Backups are not complete copies of the level.
            // When you make a backup, it generates hashes for all files in the level.
            // Check to see if those hashes are the same as the vanilla base.
            // If they are not the same as the vanilla base, check to see if they are in any other backups.
            // If they are not in any other backups and differ from vanilla, we should save the file.
            // The backup then becomes a list of hashes that point to the saved modified files.
            // When a backup is deleted, we check that the hashes within the backup are not used by any other backups.
            // If they're not, we can delete the saved modified file.
        }
    }
}
