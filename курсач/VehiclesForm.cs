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
    public partial class VehiclesForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public VehiclesForm()
        {
            InitializeComponent();
            LoadVehicles();
            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void LoadVehicles()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ТранспортныеСредства", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewVehicles.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке транспортных средств: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Открыть форму для добавления нового транспортного средства
            AddEditVehicleForm addEditForm = new AddEditVehicleForm();
            addEditForm.ShowDialog();
            LoadVehicles(); // Перезагрузить список транспортных средств после добавления
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewVehicles.SelectedRows.Count > 0)
            {
                int vehicleId = Convert.ToInt32(dataGridViewVehicles.SelectedRows[0].Cells["ИдентификаторТС"].Value);

                // Открыть форму для редактирования выбранного транспортного средства
                AddEditVehicleForm addEditForm = new AddEditVehicleForm(vehicleId);
                addEditForm.ShowDialog();
                LoadVehicles(); // Перезагрузить список транспортных средств после редактирования
            }
            else
            {
                MessageBox.Show("Выберите транспортное средство для редактирования.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewVehicles.SelectedRows.Count > 0)
            {
                int vehicleId = Convert.ToInt32(dataGridViewVehicles.SelectedRows[0].Cells["ИдентификаторТС"].Value);

                if (MessageBox.Show("Вы уверены, что хотите удалить это транспортное средство?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("DELETE FROM ТранспортныеСредства WHERE ИдентификаторТС = @ИдентификаторТС", connection);
                            command.Parameters.AddWithValue("@ИдентификаторТС", vehicleId);
                            command.ExecuteNonQuery();
                            LoadVehicles(); // Перезагрузить список транспортных средств после удаления
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
    }
}