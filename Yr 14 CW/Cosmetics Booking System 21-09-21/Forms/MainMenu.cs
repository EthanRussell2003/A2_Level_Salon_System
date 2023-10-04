using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cosmetics_Booking_System_21_09_21.Objects;
using Cosmetics_Booking_System_21_09_21.DBAccess;
using Cosmetics_Booking_System_21_09_21.Custom;

namespace Cosmetics_Booking_System_21_09_21.Forms
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            GlobalVariables.dateReturn = DateTime.MinValue;
            GlobalVariables.editEnabled = false;
        }
           

        private void OfficeRentalBtn_Click(object sender, EventArgs e)
        {
            UnfinishedModuleMessage();
        }

        private void WeddingApparelBtn_Click(object sender, EventArgs e)
        {
            UnfinishedModuleMessage();
        }

        private void DressServicesBtn_Click(object sender, EventArgs e)
        {
            UnfinishedModuleMessage();
        }

        private void HouseholdGoodsBtn_Click(object sender, EventArgs e)
        {
            UnfinishedModuleMessage();
        }

        private void CosmeticsAndBeautyBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            CosmeticsMenu form= new CosmeticsMenu();
            form.ShowDialog();
            this.Close();
        }

        private void UnfinishedModuleMessage()
        {
            MessageBox.Show("This module is not yet finished. \nOnly 'Cosmetics and Beauty' is currently available.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            lblTime.Text = dateTime.ToString();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
