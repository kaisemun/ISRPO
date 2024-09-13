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
    public partial class AddEditRepairForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";
        private int repairId = 0;

        public AddEditRepairForm()
        {
            InitializeComponent();
            buttonSave.Click += buttonSave_Click;
        }

        public AddEditRepairForm(int repairId) : this()
        {
            this.repairId = repairId;
            LoadRepairData();
        }

        private void LoadRepairData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Ремонты WHERE ИдентификаторРемонта = @ИдентификаторРемонта", connection);
                    command.Parameters.AddWithValue("@ИдентификаторРемонта", repairId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxVehicleId.Text = reader["ИдентификаторТС"].ToString();
                        textBoxDescription.Text = reader["Описание"].ToString();
                        textBoxPartsUsed.Text = reader["ИспользованныеЗапчасти"].ToString();
                        textBoxLaborHours.Text = reader["ЧасыТруда"].ToString();
                        textBoxTotalCost.Text = reader["ОбщаяСтоимость"].ToString();
                        dateTimePickerRepairDate.Value = Convert.ToDateTime(reader["ДатаРемонта"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о ремонте: " + ex.Message);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxVehicleId.Text) ||
                string.IsNullOrEmpty(textBoxDescription.Text) ||
                string.IsNullOrEmpty(textBoxLaborHours.Text) ||
                string.IsNullOrEmpty(textBoxTotalCost.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command;

                    if (repairId == 0)
                    {
                        command = new SqlCommand("INSERT INTO Ремонты (ИдентификаторТС, Описание, ИспользованныеЗапчасти, ЧасыТруда, ОбщаяСтоимость, ДатаРемонта) VALUES (@ИдентификаторТС, @Описание, @ИспользованныеЗапчасти, @ЧасыТруда, @ОбщаяСтоимость, @ДатаРемонта)", connection);
                    }
                    else
                    {
                        command = new SqlCommand("UPDATE Ремонты SET ИдентификаторТС = @ИдентификаторТС, Описание = @Описание, ИспользованныеЗапчасти = @ИспользованныеЗапчасти, ЧасыТруда = @ЧасыТруда, ОбщаяСтоимость = @ОбщаяСтоимость, ДатаРемонта = @ДатаРемонта WHERE ИдентификаторРемонта = @ИдентификаторРемонта", connection);
                        command.Parameters.AddWithValue("@ИдентификаторРемонта", repairId);
                    }

                    command.Parameters.AddWithValue("@ИдентификаторТС", Convert.ToInt32(textBoxVehicleId.Text));
                    command.Parameters.AddWithValue("@Описание", textBoxDescription.Text);
                    command.Parameters.AddWithValue("@ИспользованныеЗапчасти", textBoxPartsUsed.Text);
                    command.Parameters.AddWithValue("@ЧасыТруда", Convert.ToDecimal(textBoxLaborHours.Text));
                    command.Parameters.AddWithValue("@ОбщаяСтоимость", Convert.ToDecimal(textBoxTotalCost.Text));
                    command.Parameters.AddWithValue("@ДатаРемонта", dateTimePickerRepairDate.Value);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Информация о ремонте успешно сохранена.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении информации о ремонте: " + ex.Message);
                }
            }
        }

        private void textBoxTotalCost_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}