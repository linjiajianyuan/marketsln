namespace MarketplaceWinForm
{
    partial class OrdersForm
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
            this._OrderDgv = new System.Windows.Forms.DataGridView();
            this._UnshippedCheckBox = new System.Windows.Forms.CheckBox();
            this._ShippedCheckBox = new System.Windows.Forms.CheckBox();
            this._PrintShippingLabelBtn = new System.Windows.Forms.Button();
            this._SearchBtn = new System.Windows.Forms.Button();
            this._StartDatePicker = new System.Windows.Forms.DateTimePicker();
            this._EndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._TotalLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._EbayItemNumTxt = new System.Windows.Forms.TextBox();
            this._EmailTxt = new System.Windows.Forms.TextBox();
            this._NameTxt = new System.Windows.Forms.TextBox();
            this._BuyerUserIdTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._OrderDgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _OrderDgv
            // 
            this._OrderDgv.AllowUserToAddRows = false;
            this._OrderDgv.AllowUserToDeleteRows = false;
            this._OrderDgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._OrderDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._OrderDgv.Location = new System.Drawing.Point(12, 32);
            this._OrderDgv.Name = "_OrderDgv";
            this._OrderDgv.Size = new System.Drawing.Size(817, 414);
            this._OrderDgv.TabIndex = 0;
            this._OrderDgv.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._CellContentDoubleClick);
            // 
            // _UnshippedCheckBox
            // 
            this._UnshippedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._UnshippedCheckBox.AutoSize = true;
            this._UnshippedCheckBox.Checked = true;
            this._UnshippedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._UnshippedCheckBox.Location = new System.Drawing.Point(847, 32);
            this._UnshippedCheckBox.Name = "_UnshippedCheckBox";
            this._UnshippedCheckBox.Size = new System.Drawing.Size(77, 17);
            this._UnshippedCheckBox.TabIndex = 1;
            this._UnshippedCheckBox.Text = "Unshipped";
            this._UnshippedCheckBox.UseVisualStyleBackColor = true;
            this._UnshippedCheckBox.CheckedChanged += new System.EventHandler(this._UnshippedCheckBox_CheckedChanged);
            // 
            // _ShippedCheckBox
            // 
            this._ShippedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ShippedCheckBox.AutoSize = true;
            this._ShippedCheckBox.Location = new System.Drawing.Point(847, 55);
            this._ShippedCheckBox.Name = "_ShippedCheckBox";
            this._ShippedCheckBox.Size = new System.Drawing.Size(65, 17);
            this._ShippedCheckBox.TabIndex = 2;
            this._ShippedCheckBox.Text = "Shipped";
            this._ShippedCheckBox.UseVisualStyleBackColor = true;
            this._ShippedCheckBox.CheckedChanged += new System.EventHandler(this._ShippedCheckBox_CheckedChanged);
            // 
            // _PrintShippingLabelBtn
            // 
            this._PrintShippingLabelBtn.Location = new System.Drawing.Point(153, 296);
            this._PrintShippingLabelBtn.Name = "_PrintShippingLabelBtn";
            this._PrintShippingLabelBtn.Size = new System.Drawing.Size(111, 44);
            this._PrintShippingLabelBtn.TabIndex = 3;
            this._PrintShippingLabelBtn.Text = "Print Shipping Label";
            this._PrintShippingLabelBtn.UseVisualStyleBackColor = true;
            this._PrintShippingLabelBtn.Click += new System.EventHandler(this._PrintShippingLabelBtn_Click);
            // 
            // _SearchBtn
            // 
            this._SearchBtn.Location = new System.Drawing.Point(16, 296);
            this._SearchBtn.Name = "_SearchBtn";
            this._SearchBtn.Size = new System.Drawing.Size(108, 44);
            this._SearchBtn.TabIndex = 4;
            this._SearchBtn.Text = "Search";
            this._SearchBtn.UseVisualStyleBackColor = true;
            this._SearchBtn.Click += new System.EventHandler(this._SearchBtn_Click);
            // 
            // _StartDatePicker
            // 
            this._StartDatePicker.Location = new System.Drawing.Point(16, 34);
            this._StartDatePicker.Name = "_StartDatePicker";
            this._StartDatePicker.Size = new System.Drawing.Size(200, 20);
            this._StartDatePicker.TabIndex = 5;
            // 
            // _EndDatePicker
            // 
            this._EndDatePicker.Location = new System.Drawing.Point(15, 73);
            this._EndDatePicker.Name = "_EndDatePicker";
            this._EndDatePicker.Size = new System.Drawing.Size(200, 20);
            this._EndDatePicker.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Date:";
            // 
            // _TotalLabel
            // 
            this._TotalLabel.AutoSize = true;
            this._TotalLabel.Location = new System.Drawing.Point(12, 13);
            this._TotalLabel.Name = "_TotalLabel";
            this._TotalLabel.Size = new System.Drawing.Size(34, 13);
            this._TotalLabel.TabIndex = 10;
            this._TotalLabel.Text = "Total:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._EbayItemNumTxt);
            this.groupBox1.Controls.Add(this._EmailTxt);
            this.groupBox1.Controls.Add(this._NameTxt);
            this.groupBox1.Controls.Add(this._BuyerUserIdTxt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._StartDatePicker);
            this.groupBox1.Controls.Add(this._PrintShippingLabelBtn);
            this.groupBox1.Controls.Add(this._SearchBtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._EndDatePicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(847, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 357);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advance Search";
            // 
            // _EbayItemNumTxt
            // 
            this._EbayItemNumTxt.Location = new System.Drawing.Point(116, 102);
            this._EbayItemNumTxt.Name = "_EbayItemNumTxt";
            this._EbayItemNumTxt.Size = new System.Drawing.Size(169, 20);
            this._EbayItemNumTxt.TabIndex = 18;
            // 
            // _EmailTxt
            // 
            this._EmailTxt.Location = new System.Drawing.Point(116, 197);
            this._EmailTxt.Name = "_EmailTxt";
            this._EmailTxt.Size = new System.Drawing.Size(169, 20);
            this._EmailTxt.TabIndex = 17;
            // 
            // _NameTxt
            // 
            this._NameTxt.Location = new System.Drawing.Point(116, 162);
            this._NameTxt.Name = "_NameTxt";
            this._NameTxt.Size = new System.Drawing.Size(169, 20);
            this._NameTxt.TabIndex = 15;
            // 
            // _BuyerUserIdTxt
            // 
            this._BuyerUserIdTxt.Location = new System.Drawing.Point(116, 133);
            this._BuyerUserIdTxt.Name = "_BuyerUserIdTxt";
            this._BuyerUserIdTxt.Size = new System.Drawing.Size(169, 20);
            this._BuyerUserIdTxt.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Buyer User ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "eBay Item Number:";
            // 
            // OrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 461);
            this.Controls.Add(this._TotalLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._ShippedCheckBox);
            this.Controls.Add(this._UnshippedCheckBox);
            this.Controls.Add(this._OrderDgv);
            this.Name = "OrdersForm";
            this.Text = "OrdersForm";
            this.Load += new System.EventHandler(this.OrdersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._OrderDgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _OrderDgv;
        private System.Windows.Forms.CheckBox _UnshippedCheckBox;
        private System.Windows.Forms.CheckBox _ShippedCheckBox;
        private System.Windows.Forms.Button _PrintShippingLabelBtn;
        private System.Windows.Forms.Button _SearchBtn;
        private System.Windows.Forms.DateTimePicker _StartDatePicker;
        private System.Windows.Forms.DateTimePicker _EndDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _TotalLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _EmailTxt;
        private System.Windows.Forms.TextBox _NameTxt;
        private System.Windows.Forms.TextBox _BuyerUserIdTxt;
        private System.Windows.Forms.TextBox _EbayItemNumTxt;
    }
}