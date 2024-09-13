using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoServiceApp
{
    public partial class RepairsForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public RepairsForm()
        {
            InitializeComponent();
            LoadRepairs();
            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void LoadRepairs()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Ремонты", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewRepairs.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке ремонтов: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEditRepairForm addEditForm = new AddEditRepairForm();
            addEditForm.ShowDialog();
            LoadRepairs();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairs.SelectedRows.Count > 0)
            {
                int repairId = Convert.ToInt32(dataGridViewRepairs.SelectedRows[0].Cells["ИдентификаторРемонта"].Value);
                AddEditRepairForm addEditForm = new AddEditRepairForm(repairId);
                addEditForm.ShowDialog();
                LoadRepairs();
            }
            else
            {
                MessageBox.Show("Выберите ремонт для редактирования.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairs.SelectedRows.Count > 0)
            {
                int vehicleId = Convert.ToInt32(dataGridViewRepairs.SelectedRows[0].Cells["ИдентификаторРемонта"].Value);

                if (MessageBox.Show("Вы уверены, что хотите удалить это транспортное средство?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("DELETE FROM Ремонты WHERE ИдентификаторРемонта = @ИдентификаторРемонта", connection);
                            command.Parameters.AddWithValue("@ИдентификаторРемонта", vehicleId);
                            command.ExecuteNonQuery();
                            LoadRepairs(); // Перезагрузить список транспортных средств после удаления
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении транспортного средства: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите транспортное средство для удаления.");
            }
        }

        private void RepairsForm_Load(object sender, EventArgs e)
        {

        }
    }
}