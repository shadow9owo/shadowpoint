using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace shadowpoint
{
    public partial class Form3 : Form
    {
        private string selectedtype = "";
        private List<int> cords = new List<int>();
        private string presentationname;
        private string workspace = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\";
        private int slide = 1;
        private string[] slidecof;

        public Form3(string presname)
        {
            InitializeComponent();
            presentationname = presname;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + presentationname + @"\" + slide))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + presentationname + @"\" + slide);
            }
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + presentationname + @"\" + slide + @"\slide.cof"))
            {
                slidecof = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + presentationname + @"\" + slide + @"\slide.cof");
                int slides;
                Int32.TryParse(slidecof[0], out slides);
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {/*
            cords.Clear();
            cords.Add(e.X);
            cords.Add(e.Y);
            if (selectedtype.ToLower().Contains("text"))
            {
                int x, y, size;
                Color color = System.Drawing.ColorTranslator.FromHtml(tempfile[5]);

                Label a = new Label
                {
                    Name = "Label_" + ,
                    Text = tempfile[4].Replace("\n", Environment.NewLine),
                    Location = new System.Drawing.Point(x, y),
                    Font = new System.Drawing.Font("Arial", size),
                    BackColor = Color.Transparent,
                    ForeColor = color,
                    AutoSize = true
                };

                this.Controls.Add(a);
            }
            else if (selectedtype.ToLower().Contains("image"))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Title = "Select PNG File";
                openFileDialog.Filter = "PNG Files|*.png";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    File.Copy(filePath, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + presentationname + @"\" + slide + @"\" + Path.GetFileName(filePath));
                }
            }else
            {

            }
        */}
    }
}
