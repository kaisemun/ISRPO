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
    public partial class OrdersForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public OrdersForm()
        {
            InitializeComponent();
            LoadOrders();
            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void LoadOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Заказы", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewOrders.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке заказов: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Открыть форму для добавления нового заказа
            AddEditOrderForm addEditForm = new AddEditOrderForm();
            addEditForm.ShowDialog();
            LoadOrders(); // Перезагрузить список заказов после добавления
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Проверить, выбран ли заказ в таблице
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                // Получить идентификатор выбранного заказа
                int orderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["ИдентификаторЗаказа"].Value);

                // Открыть форму для редактирования выбранного заказа
                AddEditOrderForm addEditForm = new AddEditOrderForm(orderId);
                addEditForm.ShowDialog();
                LoadOrders(); // Перезагрузить список заказов после редактирования
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Проверить, выбран ли заказ в таблице
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                // Получить идентификатор выбранного заказа
                int orderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["ИдентификаторЗаказа"].Value);

                // Удалить выбранный заказ
                if (MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            // Удалить связанную запись из таблицы "Инвентарь"
                            SqlCommand deleteInventoryCommand = new SqlCommand("DELETE FROM Инвентарь WHERE ИдентификаторЗапчасти = @OrderId", connection);
                            deleteInventoryCommand.Parameters.AddWithValue("@OrderId", orderId);
                            deleteInventoryCommand.ExecuteNonQuery();

                            SqlCommand command = new SqlCommand("DELETE FROM Заказы WHERE ИдентификаторЗаказа = @ИдентификаторЗаказа", connection);
                            command.Parameters.AddWithValue("@ИдентификаторЗаказа", orderId);
                            command.ExecuteNonQuery();
                            LoadOrders(); // Перезагрузить список заказов после удаления
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении заказа: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления.");
            }
        }
    }
}
