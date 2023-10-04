
namespace Cosmetics_Booking_System_21_09_21.Forms
{
    partial class MasterForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.exitBtn = new System.Windows.Forms.Button();
            this.tsddbOfficeRental = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbWeddingApparel = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbDressServices = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbCosmeticsBeauty = new System.Windows.Forms.ToolStripDropDownButton();
            this.addEditDeleteCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEditDeleteBookingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbHouseholdGoods = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCodeComplexities = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(107)))));
            this.pictureBox1.Location = new System.Drawing.Point(-12, -2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1343, 73);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(107)))));
            this.pictureBox2.Location = new System.Drawing.Point(-18, 606);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1343, 73);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(548, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(209, 44);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(107)))));
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(11, 18);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(60, 26);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "Time";
            // 
            // tmrTime
            // 
            this.tmrTime.Enabled = true;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.White;
            this.exitBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exitBtn.BackgroundImage")));
            this.exitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exitBtn.ForeColor = System.Drawing.Color.White;
            this.exitBtn.Location = new System.Drawing.Point(8, 615);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(2);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(131, 56);
            this.exitBtn.TabIndex = 99;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // tsddbOfficeRental
            // 
            this.tsddbOfficeRental.Enabled = false;
            this.tsddbOfficeRental.Image = global::Cosmetics_Booking_System_21_09_21.Properties.Resources.Office_Rental_Icon;
            this.tsddbOfficeRental.Name = "tsddbOfficeRental";
            this.tsddbOfficeRental.Size = new System.Drawing.Size(112, 28);
            this.tsddbOfficeRental.Text = "Office Rental";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsddbWeddingApparel
            // 
            this.tsddbWeddingApparel.Enabled = false;
            this.tsddbWeddingApparel.Image = global::Cosmetics_Booking_System_21_09_21.Properties.Resources.Wedding_Apparel_Icon;
            this.tsddbWeddingApparel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbWeddingApparel.Name = "tsddbWeddingApparel";
            this.tsddbWeddingApparel.Size = new System.Drawing.Size(136, 28);
            this.tsddbWeddingApparel.Text = "Wedding Apparel";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsddbDressServices
            // 
            this.tsddbDressServices.Enabled = false;
            this.tsddbDressServices.Image = global::Cosmetics_Booking_System_21_09_21.Properties.Resources.Dress_Alterations_Icon_2;
            this.tsddbDressServices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDressServices.Name = "tsddbDressServices";
            this.tsddbDressServices.Size = new System.Drawing.Size(117, 28);
            this.tsddbDressServices.Text = "Dress Services";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsddbCosmeticsBeauty
            // 
            this.tsddbCosmeticsBeauty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEditDeleteCustomerToolStripMenuItem,
            this.addEditDeleteBookingToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.tsddbCosmeticsBeauty.Image = global::Cosmetics_Booking_System_21_09_21.Properties.Resources.Cosmetics_And_Beauty_Icon_4;
            this.tsddbCosmeticsBeauty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbCosmeticsBeauty.Name = "tsddbCosmeticsBeauty";
            this.tsddbCosmeticsBeauty.Size = new System.Drawing.Size(161, 28);
            this.tsddbCosmeticsBeauty.Text = "Cosmetics and Beauty";
            // 
            // addEditDeleteCustomerToolStripMenuItem
            // 
            this.addEditDeleteCustomerToolStripMenuItem.Name = "addEditDeleteCustomerToolStripMenuItem";
            this.addEditDeleteCustomerToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addEditDeleteCustomerToolStripMenuItem.Text = "Add/Edit/Delete Customer";
            this.addEditDeleteCustomerToolStripMenuItem.Click += new System.EventHandler(this.addEditDeleteCustomerToolStripMenuItem_Click);
            // 
            // addEditDeleteBookingToolStripMenuItem
            // 
            this.addEditDeleteBookingToolStripMenuItem.Name = "addEditDeleteBookingToolStripMenuItem";
            this.addEditDeleteBookingToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addEditDeleteBookingToolStripMenuItem.Text = "Add/Edit/Delete Booking";
            this.addEditDeleteBookingToolStripMenuItem.Click += new System.EventHandler(this.addEditDeleteBookingToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.reportToolStripMenuItem.Text = "Report";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsddbHouseholdGoods
            // 
            this.tsddbHouseholdGoods.Enabled = false;
            this.tsddbHouseholdGoods.Image = global::Cosmetics_Booking_System_21_09_21.Properties.Resources.Household_Goods_Icon_3;
            this.tsddbHouseholdGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbHouseholdGoods.Name = "tsddbHouseholdGoods";
            this.tsddbHouseholdGoods.Size = new System.Drawing.Size(139, 28);
            this.tsddbHouseholdGoods.Text = "Household Goods";
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(36, 28);
            this.tsbHelp.Text = "Help";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbOfficeRental,
            this.toolStripSeparator1,
            this.tsddbWeddingApparel,
            this.toolStripSeparator2,
            this.tsddbDressServices,
            this.toolStripSeparator3,
            this.tsddbCosmeticsBeauty,
            this.toolStripSeparator4,
            this.tsddbHouseholdGoods,
            this.toolStripSeparator5,
            this.tsbHelp,
            this.toolStripSeparator6,
            this.tsbCodeComplexities});
            this.toolStrip1.Location = new System.Drawing.Point(231, 46);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(861, 31);
            this.toolStrip1.TabIndex = 100;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbCodeComplexities
            // 
            this.tsbCodeComplexities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCodeComplexities.Image = ((System.Drawing.Image)(resources.GetObject("tsbCodeComplexities.Image")));
            this.tsbCodeComplexities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCodeComplexities.Name = "tsbCodeComplexities";
            this.tsbCodeComplexities.Size = new System.Drawing.Size(111, 28);
            this.tsbCodeComplexities.Text = "Code Complexities";
            this.tsbCodeComplexities.ToolTipText = "Code Complexities";
            this.tsbCodeComplexities.Click += new System.EventHandler(this.tsbCodeComplexities_Click);
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1318, 677);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MasterForm";
            this.Text = "MasterForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.ToolStripDropDownButton tsddbOfficeRental;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsddbWeddingApparel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tsddbDressServices;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton tsddbCosmeticsBeauty;
        private System.Windows.Forms.ToolStripMenuItem addEditDeleteCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEditDeleteBookingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton tsddbHouseholdGoods;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbCodeComplexities;
    }
}