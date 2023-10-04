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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace Cosmetics_Booking_System_21_09_21.Forms
{
    public partial class Report : Cosmetics_Booking_System_21_09_21.Forms.MasterForm
    {
        Database db;
        DataTable dt = new DataTable();
        List<Customer> CustomerList = new List<Customer>();
        int customerID;
        DateTime start, end;
        public Report()
        {
            InitializeComponent();
            start = this.dateStart.Value.Date;
            end = this.dateEnd.Value.Date;
            db = new Database();
            if (db.connect())
            {
            }
            else
            {
                MessageBox.Show("Database Connection Unsuccessful.", "Error");
                Application.Exit();
            }
            CustomerDBAccess cdba = new CustomerDBAccess(db);
            CustomerList = cdba.getAllCustomers();
            populatelb();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Treatment' table. You can move, or remove it, as needed.
            this.treatmentTableAdapter.Fill(this.dataSet1.Treatment,start,end, customerID);
            // TODO: This line of code loads data into the 'dataSet1.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.dataSet1.Employees,customerID, start, end);

  
            this.reportViewer1.RefreshReport();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form = new MainMenu();
            form.ShowDialog();
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }
        public void populatelb()
        {
            DataTable displayCustomer = new DataTable();

            displayCustomer.Columns.Add("Cust Id");
            displayCustomer.Columns.Add("Customer");

            for (int i = 0; i <= CustomerList.Count - 1; i++)
            {
                if (CustomerList[i].CustomerArchive != 1)
                {
                    displayCustomer.Rows.Add(CustomerList[i].CustomerID, CustomerList[i].CustomerID + " | " + CustomerList[i].CustomerForename + " " + CustomerList[i].CustomerSurname);
                }

            }
            lbCustomers.ValueMember = "Cust Id";
            lbCustomers.DisplayMember = "Customer";
            lbCustomers.DataSource = displayCustomer;
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            end = this.dateEnd.Value.Date;
            this.reportViewer1.RefreshReport();
            this.treatmentTableAdapter.Fill(this.dataSet1.Treatment, start, end, customerID);
            this.employeesTableAdapter.Fill(this.dataSet1.Employees, customerID, start, end);
        }

        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            start = this.dateStart.Value.Date;
            this.reportViewer1.RefreshReport();
            this.treatmentTableAdapter.Fill(this.dataSet1.Treatment, start, end, customerID);
            this.employeesTableAdapter.Fill(this.dataSet1.Employees, customerID, start, end);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)//Search functionality
        {

            DataTable displayCustomer = new DataTable();
            displayCustomer.Columns.Add("Cust Id");
            displayCustomer.Columns.Add("Customer");
            for (int i = 0; i <= CustomerList.Count - 1; i++)
            {
                if (CustomerList[i].CustomerForename.ToLower().Contains(txtSearch.Text.ToLower()) || CustomerList[i].CustomerSurname.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    CustomerList[i].CustomerID.ToString().Contains(txtSearch.Text.ToLower()) || (CustomerList[i].CustomerForename.ToLower() + " " + CustomerList[i].CustomerSurname.ToLower()).Contains(txtSearch.Text.ToLower()))
                {
                    if (CustomerList[i].CustomerArchive != 1)
                    {
                        displayCustomer.Rows.Add(CustomerList[i].CustomerID, CustomerList[i].CustomerID + " | " + CustomerList[i].CustomerForename + " " + CustomerList[i].CustomerSurname);
                    }
                }
            }
            lbCustomers.ValueMember = "Cust Id";
            lbCustomers.DisplayMember = "Customer";
            lbCustomers.DataSource = displayCustomer;
            if (txtSearch.Text == "")
            {
                populatelb();
            }
        }

        private void lbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID = getIDFromString(lbCustomers.GetItemText(lbCustomers.SelectedItem));
            int getIDFromString(string String)
            {
                int returnID = Convert.ToInt32(Regex.Replace(String, @"[^\d]", ""));

                return returnID;
            }
            this.reportViewer1.RefreshReport();
            this.treatmentTableAdapter.Fill(this.dataSet1.Treatment, start, end, customerID);
            this.employeesTableAdapter.Fill(this.dataSet1.Employees, customerID, start, end);
        }
    }
}
