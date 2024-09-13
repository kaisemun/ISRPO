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
    public partial class LoginForm : Form
    {
        // В этой строке подключения должны быть указаны соответствующие параметры для вашей базы данных
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public LoginForm()
        {
            InitializeComponent();
            buttonSignIn.Click += buttonSignIn_Click;
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            string Логин = textBoxLogin.Text;
            string Пароль = textBoxPassword.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Пользователи WHERE Логин = @Логин AND Пароль = @Пароль";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Логин", Логин);
                    command.Parameters.AddWithValue("@Пароль", Пароль);

                    int количествоПользователей = (int)command.ExecuteScalar();

                    if (количествоПользователей > 0)
                    {
                        MessageBox.Show("Успешный вход!");

                        this.Hide();
                        ManagerForm managerForm = new ManagerForm();
                        managerForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз.");
                    }
                }
            }
        }
    }
}