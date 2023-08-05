using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms.Design;
using OpenCAGE;

namespace LevelBackup
{
    public class AlienLevel
    {
        public List<AlienBackup> Backups = new List<AlienBackup>();
        private List<AlienFile> Files = new List<AlienFile>();

        private string LevelFolder;
        private string BackupFile;

        public AlienLevel(string level)
        {
            LevelFolder = SettingsManager.GetString("PATH_GameRoot") + "/DATA/ENV/PRODUCTION/" + level + "/";
            BackupFile = SettingsManager.GetString("PATH_GameRoot") + "/DATA/MODTOOLS/BACKUPS/" + level + ".BAK";

            Load();
        }

        /* Create a new backup */
        public void CreateBackup(string name)
        {
            AlienBackup Backup = new AlienBackup() { Name = name, Date = DateTime.Now.ToString("dd-MM-yy"), GUIDs = new List<string>() };

            string[] files = Directory.GetFiles(LevelFolder, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Substring(LevelFolder.Length);
                string fileHash = GenerateFileHash(files[i]);

                AlienFile file = Files.FirstOrDefault(o => o.Name == fileName);
                if (file == null)
                {
                    file = new AlienFile() { Name = fileName, Revisions = new List<AlienFile.Revision>() };
                    Files.Add(file);
                }

                AlienFile.Revision revision = file.Revisions.FirstOrDefault(o => o.GUID == fileHash);
                if (revision == null)
                {
                    revision = new AlienFile.Revision() { GUID = fileHash, Content = File.ReadAllBytes(files[i]) };
                    file.Revisions.Add(revision);
                }

                Backup.GUIDs.Add(fileHash);
            }

            Backups.Add(Backup);
            Save();
        }

        /* Delete an existing backup */
        public void DeleteBackup(string name)
        {
            Backups.Remove(Backups.FirstOrDefault(o => o.Name == name));

            //TODO: currently this is NOT working. it deletes all files?

            List<AlienFile> trimmedFiles = new List<AlienFile>();
            for (int i = 0; i < Files.Count; i++)
            {
                List<AlienFile.Revision> trimmedRevisions = new List<AlienFile.Revision>();
                for (int x = 0; x < Files[i].Revisions.Count; x++)
                {
                    bool used = false;
                    for (int y = 0; y < Backups.Count; y++)
                    {
                        if (Backups[y].GUIDs.Contains(Files[i].Revisions[x].GUID))
                        {
                            used = true;
                            break;
                        }
                    }
                    if (!used) continue;
                    trimmedRevisions.Add(Files[i].Revisions[x]);
                }
                if (trimmedRevisions.Count == 0) continue;
                Files[i].Revisions = trimmedRevisions;
                trimmedFiles.Add(Files[i]);
            }

            Files = trimmedFiles;
            Save();
        }

        /* Load the backup archive into memory */
        private void Load()
        {
            if (!File.Exists(BackupFile)) return;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(BackupFile)))
            {
                int backupCount = reader.ReadInt32();
                for (int i = 0; i < backupCount; i++)
                {
                    AlienBackup backup = new AlienBackup() { Name = reader.ReadString(), Date = reader.ReadString(), GUIDs = new List<string>() };
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
                    AlienFile file = new AlienFile() { Name = reader.ReadString(), Revisions = new List<AlienFile.Revision>() };
                    int revisionCount = reader.ReadInt32();
                    for (int x = 0; x < revisionCount; x++)
                    {
                        AlienFile.Revision revision = new AlienFile.Revision() { GUID = reader.ReadString() };
                        int contentLength = reader.ReadInt32();
                        revision.Content = reader.ReadBytes(contentLength);
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

                writer.Write(Backups.Count);
                for (int i = 0; i < Backups.Count; i ++)
                {
                    writer.Write(Backups[i].Name);
                    writer.Write(Backups[i].Date);
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
                        writer.Write(Files[i].Revisions[x].GUID);
                        writer.Write(Files[i].Revisions[x].Content.Length);
                        writer.Write(Files[i].Revisions[x].Content);
                    }
                }
            }
        }

        /* Generate a hash for a given file */
        private string GenerateFileHash(string path)
        {
            MD5 md5 = MD5.Create();
            byte[] contentBytes = File.ReadAllBytes(path);
            md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
            md5.TransformFinalBlock(new byte[0], 0, 0);
            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
        }


        public class AlienBackup
        {
            public string Name;
            public string Date;
            public List<string> GUIDs;
        }

        private class AlienFile
        {
            public string Name;
            public List<Revision> Revisions;

            public class Revision
            {
                public string GUID;
                public byte[] Content;
            }
        }
    }
}
