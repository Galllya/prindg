using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pwsg_2
{
    public partial class Form2 : Form
    {
    
    
        public List<Block> blocks = new List<Block>();
        public PictureBox pictureBox1 = new PictureBox();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PNG|*.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(fileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Diagram files (*.diag)|*.diag";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream file = fileDialog.OpenFile();

                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(file, blocks);
                file.Close();
            }
        }
    }
}
