using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.Text.RegularExpressions;

namespace AutoServiceApp
{
    public partial class AddEditInventoryForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        private int inventoryId = 0; // Идентификатор элемента инвентаря для редактирования, по умолчанию 0 для добавления нового

        public AddEditInventoryForm()
        {
            InitializeComponent();
            buttonSave.Click += buttonSave_Click;
        }

        public AddEditInventoryForm(int inventoryId) : this()
        {
            this.inventoryId = inventoryId;
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Инвентарь WHERE ИдентификаторЗапчасти = @ИдентификаторЗапчасти", connection);
                    command.Parameters.AddWithValue("@ИдентификаторЗапчасти", inventoryId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxItemName.Text = reader["НаименованиеЗапчасти"].ToString();
                        textBoxQuantity.Text = reader["Количество"].ToString();
                        textBoxUnitCost.Text = reader["СтоимостьЗаЕдиницу"].ToString();
                        textBoxSupplier.Text = reader["Поставщик"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных об элементе инвентаря: " + ex.Message);
                }
            }
        }
        
        private bool ValidateFields()
        {
            // Проверяем каждое поле на наличие недопустимых символов

            // Проверка наименования запчасти
            if (!Regex.IsMatch(textBoxItemName.Text, @"^[a-zA-Zа-яА-Я0-9\s\-]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректное наименование запчасти.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка количества
            if (!Regex.IsMatch(textBoxQuantity.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректное количество (только цифры).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка стоимости за единицу
            if (!Regex.IsMatch(textBoxUnitCost.Text, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Пожалуйста, введите корректную стоимость за единицу (положительное число с двумя знаками после запятой).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка поставщика
            if (string.IsNullOrEmpty(textBoxSupplier.Text))
            {
                MessageBox.Show("Пожалуйста, введите поставщика.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Проверяем валидность данных перед сохранением
            if (!ValidateFields())
            {
                return;
            }

            if (string.IsNullOrEmpty(textBoxItemName.Text) ||
                string.IsNullOrEmpty(textBoxQuantity.Text) ||
                string.IsNullOrEmpty(textBoxUnitCost.Text) ||
                string.IsNullOrEmpty(textBoxSupplier.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command;
                    if (inventoryId == 0)
                    {
                        // Добавление нового элемента инвентаря
                        command = new SqlCommand("INSERT INTO Инвентарь (НаименованиеЗапчасти, Количество, СтоимостьЗаЕдиницу, Поставщик) VALUES (@НаименованиеЗапчасти, @Количество, @СтоимостьЗаЕдиницу, @Поставщик)", connection);
                    }
                    else
                    {
                        // Редактирование существующего элемента инвентаря
                        command = new SqlCommand("UPDATE Инвентарь SET НаименованиеЗапчасти = @НаименованиеЗапчасти, Количество = @Количество, СтоимостьЗаЕдиницу = @СтоимостьЗаЕдиницу, Поставщик = @Поставщик WHERE ИдентификаторЗапчасти = @ИдентификаторЗапчасти", connection);
                        command.Parameters.AddWithValue("@ИдентификаторЗапчасти", inventoryId);
                    }
                    command.Parameters.AddWithValue("@НаименованиеЗапчасти", textBoxItemName.Text);
                    command.Parameters.AddWithValue("@Количество", textBoxQuantity.Text);
                    command.Parameters.AddWithValue("@СтоимостьЗаЕдиницу", textBoxUnitCost.Text);
                    command.Parameters.AddWithValue("@Поставщик", textBoxSupplier.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Информация об элементе инвентаря успешно сохранена.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении информации об элементе инвентаря: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
