using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace shadowpoint
{
    public partial class Form2 : Form
    {
        private bool end = false;
        private string[] tempfile;
        private string[] config;
        private string selpath;
        private string[] slidecof;
        private int intslide;

        public Form2(string path,int slide)
        {
            InitializeComponent();
            selpath = path;
            intslide = slide;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                slidecof = File.ReadAllLines(selpath + @"\" + intslide + @"\slide.cof");
                config = File.ReadAllLines(selpath + @"\" + intslide + @"\config.cof");
                this.Text = config[0];
                if (config[1] != "")
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    this.BackgroundImage = Image.FromFile(selpath + @"\" + intslide + config[1].ToString());
                }
                if (config[2] != "")
                {
                    if (config[2] == "true")
                    {
                        ShowIcon = false;
                    }else
                    {

                    }
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
                            tempfile = File.ReadAllLines(selpath +@"\"+ intslide + @"\" + slidecof[i]);

                            if (tempfile[0].ToLower().Contains("image"))
                            {
                                int x, y, size;
                                Int32.TryParse(tempfile[1], out size);
                                Int32.TryParse(tempfile[2], out x);
                                Int32.TryParse(tempfile[3], out y);

                                PictureBox a = new PictureBox
                                {
                                    Name = "PictureBox_" + i,
                                    ImageLocation = selpath + @"\" + intslide + @"\" + tempfile[4],
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
                                    Text = tempfile[4].Replace("\n", Environment.NewLine),
                                    Location = new System.Drawing.Point(x, y),
                                    Font = new System.Drawing.Font("Arial", size),
                                    BackColor = Color.Transparent,
                                    ForeColor = color,
                                    AutoSize = true
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

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                foreach (Control control in this.Controls)
                {
                    if (control is PictureBox)
                    {
                        this.Controls.Remove(control);
                        control.Dispose();
                        break;
                    }
                }

                foreach (Control control in this.Controls)
                {
                    if (control is Label)
                    {
                        this.Controls.Remove(control);
                        control.Dispose();
                        break;
                    }
                }

                intslide = intslide + 1;
                if (!Directory.Exists(selpath + @"\" + intslide))
                {
                    if (Directory.Exists(selpath + @"\end"))
                    {
                        if (end)
                        {
                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Close();
                        }else
                        {
                            end = true;
                        }
                        try
                        {
                            slidecof = File.ReadAllLines(selpath + @"\" + "end" + @"\slide.cof");
                            config = File.ReadAllLines(selpath + @"\" + "end" + @"\config.cof");
                            this.Text = config[0];
                            if (config[1] != "")
                            {
                                this.BackgroundImageLayout = ImageLayout.Stretch;
                                this.BackgroundImage = Image.FromFile(selpath + @"\" + "end" + config[1].ToString());
                            }
                            if (config[2] != "")
                            {
                                if (config[2] == "true")
                                {
                                    ShowIcon = false;
                                }
                                else
                                {

                                }
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
                                    for (int i = 1; i <= objectcount; i++)
                                    {
                                        tempfile = File.ReadAllLines(selpath + @"\" + "end" + @"\" + slidecof[i]);

                                        if (tempfile[0].ToLower().Contains("image"))
                                        {
                                            int x, y, size;
                                            Int32.TryParse(tempfile[1], out size);
                                            Int32.TryParse(tempfile[2], out x);
                                            Int32.TryParse(tempfile[3], out y);

                                            PictureBox a = new PictureBox
                                            {
                                                Name = "PictureBox_" + i,
                                                ImageLocation = selpath + @"\" + "end" + @"\" + tempfile[4],
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
                                                Text = tempfile[4].Replace("\n", Environment.NewLine),
                                                Location = new System.Drawing.Point(x, y),
                                                Font = new System.Drawing.Font("Arial", size),
                                                BackColor = Color.Transparent,
                                                ForeColor = color,
                                                AutoSize = true
                                            };

                                            this.Controls.Add(a);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Object count is not a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Form1 form1 = new Form1();
                                    form1.Show();
                                    this.Close();
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
                    }else
                    {
                        MessageBox.Show("end folder not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        slidecof = File.ReadAllLines(selpath + @"\" + intslide + @"\slide.cof");
                        config = File.ReadAllLines(selpath + @"\" + intslide + @"\config.cof");
                        this.Text = config[0];
                        if (config[1] != "")
                        {
                            this.BackgroundImageLayout = ImageLayout.Stretch;
                            this.BackgroundImage = Image.FromFile(selpath + @"\" + intslide + config[1].ToString());
                        }
                        if (config[2] != "")
                        {
                            if (config[2] == "true")
                            {
                                ShowIcon = false;
                            }
                            else
                            {

                            }
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
                                    tempfile = File.ReadAllLines(selpath + @"\" + intslide + @"\" + slidecof[i]);

                                    if (tempfile[0].ToLower().Contains("image"))
                                    {
                                        int x, y, size;
                                        Int32.TryParse(tempfile[1], out size);
                                        Int32.TryParse(tempfile[2], out x);
                                        Int32.TryParse(tempfile[3], out y);

                                        PictureBox a = new PictureBox
                                        {
                                            Name = "PictureBox_" + i,
                                            ImageLocation = selpath + @"\" + intslide + @"\" + tempfile[4],
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
                                            Text = tempfile[4].Replace("\n", Environment.NewLine),
                                            Location = new System.Drawing.Point(x, y),
                                            Font = new System.Drawing.Font("Arial", size),
                                            BackColor = Color.Transparent,
                                            ForeColor = color,
                                            AutoSize = true
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
    }
}
