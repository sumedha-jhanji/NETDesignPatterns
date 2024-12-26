namespace WindowcCustomer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtCustomerName = new TextBox();
            txtPhoneNumber = new TextBox();
            txtBillAmount = new TextBox();
            txtBillDate = new TextBox();
            txtAddress = new TextBox();
            cmbCustomerType = new ComboBox();
            btnValidate = new Button();
            addCustomer = new Button();
            dtgGridCustomer = new DataGridView();
            DalLayer = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dtgGridCustomer).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 32);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Customer Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(74, 72);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 1;
            label2.Text = "Customer Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(74, 112);
            label3.Name = "label3";
            label3.Size = new Size(88, 15);
            label3.TabIndex = 2;
            label3.Text = "Phone Number";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(487, 34);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 3;
            label4.Text = "Bill Amount";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(500, 72);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 4;
            label5.Text = "Bill Date";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(478, 112);
            label6.Name = "label6";
            label6.Size = new Size(104, 15);
            label6.TabIndex = 5;
            label6.Text = "Customer Address";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(174, 72);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(165, 23);
            txtCustomerName.TabIndex = 6;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(174, 112);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(165, 23);
            txtPhoneNumber.TabIndex = 7;
            // 
            // txtBillAmount
            // 
            txtBillAmount.Location = new Point(600, 31);
            txtBillAmount.Name = "txtBillAmount";
            txtBillAmount.Size = new Size(163, 23);
            txtBillAmount.TabIndex = 8;
            // 
            // txtBillDate
            // 
            txtBillDate.Location = new Point(600, 67);
            txtBillDate.Name = "txtBillDate";
            txtBillDate.Size = new Size(163, 23);
            txtBillDate.TabIndex = 9;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(600, 104);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(163, 111);
            txtAddress.TabIndex = 10;
            txtAddress.TextChanged += txtAddress_TextChanged;
            // 
            // cmbCustomerType
            // 
            cmbCustomerType.FormattingEnabled = true;
            cmbCustomerType.Items.AddRange(new object[] { "Customer", "Lead" });
            cmbCustomerType.Location = new Point(174, 31);
            cmbCustomerType.Name = "cmbCustomerType";
            cmbCustomerType.Size = new Size(165, 23);
            cmbCustomerType.TabIndex = 11;
            cmbCustomerType.SelectedIndexChanged += cmbCustomerType_SelectedIndexChanged;
            // 
            // btnValidate
            // 
            btnValidate.Location = new Point(74, 172);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(127, 23);
            btnValidate.TabIndex = 12;
            btnValidate.Text = "Validate Customer";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Click += btnValidate_Click;
            // 
            // addCustomer
            // 
            addCustomer.Location = new Point(207, 172);
            addCustomer.Name = "addCustomer";
            addCustomer.Size = new Size(137, 23);
            addCustomer.TabIndex = 13;
            addCustomer.Text = "Add Customer";
            addCustomer.UseVisualStyleBackColor = true;
            addCustomer.Click += addCustomer_Click;
            // 
            // dtgGridCustomer
            // 
            dtgGridCustomer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgGridCustomer.Location = new Point(74, 222);
            dtgGridCustomer.Name = "dtgGridCustomer";
            dtgGridCustomer.Size = new Size(689, 216);
            dtgGridCustomer.TabIndex = 14;
            // 
            // DalLayer
            // 
            DalLayer.FormattingEnabled = true;
            DalLayer.Location = new Point(358, 32);
            DalLayer.Name = "DalLayer";
            DalLayer.Size = new Size(121, 23);
            DalLayer.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DalLayer);
            Controls.Add(dtgGridCustomer);
            Controls.Add(addCustomer);
            Controls.Add(btnValidate);
            Controls.Add(cmbCustomerType);
            Controls.Add(txtAddress);
            Controls.Add(txtBillDate);
            Controls.Add(txtBillAmount);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtCustomerName);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dtgGridCustomer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtCustomerName;
        private TextBox txtPhoneNumber;
        private TextBox txtBillAmount;
        private TextBox txtBillDate;
        private TextBox txtAddress;
        private ComboBox cmbCustomerType;
        private Button btnValidate;
        private Button addCustomer;
        private DataGridView dtgGridCustomer;
        private ComboBox DalLayer;
    }
}
