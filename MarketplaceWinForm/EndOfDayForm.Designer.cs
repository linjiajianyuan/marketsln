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
            this.SuspendLayout();
            // 
            // DomesticEndOfDayBtn
            // 
            this.DomesticEndOfDayBtn.Location = new System.Drawing.Point(58, 43);
            this.DomesticEndOfDayBtn.Name = "DomesticEndOfDayBtn";
            this.DomesticEndOfDayBtn.Size = new System.Drawing.Size(150, 44);
            this.DomesticEndOfDayBtn.TabIndex = 0;
            this.DomesticEndOfDayBtn.Text = "Domestic";
            this.DomesticEndOfDayBtn.UseVisualStyleBackColor = true;
            this.DomesticEndOfDayBtn.Click += new System.EventHandler(this.DomesticEndOfDayBtn_Click);
            // 
            // EndOfDayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.DomesticEndOfDayBtn);
            this.Name = "EndOfDayForm";
            this.Text = "EndOfDayForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DomesticEndOfDayBtn;
    }
}