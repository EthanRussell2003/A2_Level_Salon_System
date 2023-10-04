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

namespace Cosmetics_Booking_System_21_09_21.Forms
{
    public partial class MasterForm : Form
    {
        public MasterForm()
        {
            InitializeComponent();
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            lblTime.Text = dateTime.ToString();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void addEditDeleteCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddEditDeleteForm form = new AddEditDeleteForm();
            form.ShowDialog();
            this.Close();
        }

        private void addEditDeleteBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookingDateTime form = new BookingDateTime();
            form.ShowDialog();
            this.Close();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report form = new Report();
            form.ShowDialog();
            this.Close();
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"User Guide.docx");
        }

        private void tsbCodeComplexities_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Complexities.odt");
        }
    }
}
