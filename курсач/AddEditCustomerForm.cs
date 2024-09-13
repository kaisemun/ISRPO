using AutoServiceApp;
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
    public partial class AddEditCustomerForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        private int customerId = 0; // Идентификатор клиента для редактирования, по умолчанию 0 для добавления нового
        public AddEditCustomerForm()
        {
            InitializeComponent();
            buttonSave.Click += buttonSave_Click;

        }

        public AddEditCustomerForm(int customerId) : this()
        {
            this.customerId = customerId;
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Клиенты WHERE ИдентификаторКлиента = @ИдентификаторКлиента", connection);
                    command.Parameters.AddWithValue("@ИдентификаторКлиента", customerId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxFirstName.Text = reader["Имя"].ToString();
                        textBoxLastName.Text = reader["Фамилия"].ToString();
                        textBoxPhone.Text = reader["Телефон"].ToString();
                        textBoxEmail.Text = reader["ЭлектроннаяПочта"].ToString();
                        textBoxAddress.Text = reader["Адрес"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о клиенте: " + ex.Message);
                }
            }
        }

        private bool ValidateFields()
        {
            // Проверяем каждое поле на наличие недопустимых символов
            if (!Regex.IsMatch(textBoxFirstName.Text, @"^[a-zA-Zа-яА-Я]+$"))
                return false;

            if (!Regex.IsMatch(textBoxLastName.Text, @"^[a-zA-Zа-яА-Я]+$"))
                return false;

            if (!Regex.IsMatch(textBoxPhone.Text, @"^[0-9]+$"))
                return false;

            if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return false;

            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Проверяем валидность данных перед сохранением
            if (!ValidateFields())
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxFirstName.Text) ||
                string.IsNullOrEmpty(textBoxLastName.Text) ||
                string.IsNullOrEmpty(textBoxPhone.Text) ||
                string.IsNullOrEmpty(textBoxEmail.Text) ||
                string.IsNullOrEmpty(textBoxAddress.Text))
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
                    if (customerId == 0)
                    {
                        // Добавление нового клиента
                        command = new SqlCommand("INSERT INTO Клиенты (Имя, Фамилия, Телефон, ЭлектроннаяПочта, Адрес) VALUES (@Имя, @Фамилия, @Телефон, @ЭлектроннаяПочта, @Адрес)", connection);
                    }
                    else
                    {
                        // Редактирование существующего клиента
                        command = new SqlCommand("UPDATE Клиенты SET Имя = @Имя, Фамилия = @Фамилия, Телефон = @Телефон, ЭлектроннаяПочта = @ЭлектроннаяПочта, Адрес = @Адрес WHERE ИдентификаторКлиента = @ИдентификаторКлиента", connection);
                        command.Parameters.AddWithValue("@ИдентификаторКлиента", customerId);
                    }
                    command.Parameters.AddWithValue("@Имя", textBoxFirstName.Text);
                    command.Parameters.AddWithValue("@Фамилия", textBoxLastName.Text);
                    command.Parameters.AddWithValue("@Телефон", textBoxPhone.Text);
                    command.Parameters.AddWithValue("@ЭлектроннаяПочта", textBoxEmail.Text);
                    command.Parameters.AddWithValue("@Адрес", textBoxAddress.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Информация о клиенте успешно сохранена.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении информации о клиенте: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
