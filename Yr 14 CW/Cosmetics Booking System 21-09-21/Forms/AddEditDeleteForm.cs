using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using Cosmetics_Booking_System_21_09_21.Objects;
using Cosmetics_Booking_System_21_09_21.DBAccess;

namespace Cosmetics_Booking_System_21_09_21.Forms
{
    public partial class AddEditDeleteForm : MasterForm
    {
        Database db;
        List<Customer> CustomerList = new List<Customer>();
        List<string> customersForlb = new List<string>();
        CustomerDBAccess cdba;
        DateTime dateEdit, dateAdd;
        bool txtAddTelNoBlank = false, txtAddForenameBlank = false, txtAddSurnameBlank = false, txtAddAddressBlank = false, txtAddTownBlank = false, txtAddEmailBlank = false,
            txtForenameBlank = false, txtSurnameBlank = false, txtTelNoBlank = false, txtEmailBlank = false, txtTownBlank = false, txtAddressBlank = false, txtPostcodeValid = false,
            txtAddPostcodeValid = false, btnEditEnabled = true, btnAddEnabled = true, editCheck1 =false, editCheck2 = false, editCheck3 = false;
        string forename, surname, town, address;
        public AddEditDeleteForm()
        {
            InitializeComponent();
            db = new Database();
            if (db.connect()) //Testing database connection
            {
            }
            else
            {
                MessageBox.Show("Database Connection Unsuccessful.", "Error");
                Application.Exit();
            }
            cdba = new CustomerDBAccess(db);
            populatelb();
            calAdd.MaxDate = DateTime.Today.AddYears(-13);
            calAdd.MinDate = DateTime.Today.AddYears(-123);
            calEdit.MaxDate = DateTime.Today.AddYears(-13);
            calEdit.MinDate = DateTime.Today.AddYears(-123);
        }

