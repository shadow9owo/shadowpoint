using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace shadowpoint
{
    public partial class Form2 : Form
    {
        private string[] tempfile;
        private string[] config;
        private string selpath;
        private string[] slidecof;

        public Form2(string path)
        {
            InitializeComponent();
            selpath = path;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                slidecof = File.ReadAllLines(selpath + @"\slide.cof");
                config = File.ReadAllLines(selpath + @"\config.cof");
                this.Text = config[0];
                if (config[1] != "")
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    this.BackgroundImage = Image.FromFile(selpath + config[1].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Text = "default title";
            }

            try
            {
                try
                {
                    int objectcount;
                    if (Int32.TryParse(slidecof[0], out objectcount))
                    {
                        for (int i = 1; i <= objectcount; i++) // Start at index 1 and iterate up to objectcount
                        {
                            tempfile = File.ReadAllLines(selpath + slidecof[i]);

                            if (tempfile[0].ToLower().Contains("image"))
                            {
                                int x, y, size;
                                Int32.TryParse(tempfile[1], out size);
                                Int32.TryParse(tempfile[2], out x);
                                Int32.TryParse(tempfile[3], out y);

                                PictureBox a = new PictureBox
                                {
                                    Name = "PictureBox_" + i,
                                    ImageLocation = selpath + tempfile[4],
                                    Location = new System.Drawing.Point(x, y),
                                    Size = new System.Drawing.Size(size, size),
                                    SizeMode = PictureBoxSizeMode.StretchImage
                                };

                                this.Controls.Add(a);
                            }
                            else if (tempfile[0].ToLower().Contains("text"))
                            {
                                int x, y, size;
                                Int32.TryParse(tempfile[1], out size);
                                Int32.TryParse(tempfile[2], out x);
                                Int32.TryParse(tempfile[3], out y);
                                Color color = System.Drawing.ColorTranslator.FromHtml(tempfile[5]);

                                Label a = new Label
                                {
                                    Name = "Label_" + i,
                                    Text = tempfile[4],
                                    Location = new System.Drawing.Point(x, y),
                                    Font = new System.Drawing.Font("Arial", size),
                                    BackColor = Color.Transparent,
                                    ForeColor = color
                                };

                                this.Controls.Add(a);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Object count is not a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {

            }
        }
    }
}
