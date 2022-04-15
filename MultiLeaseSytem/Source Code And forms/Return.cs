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
using System.IO;

namespace MultiLeaseSytem
{
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
            ReturnData();
        }

        readonly SqlConnection sqlcon = new SqlConnection("Data Source = LAPTOP-VCPDJHAL; Initial Catalog = carRental; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader sdr;

        string sql;

        public void ReturnData()
        {
            sql = "select * from ReturnCar";
            cmd = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sdr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (sdr.Read())
            {
                dataGridView1.Rows.Add(sdr[0], sdr[1], sdr[2], sdr[3], sdr[4], sdr[5]);
            }
            sqlcon.Close();
        }



        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void carID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                cmd = new SqlCommand("select carid ,customerid, date, duedate, DATEDIFF(dd, duedate, GETDATE()) as elap from Rental where carid = '" + txtcarID.Text  + "'", sqlcon);
                
                sqlcon.Open();
                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    txtcusid.Text = sdr["customerid"].ToString();
                    txtDate.Text = sdr["duedate"].ToString();

                    string elap = sdr["elap"].ToString();

                    int elapped = int.Parse(elap);

                    if(elapped > 0)
                    {
                        txtDateEl.Text = (elap);
                        int fine = elapped * 100;
                        txtFine.Text = fine.ToString();
                    }

                    else
                    {
                        txtFine.Text = "0";
                        txtFine.Text = "0";
                    }

                }
                    
                sqlcon.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carid = txtcarID.Text;
            string cid = txtcusid.Text;
            string date = txtDate.Text;
            string dateel = txtDateEl.Text;
            string fine = txtFine.Text;

            sql = "insert into ReturnCar(CARID, CUSTID, DATE, DATEELP, FINE)values(@CARID, @CUSTID, @DATE, @DATEELP, @FINE)";
            sqlcon.Open();
            cmd = new SqlCommand(sql, sqlcon);
            cmd.Parameters.AddWithValue("@CARID", carid);
            cmd.Parameters.AddWithValue("@CUSTID", cid);
            cmd.Parameters.AddWithValue("@DATE", date);
            cmd.Parameters.AddWithValue("@DATEELP", dateel);
            cmd.Parameters.AddWithValue("@FINE", fine);
                cmd.ExecuteNonQuery();
            MessageBox.Show("Record Added");

            sqlcon.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
