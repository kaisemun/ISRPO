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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoServiceApp
{
    public partial class AddEditFinancialTransactionsForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        private int transactionId = 0;

        public AddEditFinancialTransactionsForm()
        {
            InitializeComponent();
            buttonSave.Click += buttonSave_Click;
        }

        public AddEditFinancialTransactionsForm(int transactionId) : this()
        {
            this.transactionId = transactionId;
            LoadTransactionData();
        }

        private void LoadTransactionData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM ФинансовыеТранзакции WHERE ИдентификаторТранзакции = @ИдентификаторТранзакции", connection);
                    command.Parameters.AddWithValue("@ИдентификаторТранзакции", transactionId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxCustomerId.Text = reader["ИдентификаторКлиента"].ToString();
                        textBoxAmount.Text = reader["Сумма"].ToString();
                        textBoxTransactionType.Text = reader["ТипТранзакции"].ToString();
                        dateTimePickerTransactionDate.Value = Convert.ToDateTime(reader["ДатаТранзакции"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о транзакции: " + ex.Message);
                }
            }
        }
        private bool ValidateFields()
        {
            // Проверяем каждое поле на наличие недопустимых символов

            // Проверка идентификатора клиента
            if (!string.IsNullOrEmpty(textBoxCustomerId.Text) && !Regex.IsMatch(textBoxCustomerId.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректный идентификатор клиента (число).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка суммы
            if (!Regex.IsMatch(textBoxAmount.Text, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Пожалуйста, введите корректную сумму (положительное число с двумя знаками после точки).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка типа транзакции
            if (string.IsNullOrEmpty(textBoxTransactionType.Text))
            {
                MessageBox.Show("Пожалуйста, введите тип транзакции.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(textBoxCustomerId.Text) ||
                string.IsNullOrEmpty(textBoxAmount.Text) ||
                string.IsNullOrEmpty(textBoxTransactionType.Text) ||
                dateTimePickerTransactionDate.Value == null)
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
                    if (transactionId == 0)
                    {
                        // Если добавляется новая запись
                        command = new SqlCommand("INSERT INTO ФинансовыеТранзакции (ИдентификаторКлиента, Сумма, ТипТранзакции, ДатаТранзакции) VALUES (@ИдентификаторКлиента, @Сумма, @ТипТранзакции, @ДатаТранзакции)", connection);
                        // Присваиваем значение идентификатора клиента
                        if (!string.IsNullOrEmpty(textBoxCustomerId.Text))
                            command.Parameters.AddWithValue("@ИдентификаторКлиента", textBoxCustomerId.Text);
                        else
                        {
                            MessageBox.Show("Пожалуйста, укажите идентификатор клиента.");
                            return;
                        }
                    }
                    else
                    {
                        // Если редактируется существующая запись
                        command = new SqlCommand("UPDATE ФинансовыеТранзакции SET ИдентификаторКлиента = @ИдентификаторКлиента, Сумма = @Сумма, ТипТранзакции = @ТипТранзакции, ДатаТранзакции = @ДатаТранзакции WHERE ИдентификаторТранзакции = @ИдентификаторТранзакции", connection);
                        // Присваиваем значение идентификатора клиента
                        if (!string.IsNullOrEmpty(textBoxCustomerId.Text))
                            command.Parameters.AddWithValue("@ИдентификаторКлиента", textBoxCustomerId.Text);
                        else
                        {
                            MessageBox.Show("Пожалуйста, укажите идентификатор клиента.");
                            return;
                        }
                        command.Parameters.AddWithValue("@ИдентификаторТранзакции", transactionId);
                    }
                    // Прочие параметры
                    command.Parameters.AddWithValue("@Сумма", textBoxAmount.Text);
                    command.Parameters.AddWithValue("@ТипТранзакции", textBoxTransactionType.Text);
                    command.Parameters.AddWithValue("@ДатаТранзакции", dateTimePickerTransactionDate.Value);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Информация о транзакции успешно сохранена.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении информации о транзакции: " + ex.Message);
                }
            }
        }
    }
}