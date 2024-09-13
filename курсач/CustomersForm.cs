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
    public partial class CustomersForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        public CustomersForm()
        {
            InitializeComponent();
            LoadCustomers();
            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Клиенты", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewCustomers.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке клиентов: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Открыть форму для добавления нового клиента
            AddEditCustomerForm addEditForm = new AddEditCustomerForm();
            addEditForm.ShowDialog();

            // Перезагрузить список клиентов после добавления
            LoadCustomers();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Проверить, выбран ли клиент в таблице
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                // Получить идентификатор выбранного клиента
                int customerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["ИдентификаторКлиента"].Value);

                // Открыть форму для редактирования выбранного клиента
                AddEditCustomerForm addEditForm = new AddEditCustomerForm(customerId);
                addEditForm.ShowDialog();
                LoadCustomers(); // Перезагрузить список клиентов после редактирования
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли запись в DataGridView клиентов
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                // Получаем идентификатор выбранного клиента
                int clientId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["ИдентификаторКлиента"].Value);

                // Удаляем выбранного клиента из базы данных
                if (MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            // Удаляем запись о клиенте из таблицы "Клиенты"
                            SqlCommand deleteClientCommand = new SqlCommand("DELETE FROM Клиенты WHERE ИдентификаторКлиента = @ClientId", connection);
                            deleteClientCommand.Parameters.AddWithValue("@ClientId", clientId);
                            deleteClientCommand.ExecuteNonQuery();

                            // Обновляем данные в DataGridView после удаления
                            LoadCustomers(); // Пример метода для обновления данных в DataGridView
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении клиента: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления.");
            }
        }

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {

        }
    }
}