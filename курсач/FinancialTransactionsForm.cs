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
    public partial class FinancialTransactionsForm : Form
    {
        DataBase dataBase = new DataBase();
        private const string connectionString = "Data Source=DESKTOP-17763KV\\SONYA;Initial Catalog=autoservis1;Integrated Security=True;";

        public FinancialTransactionsForm()
        {
            InitializeComponent();
            LoadTransactions();
            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void FinancialTransactionsForm_Load(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ФинансовыеТранзакции", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewTransactions.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке финансовых транзакций: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEditFinancialTransactionsForm addEditForm = new AddEditFinancialTransactionsForm();
            addEditForm.ShowDialog();
            LoadTransactions();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.SelectedRows.Count > 0)
            {
                int transactionId = Convert.ToInt32(dataGridViewTransactions.SelectedRows[0].Cells["ИдентификаторТранзакции"].Value);
                AddEditFinancialTransactionsForm addEditForm = new AddEditFinancialTransactionsForm(transactionId);
                addEditForm.ShowDialog();
                LoadTransactions();
            }
            else
            {
                MessageBox.Show("Выберите транзакцию для редактирования.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.SelectedRows.Count > 0)
            {
                int transactionId = Convert.ToInt32(dataGridViewTransactions.SelectedRows[0].Cells["ИдентификаторТранзакции"].Value);
                if (MessageBox.Show("Вы уверены, что хотите удалить эту транзакцию?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("DELETE FROM ФинансовыеТранзакции WHERE ИдентификаторТранзакции = @ИдентификаторТранзакции", connection);
                            command.Parameters.AddWithValue("@ИдентификаторТранзакции", transactionId);
                            command.ExecuteNonQuery();
                            LoadTransactions();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении транзакции: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите транзакцию для удаления.");
            }
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {

        }
    }
}