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
    public partial class InventoryForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public InventoryForm()
        {
            InitializeComponent();
            LoadInventory();
            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void LoadInventory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Инвентарь", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewInventory.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке инвентаря: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Открыть форму для добавления новой записи инвентаря
            AddEditInventoryForm addEditForm = new AddEditInventoryForm();
            addEditForm.ShowDialog();
            LoadInventory(); // Перезагрузить список инвентаря после добавления
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Проверить, выбран ли элемент инвентаря в таблице
            if (dataGridViewInventory.SelectedRows.Count > 0)
            {
                // Получить идентификатор выбранного элемента инвентаря
                int inventoryId = Convert.ToInt32(dataGridViewInventory.SelectedRows[0].Cells["ИдентификаторЗапчасти"].Value);

                // Открыть форму для редактирования выбранного элемента инвентаря
                AddEditInventoryForm addEditForm = new AddEditInventoryForm(inventoryId);
                addEditForm.ShowDialog();
                LoadInventory(); // Перезагрузить список инвентаря после редактирования
            }
            else
            {
                MessageBox.Show("Выберите элемент инвентаря для редактирования.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Проверить, выбран ли элемент инвентаря в таблице
            if (dataGridViewInventory.SelectedRows.Count > 0)
            {
                // Получить идентификатор выбранного элемента инвентаря
                int inventoryId = Convert.ToInt32(dataGridViewInventory.SelectedRows[0].Cells["ИдентификаторЗапчасти"].Value);

                // Удалить выбранный элемент инвентаря
                if (MessageBox.Show("Вы уверены, что хотите удалить этот элемент инвентаря?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand deleteTransactionsCommand = new SqlCommand("DELETE FROM Заказы WHERE ИдентификаторЗапчасти = @ИдентификаторЗапчасти", connection);
                            deleteTransactionsCommand.Parameters.AddWithValue("@ИдентификаторЗапчасти", inventoryId);
                            deleteTransactionsCommand.ExecuteNonQuery();

                            SqlCommand command = new SqlCommand("DELETE FROM Инвентарь WHERE ИдентификаторЗапчасти = @ИдентификаторЗапчасти", connection);
                            command.Parameters.AddWithValue("@ИдентификаторЗапчасти", inventoryId);
                            command.ExecuteNonQuery();
                            LoadInventory(); // Перезагрузить список инвентаря после удаления
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении элемента инвентаря: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент инвентаря для удаления.");
            }
        }
    }
}
