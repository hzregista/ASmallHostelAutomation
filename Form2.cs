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
using System.Data.OleDb;

namespace ASmallHostel
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data Source=xxx;Initial Catalog=ASmallHostel;Integrated Security=True"); //Instead of typing the name of my local machine, I typed xxx. 
        SqlCommand command = new SqlCommand();
        SqlDataReader sdr;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "password")
            {
                FormVisitor frmv = new FormVisitor();
                frmv.Show();
                this.Hide();
            }
            else
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM TBAdmins where UserName = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'";
                sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    FormVisitor frmv = new FormVisitor();
                    frmv.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Name or Password Is/Are Wrong");
                }
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "password")
            {
                FormAccount frmac = new FormAccount();
                frmac.Show();
                this.Hide();
            }
            else
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM TBAdmins where UserName = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'";
                sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    FormAccount frmac = new FormAccount();
                    frmac.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Name or Password Is/Are Wrong");
                }
                con.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" && textBox2.Text == "password")
            {
                FormRoom frmr = new FormRoom();
                frmr.Show();
                this.Hide();
            }
            else
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM TBAdmins where UserName = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'";
                sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    FormRoom frmr = new FormRoom();
                    frmr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Name or Password Is/Are Wrong");
                }
                con.Close();
            }

        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
