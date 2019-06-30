using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RL_Decrypt_Extract
{
    public partial class MainForm : Form
    {
        string decryptedPath = "Decrypted";
        string extractedPath = "Extracted_Files";
        string[] encFiles;
        string[] decFiles;
        string rootDir;
        string decDir;
        string extDir;

        /// <summary>
        /// Constructor.
        /// Initialises the form.
        /// </summary>
        public MainForm()
        {
            rootDir = Path.GetDirectoryName(Application.StartupPath).ToString();
            InitializeComponent();
        }

        /// <summary>
        /// Decrypts, moves, then extracts the upk files.
        /// </summary>
        private void btnRun_Click(object sender, EventArgs e)
        {
            decryptFiles();
            moveFiles();
            extractMeshes();
            MessageBox.Show("Extraction Completed.", "Done");
        }

        /// <summary>
        /// Runs decryption on any upk files contained in the same directory as this executable.
        /// </summary>
        private void decryptFiles()
        {
            encFiles = Directory.GetFiles(rootDir, "*.upk");
            
            // Decrypt the files.
            foreach (string file in encFiles)
            {
                string fileName = Path.GetFileName(file);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = rootDir + @"\rocketleague_decrypt.exe",
                    Arguments = fileName
                };
                Process.Start(startInfo);
            }
        }

        /// <summary>
        /// Deletes any files that are encrypted within the folder.
        /// </summary>
        private void btnDeleteEncrypted_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your UPK? \nTHIS CANNOT BE UNDONE!", "Warning", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Get all the files and delete them.
                string[] files = Directory.GetFiles(rootDir, "*.upk");
                foreach(string file in files)
                    System.IO.File.Delete(file);
            }
        }

        /// <summary>
        /// Moves all decrypted files into a separate folder (named 'decrypted').
        /// </summary>
        private void moveFiles()
        {
            decDir = Path.Combine(rootDir, decryptedPath);

            // Create the decrypted file directory.
            Directory.CreateDirectory(decDir);

            // Allocate 5 seconds for files to be created (varying file sizes are not accounted for!).
            int timeToSleep = 5000;
            MessageBox.Show("Waiting " + timeToSleep / 1000 + " seconds before continuing, to allow decryption", "Click OK");
            Thread.Sleep(timeToSleep);

            try
            {
                foreach (string encryptedFile in encFiles)
                {
                    bool fileInUse = true;
                    
                    // Keep trying to move the files as they may be in use if they are still being extracted.
                    while (fileInUse)
                    {
                        try
                        {
                            // Move the decrypted file if it isn't already present.
                            if (!System.IO.File.Exists(decDir + @"\" + Path.GetFileName(encryptedFile)))
                                System.IO.File.Move(encryptedFile + ".unpacked", decDir + @"\" + Path.GetFileName(encryptedFile));
                            fileInUse = false;

                            // Delete the old file.
                            if (System.IO.File.Exists(encryptedFile + ".unpacked"))
                                System.IO.File.Delete(encryptedFile + ".unpacked");
                        } catch (IOException) { continue; }
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("Something has gone wrong moving the unpacked files", "Unexpected Error");
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Run each upk using the extractor, extract to Extracted folder.
        /// </summary>
        private void extractMeshes()
        {
            // Set the dir to extract meshes from files to.
            extDir = Path.Combine(rootDir, extractedPath);

            // Get decrypted files.
            Directory.CreateDirectory(extDir);
            decFiles = Directory.GetFiles(decDir, "*.upk");
            
            // Extract the files.
            foreach (string decryptedFile in decFiles)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = rootDir + @"\umodel.exe";
                startInfo.Arguments = "-noanim -export \"" + decryptedFile + "\" -out=\"" + extDir + "\"";
                Console.WriteLine(startInfo.Arguments);
                Process.Start(startInfo);
            }
        }
    }
}
