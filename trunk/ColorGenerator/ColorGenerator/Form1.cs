using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ColorGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtNumImages.KeyDown += new KeyEventHandler(txtNumImages_KeyDown);
            txtNumImages.Focus();
        }

        void txtNumImages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartGenerating();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGenerating();
        }

        private void StartGenerating()
        {
            int numImages;
            if (!int.TryParse(txtNumImages.Text, out numImages))
            {
                lblStatus.Text = "You must enter an integer between 0 and 256^2, inclusive.";
                return;
            }

            lblStatus.Text = "Generating " + numImages + " colors...";
            ColorGenerator generator = new ColorGenerator();
            ICollection<Color> colors = generator.GenerateColors(numImages, grayScale.Checked);
            
            lblStatus.Text = "Saving colors...";
            ColorSaver saver = new ColorSaver();
            saver.Save(colors);
            lblStatus.Text = "Done.";
        }
    }
}