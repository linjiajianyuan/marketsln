namespace MarketplaceWinForm
{
    partial class EndOfDayForm
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
            this.DomesticEndOfDayBtn = new System.Windows.Forms.Button();
            this._StartDateTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this._EndDateTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DomesticEndOfDayBtn
            // 
            this.DomesticEndOfDayBtn.Location = new System.Drawing.Point(172, 43);
            this.DomesticEndOfDayBtn.Name = "DomesticEndOfDayBtn";
            this.DomesticEndOfDayBtn.Size = new System.Drawing.Size(150, 44);
            this.DomesticEndOfDayBtn.TabIndex = 0;
            this.DomesticEndOfDayBtn.Text = "Run";
            this.DomesticEndOfDayBtn.UseVisualStyleBackColor = true;
            this.DomesticEndOfDayBtn.Click += new System.EventHandler(this.DomesticEndOfDayBtn_Click);
            // 
            // _StartDateTime
            // 
            this._StartDateTime.Location = new System.Drawing.Point(23, 127);
            this._StartDateTime.Name = "_StartDateTime";
            this._StartDateTime.Size = new System.Drawing.Size(200, 20);
            this._StartDateTime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Date Time";
            // 
            // _EndDateTime
            // 
            this._EndDateTime.Location = new System.Drawing.Point(283, 127);
            this._EndDateTime.Name = "_EndDateTime";
            this._EndDateTime.Size = new System.Drawing.Size(200, 20);
            this._EndDateTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Date Time";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(50, 155);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(400, 150);
            this.dataGridView1.TabIndex = 5;
            // 
            // EndOfDayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 317);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._EndDateTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._StartDateTime);
            this.Controls.Add(this.DomesticEndOfDayBtn);
            this.Name = "EndOfDayForm";
            this.Text = "EndOfDayForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DomesticEndOfDayBtn;
        private System.Windows.Forms.DateTimePicker _StartDateTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker _EndDateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}