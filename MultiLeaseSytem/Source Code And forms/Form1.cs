using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLeaseSytem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var uname = txtUname.Text;
            var pass = txtPass.Text;

            if(uname.Equals("Admin") && pass.Equals("1234"))
            {
                MainForm ms = new MainForm();
                this.Hide();
                ms.Show();
            }
            else
            {
                MessageBox.Show("username and password does not match, Try again");
                txtUname.Clear();
                txtPass.Clear();
                txtUname.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