        private void lbCustomers_SelectedIndexChanged(object sender, EventArgs e) //Updates edit elements with selected customer data
        {
            lblCustomerID.Text = "";
            txtForename.Text = "";
            txtSurname.Text = "";
            txtTelNo.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtTown.Text = "";
            txtPostcode.Text = "";


            lblCustomerID.Text = Convert.ToString(getIDFromString(lbCustomers.GetItemText(lbCustomers.SelectedItem)));
            foreach (Customer customer in CustomerList)
            {
                if (Convert.ToInt32(lblCustomerID.Text) == customer.CustomerID)
                {
                    txtForename.Text = customer.CustomerForename.Trim();
                    txtSurname.Text = customer.CustomerSurname.Trim();
                    txtTelNo.Text = customer.CustomerTelNo.Trim();
                    txtEmail.Text = customer.CustomerEmail.Trim();
                    txtAddress.Text = customer.CustomerAddress.Trim();
                    txtTown.Text = customer.CustomerTown.Trim();
                    txtPostcode.Text = customer.CustomerPostcode.Trim();
                    calEdit.SelectionStart = customer.CustomerDateOfBirth;
                    calEdit.SelectionEnd = customer.CustomerDateOfBirth;
                    dateEdit = customer.CustomerDateOfBirth;
                }
            }
            int getIDFromString(string String)
            {
                int returnID = Convert.ToInt32(Regex.Replace(String, @"[^\d]", ""));

                return returnID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) //Will enable the edit section or save customer data if edits are made
        {
            if(btnEditEnabled == true)
            {
                edit();
            }
            else
            {
                CustomerDBAccess cdba = new CustomerDBAccess(db);
                DialogResult mb = MessageBox.Show("Are you sure you want to save?", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (mb == DialogResult.Yes)
                {
                    foreach (Customer customer in CustomerList)
                    {
                        if (customer.CustomerID.ToString() == lblCustomerID.Text.ToString())
                        {
                            forename = txtForename.Text;
                            surname = txtSurname.Text;
                            town = txtTown.Text;
                            address = txtAddress.Text;
                            customer.CustomerForename = Convert.ToString(char.ToUpper(forename[0]) + forename.Substring(1));
                            customer.CustomerSurname = Convert.ToString(char.ToUpper(surname[0]) + surname.Substring(1));
                            customer.CustomerAddress = Regex.Replace(address, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                            customer.CustomerTelNo = Convert.ToString(txtTelNo.Text);
                            customer.CustomerDateOfBirth = Convert.ToDateTime(calEdit.SelectionRange.Start);
                            customer.CustomerArchive = 0;
                            customer.CustomerTown = Convert.ToString(char.ToUpper(town[0]) + town.Substring(1));
                            customer.CustomerPostcode = txtPostcode.Text.ToUpper();
                            customer.CustomerEmail = txtEmail.Text;
                            customer.CustomerID = Convert.ToInt32(lblCustomerID.Text);
                            cdba.updateCustomer(customer);
                            this.Hide();
                            AddEditDeleteForm form = new AddEditDeleteForm();
                            form.ShowDialog();
                            this.Close();
                        }
                    }
                }
            }
        }
        public void populatelb() //Fills the list box with customers
        {
            CustomerList = cdba.getAllCustomers();
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

        private void btnDelete_Click(object sender, EventArgs e) //Will delete a selected customer
        {
            CustomerDBAccess cdba = new CustomerDBAccess(db);
            DialogResult mb = MessageBox.Show("Are you sure you want to delete this customer?\nAny future bookings for this customer will be deleted", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (mb == DialogResult.Yes)
            {
                foreach (Customer customer in CustomerList)
                {
                    if (customer.CustomerID.ToString() == lblCustomerID.Text.ToString())
                    {
                        customer.CustomerArchive = 1;
                        cdba.updateCustomer(customer);
                    }
                }
                this.Hide();
                AddEditDeleteForm form = new AddEditDeleteForm();
                form.ShowDialog();
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e) //Returns to the cosmetics menu
        {
            foreach(Customer customer in CustomerList)
            {
                if(customer.CustomerID.ToString() == lblCustomerID.Text)
                {
                    if (txtAddForename.Text != "" || txtAddSurname.Text != "" || dateAdd != default(DateTime) || txtAddTelNo.Text != "" || txtAddEmail.Text != "" || txtAddAddress.Text != "" || txtAddTown.Text != "" || txtAddPostcode.Text != "" || customer.CustomerForename != txtForename.Text ||
                        customer.CustomerSurname != txtSurname.Text || customer.CustomerDateOfBirth != dateEdit || customer.CustomerTelNo != txtTelNo.Text || customer.CustomerEmail != txtEmail.Text || customer.CustomerAddress != txtAddress.Text || customer.CustomerTown != txtTown.Text || customer.CustomerPostcode != txtPostcode.Text)
                    {
                        DialogResult mb = MessageBox.Show("Are you sure you want to go back?\nUnsaved changes will be lost.", "Back", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (mb == DialogResult.Yes)
                        {
                            this.Hide();
                            CosmeticsMenu form = new CosmeticsMenu();
                            form.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        DialogResult mb = MessageBox.Show("Are you sure you want to go back?", "Back", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (mb == DialogResult.Yes)
                        {
                            this.Hide();
                            CosmeticsMenu form = new CosmeticsMenu();
                            form.ShowDialog();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) //Will enable the add section or add a customer
        {
            if (btnAddEnabled == true)
            {
                btnAddEnabled = false;
                btnAdd.Text = "Save";
                btnEditEnabled = true;
                btnSave.Text = "Edit";
                txtAddForename.Enabled = true;
                txtAddSurname.Enabled = true;
                txtAddTelNo.Enabled = true;
                txtAddEmail.Enabled = true;
                txtAddAddress.Enabled = true;
                txtAddTown.Enabled = true;
                txtAddPostcode.Enabled = true;
                calAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                txtForename.Enabled = false;
                txtSurname.Enabled = false;
                txtEmail.Enabled = false;
                txtTelNo.Enabled = false;
                txtAddress.Enabled = false;
                txtTown.Enabled = false;
                txtPostcode.Enabled = false;               
                calEdit.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                DialogResult mb = MessageBox.Show("Are you sure you want to add this customer?", "Add", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (mb == DialogResult.Yes)
                {
                    try
                    {
                        int highestID = 0;
                        foreach (Customer Cust in CustomerList)
                        {
                            if (Cust.CustomerID >= highestID)
                            {
                                highestID = Cust.CustomerID + 1;
                            }
                        }
                        CustomerDBAccess cdba = new CustomerDBAccess(db);
                        forename = txtAddForename.Text;
                        surname = txtAddSurname.Text;
                        town = txtAddTown.Text;
                        address = txtAddAddress.Text;
                        int CustomerID = highestID;
                        string Forename = char.ToUpper(forename[0]) + forename.Substring(1);
                        string Surname = char.ToUpper(surname[0]) + surname.Substring(1);
                        string Address = Regex.Replace(address, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                        string TelNo = txtAddTelNo.Text;
                        DateTime DateOfBirth = dateAdd;
                        string Town = char.ToUpper(town[0]) + town.Substring(1);
                        string Postcode = txtAddPostcode.Text.ToUpper();
                        string Email = txtAddEmail.Text;
                        int Archive = 0;
                        cdba.insertCustomer(CustomerID, Forename, Surname, Address, TelNo, DateOfBirth, Archive, Town, Postcode, Email);
                        this.Hide();
                        AddEditDeleteForm form = new AddEditDeleteForm();
                        form.ShowDialog();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An error occured");
                    }
                }
            }
        }
                

        private void calAdd_DateSelected(object sender, DateRangeEventArgs e)
        {
            dateAdd = calAdd.SelectionRange.Start;
            dateAdd.ToString("dd/MM/yyyy");
            if(dateAdd < DateTime.Today)
            {
                lblAddDOB.ForeColor = Color.Green;
            }
            else
            {
                lblAddDOB.ForeColor = Color.Red;
            }
        }

        private void calEdit_DateSelected(object sender, DateRangeEventArgs e)
        {
            dateEdit = calEdit.SelectionRange.Start;
            dateEdit.ToString("dd/MM/yyyy");
            if (dateEdit < DateTime.Today)
            {
                lblDOB.ForeColor = Color.Green;
            }
            else
            {
                lblDOB.ForeColor = Color.Red;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //Every tick these validations are cehcked
        {
            if (lblAddForename.ForeColor == Color.Green && lblAddSurname.ForeColor == Color.Green && txtAddTelNoBlank == true && txtAddEmailBlank == true && lblAddAddress.ForeColor == Color.Green && lblAddTown.ForeColor == Color.Green && txtAddPostcodeValid == true && dateAdd != Convert.ToDateTime("01/01/0001") && btnAddEnabled == false)
            {
                btnAdd.Enabled = true;
            }
            else if (btnAddEnabled == true)
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
            if (editCheck1 == true && editCheck2 ==true && editCheck3 == true)
            {
                btnSave.Enabled = true;
            }
            else if(btnEditEnabled == true)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
            if(txtAddForenameBlank == true && txtAddSurnameBlank == true && dateAdd < DateTime.Today && dateAdd > DateTime.MinValue)
            {
                pbCheck1.BackColor = Color.Green;
            }
            else
            {
                pbCheck1.BackColor = Color.Red;
            }
            if (txtAddTelNoBlank == true && txtAddEmailBlank == true)
            {
                pbCheck2.BackColor = Color.Green;
            }
            else
            {
                pbCheck2.BackColor = Color.Red;
            }
            if (txtAddAddressBlank == true && txtAddTownBlank == true && txtAddPostcodeValid == true)
            {
                pbCheck3.BackColor = Color.Green;
            }
            else
            {
                pbCheck3.BackColor = Color.Red;
            }
            if (txtTelNoBlank == true && txtEmailBlank == true)
            {
                pbEditCheck2.BackColor = Color.Green;
                editCheck2 = true;
            }
            else
            {
                pbEditCheck2.BackColor = Color.Red;
                editCheck2 = false;
            }
            if (txtAddressBlank == true && txtTownBlank == true && txtPostcodeValid == true)
            {
                pbEditCheck3.BackColor = Color.Green;
                editCheck3 = true;
            }
            else
            {
                pbEditCheck3.BackColor = Color.Red;
                editCheck3 = false;
            }
            if (txtForenameBlank == true && txtSurnameBlank == true)
            {
                pbEditCheck1.BackColor = Color.Green;
                editCheck1 = true;
            }
            else
            {
                pbEditCheck1.BackColor = Color.Red;
                editCheck1 = false;
            }
        }
        private void txtAddTelNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = false;
                }
            }

            else if (e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (txtAddTelNo.Text.Length >= 11)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
        private void txtTelNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = false;
                }
            }

            else if (e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (txtTelNo.Text.Length >= 11)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAddTelNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAddTelNo.Text.Length == 11)
            {
                txtAddTelNoBlank = true;
                lblAddTelNo.ForeColor = Color.Green;
            }
            else
            {
                txtAddTelNoBlank = false;
                lblAddTelNo.ForeColor = Color.Red;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            txtAddressBlank = blankCheck(txtAddress);
            if (txtAddressBlank == true)
            {
                lblAddress.ForeColor = Color.Green;
            }
            else
            {
                lblAddress.ForeColor = Color.Red;
            }
        }

        private void txtAddForename_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoNumbers(e);
        }

        private void txtForename_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoNumbers(e);
        }

        private void txtAddSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoNumbers(e);
        }

        private void txtAddTown_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoNumbers(e);
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoNumbers(e);
        }

        private void txtTown_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoNumbers(e);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e) //Search functionality
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

        private void btnCancel_Click(object sender, EventArgs e) //Undos any edits and disables the edit section
        {
            foreach (Customer customer in CustomerList)
            {
                if (Convert.ToInt32(lblCustomerID.Text) == customer.CustomerID)
                {
                    txtForename.Text = customer.CustomerForename.Trim();
                    txtSurname.Text = customer.CustomerSurname.Trim();
                    txtTelNo.Text = customer.CustomerTelNo.Trim();
                    txtEmail.Text = customer.CustomerEmail.Trim();
                    txtAddress.Text = customer.CustomerAddress.Trim();
                    txtTown.Text = customer.CustomerTown.Trim();
                    txtPostcode.Text = customer.CustomerPostcode.Trim();
                    calEdit.SelectionStart = customer.CustomerDateOfBirth;
                    calEdit.SelectionEnd = customer.CustomerDateOfBirth;
                    dateEdit = customer.CustomerDateOfBirth;
                }
            }
            btnSave.Text = "Edit";
            btnEditEnabled = true;
            btnDelete.Enabled = false;
            txtForename.Enabled = false;
            txtSurname.Enabled = false;
            txtEmail.Enabled = false;
            txtTelNo.Enabled = false;
            txtAddress.Enabled = false;
            txtTown.Enabled = false;
            txtPostcode.Enabled = false;
            calEdit.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void txtAddPostcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            postcodeValidation(txtAddPostcode, e);
        }

        private void txtAddPostcode_TextChanged(object sender, EventArgs e) //Only allowing valid postcodes to be entered
        {
            if (txtAddPostcode.Text.Length >= 5 && txtAddPostcode.Text.Length <= 7)
            {
                txtAddPostcodeValid = true;
                lblAddPostcodeLength.ForeColor = Color.Green;
            }
            else
            {
                txtAddPostcodeValid = false;
                lblAddPostcodeLength.ForeColor = Color.Red;
            }
            if (txtAddPostcode.Text.Length >= 3)
            {
                lblAddPostcode.ForeColor = Color.Green;
            }
            else if(txtAddPostcode.Text.Length < 3)
            {
                lblAddPostcode.ForeColor = Color.Red;
            }
        }

        private void txtAddAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoInvalidChars(e);
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNoInvalidChars(e);
        }
        private void txtForename_TextChanged(object sender, EventArgs e)
        {
            txtForenameBlank = blankCheck(txtForename);
            if (txtForenameBlank == true)
            {
                lblForename.ForeColor = Color.Green;
            }
            else
            {
                lblForename.ForeColor = Color.Red;
            }
        }
        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            txtSurnameBlank = blankCheck(txtSurname);
            if (txtSurnameBlank == true)
            {
                lblSurname.ForeColor = Color.Green;
            }
            else
            {
                lblSurname.ForeColor = Color.Red;
            }
        }

        private void txtPostcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            postcodeValidation(txtPostcode, e);
        }

        private void txtTelNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTelNo.Text.Length == 11)
            {
                txtTelNoBlank = true;
                lblTelNo.ForeColor = Color.Green;
            }
            else
            {
                txtTelNoBlank = false;
                lblTelNo.ForeColor = Color.Red;
            }
        }

        private void txtTown_TextChanged(object sender, EventArgs e)
        {
            txtTownBlank = blankCheck(txtTown);
            if (txtTownBlank == true)
            {
                lblTown.ForeColor = Color.Green;
            }
            else
            {
                lblTown.ForeColor = Color.Red;
            }
        }

        private void txtPostcode_TextChanged(object sender, EventArgs e)
        {
            if (txtPostcode.Text.Length >= 5 && txtPostcode.Text.Length <= 7)
            {
                txtPostcodeValid = true;
                lblPostcodeLength.ForeColor = Color.Green;
            }
            else
            {
                txtPostcodeValid = false;
                lblPostcodeLength.ForeColor = Color.Red;
            }
            if (txtPostcode.Text.Length >= 3)
            {
                lblPostcode.ForeColor = Color.Green;
            }
            else if (txtPostcode.Text.Length < 3)
            {
                lblPostcode.ForeColor = Color.Red;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmailBlank = IsValidEmail(txtEmail.Text);
            if (txtEmailBlank == true)
            {
                lblEmail.ForeColor = Color.Green;
            }
            else
            {
                lblEmail.ForeColor = Color.Red;
            }
        }
        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            txtEmailBlank = IsValidEmail(txtEmail.Text);
        }
        private void txtAddEmail_KeyUp(object sender, KeyEventArgs e)
        {
            txtAddEmailBlank = IsValidEmail(txtAddEmail.Text);
            if (txtAddEmailBlank == true)
            {
                lblAddEmail.ForeColor = Color.Green;
            }
            else
            {
                lblAddEmail.ForeColor = Color.Red;
            }
        }

        private void txtAddTown_KeyUp(object sender, KeyEventArgs e)
        {
            txtAddTownBlank = blankCheck(txtAddTown);
            if (txtAddTownBlank == true)
            {
                lblAddTown.ForeColor = Color.Green;
            }
            else
            {
                lblAddTown.ForeColor = Color.Red;
            }
        }

        private void txtAddAddress_KeyUp(object sender, KeyEventArgs e)
        {
            txtAddAddressBlank = blankCheck(txtAddAddress);
            if (txtAddAddressBlank == true)
            {
                lblAddAddress.ForeColor = Color.Green;
            }
            else
            {
                lblAddAddress.ForeColor = Color.Red;
            }
        }

        private void txtAddSurname_KeyUp(object sender, KeyEventArgs e)
        {
            txtAddSurnameBlank = blankCheck(txtAddSurname);
            if (txtAddSurnameBlank == true)
            {
                lblAddSurname.ForeColor = Color.Green;
            }
            else
            {
                lblAddSurname.ForeColor = Color.Red;
            }
        }

        private void txtAddForename_KeyUp(object sender, KeyEventArgs e)
        {
            txtAddForenameBlank = blankCheck(txtAddForename);
            if (txtAddForenameBlank == true)
            {
                lblAddForename.ForeColor = Color.Green;
            }
            else
            {
                lblAddForename.ForeColor = Color.Red;
            }
        }

        private bool blankCheck(TextBox txt) //Checks for null values and to stop strings ending with "." or "-"
        {
            string text = txt.Text.Trim();
            Boolean result = false;
            if (text != "")
            {
                result = char.IsLetter(text[text.Length - 1]);
                if(text.Length > 1 && result != true)
                {
                    result = text.ToLower().Trim().EndsWith("jr.");
                }
            }
            bool flag;
            if (text != "" && result == true)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        private void txtNoNumbers(KeyPressEventArgs e) //Prevents numbers and allows "." and "-"
        {
            if (e.KeyChar == Convert.ToChar("-") || e.KeyChar == Convert.ToChar("."))
            {
                e.Handled = false;
            }
            else if(Char.IsDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        private void txtNoInvalidChars(KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar("-") || e.KeyChar == Convert.ToChar("."))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar) || Char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.Contains("@"))
            {
                if (trimmedEmail.EndsWith(".com") || trimmedEmail.EndsWith(".co.uk"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void postcodeValidation(TextBox txt, KeyPressEventArgs e) //Validates a postcode
        {
            if (txt.Text.Length < 2)
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = false;
                    }
                }

                else if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else if (txt.Text.Length == 2)
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = false;
                    }
                }

                else if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else if (txt.Text.Length >= 3 && txt.Text.Length < 7)
            {
                if (Char.IsDigit(e.KeyChar) || Char.IsLetter(e.KeyChar))
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = false;
                    }
                }

                else if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            if (txt.Text.Length >= 7)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
        private void add()
        {
            btnAddEnabled = false;
            btnEditEnabled = true;
            btnAdd.Text = "Save";
            btnSave.Text = "Edit";
            txtAddForename.Enabled = true;
            txtAddSurname.Enabled = true;
            txtAddTelNo.Enabled = true;
            txtAddEmail.Enabled = true;
            txtAddAddress.Enabled = true;
            txtAddTown.Enabled = true;
            txtAddPostcode.Enabled = true;
            calAdd.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            txtForename.Enabled = false;
            txtSurname.Enabled = false;
            txtEmail.Enabled = false;
            txtTelNo.Enabled = false;
            txtAddress.Enabled = false;
            txtTown.Enabled = false;
            txtPostcode.Enabled = false;
            calEdit.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void edit()
        {
            btnAddEnabled = true;
            btnEditEnabled = false;
            txtAddForename.Enabled = false;
            txtAddSurname.Enabled = false;
            txtAddTelNo.Enabled = false;
            txtAddEmail.Enabled = false;
            txtAddAddress.Enabled = false;
            txtAddTown.Enabled = false;
            txtAddPostcode.Enabled = false;
            calAdd.Enabled = false;
            btnSave.Text = "Save";
            btnAdd.Text = "Add";
            btnDelete.Enabled = true;
            txtForename.Enabled = true;
            txtSurname.Enabled = true;
            txtEmail.Enabled = true;
            txtTelNo.Enabled = true;
            txtAddress.Enabled = true;
            txtTown.Enabled = true;
            txtPostcode.Enabled = true;
            txtSearch.Enabled = true;
            lbCustomers.Enabled = true;
            calEdit.Enabled = true;
            btnCancel.Enabled = true;
        }
    }
}
