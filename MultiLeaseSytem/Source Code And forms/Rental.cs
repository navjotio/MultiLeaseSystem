using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MultiLeaseSytem
{
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
            RentLoad();
            RentalData();
        }

        readonly SqlConnection sqlcon = new SqlConnection("Data Source = LAPTOP-VCPDJHAL; Initial Catalog = carRental; Integrated Security = True");
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader sdr;

        string PID;
        string sql;
        string sql1;
        bool resu = true;
        string id;


        public void RentLoad()
        {
            cmd = new SqlCommand("select * from CarReg", sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                cmbCarId.Items.Add(sdr["RegNo"].ToString());
            }
            sqlcon.Close();
        }

        public void RentalData()
        {
            sql = "select * from Rental";
            cmd = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (sdr.Read())
            {
                dataGridView1.Rows.Add(sdr[0], sdr[1], sdr[2], sdr[3], sdr[4], sdr[5], sdr[6]);
            }
            sqlcon.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Rental_Load(object sender, EventArgs e)
        {

        }

        private void cmbCarId_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from CarReg where RegNo = '" + cmbCarId.Text + "' ", sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();

            string aval;

            if (sdr.Read()) 
            {
                aval = sdr["Available"].ToString();
                Label8.Text = aval;

                if (aval == "No")
                {
                    tCustomerId.Enabled = false;
                    tCustomerName.Enabled = false;
                    txtfee.Enabled = false;
                    pickdate.Enabled = false;
                    dueDate.Enabled = false;
                }
                else
                {
                    tCustomerId.Enabled = true;
                    tCustomerName.Enabled = true;
                    txtfee.Enabled = true;
                    pickdate.Enabled = true;
                    dueDate.Enabled = true;
                }
            }
            else
            {
                Label8.Text = "Car not Available";
            }
            sqlcon.Close();
        }

        private void tCustomerId_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                cmd = new SqlCommand("select * from customers where custid = '" + cmbCarId.Text + "' ", sqlcon);
                sqlcon.Open();
                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    tCustomerName.Text = sdr["custname"].ToString();
                }
                else
                {
                    MessageBox.Show("Customer ID not found");
                }
                sqlcon.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string caid = cmbCarId.SelectedItem.ToString();
            string cid = tCustomerId.Text;
            string cname = tCustomerName.Text;
            string fee = txtfee.Text;
            string Date = pickdate.Value.Date.ToString("yyyy-MM-dd");
            string dDate = dueDate.Value.Date.ToString("yyyy-MM-dd");

            sql = "insert into Rental(carid, customerid, customername, fee, date, duedate)values(@carid, @customerid, @customername, @fee, @date, @duedate)";
            sqlcon.Open();
            cmd = new SqlCommand(sql, sqlcon);
            cmd.Parameters.AddWithValue("@carid", caid);
            cmd.Parameters.AddWithValue("@customerid", cid);
            cmd.Parameters.AddWithValue("@customername", cname);
            cmd.Parameters.AddWithValue("@fee", fee);
            cmd.Parameters.AddWithValue("@date", Date);
            cmd.Parameters.AddWithValue("@duedate", dDate); 
            cmd.ExecuteNonQuery();
            sqlcon.Close();

            sql1 = "update CarReg set Available = 'No' where RegNo = @RegNo";
            sqlcon.Open();

            cmd1 = new SqlCommand(sql1, sqlcon);
            cmd1.Parameters.AddWithValue("@RegNo", caid);
            cmd1.ExecuteNonQuery();

            MessageBox.Show("Records Added");
            sqlcon.Close();
        }
    }
}
