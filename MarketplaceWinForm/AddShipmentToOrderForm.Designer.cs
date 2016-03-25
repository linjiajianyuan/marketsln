namespace MarketplaceWinForm
{
    partial class AddShipmentToOrderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._OrderNumTxt = new System.Windows.Forms.TextBox();
            this._TrackingNumTxt = new System.Windows.Forms.TextBox();
            this._CarrierTxt = new System.Windows.Forms.TextBox();
            this._SaveBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this._ChannelComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this._AccountCombox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tracking Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Carrier:";
            // 
            // _OrderNumTxt
            // 
            this._OrderNumTxt.Location = new System.Drawing.Point(135, 34);
            this._OrderNumTxt.Name = "_OrderNumTxt";
            this._OrderNumTxt.Size = new System.Drawing.Size(309, 20);
            this._OrderNumTxt.TabIndex = 3;
            // 
            // _TrackingNumTxt
            // 
            this._TrackingNumTxt.Location = new System.Drawing.Point(135, 151);
            this._TrackingNumTxt.Name = "_TrackingNumTxt";
            this._TrackingNumTxt.Size = new System.Drawing.Size(309, 20);
            this._TrackingNumTxt.TabIndex = 4;
            // 
            // _CarrierTxt
            // 
            this._CarrierTxt.Location = new System.Drawing.Point(135, 211);
            this._CarrierTxt.Name = "_CarrierTxt";
            this._CarrierTxt.Size = new System.Drawing.Size(309, 20);
            this._CarrierTxt.TabIndex = 5;
            // 
            // _SaveBtn
            // 
            this._SaveBtn.Location = new System.Drawing.Point(188, 246);
            this._SaveBtn.Name = "_SaveBtn";
            this._SaveBtn.Size = new System.Drawing.Size(150, 23);
            this._SaveBtn.TabIndex = 6;
            this._SaveBtn.Text = "Save";
            this._SaveBtn.UseVisualStyleBackColor = true;
            this._SaveBtn.Click += new System.EventHandler(this._SaveBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Channel";
            // 
            // _ChannelComboBox
            // 
            this._ChannelComboBox.FormattingEnabled = true;
            this._ChannelComboBox.Location = new System.Drawing.Point(135, 73);
            this._ChannelComboBox.Name = "_ChannelComboBox";
            this._ChannelComboBox.Size = new System.Drawing.Size(166, 21);
            this._ChannelComboBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Account";
            // 
            // _AccountCombox
            // 
            this._AccountCombox.FormattingEnabled = true;
            this._AccountCombox.Location = new System.Drawing.Point(135, 115);
            this._AccountCombox.Name = "_AccountCombox";
            this._AccountCombox.Size = new System.Drawing.Size(166, 21);
            this._AccountCombox.TabIndex = 10;
            // 
            // AddShipmentToOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 281);
            this.Controls.Add(this._AccountCombox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._ChannelComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._SaveBtn);
            this.Controls.Add(this._CarrierTxt);
            this.Controls.Add(this._TrackingNumTxt);
            this.Controls.Add(this._OrderNumTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddShipmentToOrderForm";
            this.Text = "AddShipmentToOrder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _OrderNumTxt;
        private System.Windows.Forms.TextBox _TrackingNumTxt;
        private System.Windows.Forms.TextBox _CarrierTxt;
        private System.Windows.Forms.Button _SaveBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _ChannelComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _AccountCombox;
    }
}