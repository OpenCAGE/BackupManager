﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms.Design;
using OpenCAGE;
using System.Xml.Linq;

namespace LevelBackup
{
    public class AlienLevel
    {
        public List<AlienBackup> Backups = new List<AlienBackup>();
        private List<AlienFile> Files = new List<AlienFile>();

        //TODO: change this to write the contents out to external files so we don't have to load everything every time 

        private string LevelFolder;
        private string BackupFolder;
        private string BackupFile;

        private int Version = 1;

        public AlienLevel(string level)
        {
            LevelFolder = SettingsManager.GetString("PATH_GameRoot") + "/DATA/ENV/PRODUCTION/" + level;
            BackupFolder = SettingsManager.GetString("PATH_GameRoot") + "/DATA/MODTOOLS/BACKUPS/" + level;
            BackupFile = BackupFolder + ".BAK";

            Directory.CreateDirectory(BackupFolder);

            Load();
        }

        /* Create a new backup */
        public void CreateBackup(string name)
        {
            AlienBackup Backup = new AlienBackup() { Name = name, Date = DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), ID = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), GUIDs = new List<string>() };

            string[] files = Directory.GetFiles(LevelFolder, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Substring(LevelFolder.Length + 1);
                string fileHash = GenerateFileHash(files[i]);

                AlienFile file = Files.FirstOrDefault(o => o.Name == fileName);
                if (file == null)
                {
                    file = new AlienFile() { Name = fileName, Revisions = new List<string>() };
                    Files.Add(file);
                }

                string revisionFile = BackupFolder + "/" + fileHash;
                if (!File.Exists(revisionFile))
                    File.WriteAllBytes(revisionFile, File.ReadAllBytes(files[i]));
                if (!file.Revisions.Contains(fileHash))
                    file.Revisions.Add(fileHash);

                Backup.GUIDs.Add(fileHash);
            }

