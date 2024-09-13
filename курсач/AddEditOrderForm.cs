using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoServiceApp
{
    public partial class AddEditOrderForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        private int orderId = 0; // Идентификатор заказа для редактирования, по умолчанию 0 для добавления нового

        public AddEditOrderForm()
        {
            InitializeComponent();
            buttonSave.Click += buttonSave_Click;
        }

        public AddEditOrderForm(int orderId) : this()
        {
            this.orderId = orderId;
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Заказы WHERE ИдентификаторЗаказа = @ИдентификаторЗаказа", connection);
                    command.Parameters.AddWithValue("@ИдентификаторЗаказа", orderId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxPartId.Text = reader["ИдентификаторЗапчасти"].ToString();
                        textBoxQuantity.Text = reader["Количество"].ToString();
                        dateTimePickerOrderDate.Value = Convert.ToDateTime(reader["ДатаЗаказа"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о заказе: " + ex.Message);
                }
            }
        }

        private bool ValidateFields()
        {
            // Проверяем каждое поле на наличие недопустимых символов

            // Проверка идентификатора запчасти
            if (!string.IsNullOrEmpty(textBoxPartId.Text) && !Regex.IsMatch(textBoxPartId.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректный идентификатор запчасти (только цифры).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка количества
            if (!Regex.IsMatch(textBoxQuantity.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректное количество (только цифры).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(textBoxPartId.Text) ||
        string.IsNullOrEmpty(textBoxQuantity.Text))
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
                    if (orderId == 0)
                    {
                        // Добавление нового заказа
                        command = new SqlCommand("INSERT INTO Заказы (ИдентификаторЗапчасти, Количество, ДатаЗаказа) VALUES (@ИдентификаторЗапчасти, @Количество, @ДатаЗаказа)", connection);
                    }
                    else
                    {
                        // Редактирование существующего заказа
                        command = new SqlCommand("UPDATE Заказы SET ИдентификаторЗапчасти = @ИдентификаторЗапчасти, Количество = @Количество, ДатаЗаказа = @ДатаЗаказа WHERE ИдентификаторЗаказа = @ИдентификаторЗаказа", connection);
                        command.Parameters.AddWithValue("@ИдентификаторЗаказа", orderId);
                    }

                    command.Parameters.AddWithValue("@ИдентификаторЗапчасти", Convert.ToInt32(textBoxPartId.Text));
                    command.Parameters.AddWithValue("@Количество", Convert.ToInt32(textBoxQuantity.Text));
                    command.Parameters.AddWithValue("@ДатаЗаказа", dateTimePickerOrderDate.Value);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Информация о заказе успешно сохранена.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении информации о заказе: " + ex.Message);
                }
            }
        }
    }
}