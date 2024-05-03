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

namespace COMP1551_SeaAnimal_Game
{
    public partial class Score_Page : Form
    {
        OleDbConnection con = null;
        OleDbConnection conn = new OleDbConnection(Properties.Resources.DB_Connection);

        public Score_Page()
        {
            InitializeComponent();
        }

        private void gobackbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            LevelsPage lp = new LevelsPage();
            lp.Show();
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Score_Page_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OleDbCommand command = new OleDbCommand("select PlayerName,Score from Game order by Score desc", conn);

                OleDbDataAdapter dbAdapter = new OleDbDataAdapter(command);
                DataTable dbTable = new DataTable();
                dbAdapter.Fill(dbTable);
                dataGridView.DataSource = dbTable;

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    chart1.Series["Score"].Points.AddXY(reader["PlayerName"].ToString(), reader["Score"].ToString());
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Hide();
            Score_Page sp = new Score_Page();
            sp.Show();
        }
    }
}
