namespace MarketplaceWinForm
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.orderAndInvoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addShipmentToOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.domesticEndOfDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkShipmentInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SKUMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importVisionInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlySalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.monthlySalesByChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderAndInvoicesToolStripMenuItem,
            this.shipmentToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.statisticsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1148, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // orderAndInvoicesToolStripMenuItem
            // 
            this.orderAndInvoicesToolStripMenuItem.Name = "orderAndInvoicesToolStripMenuItem";
            this.orderAndInvoicesToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.orderAndInvoicesToolStripMenuItem.Text = "Orders";
            this.orderAndInvoicesToolStripMenuItem.Click += new System.EventHandler(this.orderAndInvoicesToolStripMenuItem_Click);
            // 
            // shipmentToolStripMenuItem
            // 
            this.shipmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addShipmentToOrderToolStripMenuItem,
            this.domesticEndOfDayToolStripMenuItem,
            this.checkShipmentInformationToolStripMenuItem});
            this.shipmentToolStripMenuItem.Name = "shipmentToolStripMenuItem";
            this.shipmentToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.shipmentToolStripMenuItem.Text = "Shipment";
            // 
            // addShipmentToOrderToolStripMenuItem
            // 
            this.addShipmentToOrderToolStripMenuItem.Name = "addShipmentToOrderToolStripMenuItem";
            this.addShipmentToOrderToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addShipmentToOrderToolStripMenuItem.Text = "Add Shipment To Order";
            this.addShipmentToOrderToolStripMenuItem.Click += new System.EventHandler(this.addShipmentToOrderToolStripMenuItem_Click);
            // 
            // domesticEndOfDayToolStripMenuItem
            // 
            this.domesticEndOfDayToolStripMenuItem.Name = "domesticEndOfDayToolStripMenuItem";
            this.domesticEndOfDayToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.domesticEndOfDayToolStripMenuItem.Text = "End of Day";
            this.domesticEndOfDayToolStripMenuItem.Click += new System.EventHandler(this.EndOfDayToolStripMenuItem_Click);
            // 
            // checkShipmentInformationToolStripMenuItem
            // 
            this.checkShipmentInformationToolStripMenuItem.Name = "checkShipmentInformationToolStripMenuItem";
            this.checkShipmentInformationToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.checkShipmentInformationToolStripMenuItem.Text = "CheckShipment Information";
            this.checkShipmentInformationToolStripMenuItem.Click += new System.EventHandler(this.checkShipmentInformationToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SKUMapToolStripMenuItem,
            this.importVisionInventoryToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            this.inventoryToolStripMenuItem.Click += new System.EventHandler(this.inventoryToolStripMenuItem_Click);
            // 
            // SKUMapToolStripMenuItem
            // 
            this.SKUMapToolStripMenuItem.Name = "SKUMapToolStripMenuItem";
            this.SKUMapToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.SKUMapToolStripMenuItem.Text = "SKU Map";
            this.SKUMapToolStripMenuItem.Click += new System.EventHandler(this.SKUMapToolStripMenuItem_Click);
            // 
            // importVisionInventoryToolStripMenuItem
            // 
            this.importVisionInventoryToolStripMenuItem.Name = "importVisionInventoryToolStripMenuItem";
            this.importVisionInventoryToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.importVisionInventoryToolStripMenuItem.Text = "Import Vision Inventory";
            this.importVisionInventoryToolStripMenuItem.Click += new System.EventHandler(this.importVisionInventoryToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monthlySalesToolStripMenuItem,
            this.monthlySalesByChannelToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // monthlySalesToolStripMenuItem
            // 
            this.monthlySalesToolStripMenuItem.Name = "monthlySalesToolStripMenuItem";
            this.monthlySalesToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.monthlySalesToolStripMenuItem.Text = "Monthly Sales";
            this.monthlySalesToolStripMenuItem.Click += new System.EventHandler(this.monthlySalesToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(167, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(402, 219);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // monthlySalesByChannelToolStripMenuItem
            // 
            this.monthlySalesByChannelToolStripMenuItem.Name = "monthlySalesByChannelToolStripMenuItem";
            this.monthlySalesByChannelToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.monthlySalesByChannelToolStripMenuItem.Text = "Monthly Sales By Channel";
            this.monthlySalesByChannelToolStripMenuItem.Click += new System.EventHandler(this.monthlySalesByChannelToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 568);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MarketplaceSln";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem orderAndInvoicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shipmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addShipmentToOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem domesticEndOfDayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkShipmentInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SKUMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importVisionInventoryToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlySalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlySalesByChannelToolStripMenuItem;
    }
}

