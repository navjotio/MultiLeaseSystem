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
using System.Reflection;

namespace MultiLeaseSytem
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            AutoCon();
            CusData();
        }

        readonly SqlConnection sqlcon = new SqlConnection("Data Source = LAPTOP-VCPDJHAL; Initial Catalog = carRental; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader sdr;

        string PID;
        string sql;
        bool resu = true;
        string id;

        public void AutoCon()
        {
            sql = "select custid from customers order by custid desc";
            cmd = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                int id = int.Parse(sdr[0].ToString()) + 1;
                PID = id.ToString("00000");
            }
            else if (Convert.IsDBNull(sdr))
            {
                PID = ("00001");
            }
            else
            {
                PID = ("00001");
            }

            txtcusid.Text = PID.ToString();
            sqlcon.Close();
        }

        public void CusData()
        {
            sql = "select * from customers";
            cmd = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (sdr.Read())
            {
                dataGridView1.Rows.Add(sdr[0], sdr[1], sdr[2], sdr[3]);
            }
            sqlcon.Close();
        }
        public void CusGetId(string id)
        {
            sql = "select * from customers where custid = '" + id + "' ";
            cmd = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                txtcusid.Text = sdr[0].ToString();
                txtcusname.Text = sdr[1].ToString();
                txtaddress.Text = sdr[2].ToString();
                txtmob.Text = sdr[3].ToString();
            }
            sqlcon.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string custid = txtcusid.Text;
            string custname = txtcusname.Text;
            string address = txtaddress.Text;
            string mobile = txtmob.Text;

            if (resu == true)
            {
                sql = "insert into customers(custid, custname, address, mobile)values(@custid, @custname, @address, @mobile)";
                sqlcon.Open();
                cmd = new SqlCommand(sql, sqlcon);
                cmd.Parameters.AddWithValue("@custid", custid);
                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtcusname.Clear();
                txtaddress.Clear();
                txtmob.Clear();
                txtcusname.Focus();

            }
            else
            {
                sql = "update customers set custname = @custname, address = @address, mobile = @mobile where custid = @custid";
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sqlcon.Open();
                cmd = new SqlCommand(sql, sqlcon);
                cmd.Parameters.AddWithValue("@cusname", custname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@custid", id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");
                txtcusid.Enabled = true;
                resu = true;

                txtcusname.Clear();
                txtaddress.Clear();
                txtmob.Clear();
                txtcusname.Focus();

            }
            sqlcon.Close();
        }

        private void txtAv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtcusid_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AutoCon();
            CusData();

            txtcusname.Clear();
            txtaddress.Clear();
            txtmob.Clear();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                resu = false;
                txtcusid.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                CusGetId(id);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                resu = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from customers where custid = @id";
                sqlcon.Open();
                cmd = new SqlCommand(sql, sqlcon);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                sqlcon.Close();
            }
        }
    }

}