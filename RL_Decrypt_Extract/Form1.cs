using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using IWshRuntimeLibrary;

namespace RL_Decrypt_Extract
{
    public partial class Form1 : Form
    {
        string decryptedPath = "Decrypted";
        string extractedPath = "Extracted_Files";
        string[] encFiles;
        string[] decFiles;
        string rootDir;
        string decDir;
        string extDir;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            decryptFiles();
            moveFiles();
            extractMeshes();
            MessageBox.Show("Extraction Completed.", "Done");
        }

        private void decryptFiles()
        {
            
            encFiles = Directory.GetFiles(Application.StartupPath, "*.upk");
            
            // Decrypt the files
            foreach (string s in encFiles)
            {
                string f = Path.GetFileName(s);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.StartupPath + @"\rocketleague_decrypt.exe";
                startInfo.Arguments = @f;
                Process.Start(startInfo);
            }
        }

        private void btnDeleteEncrypted_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your UPK? \nTHIS CANNOT BE UNDONE!", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(Application.StartupPath, "*.upk");
                } catch (Exception){ }

                foreach(string s in files)
                    System.IO.File.Delete(s);
            }
        }

        // Moves all decrypted files into a separate folder (named 'decrypted')
        private void moveFiles()
        {
            rootDir = Path.GetDirectoryName(Application.StartupPath).ToString();

            decDir = Path.Combine(rootDir, decryptedPath);

            Directory.CreateDirectory(decDir);

            // Allocate 5 seconds for files to be created (varying file sizes are not accounted for)
            int timeToSleep = 5000;
            MessageBox.Show("Waiting " + timeToSleep / 1000 + " seconds before continuing, to allow decryption", "Click OK");
            Thread.Sleep(timeToSleep);

            try
            {
                foreach (string s in encFiles)
                {
                    bool fileInUse = true;

                    while (fileInUse)
                    {
                        try
                        {
                            // Move the decrypted file if it isn't already present
                            if (!System.IO.File.Exists(decDir + @"\" + Path.GetFileName(s)))
                                System.IO.File.Move(s + ".unpacked", decDir + @"\" + Path.GetFileName(s));
                            fileInUse = false;

                            // Delete the old file
                            if (System.IO.File.Exists(s + ".unpacked"))
                                System.IO.File.Delete(s + ".unpacked");
                        } catch (IOException) { continue; }
                    }
                }
            } catch (Exception e) { MessageBox.Show("Something has gone wrong moving the unpacked files", "Unexpected Error"); Console.WriteLine(e.ToString()); }
        }

        // Working
        //Extract all the meshes from these files
        private void extractMeshes()
        {
            // Run each upk using the extractor, extract to Extracted folder

            extDir = Path.Combine(rootDir, extractedPath); // Set the dir to extract meshes from files

            // Get decrypted files
            Directory.CreateDirectory(extDir);
            decFiles = Directory.GetFiles(decDir, "*.upk");
            // Create the shortcut (OBSELETE)
            //createShortcut();
            
            // Extract the files
            foreach (string s in decFiles)
            {
                string f = s;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.StartupPath + @"\umodel.exe";
                startInfo.Arguments = "-noanim -export \"" +@s + "\" -out=\"" + extDir + "\"";
                Console.WriteLine(startInfo.Arguments);
                Process.Start(startInfo);
            }
        }

        // Working (OBSELETE, as arguments passed directly to application)
        private void createShortcut()
        {
            WshShell shell = new WshShell();
            IWshShortcut link = null;
            try
            {
                link = (IWshShortcut)shell.CreateShortcut(Application.StartupPath + "\\umodel.lnk");
                link.Arguments = "-export -out=\"" + extDir + "\"";
                link.TargetPath = Application.StartupPath + @"\umodel.exe";
                link.Save();
            } catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
    }
}
