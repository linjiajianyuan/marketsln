namespace MarketplaceWinForm
{
    partial class ShipmentInfoForm
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
            this._ShipmentInfoDgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._ShipmentInfoDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // _ShipmentInfoDgv
            // 
            this._ShipmentInfoDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._ShipmentInfoDgv.Location = new System.Drawing.Point(12, 12);
            this._ShipmentInfoDgv.Name = "_ShipmentInfoDgv";
            this._ShipmentInfoDgv.Size = new System.Drawing.Size(814, 228);
            this._ShipmentInfoDgv.TabIndex = 0;
            // 
            // ShipmentInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 292);
            this.Controls.Add(this._ShipmentInfoDgv);
            this.Name = "ShipmentInfoForm";
            this.Text = "ShipmentInfoForm";
            ((System.ComponentModel.ISupportInitialize)(this._ShipmentInfoDgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _ShipmentInfoDgv;
    }
}