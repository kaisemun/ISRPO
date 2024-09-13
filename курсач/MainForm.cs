using AutoServiceApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; // Добавляем пространство имен для работы с регулярными выражениями
using System.Windows.Forms;

namespace AutoServiceApp
{
    public partial class MainForm : Form
    {
        // Строка подключения к вашей базе данных
        string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Здесь можно добавить код, который будет выполнен при загрузке формы
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            // Проверяем, содержат ли поля недопустимые символы
            if (!ValidateFields())
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем данные о клиенте
            string имя = textBoxFirstName.Text;
            string фамилия = textBoxLastName.Text;
            string телефон = textBoxPhone.Text;
            string почта = textBoxEmail.Text;
            string адрес = textBoxAddress.Text;

            // Выполняем операцию вставки данных в таблицу "Клиенты"
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Клиенты (Имя, Фамилия, Телефон, ЭлектроннаяПочта, Адрес) VALUES (@Имя, @Фамилия, @Телефон, @ЭлектроннаяПочта, @Адрес)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@Имя", имя);
                    command.Parameters.AddWithValue("@Фамилия", фамилия);
                    command.Parameters.AddWithValue("@Телефон", телефон);
                    command.Parameters.AddWithValue("@ЭлектроннаяПочта", почта);
                    command.Parameters.AddWithValue("@Адрес", адрес);

                    // Открываем соединение и выполняем команду
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены в базу данных.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
                    }
                }
            }

            // Очищаем поля формы после вставки данных
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
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

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Открываем форму авторизации
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}