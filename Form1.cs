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
    public partial class FormVisitor : Form
    {
        SqlConnection con = new SqlConnection("Data Source=xxx;Initial Catalog=ASmallHostel;Integrated Security=True");//Instead of typing the name of my local machine, I typed xxx. 
        SqlCommand command = new SqlCommand();
        SqlDataReader sdr;
        double control;
        public FormVisitor()
        {
            InitializeComponent();
        }
        private void getvisitors()
        {
            con.Open();            
            command.CommandText = "SELECT * FROM TBVisitors";
            command.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();   
        }
        private void getrooms()
        {
            con.Open();
            command.CommandText = "SELECT RoomNumber, State, Type, Price FROM TBRooms WHERE State='" + "Available" + "'";
            command.Connection = con;
            SqlDataAdapter da2 = new SqlDataAdapter(command);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getvisitors();
            getrooms();
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Please Enter All Information");
            }
            else
            {
                con.Open();
                command.CommandText = "INSERT INTO TBVisitors(Name, Surname, GSM, Room, DateOfEntry, DateOfExit) Values ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"','"+dateTimePicker1.Text+"','"+dateTimePicker2.Text+"')";
                command.ExecuteNonQuery();
                con.Close();
                getvisitors();
                visitorhascame();
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        private void visitorhascame()
        {
            con.Open();
            command.Connection = con;
            command.CommandText = "SELECT * FROM TBRooms where State = '" + "Available" +"'";
            sdr = command.ExecuteReader();
            if (sdr.Read())
            {
                sdr.Close();
                command.CommandText = "UPDATE TBRooms Set State = '" + "Full" + "'Where RoomNumber = '" + textBox4.Text + "'";
                command.ExecuteNonQuery();
            }
            con.Close();
            getvisitors();
            getrooms();
        }
        private void visitorhasgone()
        {
            
            con.Open();
            command.Connection = con;
            command.CommandText = "UPDATE TBRooms Set State = '" + "Available" + "'Where RoomNumber = '" + textBox6.Text + "'";                      
            command.ExecuteNonQuery();
            con.Close();
            getvisitors();
            getrooms();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "") { MessageBox.Show("Please Choose A Visitor You Want To Delete"); }
            else {             
            con.Open();
            command.Connection = con;
            command.CommandText = "SELECT * FROM TBVisitors where ID = '" + textBox5.Text + "'";
            sdr = command.ExecuteReader();
            if (sdr.Read())
            {
                sdr.Close();
                command.CommandText = "DELETE FROM TBVisitors WHERE ID=" + textBox5.Text;
                command.ExecuteNonQuery();
                con.Close();
                getvisitors();
                visitorhasgone();
            }
            else
            {
                MessageBox.Show("There Is No This Visitor");
                sdr.Close();
                con.Close();
            }
        }
        }        
        private void button3_Click(object sender, EventArgs e)
        {
            FormAdmin frma = new FormAdmin();
            frma.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            con.Open();
            command.CommandText = "SELECT RoomNumber, State, Type, Price FROM TBRooms WHERE State='" + "Available" + "' AND Type = '" + "Standard" + "'";
            command.Connection = con;
            SqlDataAdapter da2 = new SqlDataAdapter(command);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            con.Close();

        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox2.Enabled = false;
            checkBox1.Enabled = true;
            checkBox3.Enabled = true;
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            con.Open();
            command.CommandText = "SELECT RoomNumber, State, Type, Price FROM TBRooms WHERE State='" + "Available" + "'AND Type = '" + "Lux" + "' ";
            command.Connection = con;
            SqlDataAdapter da2 = new SqlDataAdapter(command);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            con.Close();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Enabled = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            getrooms();
        }
    }
}
