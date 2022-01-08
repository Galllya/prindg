using System;
using System.Windows.Forms;

namespace pwsg_2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            Hide();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
   
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
