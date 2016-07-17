using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace wordwars1
{
    public partial class Form1 : Form
    {
        private OleDbConnection con = new OleDbConnection();
        private OleDbCommand cmd;
        private OleDbDataReader dr;
        public string word = "";
        string query;
        private DateTime time = DateTime.Now + new TimeSpan(0, 3, 0);
        private TimeSpan rem = TimeSpan.FromMinutes(3);

        public Form1()
        {
            InitializeComponent();
            con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\RowellChristian\Documents\testDB.accdb;Persist Security Info=False";
        }

        String[] arr = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        
        private void Start_Click(object sender, EventArgs e)
        {
            var rand = new Random();

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    c.Text = arr[rand.Next(arr.Length)];
                    c.Enabled = true;
                }
            }

            time = DateTime.Now + new TimeSpan(0, 3, 1);
            timeLabel.Text = rem.ToString(@"m\:ss"); ;
            Attack.Text = "Attack";
            Start.Text = "Start";
            timer1.Enabled = true;
        }

        private void Attack_Click(object sender, EventArgs e)
        {
            word = label1.Text.ToString();

            try
            {
                con.Open();

                query = "select * from Table1 where words = '" + word + "'";
                cmd = new OleDbCommand(query, con);
                dr = cmd.ExecuteReader();

                int count = 0;

                while (dr.Read())
                {
                    word = dr["words"].ToString();
                    count++;

                    if (count == 1)
                    {
                        string greet = "Word!";
                        MessageBox.Show(greet);
                    }
                    
                }
                if (count != 1)
                {
                    string grt = "No Word!";
                    MessageBox.Show(grt);
			   MessageBox.show("Test");
                }
            }
            catch (Exception q)
            {
                MessageBox.Show(q.Message);
            }
            finally
            {
                label1.Text = "";
                con.Close();
            }

            foreach (Control c in this.Controls)
            {
                if (c is Button)
                c.Enabled = true;
            }
        }

        private void Buttons(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            label1.Text = label1.Text + b.Text;
            b.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var rand = new Random();

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    c.Text = arr[rand.Next(arr.Length)];
                    c.Enabled = false;
                }
            }

            Start.Enabled = true;
            Attack.Text = "Attack";
            Start.Text = "Start";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rem = time - DateTime.Now;
            timeLabel.Text = rem.ToString(@"m\:ss");
            
            if (rem <= TimeSpan.Zero)
            {
                timer1.Stop();
                timeLabel.Visible = false;
                label2.Visible = true;
            }
        }

    }
}
