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

namespace ASmallHostel
{
    public partial class FormAccount : Form
    {
        SqlConnection con = new SqlConnection("Data Source=xxx;Initial Catalog=ASmallHostel;Integrated Security=True"); //Instead of typing the name of my local machine, I typed xxx.
        SqlCommand command = new SqlCommand();
        SqlDataReader sdr;
        public FormAccount()
        {
            InitializeComponent();
        }
        private void getlist ()
        {
            con.Open();
            command.CommandText = "SELECT UserName, Password FROM TBAdmins";
            command.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void FormAccount_Load(object sender, EventArgs e)
        {
            getlist();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            command.CommandText = "INSERT INTO TBAdmins(UserName, Password) Values ('" + textBox1.Text + "','" + textBox2.Text + "')";
            command.ExecuteNonQuery();
            con.Close();
            getlist();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string un = textBox1.Text;
            con.Open();
            command.Connection = con;
            command.CommandText = "SELECT * FROM TBAdmins where UserName = '" + un + "'";
            sdr = command.ExecuteReader();
            if (sdr.Read())
            {
                sdr.Close();
                command.CommandText = "DELETE FROM TBAdmins WHERE UserName=" + un;
                command.ExecuteNonQuery();
                con.Close();
                getlist();
            }
            else
            {
                MessageBox.Show("There Is No This User");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAdmin frma = new FormAdmin();
            frma.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsWhiteSpace(e.KeyChar);
        }
    }
}
