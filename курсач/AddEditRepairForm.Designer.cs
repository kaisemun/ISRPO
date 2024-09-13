namespace AutoServiceApp
{
    partial class AddEditRepairForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditRepairForm));
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxPartsUsed = new System.Windows.Forms.TextBox();
            this.textBoxLaborHours = new System.Windows.Forms.TextBox();
            this.textBoxTotalCost = new System.Windows.Forms.TextBox();
            this.dateTimePickerRepairDate = new System.Windows.Forms.DateTimePicker();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxVehicleId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(398, 139);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(170, 22);
            this.textBoxDescription.TabIndex = 0;
            // 
            // textBoxPartsUsed
            // 
            this.textBoxPartsUsed.Location = new System.Drawing.Point(208, 139);
            this.textBoxPartsUsed.Name = "textBoxPartsUsed";
            this.textBoxPartsUsed.Size = new System.Drawing.Size(170, 22);
            this.textBoxPartsUsed.TabIndex = 1;
            // 
            // textBoxLaborHours
            // 
            this.textBoxLaborHours.Location = new System.Drawing.Point(208, 195);
            this.textBoxLaborHours.Name = "textBoxLaborHours";
            this.textBoxLaborHours.Size = new System.Drawing.Size(170, 22);
            this.textBoxLaborHours.TabIndex = 2;
            // 
            // textBoxTotalCost
            // 
            this.textBoxTotalCost.Location = new System.Drawing.Point(398, 195);
            this.textBoxTotalCost.Name = "textBoxTotalCost";
            this.textBoxTotalCost.Size = new System.Drawing.Size(170, 22);
            this.textBoxTotalCost.TabIndex = 3;
            this.textBoxTotalCost.TextChanged += new System.EventHandler(this.textBoxTotalCost_TextChanged);
            // 
            // dateTimePickerRepairDate
            // 
            this.dateTimePickerRepairDate.Location = new System.Drawing.Point(288, 249);
            this.dateTimePickerRepairDate.Name = "dateTimePickerRepairDate";
            this.dateTimePickerRepairDate.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerRepairDate.TabIndex = 4;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonSave.Location = new System.Drawing.Point(241, 294);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(290, 52);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(254, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Описание:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(395, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Использованные запчасти:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(254, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Часы труда:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(424, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Общая стоимость:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(336, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Дата ремонта:";
            // 
            // textBoxVehicleId
            // 
            this.textBoxVehicleId.Location = new System.Drawing.Point(303, 81);
            this.textBoxVehicleId.Name = "textBoxVehicleId";
            this.textBoxVehicleId.Size = new System.Drawing.Size(170, 22);
            this.textBoxVehicleId.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(321, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Идентификатор ТС:";
            // 
            // AddEditRepairForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxVehicleId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dateTimePickerRepairDate);
            this.Controls.Add(this.textBoxTotalCost);
            this.Controls.Add(this.textBoxLaborHours);
            this.Controls.Add(this.textBoxPartsUsed);
            this.Controls.Add(this.textBoxDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEditRepairForm";
            this.Text = "AutoService (Добавить ремонт)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxPartsUsed;
        private System.Windows.Forms.TextBox textBoxLaborHours;
        private System.Windows.Forms.TextBox textBoxTotalCost;
        private System.Windows.Forms.DateTimePicker dateTimePickerRepairDate;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxVehicleId;
        private System.Windows.Forms.Label label3;
    }
}