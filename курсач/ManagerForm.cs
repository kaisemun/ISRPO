using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoServiceApp
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            КлиентыToolStripMenuItem.Click += КлиентыToolStripMenuItem_Click;
            ТранспортныеСредстваToolStripMenuItem.Click += ТранспортныеСредстваToolStripMenuItem_Click;
            РемонтыToolStripMenuItem.Click += РемонтыToolStripMenuItem_Click;
            ИнвентарьToolStripMenuItem.Click += ИнвентарьToolStripMenuItem_Click;
            ЗаказыToolStripMenuItem.Click += ЗаказыToolStripMenuItem_Click;
            ФинансовыеОперацииToolStripMenuItem.Click += ФинансовыеОперацииToolStripMenuItem_Click;
            toolStripButtonДобавитьКлиента.Click += toolStripButtonДобавитьКлиента_Click;
            toolStripButtonДобавитьАвтомобиль.Click += toolStripButtonДобавитьАвтомобиль_Click;
            toolStripButtonНовыйРемонт.Click += toolStripButtonНовыйРемонт_Click;
            toolStripButtonУправлениеСкладом.Click += toolStripButtonУправлениеСкладом_Click;
            toolStripButtonДобавитьЗаказ.Click += toolStripButtonДобавитьЗаказ_Click;
            toolStripButtonДобавитьФинансовуюОперацию.Click += toolStripButtonДобавитьФинансовуюОперацию_Click;
            ВыходToolStripMenuItem.Click += ВыходToolStripMenuItem_Click;
            buttonExit.Click += buttonExit_Click;
        }

        private void КлиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы Клиентов (CustomersForm)
            CustomersForm customersForm = new CustomersForm();
            customersForm.Show();
        }

        private void ТранспортныеСредстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы Транспортных средств (VehiclesForm)
            VehiclesForm vehiclesForm = new VehiclesForm();
            vehiclesForm.Show();
        }

        private void РемонтыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы Ремонтов (RepairsForm)
            RepairsForm repairsForm = new RepairsForm();
            repairsForm.Show();
        }

        private void ИнвентарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы Инвентаря (InventoryForm)
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.Show();
        }

        private void ЗаказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы Заказов (OrdersForm)
            OrdersForm ordersForm = new OrdersForm();
            ordersForm.Show();
        }

        private void ФинансовыеОперацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы Финансовых операций (FinancialTransactionsForm)
            FinancialTransactionsForm financialTransactionsForm = new FinancialTransactionsForm();
            financialTransactionsForm.Show();
        }

        private void toolStripButtonДобавитьКлиента_Click(object sender, EventArgs e)
        {
            // Открытие формы для добавления нового клиента (AddEditCustomerForm)
            AddEditCustomerForm addEditCustomerForm = new AddEditCustomerForm();
            addEditCustomerForm.Show();
        }

        private void toolStripButtonДобавитьАвтомобиль_Click(object sender, EventArgs e)
        {
            // Открытие формы для добавления нового автомобиля (AddEditVehicleForm)
            AddEditVehicleForm addEditVehicleForm = new AddEditVehicleForm();
            addEditVehicleForm.Show();
        }

        private void toolStripButtonНовыйРемонт_Click(object sender, EventArgs e)
        {
            // Открытие формы для добавления нового ремонта (AddEditRepairForm)
            AddEditRepairForm addEditRepairForm = new AddEditRepairForm();
            addEditRepairForm.Show();
        }

        private void toolStripButtonУправлениеСкладом_Click(object sender, EventArgs e)
        {
            // Открытие формы для управления складом (AddEditInventoryForm)
            AddEditInventoryForm addEditInventoryForm = new AddEditInventoryForm();
            addEditInventoryForm.Show();
        }

        private void toolStripButtonДобавитьЗаказ_Click(object sender, EventArgs e)
        {
            // Открытие формы для добавления нового заказа (AddEditOrderForm)
            AddEditOrderForm addEditOrderForm = new AddEditOrderForm();
            addEditOrderForm.Show();
        }

        private void toolStripButtonДобавитьФинансовуюОперацию_Click(object sender, EventArgs e)
        {
            // Открытие формы для добавления новой финансовой операции (AddEditFinancialTransactionsForm)
            AddEditFinancialTransactionsForm addEditFinancialTransactionsForm = new AddEditFinancialTransactionsForm();
            addEditFinancialTransactionsForm.Show();
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {

        }
        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создание экземпляра формы MainForm
            MainForm mainForm = new MainForm();

            // Скрытие текущей формы
            this.Hide();

            // Отображение формы MainForm
            mainForm.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // Вызов обработчика события для кнопки "ВыходToolStripMenuItem"
            ВыходToolStripMenuItem_Click(sender, e);
        }
    }
}