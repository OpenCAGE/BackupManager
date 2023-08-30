using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using CATHODE;
using System.Xml;

namespace LevelBackup
{
    public partial class Landing : Form
    {
        public Landing()
        {
            InitializeComponent();
            this.Focus();
        }

        private void BehaviourPacker_Load(object sender, EventArgs e)
        {
            LandingWPF wpf = (LandingWPF)elementHost1.Child;
            wpf.SetVersionInfo(ProductVersion);
            wpf.OnManageLevels += ManageLevels;
            wpf.OnManageConfigs += ManageConfigs;
            wpf.OnManageBehaviours += ManageBehaviours;
        }

        private void ManageLevels()
        {
            Form1 dialog = new Form1();
            dialog.Show();
            dialog.FormClosed += Dialog_FormClosed;
            this.Hide();
        }
        private void Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void ManageConfigs()
        {

        }

        private void ManageBehaviours()
        {
            //Kill Brainiac
            List<Process> allProcesses = new List<Process>(Process.GetProcessesByName("BehaviourTreeEditor"));
            for (int i = 0; i < allProcesses.Count; i++) allProcesses[i].Kill();

            //Reset binary behaviour
            string pathToBML = SharedData.pathToAI + @"\DATA\BINARY_BEHAVIOR\_DIRECTORY_CONTENTS.BML";
            string pathToFolder = SharedData.pathToAI + @"\DATA\BEHAVIOR\";
            File.WriteAllBytes(pathToBML, Properties.Resources._DIRECTORY_CONTENTS);
            if (Directory.Exists(pathToFolder)) Directory.Delete(pathToFolder, true);
            Directory.CreateDirectory(pathToFolder);

            //Export binary behaviours to XML folder
            BML bml = new BML(pathToBML);
            XmlDocument xml = bml.Content;
            XmlNodeList files = xml.SelectNodes("//DIR/File");
            foreach (XmlNode file in files)
                File.WriteAllText(pathToFolder + file.Attributes["name"].Value.Substring(0, file.Attributes["name"].Value.Length - 3) + "xml", file.InnerXml);

            MessageBox.Show("Behaviour trees have been reset to defaults!", "Reset complete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
