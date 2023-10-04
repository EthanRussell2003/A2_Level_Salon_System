using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cosmetics_Booking_System_21_09_21.Forms
{
    public partial class CosmeticsMenu : Cosmetics_Booking_System_21_09_21.Forms.MasterForm
    {
        public CosmeticsMenu()
        {
         
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form = new MainMenu();
            form.ShowDialog();
            this.Close();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddEditDeleteForm form = new AddEditDeleteForm();
            form.ShowDialog();
            this.Close();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookingDateTime form = new BookingDateTime();
            form.ShowDialog();
            this.Close();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report form = new Report();
            form.ShowDialog();
            this.Close();
        }
    }
}
