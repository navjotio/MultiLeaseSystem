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
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace MultiLeaseSytem
{
    public partial class CarReg : Form
    {
        public CarReg()
        {
            InitializeComponent();
            AutoCon();
            ConData();
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
            sql = "select RegNo from CarReg order by RegNo desc";
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

            txtregno.Text = PID.ToString();
            sqlcon.Close();
        }

        public void ConData()
        {
            sql = "select * from CarReg";
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

        public void GetId(string id)
        {
            sql = "select * from CarReg where RegNo = '" + id + "' ";
            cmd = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                txtregno.Text = sdr[0].ToString();
                textmake.Text = sdr[1].ToString();
                txtmodel.Text = sdr[2].ToString();
                txtAv.Text = sdr[3].ToString();
            }
            sqlcon.Close();
        }

        private void CarReg_Load(object sender, EventArgs e)
        {
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var regno = txtregno.Text;
            var make = textmake.Text;
            var model = txtmodel.Text;
            string ava = txtAv.SelectedItem.ToString();

            if (resu == true)
            {
                sql = "insert into CarReg(RegNo, Make, Model, Available)values(@RegNo, @Make, @Model, @Available)";
                sqlcon.Open();
                cmd = new SqlCommand(sql, sqlcon);
                cmd.Parameters.AddWithValue("@RegNo", regno);
                cmd.Parameters.AddWithValue("@Make", make);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@Available", ava);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                textmake.Clear();
                txtmodel.Clear();
                txtAv.Items.Clear();
                textmake.Focus();
            } 
            else
            {
                sql = "update CarReg set make = @Make, Model = @Model, Available = @Available where RegNo = @RegNo";
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sqlcon.Open();
                cmd = new SqlCommand(sql, sqlcon);
                cmd.Parameters.AddWithValue("@Make", make);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@Available", ava);
                cmd.Parameters.AddWithValue("@RegNo", id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");
                txtregno.Enabled = true;
                resu = true;

                textmake.Clear();
                txtmodel.Clear();
                txtAv.Items.Clear();
                textmake.Focus();
                
            }
            sqlcon.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                resu = false;
                txtregno.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                GetId(id);
            }
            else if(e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                resu = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from CarReg where RegNo = @id";
                sqlcon.Open();
                cmd = new SqlCommand(sql, sqlcon);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                sqlcon.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AutoCon();
            ConData();

            textmake.Clear();
            txtmodel.Clear();
            textmake.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        
    }
}
