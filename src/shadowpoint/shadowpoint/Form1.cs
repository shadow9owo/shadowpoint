using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Ionic.Zip;

namespace shadowpoint
{
    public partial class Form1 : Form
    {
        private static string selpath;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath.ToString();
                selpath = folderBrowserDialog1.SelectedPath.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selpath != null)
            {
                this.Hide();
                Form2 a = new Form2(selpath, 1, checkBox1.Checked);
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an presentation folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath.ToString();
                selpath = folderBrowserDialog1.SelectedPath.ToString();
            }
            if (IsValidProject(selpath))
            {
                CompressFolder(selpath,selpath + ".spoint");
                MessageBox.Show("project has been exported","info",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }else
            {
                if (!Directory.Exists(selpath))
                {
                    MessageBox.Show("directory not found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("this isnt a valid project","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                return;
            }
        }
        private static bool IsValidProject(string path)
        {
            List<string> tmparr = new List<string>();
            if (!Directory.Exists(path))
            {
                return false;
            }
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles("*.*", SearchOption.AllDirectories))
            {
                tmparr.Add(file.Name); // Collect only file names
            }

            if (!tmparr.Contains("config.cof"))
            {
                return false;
            }

            if (!tmparr.Contains("slide.cof"))
            {
                return false;
            }

            tmparr.Clear();

            foreach (DirectoryInfo dir in di.GetDirectories("*.*", SearchOption.AllDirectories))
            {
                tmparr.Add(dir.Name);
            }

            if (!tmparr.Contains("end"))
            {
                return false;
            }

            return true;
        }
        static void CompressFolder(string sourceFolderPath, string compressedFilePath)
        {
            try
            {
                sourceFolderPath = Path.GetFullPath(sourceFolderPath);
                compressedFilePath = Path.GetFullPath(compressedFilePath);

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(sourceFolderPath);

                    zip.Save(compressedFilePath);
                }

                Console.WriteLine("Compression successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DecompressFolder(string zipFilePath, string extractPath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(extractPath, ExtractExistingFileAction.DoNotOverwrite);
                }
                zip.Dispose();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Spoint Files|*.spoint|All Files|*.*";
            openFileDialog1.Title = "Open an Spoint File";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                try
                {
                    Directory.Delete(Path.GetTempPath() + @"\" + Path.GetFileName(openFileDialog1.FileName), true);
                }catch { }
                DecompressFolder(openFileDialog1.FileName, Path.GetTempPath() + @"\" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName));
                this.Hide();
                selpath = Path.GetTempPath() + @"\" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                Form2 a = new Form2(selpath, 1,checkBox1.Checked);
                a.ShowDialog();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
