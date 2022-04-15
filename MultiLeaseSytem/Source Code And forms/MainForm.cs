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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarReg cr = new CarReg();
            cr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rental r = new Rental();
            r.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Return rn = new Return();
            rn.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure you want to logout");
            this.Close();
            Application.Exit();   
        }
    }
}