            Backups.Add(Backup);
            Save();
        }

        /* Restore a backup */
        public bool RestoreBackup(Int64 ID)
        {
            AlienBackup backup = Backups.FirstOrDefault(o => o.ID == ID);
            if (backup == null) return false;

            //First, try keep a version of the current folder
            try
            {
                if (Directory.Exists(LevelFolder + "_COPY"))
                    Directory.Delete(LevelFolder + "_COPY", true);
                CopyDirectory(LevelFolder, LevelFolder + "_COPY", true);
            }
            catch
            {
                return false;
            }

            //Now, try clear the current folder
            try
            {
                Directory.Delete(LevelFolder, true);
                Directory.CreateDirectory(LevelFolder);
            }
            catch
            {
                try
                {
                    Directory.Delete(LevelFolder + "_COPY", true);
                }
                catch
                {
                    return false;
                }
                return false;
            }

            //Restore the backup
            try
            {
                for (int i = 0; i < Files.Count; i++)
                {
                    for (int x = 0; x < Files[i].Revisions.Count; x++)
                    {
                        for (int z = 0; z < backup.GUIDs.Count; z++)
                        {
                            if (Files[i].Revisions[x] == backup.GUIDs[z])
                            {
                                Directory.CreateDirectory(LevelFolder + "/" + Files[i].Name.Substring(0, Files[i].Name.Length - Path.GetFileName(Files[i].Name).Length));
                                File.WriteAllBytes(LevelFolder + "/" + Files[i].Name, File.ReadAllBytes(BackupFolder + "/" + Files[i].Revisions[x]));
                            }
                        }
                    }
                }
            }
            catch
            {
                try
                {
                    Directory.Delete(LevelFolder + "_COPY", true);
                }
                catch
                {
                    return false;
                }
                return false;
            }

            //Clear the copy
            try
            {
                Directory.Delete(LevelFolder + "_COPY", true);
            }
            catch
            {
                return false;
            }

            return true;
        }
        private void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists) throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        /* Delete an existing backup */
        public void DeleteBackup(Int64 ID)
        {
            Backups.Remove(Backups.FirstOrDefault(o => o.ID == ID));

            List<AlienFile> trimmedFiles = new List<AlienFile>();
            for (int i = 0; i < Files.Count; i++)
            {
                List<string> trimmedRevisions = new List<string>();
                for (int x = 0; x < Files[i].Revisions.Count; x++)
                {
                    bool used = false;
                    for (int y = 0; y < Backups.Count; y++)
                    {
                        if (Backups[y].GUIDs.Contains(Files[i].Revisions[x]))
                        {
                            used = true;
                            break;
                        }
                    }
                    if (!used)
                    {
                        if (File.Exists(BackupFolder + "/" + Files[i].Revisions[x]))
                            File.Delete(BackupFolder + "/" + Files[i].Revisions[x]);
                        continue;
                    }
                    trimmedRevisions.Add(Files[i].Revisions[x]);
                }
                if (trimmedRevisions.Count == 0) continue;
                Files[i].Revisions = trimmedRevisions;
                trimmedFiles.Add(Files[i]);
            }

            Files = trimmedFiles;
            Save();
        }

        /* Get the number of files changed between backups - leave 2nd arg null to calculate current state */
        public int CalculateDiff(AlienBackup orig, AlienBackup mod = null)
        {
            string[] GUIDs = mod?.GUIDs?.ToArray();
            if (mod == null)
            {
                string[] files = Directory.GetFiles(LevelFolder, "*.*", SearchOption.AllDirectories);
                GUIDs = new string[files.Length];
                Parallel.For(0, files.Length, (i) =>
                {
                    GUIDs[i] = GenerateFileHash(files[i]);
                });
            }
            if (orig == null) return GUIDs.Length;

            int changes = 0;
            for (int i = 0; i < GUIDs.Length; i++)
            {
                if (!orig.GUIDs.Contains(GUIDs[i]))
                    changes++;
            }
            changes += Math.Abs(GUIDs.Length - orig.GUIDs.Count);
            return changes;
        }


        /* Load the backup archive into memory */
        private void Load()
        {
            if (!File.Exists(BackupFile)) return;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(BackupFile)))
            {
                if (reader.ReadInt32() != Version)
                {
                    throw new Exception("Backup version mismatch");
                }

                int backupCount = reader.ReadInt32();
                for (int i = 0; i < backupCount; i++)
                {
                    AlienBackup backup = new AlienBackup() { Name = reader.ReadString(), Date = reader.ReadString(), ID = reader.ReadInt64(), GUIDs = new List<string>() };
                    int guidCount = reader.ReadInt32();
                    for (int x = 0; x < guidCount; x++)
                    {
                        backup.GUIDs.Add(reader.ReadString());
                    }
                    Backups.Add(backup);
                }

                int fileCount = reader.ReadInt32();
                for (int i = 0; i < fileCount; i++)
                {
                    AlienFile file = new AlienFile() { Name = reader.ReadString(), Revisions = new List<string>() };
                    int revisionCount = reader.ReadInt32();
                    for (int x = 0; x < revisionCount; x++)
                    {
                        file.Revisions.Add(reader.ReadString());
                    }
                    Files.Add(file);
                }
            }
        }

        /* Save the backup archive out to disk */
        private void Save()
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(BackupFile)))
            {
                writer.BaseStream.SetLength(0);
                writer.Write(Version);

                writer.Write(Backups.Count);
                for (int i = 0; i < Backups.Count; i ++)
                {
                    writer.Write(Backups[i].Name);
                    writer.Write(Backups[i].Date);
                    writer.Write(Backups[i].ID);
                    writer.Write(Backups[i].GUIDs.Count);
                    for (int x = 0; x < Backups[i].GUIDs.Count; x++)
                    {
                        writer.Write(Backups[i].GUIDs[x]);
                    }
                }

                writer.Write(Files.Count);
                for (int i = 0; i < Files.Count; i++)
                {
                    writer.Write(Files[i].Name);
                    writer.Write(Files[i].Revisions.Count);
                    for (int x = 0; x < Files[i].Revisions.Count; x++)
                    {
                        writer.Write(Files[i].Revisions[x]);
                    }
                }
            }
        }

        /* Generate a hash for a given file */
        private string GenerateFileHash(string path)
        {
            string hash = "";
            {
                byte[] contentBytes = File.ReadAllBytes(path);

                MD5 md5 = MD5.Create();
                md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
                md5.TransformFinalBlock(new byte[0], 0, 0);
                hash = BitConverter.ToString(md5.Hash);
            }
            return hash.Replace("-", "").ToLower();
        }


        public class AlienBackup
        {
            public string Name;
            public string Date;
            public Int64 ID;
            public List<string> GUIDs;
        }

        private class AlienFile
        {
            public string Name;
            public List<string> Revisions;
        }
    }
}
