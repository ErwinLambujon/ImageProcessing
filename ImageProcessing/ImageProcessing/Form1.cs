using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{

    public partial class Form1 : Form
    {
        Bitmap loaded;
        Bitmap processed;
        public Form1()
        {
            InitializeComponent();

             basicCopyToolStripMenuItem1.Enabled = false;
             greyscaleToolStripMenuItem1.Enabled = false;
             colorInversionToolStripMenuItem.Enabled = false;
             histogramToolStripMenuItem.Enabled = false;
             sepiaToolStripMenuItem.Enabled = false;
             saveToolStripMenuItem.Enabled = false;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                String file_name = openDialog.FileName;
                loaded = (Bitmap)Image.FromFile(file_name);

                pictureBox1.Image = loaded;

                basicCopyToolStripMenuItem1.Enabled = true;
                greyscaleToolStripMenuItem1.Enabled = true;
                colorInversionToolStripMenuItem.Enabled = true;
                histogramToolStripMenuItem.Enabled = true;
                sepiaToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }

        }

        private void basicCopyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        private void greyscaleToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Color pixel;
            int greyscale;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    greyscale = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(x, y, Color.FromArgb(greyscale, greyscale, greyscale));
                }
            }
            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.B, 255 - pixel.G));
                }
            }
            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            int greyscale;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    greyscale = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(x, y, Color.FromArgb(greyscale, greyscale, greyscale));
                }

            int[] histogram = new int[256];
            Color value;
            for (int x = 0; x < loaded.Width; x++)
                for (int y = 0; y < loaded.Height; y++)
                {
                    value = processed.GetPixel(x, y);
                    histogram[value.R]++;
                }

            Bitmap matrix = new Bitmap(256, 800);
               for (int x = 0; x < 256; x++)
               
                   for (int y = 0; y < 800; y++)
                   {
                    matrix.SetPixel(x, y, Color.White);
                   }

            for (int x = 0; x < 256; x++)

                for (int y = 0; y < Math.Min(histogram[x]/5, 800); y++)
                {
                    matrix.SetPixel(x, 799 - y, Color.Black);
                }
            pictureBox2.Image = matrix;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for(int x = 0; x < loaded.Width; x++)
            {
                for(int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);

                    int green = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int red = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int blue = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    processed.SetPixel(x, y, Color.FromArgb(Math.Min(255, red), Math.Min(255, green), Math.Min(255, blue)));
                }
            }
            pictureBox2.Image = processed;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (processed != null)
             {
                 saveFileDialog1.ShowDialog();
             }
             else
             {
                 MessageBox.Show("No image to be save.");
             }

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }
    }
}