using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
                if (textBox2.Text != "")
                {
                    int tempint;
                    Int32.TryParse(textBox2.Text, out tempint);
                    Form2 a = new Form2(selpath, tempint);
                    a.Show();
                    this.Hide();
                }
                else
                {
                    Form2 a = new Form2(selpath, 1);
                    a.Show();
                    this.Hide();
                }
            }else
            {
                MessageBox.Show("Please select an presentation folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
