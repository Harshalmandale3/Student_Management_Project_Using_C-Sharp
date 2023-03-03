

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace StudentManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-DME1FUOO\\SQLEXPRESS; Initial Catalog=studentdata; User Id=harsh; Password=harshal12");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool Mode = true;
        string sql;


        public void Load()
        {
            try
            {
                sql = "select * from StudData";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);

                }

                con.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void getID(string id)
        {
            sql = "select * from StudData where id = '"+ id +"' ";

            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while(read.Read())
            {

                textBoxSname.Text= read[1].ToString();
                textBoxCourse.Text= read[2].ToString();
                textBoxFees.Text= read[3].ToString();

            }

            con.Close();

        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //if the mode is true means add records otherwise update the record
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxSname.Text;
            string course = textBoxCourse.Text;
            string fee = textBoxFees.Text;

            if (Mode == true)
            {
                sql = "insert into StudData(sname,course,fees) values(@sname,@course,@fees)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fee);
                MessageBox.Show("Recordd Addedd");
                cmd.ExecuteNonQuery();


                textBoxSname.Clear();
                textBoxCourse.Clear();
                textBoxFees.Clear();
                textBoxSname.Focus();



            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update StudData set sname = @sname, course = @course, fees = @fees where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Recordd updateddd");
                cmd.ExecuteNonQuery();


                textBoxSname.Clear();
                textBoxCourse.Clear();
                textBoxFees.Clear();
                textBoxSname.Focus();
                buttonSave.Text = "Save";
                Mode = true;



            }

            con.Close();

            Load();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if(e.ColumnIndex== dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {

                Mode= false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);

                buttonSave.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from StudData where id = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id ", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deletedd");

                

                con.Close() ;

                Load();

            }



        }



        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSname.Clear();
            textBoxCourse.Clear();
            textBoxFees.Clear();
            textBoxSname.Focus();
            buttonSave.Text = "Save";
            Mode = true;
        }

        private void textBoxSname_TextChanged(object sender, EventArgs e)
        {

        }


        public void Search()
        {
            try
            {
                sql = "select * from StudData where sname = '"+SearchtextBox.Text+"'";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);

                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void Searchbutton_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void SearchtextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


