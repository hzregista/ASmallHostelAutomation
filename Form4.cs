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
    public partial class FormRoom : Form
    {
        SqlConnection con = new SqlConnection("Data Source=xxx;Initial Catalog=ASmallHostel;Integrated Security=True"); ////Instead of typing the name of my local machine, I typed xxx.
        SqlCommand command = new SqlCommand();
        SqlDataReader sdr;
        public FormRoom()
        {
            InitializeComponent();
        }
        private void getlist()
        {
            con.Open();
            command.CommandText = "SELECT RoomNumber, Type, State, Price FROM TBRooms";
            command.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            command.Connection = con;
            command.CommandText = "SELECT * FROM TBRooms where RoomNumber = '" + textBox3.Text + "'";
            sdr = command.ExecuteReader();
            if (sdr.Read())
            {
                sdr.Close();
                command.CommandText = "SELECT * FROM TBRooms where State = '" + "Available" + "'";
                sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    sdr.Close();
                    command.CommandText = "DELETE FROM TBRooms WHERE RoomNumber='" + textBox3.Text + "'";
                    command.ExecuteNonQuery();
                    con.Close();
                    getlist();
                }
                else 
                {
                    MessageBox.Show("You Cannot Delete A Non-Available Room");
                }
            }
            else
            {
                MessageBox.Show("There Is No This Room");
            }
            con.Close();
        }

        private void FormRoom_Load(object sender, EventArgs e)
        {
            getlist();
            comboBox1.Items.Add("Standard");
            comboBox1.Items.Add("Lux");
            comboBox1.Text = "Standard";
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Please Enter All Information");
            }
            else
            {
                double control = double.Parse(textBox3.Text);
                if (control % 2 != 0 && comboBox1.Text == "Standard")
                { MessageBox.Show("Odd-numbered Room Numbers Can Only Be Lux."); }
                else if (control % 2 == 0 && comboBox1.Text == "Lux")
                { MessageBox.Show("Even-numbered Room Numbers Can Only Be Standard."); }
                else
                {
                    con.Open();
                    command.Connection = con;
                    command.CommandText = "SELECT * FROM TBRooms where RoomNumber = '" + textBox3.Text + "'";
                    sdr = command.ExecuteReader();
                    if (sdr.Read())
                    {
                        MessageBox.Show("There Is Already This Room.");
                    }
                    else
                    {

                        sdr.Close();
                        command.CommandText = "INSERT INTO TBRooms(RoomNumber, Type, State, Price) Values ('" + textBox3.Text + "','" + comboBox1.Text + "','" + "Available" + "','" + textBox4.Text + "')";
                        command.ExecuteNonQuery();
                    }
                }
                con.Close();
                getlist();
            }
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "Standard";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAdmin frma = new FormAdmin();
            frma.Show();
            this.Hide();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text == "")
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "UPDATE TBRooms Set Price = '" + textBox1.Text + "'Where Type = '" + label4.Text + "'";
                command.ExecuteNonQuery();
                con.Close();
                getlist();
            }
            else if (textBox1.Text == "" && textBox2.Text != "")
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "UPDATE TBRooms Set Price = '" + textBox2.Text + "'Where Type = '" + label5.Text + "'";
                command.ExecuteNonQuery();
                con.Close();
                getlist();
            }
            else if (textBox1.Text != "" && textBox2.Text != "")
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "UPDATE TBRooms Set Price = '" + textBox1.Text + "'Where Type = '" + label4.Text + "'";
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE TBRooms Set Price = '" + textBox2.Text + "'Where Type = '" + label5.Text + "'";
                command.ExecuteNonQuery();
                con.Close();
                getlist();
            }
            else
            {
                MessageBox.Show("You Didn't Enter Any Value.");
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
