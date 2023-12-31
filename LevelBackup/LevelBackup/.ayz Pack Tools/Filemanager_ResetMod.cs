﻿using System;
using System.Windows.Forms;

namespace LevelBackup
{
    public partial class Filemanager_ResetMod : Form
    {
        AlienModPack AlienPacker = new AlienModPack();

        public Filemanager_ResetMod()
        {
            InitializeComponent();
        }

        //reset all
        private void SelectMod_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("ALL", false);
        }

        //reset individual
        private void ResetGraphics_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("GRAPHICS", false);
        }
        private void ResetLighting_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("LIGHTING", false);
        }
        private void ResetAlienConfigs_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("ALIENCONFIGS", false);
        }
        private void ResetDifficulties_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("DIFFICULTIES", false);
        }
        private void ResetViewconesets_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("VIEWCONES", false);
        }
        private void ResetAmmo_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("AMMO", false);
        }
        private void ResetGblItem_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("GBL_ITEM", false);
        }
        private void ResetChrInfo_Click(object sender, EventArgs e)
        {
            AlienPacker.ResetFiles("CHR_INFO", false);
        }
    }
}
