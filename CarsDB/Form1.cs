using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDB
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["CarsDB.Properties.Settings.Database1ConnectionString"].ConnectionString;
        }

        private void PopulateMarkTable()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select * FROM CarMark", connection))
            {
                DataTable carTable = new DataTable();
                adapter.Fill(carTable);

                listMark.DisplayMember = "CarMarkName";
                listMark.ValueMember = "Id";
                listMark.DataSource = carTable;
            }
        }
        private void PopulateModelTable()
        {
            string query = "SELECT CarInGarage.CarModelName From CarInGarage INNER JOIN CarMark ON CarMark.Id = CarInGarage.Id WHERE CarInGarage.Id = @Id";  
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", listMark.SelectedValue);
                DataTable modelNameTable = new DataTable();
                adapter.Fill(modelNameTable);

                listModel.DisplayMember = "CarModelName";
                listModel.ValueMember = "Id";
                listModel.DataSource = modelNameTable;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateMarkTable();
        }

        private void listMark_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateModelTable();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
