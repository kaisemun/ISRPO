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
    public partial class AddEditVehicleForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        private int vehicleId = 0;
        public AddEditVehicleForm()
        {
            InitializeComponent();
            buttonSave.Click += buttonSave_Click;
        }

        public AddEditVehicleForm(int vehicleId) : this()
        {
            this.vehicleId = vehicleId;
            LoadVehicleData();
        }

        private void LoadVehicleData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM ТранспортныеСредства WHERE ИдентификаторТС = @ИдентификаторТС", connection);
                    command.Parameters.AddWithValue("@ИдентификаторТС", vehicleId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxCustomerId.Text = reader["ИдентификаторКлиента"].ToString();
                        textBoxBrand.Text = reader["Марка"].ToString();
                        textBoxModel.Text = reader["Модель"].ToString();
                        textBoxYear.Text = reader["Год"].ToString();
                        textBoxVIN.Text = reader["VIN"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о транспортном средстве: " + ex.Message);
                }
            }
        }
        private bool ValidateYear(string year)
        {
            // Проверяем, состоит ли строка из 4 цифр
            if (year.Length != 4 || !year.All(char.IsDigit))
            {
                MessageBox.Show("Пожалуйста, введите корректный год (четыре цифры).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверяем, входит ли год в разумный диапазон (например, от 1900 до текущего года)
            int yearValue = int.Parse(year);
            int currentYear = DateTime.Now.Year;

            if (yearValue < 1900 || yearValue > currentYear)
            {
                MessageBox.Show($"Пожалуйста, введите год в разумном диапазоне от 1900 до {currentYear}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Проверяем валидность данных перед сохранением
            if (string.IsNullOrEmpty(textBoxCustomerId.Text) ||
                string.IsNullOrEmpty(textBoxBrand.Text) ||
                string.IsNullOrEmpty(textBoxModel.Text) ||
                string.IsNullOrEmpty(textBoxYear.Text) ||
                string.IsNullOrEmpty(textBoxVIN.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем валидность года выпуска
            if (!ValidateYear(textBoxYear.Text))
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            SqlCommand command;
            if (vehicleId == 0)
            {
                command = new SqlCommand("INSERT INTO ТранспортныеСредства (ИдентификаторКлиента, Марка, Модель, Год, VIN) VALUES (@ИдентификаторКлиента, @Марка, @Модель, @Год, @VIN)", connection);
            }
            else
            {
                command = new SqlCommand("UPDATE ТранспортныеСредства SET ИдентификаторКлиента = @ИдентификаторКлиента, Марка = @Марка, Модель = @Модель, Год = @Год, VIN = @VIN WHERE ИдентификаторТС = @ИдентификаторТС", connection);
                command.Parameters.AddWithValue("@ИдентификаторТС", vehicleId);
            }
            command.Parameters.AddWithValue("@ИдентификаторКлиента", Convert.ToInt32(textBoxCustomerId.Text));
            command.Parameters.AddWithValue("@Марка", textBoxBrand.Text);
            command.Parameters.AddWithValue("@Модель", textBoxModel.Text);
            command.Parameters.AddWithValue("@Год", textBoxYear.Text);
            command.Parameters.AddWithValue("@VIN", textBoxVIN.Text);

            command.ExecuteNonQuery();
            MessageBox.Show("Информация о транспортном средстве успешно сохранена.");
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при сохранении информации о транспортном средстве: " + ex.Message);
        }
    }
}

        private void AddEditVehicleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
