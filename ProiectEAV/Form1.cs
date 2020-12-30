using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProiectEAV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Default home page
            webView1.Navigate("https://www.google.ro/maps/search/oficii+postale+craiova/@44.3080085,23.767188,13z/data=!3m1!4b1?hl=ro");
        }

        DataTable dt = new DataTable();
        public void LoadData()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-CUHAUG6\SQLEXPRESS;" +
                "Initial Catalog=PostOficces;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM oficii_postale", conn);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OficiiPostale.Items.Add(Convert.ToString(dt.Rows[i]["denumire"]));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (OficiiPostale.Text == Convert.ToString(dt.Rows[i]["denumire"]))
                {
                    webView1.Navigate(Convert.ToString(dt.Rows[i]["link_gm"]));
                    break;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string StringComp = "https://www.google.com/maps/search/" + textBoxCautare.Text;
            webView1.Navigate(StringComp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (OficiiPostale.Text == Convert.ToString(dt.Rows[i]["denumire"]))
                {
                    webView1.Navigate(Convert.ToString(dt.Rows[i]["link_gsv"]));
                    break;
                }
            }
        }
    }
}
