using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

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
                Form2 a = new Form2(selpath, 1);
                a.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Please select an presentation folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\");
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\");
                }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text);
                }else
                {
                    DialogResult result = MessageBox.Show("this project already exists do you want to overwrite it?","question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text, true);
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text);
                    }else
                    {
                        return;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\"))
            {
                MessageBox.Show("please create a project first", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text) && textBox2.Text != "")
            {
                DialogResult result = MessageBox.Show("do you really want to delete this directory?", "question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text, true);
                }
                else
                {
                    return;
                }
            }else
            {
                if (textBox2.Text != "")
                {
                    MessageBox.Show("directory was not found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show("please provide an directory", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\"))
            {
                MessageBox.Show("please create a project first", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\shadowpoint\projects\" + textBox2.Text) && textBox2.Text != "")
            {
                Form3 form3 = new Form3(textBox2.Text);
                form3.Show();
                this.Hide();
            }
            else
            {
                if (textBox2.Text != "")
                {
                    MessageBox.Show("directory was not found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("please provide an directory", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
