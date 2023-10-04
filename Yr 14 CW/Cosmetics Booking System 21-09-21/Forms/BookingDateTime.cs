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

namespace Cosmetics_Booking_System_21_09_21.Forms
{
    public partial class BookingDateTime : Cosmetics_Booking_System_21_09_21.Forms.MasterForm
    {
        Database db;
        DataTable table = new DataTable();
        List<Customer> CustomerList = new List<Customer>();
        List<Booking> BookingList = new List<Booking>();
        List<Treatment> TreatmentList = new List<Treatment>();
        List<BookingTreatment> BookingTreatmentList = new List<BookingTreatment>();
        List<Int32> treatments = new List<Int32>();
        int durationGot, costGot, cost, duration, columnIndex, customerID, roomNum, returnIndex, checkBoxTally, janeTally, jamesTally, julieTally, joanneTally, cellClickedJane = 99, cellClickedJames = 99,
            cellClickedJulie = 99, cellClickedJoanne = 99, therapist, bookingID, previousColumnIndex = 99, currentBookingArchive = 0;
        bool Hair = false, Nails = false, Makeup = false, HairDye = false, Waxing = false, LashesAndBrows = false, james = false, joanne = false, julie = false, jane = false,
            calDateFlag = false, friday = false, hairCutFlag = false, nailsFlag = false, waxingFlag = false, hairDyeFlag = false, lashesAndBrowsFlag = false, makeupFlag = false,
            bookingFlag = false, selected = false;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int listBox;
            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
            lbCustomers.SetSelected(listBox, true);
        }

        private void BookingDateTime_Load(object sender, EventArgs e)
        {
   
        }

        DataGridView dgv;
        string previousDGV;
        DateTime date, previousDate;
        public BookingDateTime()
        {
            InitializeComponent();
            previousDate = GlobalVariables.dateReturn;
            if (previousDate != DateTime.MinValue)
            {
                calDate.SelectionRange.Start = previousDate;
                calDate.SelectionStart = previousDate;
                date = previousDate;
                clearBooking();
                calDateFlag = true;
                dgvJames.Enabled = true;
                dgvJane.Enabled = true;
                dgvJulie.Enabled = true;
                dgvJoanne.Enabled = true;

                if (previousDate.DayOfWeek == DayOfWeek.Friday) //Ensures part-time staff shifts are considered
                {
                    friday = true;
                    clearBooking();
                }
                else
                {
                    friday = false;
                    clearBooking();
                }
            }
            db = new Database();
            updateDuration.Enabled = true;
            if (db.connect()) //Testing the data connection
            {
            }
            else
            {
                MessageBox.Show("Database Connection Unsuccessful.", "Error");
                Application.Exit();
            }
            TreatmentDBAccess tdba = new TreatmentDBAccess(db);
            CustomerDBAccess cdba = new CustomerDBAccess(db);
            BookingsDBAccess bdba = new BookingsDBAccess(db);
            BookingTreatmentDBAccess btdba = new BookingTreatmentDBAccess(db);
            CustomerList = cdba.getAllCustomers();
            TreatmentList = tdba.getAllTreatments();
            BookingList = bdba.getAllBookings();
            BookingTreatmentList = btdba.getAllBookingTreatments();
            if (GlobalVariables.editEnabled == true) //Checking which state the form should be in
            {
                lblBlue.Text = "Current Booking";
                lblRed.Text = "Another Booking";
                lblSelectCustomer.Text = "Select Booking";
                lblSearch.Visible = false;
                txtSearch.Visible = false;
                btnEdit.Text = "Enable Booking";
                btnDelete.Visible = true;
                btnCancel.Visible = true;
                populatelbEdit();
            }
            else
            {
                lblBlue.Text = "Current Customer's Booking";
                lblRed.Text = "Another Customer's Booking";
                lblSelectCustomer.Text = "Select Customer";
                lblSearch.Visible = true;
                txtSearch.Visible = true;
                btnEdit.Text = "Enable Editing";
                btnDelete.Visible = false;
                btnCancel.Visible = false;
                populatelb();
                calDate.MinDate = DateTime.Today.AddDays(0);
            }
            foreach (Booking booking in BookingList) //This will archive any past bookings and delete any future bookings of archived customers
            {
                foreach (Customer customer in CustomerList)
                {
                    if (customer.CustomerArchive == 1 && booking.DateTime < DateTime.Today && customer.CustomerID == booking.CustomerID)
                    {
                        booking.BookingID = booking.BookingID;
                        booking.Archive = 1;
                        bdba.archiveBooking(booking);
                        BookingList = bdba.getAllBookings();
                        CustomerList = cdba.getAllCustomers();
                    }
                    else if (customer.CustomerArchive == 1 && booking.DateTime >= DateTime.Today && customer.CustomerID == booking.CustomerID)
                    {
                        btdba.deleteBookingTreatment(booking.BookingID);
                        bdba.deleteBooking(booking.BookingID);
                        BookingList = bdba.getAllBookings();
                        CustomerList = cdba.getAllCustomers();
                    }
                }
                if (booking.DateTime < DateTime.Today)
                {
                    booking.BookingID = booking.BookingID;
                    booking.Archive = 1;
                    bdba.archiveBooking(booking);
                }
                if (booking.EmployeeID == 2 && booking.Archive != 1)
                {
                    jamesTally += 1;
                }
                else if (booking.EmployeeID == 1 && booking.Archive != 1)
                {
                    janeTally += 1;
                }
                else if (booking.EmployeeID == 4 && booking.Archive != 1)
                {
                    julieTally += 1;
                }
                else if (booking.EmployeeID == 3 && booking.Archive != 1)
                {
                    joanneTally += 1;
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            BookingsDBAccess bdba = new BookingsDBAccess(db);
            TreatmentDBAccess tdba = new TreatmentDBAccess(db);
            CustomerDBAccess cdba = new CustomerDBAccess(db);
            BookingTreatmentDBAccess btdba = new BookingTreatmentDBAccess(db);
            BookingList = bdba.getAllBookings();
            CustomerList = cdba.getAllCustomers();
            TreatmentList = tdba.getAllTreatments();
            BookingTreatmentList = btdba.getAllBookingTreatments();
            if (MessageBox.Show("Are you sure you want to delete this booking?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach(Booking booking in BookingList)
                {
                    btdba.deleteBookingTreatment(bookingID);
                    bdba.deleteBooking(bookingID);
                }
                this.Hide();
                BookingDateTime form = new BookingDateTime();
                form.ShowDialog();
                this.Close();
            }
        }
        private void dgvJane_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) //These will determine what cells are available for booking with each therapist
        {
            bookingInfo(dgvJane, 1, e);
        }

        private void dgvJames_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bookingInfo(dgvJames, 2, e);
        }

        private void dgvJoanne_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bookingInfo(dgvJoanne, 3, e);
        }

        private void dgvJulie_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bookingInfo(dgvJulie, 4, e);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e) //This is the search functionality
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

        private void btnBack_Click(object sender, EventArgs e) //Returns to the Cosmetics Menu
        {
            if (MessageBox.Show("Are you sure you want to back? Booking will be lost", "Back", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GlobalVariables.dateReturn = DateTime.MinValue;
                this.Hide();
                CosmeticsMenu form = new CosmeticsMenu();
                form.ShowDialog();
                this.Close();
            }
        }


        private void lbCustomers_SelectedIndexChanged(object sender, EventArgs e) //This will make all the checks for when a customer is changed
        {
            cbHairDye.Checked = false;
            cbHairCut.Checked = false;
            cbLashesAndBrows.Checked = false;
            cbWaxing.Checked = false;
            cbMakeup.Checked = false;
            cbNails.Checked = false;
            if (GlobalVariables.editEnabled == true)
            {
                bookingID = getIDFromString(lbCustomers.GetItemText(lbCustomers.SelectedItem));
                int getIDFromString(string String)
                {
                    int returnID = Convert.ToInt32(new string(String.TakeWhile(char.IsDigit).ToArray()));
                    return returnID;
                }
                foreach (Booking booking in BookingList)
                {
                    if (bookingID == booking.BookingID)
                    {
                        calDate.SetDate(booking.DateTime);
                        currentBookingArchive = booking.Archive;
                        foreach (BookingTreatment bookingTreatment in BookingTreatmentList) //So a customer cannot have duplicate treatments
                        {
                            if (bookingTreatment.BookingID == booking.BookingID)
                            {
                                if (bookingTreatment.TreatmentID == 1)
                                {
                                    cbHairCut.Checked = true;
                                }
                                else if (bookingTreatment.TreatmentID == 2)
                                {
                                    cbNails.Checked = true;
                                }
                                else if (bookingTreatment.TreatmentID == 3)
                                {
                                    cbMakeup.Checked = true;
                                }
                                else if (bookingTreatment.TreatmentID == 4)
                                {
                                    cbHairDye.Checked = true;
                                }
                                else if (bookingTreatment.TreatmentID == 5)
                                {
                                    cbWaxing.Checked = true;
                                }
                                else if (bookingTreatment.TreatmentID == 6)
                                {
                                    cbLashesAndBrows.Checked = true;
                                }
                            }
                        }
                        
                        previousDGV = null;
                        date = booking.DateTime;
                        

                        
                        customerID = booking.CustomerID;
                        calDateFlag = true;
                        if (booking.Archive == 1)
                        {
                            cbHairCut.Enabled = false;
                            cbHairDye.Enabled = false;
                            cbLashesAndBrows.Enabled = false;
                            cbMakeup.Enabled = false;
                            cbWaxing.Enabled = false;
                            cbNails.Enabled = false;
                            dgvJames.Enabled = true;
                            dgvJane.Enabled = true;
                            dgvJulie.Enabled = true;
                            dgvJoanne.Enabled = true;
                            btnDelete.Enabled = false;
                        }
                        else
                        {
                            dgvJames.Enabled = true;
                            dgvJane.Enabled = true;
                            dgvJulie.Enabled = true;
                            dgvJoanne.Enabled = true;
                            btnDelete.Enabled = true;
                            clearComboBox();
                        }
                        cellClickedJoanne = 99;
                        cellClickedJames = 99;
                        cellClickedJane = 99;
                        cellClickedJulie = 99;
                        clearBooking();
                        if (date.DayOfWeek == DayOfWeek.Friday)
                        {
                            friday = true;
                            clearBooking();
                        }
                        else
                        {
                            friday = false;
                            clearBooking();
                        }
                        var data = checkboxChecked(cbHairCut, Hair, 1, returnIndex, duration, cost, bookingID);
                        Hair = data.Item1;
                        duration = data.Item2;
                        cost = data.Item3;
                        columnIndex = data.Item4;
                        var data2 = checkboxUnchecked(cbHairCut, Hair, 1, returnIndex, duration, cost);
                        Hair = data2.Item1;
                        duration = data2.Item2;
                        cost = data2.Item3;
                        columnIndex = data2.Item4;
                        var data3 = checkboxChecked(cbNails, Nails, 2, returnIndex, duration, cost, bookingID);
                        Nails = data3.Item1;
                        duration = data3.Item2;
                        cost = data3.Item3;
                        columnIndex = data3.Item4;
                        var data4 = checkboxUnchecked(cbNails, Nails, 2, returnIndex, duration, cost);
                        Nails = data4.Item1;
                        duration = data4.Item2;
                        cost = data4.Item3;
                        columnIndex = data4.Item4;
                        var data5 = checkboxChecked(cbMakeup, Makeup, 3, returnIndex, duration, cost, bookingID);
                        Makeup = data5.Item1;
                        duration = data5.Item2;
                        cost = data5.Item3;
                        columnIndex = data5.Item4;
                        var data6 = checkboxUnchecked(cbMakeup, Makeup, 3, returnIndex, duration, cost);
                        Makeup = data6.Item1;
                        duration = data6.Item2;
                        cost = data6.Item3;
                        columnIndex = data6.Item4;
                        var data7 = checkboxChecked(cbHairDye, HairDye, 4, returnIndex, duration, cost, bookingID);
                        HairDye = data7.Item1;
                        duration = data7.Item2;
                        cost = data7.Item3;
                        columnIndex = data7.Item4;
                        var data8 = checkboxUnchecked(cbHairDye, HairDye, 4, returnIndex, duration, cost);
                        HairDye = data8.Item1;
                        duration = data8.Item2;
                        cost = data8.Item3;
                        columnIndex = data8.Item4;
                        var data9 = checkboxChecked(cbWaxing, Waxing, 5, returnIndex, duration, cost, bookingID);
                        Waxing = data9.Item1;
                        duration = data9.Item2;
                        cost = data9.Item3;
                        columnIndex = data9.Item4;
                        var data10 = checkboxUnchecked(cbWaxing, Waxing, 5, returnIndex, duration, cost);
                        Waxing = data10.Item1;
                        duration = data10.Item2;
                        cost = data10.Item3;
                        columnIndex = data10.Item4;
                        var data11 = checkboxChecked(cbLashesAndBrows, LashesAndBrows, 6, returnIndex, duration, cost, bookingID);
                        LashesAndBrows = data11.Item1;
                        duration = data11.Item2;
                        cost = data11.Item3;
                        columnIndex = data11.Item4;
                        var data12 = checkboxUnchecked(cbLashesAndBrows, LashesAndBrows, 6, returnIndex, duration, cost);
                        LashesAndBrows = data12.Item1;
                        duration = data12.Item2;
                        cost = data12.Item3;
                        columnIndex = data12.Item4;
                        columnIndex = booking.ColumnIndex;
                        if (booking.EmployeeID == 1)
                        {
                            jane = true;
                        }
                        else if (booking.EmployeeID == 2)
                        {
                            james = true;
                        }
                        else if (booking.EmployeeID == 3)
                        {
                            joanne = true;
                        }
                        else if (booking.EmployeeID == 4)
                        {
                            julie = true;
                        }
                        if(booking.EmployeeID == 1)
                        {
                            therapist = 1;
                            cbHairCut.Enabled = false;
                            cbHairDye.Enabled = false;
                            cbLashesAndBrows.Enabled = false;
                            cbMakeup.Enabled = false;
                            cbWaxing.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                clearComboBox();
                customerID = getIDFromString(lbCustomers.GetItemText(lbCustomers.SelectedItem));
                int getIDFromString(string String)
                {
                    int returnID = Convert.ToInt32(Regex.Replace(String, @"[^\d]", ""));

                    return returnID;
                }
            }
        }


        private void dgvJames_CellClick(object sender, DataGridViewCellEventArgs e) //James click event
        {

            if (GlobalVariables.editEnabled == true && currentBookingArchive == 1)
            {
                returnIndex = returnColumnIndex(dgvJames);
                foreach(Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 2)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                        }
                    }
                }
            }
            else
            {
                therapist = 0;
                returnIndex = returnColumnIndex(dgvJames);
                clearBooking();
                bool temp = false;
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 2 && GlobalVariables.editEnabled == true && booking.BookingID != bookingID)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            therapist = 2;
                            selected = true;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                            temp = true;

                            return;
                        }
                    }
                }
                if (temp == true)
                {
                    return;
                }
                selected = false;
                if (GlobalVariables.editEnabled == true && date < DateTime.Today)
                {
                    MessageBox.Show("Cannot edit the date to be in the past");
                    clearBooking();
                    returnIndex = -1;
                    therapist = 0;
                    cellClickedJames = 99;
                }
                else
                {


                    if (duration == 0)
                    {
                        columnIndex = 99;
                    }
                    else
                    {
                        if ((returnIndex + (duration / 15)) <= 32)
                        {
                            if (returnIndex == cellClickedJames)
                            {
                                clearBooking();
                                cellClickedJames = 99;
                            }
                            else
                            {
                                cellClickedJane = 99;
                                cellClickedJoanne = 99;
                                cellClickedJulie = 99;
                                foreach (Booking booking in BookingList)
                                {
                                    if (booking.CustomerID == customerID && booking.DateTime == date && booking.BookingID != bookingID)
                                    {
                                        if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                        {
                                            MessageBox.Show("Customer already has a booking at this time");
                                            clearBooking();
                                            returnIndex = -1;
                                            therapist = 0;
                                            cellClickedJames = 99;
                                            break;
                                        }
                                    }
                                    else if (jamesTally < 1)
                                    {
                                        cellClickedJames = returnIndex;
                                        previousColumnIndex = returnIndex;
                                        clearBooking();
                                        james = true;
                                        previousDGV = "james";
                                        therapist = 2;
                                    }
                                    if (booking.DateTime == date && booking.EmployeeID == 2 && booking.BookingID != bookingID)
                                    {
                                        if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                        {
                                            try
                                            {
                                                clearBooking();
                                                returnIndex = -1;
                                                therapist = 0;
                                                cellClickedJames = 99;
                                                previousColumnIndex = 99;
                                                throw new ExistingBooking("This booking will overlap with another booking.\nPlease choose a new time");
                                            }
                                            catch (ExistingBooking j)
                                            {
                                                j.BookingOverlap();
                                                clearBooking();
                                                returnIndex = -1;
                                                therapist = 0;
                                                cellClickedJames = 99;
                                                previousColumnIndex = 99;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            cellClickedJames = returnIndex;
                                            returnIndex = columnIndex;
                                            clearBooking();
                                            james = true;
                                            previousDGV = "james";
                                            therapist = 2;
                                            roomNum = 2;
                                        }
                                    }
                                    else if (jamesTally >= 1)
                                    {
                                        cellClickedJames = returnIndex;
                                        previousColumnIndex = returnIndex;
                                        clearBooking();
                                        james = true;
                                        previousDGV = "james";
                                        therapist = 2;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("James will finish his shift before this booking would finish.\nPlease book an earlier time.");
                        }
                    }
                }
            }
        }

        private void dgvJoanne_CellClick(object sender, DataGridViewCellEventArgs e) //Joanne click event
        {
            if (GlobalVariables.editEnabled == true && currentBookingArchive == 1)
            {
                returnIndex = returnColumnIndex(dgvJoanne);
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 3)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                        }
                    }
                }
            }
            else
            {
                therapist = 0;
                returnIndex = returnColumnIndex(dgvJoanne);
                clearBooking();
                bool temp = false;
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 3 && GlobalVariables.editEnabled == true && booking.BookingID != bookingID)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            therapist = 3;
                            selected = true;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                            temp = true;
                            return;
                        }
                    }
                }
                if (temp == true)
                {
                    return;
                }
                selected = false;
                if (GlobalVariables.editEnabled == true && date < DateTime.Today)
                {
                    MessageBox.Show("Cannot edit the date to be in the past");
                    clearBooking();
                    returnIndex = -1;
                    therapist = 0;
                    cellClickedJoanne = 99;
                }
                else
                {


                    if (duration == 0)
                    {
                        columnIndex = 99;
                    }
                    else
                    {
                        if (friday == true)
                        {
                            MessageBox.Show("Joanne doesn't work on fridays");
                        }
                        else
                        {
                            if (returnIndex == cellClickedJoanne)
                            {
                                clearBooking();
                                cellClickedJoanne = 99;
                            }
                            else
                            {
                                cellClickedJane = 99;
                                cellClickedJames = 99;
                                cellClickedJulie = 99;
                                if (returnIndex <= 16 && (returnIndex + (duration / 15)) <= 16)
                                {
                                    foreach (Booking booking in BookingList)
                                    {
                                        if (booking.CustomerID == customerID && booking.DateTime == date && booking.BookingID != bookingID)
                                        {
                                            if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                            {
                                                MessageBox.Show("Customer already has a booking at this time");
                                                clearBooking();
                                                returnIndex = -1;
                                                therapist = 0;
                                                cellClickedJoanne = 99;
                                                break;
                                            }
                                        }
                                        else if (joanneTally < 1)
                                        {
                                            cellClickedJoanne = returnIndex;
                                            previousColumnIndex = returnIndex;
                                            clearBooking();
                                            joanne = true;
                                            previousDGV = "joanne";
                                            therapist = 3;
                                        }
                                        if (booking.DateTime == date && booking.EmployeeID == 3 && booking.BookingID != bookingID)
                                        {
                                            if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                            {
                                                try
                                                {
                                                    clearBooking();
                                                    returnIndex = -1;
                                                    therapist = 0;
                                                    cellClickedJoanne = 99;
                                                    previousColumnIndex = 99;
                                                    throw new ExistingBooking("This booking will overlap with another booking.\nPlease choose a new time");
                                                }
                                                catch (ExistingBooking j)
                                                {
                                                    j.BookingOverlap();
                                                    clearBooking();
                                                    returnIndex = -1;
                                                    therapist = 0;
                                                    cellClickedJoanne = 99;
                                                    previousColumnIndex = 99;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                cellClickedJoanne = returnIndex;
                                                returnIndex = columnIndex;
                                                previousColumnIndex = returnIndex;
                                                clearBooking();
                                                joanne = true;
                                                previousDGV = "joanne";
                                                therapist = 3;
                                                roomNum = 3;
                                            }
                                        }
                                        else if (joanneTally >= 1)
                                        {
                                            cellClickedJoanne = returnIndex;
                                            previousColumnIndex = returnIndex;
                                            clearBooking();
                                            joanne = true;
                                            previousDGV = "joanne";
                                            therapist = 3;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Joanne will finish her shift before this booking would finish.\nPlease book an earlier time.");
                                }
                            }
                        }
                    }
                }
            }
        }
        private void dgvJulie_CellClick(object sender, DataGridViewCellEventArgs e) //Julie click event
        {
            if (GlobalVariables.editEnabled == true && currentBookingArchive == 1)
            {
                returnIndex = returnColumnIndex(dgvJulie);
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 4)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                        }
                    }
                }
            }
            else
            {
                therapist = 0;
                returnIndex = returnColumnIndex(dgvJulie);
                clearBooking();
                bool temp = false;
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 4 && GlobalVariables.editEnabled == true && booking.BookingID != bookingID)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            therapist = 4;
                            selected = true;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                            temp = true;
                            return;
                        }
                    }
                }
                if (temp == true)
                {
                    return;
                }
                selected = false;
                if (GlobalVariables.editEnabled == true && date < DateTime.Today)
                {
                    MessageBox.Show("Cannot edit the date to be in the past");
                    clearBooking();
                    returnIndex = -1;
                    therapist = 0;
                    cellClickedJulie = 99;
                }
                else
                {
                    if (duration == 0)
                    {
                        columnIndex = 99;
                    }
                    else
                    {
                        cellClickedJane = 99;
                        cellClickedJames = 99;
                        cellClickedJoanne = 99;
                        if (friday == true)
                        {
                            MessageBox.Show("Julie doesn't work fridays");
                        }
                        else
                        {
                            if (returnIndex == cellClickedJulie)
                            {
                                clearBooking();
                                cellClickedJulie = 99;
                            }
                            else
                            {
                                if (returnIndex >= 16 && (returnIndex + (duration / 15)) <= 32)
                                {
                                    foreach (Booking booking in BookingList)
                                    {
                                        if (booking.CustomerID == customerID && booking.DateTime == date && booking.BookingID != bookingID)
                                        {
                                            if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                            {
                                                MessageBox.Show("Customer already has a booking at this time");
                                                clearBooking();
                                                returnIndex = -1;
                                                therapist = 0;
                                                break;
                                            }
                                        }
                                        else if (julieTally < 1)
                                        {
                                            cellClickedJulie = returnIndex;
                                            previousColumnIndex = returnIndex;
                                            clearBooking();
                                            julie = true;
                                            previousDGV = "julie";
                                            therapist = 4;
                                        }
                                        else
                                        {
                                            if (booking.DateTime == date && booking.EmployeeID == 4 && booking.BookingID != bookingID)
                                            {
                                                if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                                {
                                                    try
                                                    {
                                                        clearBooking();
                                                        returnIndex = -1;
                                                        therapist = 0;
                                                        previousColumnIndex = 99;
                                                        cellClickedJulie = 99;
                                                        throw new ExistingBooking("This booking will overlap with another booking.\nPlease choose a new time");
                                                    }
                                                    catch (ExistingBooking j)
                                                    {
                                                        j.BookingOverlap();
                                                        clearBooking();
                                                        returnIndex = -1;
                                                        therapist = 0;
                                                        cellClickedJulie = 99;
                                                        previousColumnIndex = 99;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    cellClickedJulie = returnIndex;
                                                    returnIndex = columnIndex;
                                                    previousColumnIndex = returnIndex;
                                                    clearBooking();
                                                    julie = true;
                                                    previousDGV = "julie";
                                                    therapist = 4;
                                                    roomNum = 4;
                                                }
                                            }
                                            else if (julieTally >= 1)
                                            {
                                                cellClickedJulie = returnIndex;
                                                previousColumnIndex = returnIndex;
                                                clearBooking();
                                                julie = true;
                                                previousDGV = "julie";
                                                therapist = 4;
                                            }
                                        }
                                    }
                                }
                                else if (returnIndex < 16)
                                {
                                    MessageBox.Show("Julie will not have started her shift yet.\nPlease book a later time.");
                                }
                                else
                                {
                                    MessageBox.Show("Julie will finish her shift before this booking would finish.\nPlease book an earlier time.");
                                }
                            }
                        }
                    }
                }
            }
        }
        private void dgvJane_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GlobalVariables.editEnabled == true && currentBookingArchive == 1)
            {
                returnIndex = returnColumnIndex(dgvJane);
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 1)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                        }
                    }
                }
            }
            else
            {
                therapist = 0;
                returnIndex = returnColumnIndex(dgvJane);
                clearBooking();
                bool temp = false;
                foreach (Booking booking in BookingList)
                {
                    if (booking.DateTime == date && booking.EmployeeID == 1 && GlobalVariables.editEnabled == true && booking.BookingID != bookingID)
                    {
                        if (booking.ColumnIndex == returnIndex || returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) < 0 &&
                            returnIndex - (booking.ColumnIndex + (booking.Duration / 15)) > 0 - (booking.Duration / 15))
                        {
                            int listBox;
                            bookingID = booking.BookingID;
                            therapist = 1;
                            selected = true;
                            listBox = lbCustomers.FindString(Convert.ToString(bookingID));
                            lbCustomers.SetSelected(listBox, true);
                            temp = true;
                            return;
                        }
                    }
                }
                if (temp == true)
                {
                    return;
                }
                selected = false;
                if (GlobalVariables.editEnabled == true && date < DateTime.Today)
                {
                    MessageBox.Show("Cannot edit the date to be in the past");
                    clearBooking();
                    returnIndex = -1;
                    therapist = 0;
                    cellClickedJane = 99;
                }
                else
                {

                    if (duration == 0)
                    {
                        columnIndex = 99;
                    }
                    else
                    {
                        if (checkBoxTally == 1 && cbNails.Checked == true)
                        {
                            if (returnIndex == cellClickedJane)
                            {
                                clearBooking();
                                cellClickedJane = 99;
                            }
                            else
                            {
                                cellClickedJulie = 99;
                                cellClickedJames = 99;
                                cellClickedJoanne = 99;
                                if ((returnIndex + (duration / 15)) <= 32)
                                {
                                    foreach (Booking booking in BookingList)
                                    {
                                        if (booking.CustomerID == customerID && booking.DateTime == date && booking.BookingID != bookingID)
                                        {
                                            if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                            {
                                                MessageBox.Show("Customer already has a booking at this time");
                                                clearBooking();
                                                returnIndex = -1;
                                                therapist = 0;
                                                previousColumnIndex = 99;
                                                break;
                                            }
                                        }
                                        else if (janeTally < 1)
                                        {
                                            cellClickedJane = returnIndex;
                                            previousColumnIndex = returnIndex;
                                            clearBooking();
                                            jane = true;
                                            therapist = 1;
                                        }
                                        else
                                        {
                                            if (booking.DateTime == date && booking.EmployeeID == 1 && booking.BookingID != bookingID)
                                            {
                                                if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)))
                                                {
                                                    try
                                                    {
                                                        clearBooking();
                                                        returnIndex = -1;
                                                        therapist = 0;
                                                        previousColumnIndex = 99;
                                                        throw new ExistingBooking("This booking will overlap with another booking.\nPlease choose a new time");
                                                    }
                                                    catch (ExistingBooking j)
                                                    {
                                                        j.BookingOverlap();
                                                        clearBooking();
                                                        returnIndex = -1;
                                                        therapist = 0;
                                                        previousColumnIndex = 99;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    cellClickedJane = returnIndex;
                                                    returnIndex = columnIndex;
                                                    previousColumnIndex = returnIndex;
                                                    clearBooking();
                                                    jane = true;
                                                    therapist = 1;
                                                    roomNum = 1;
                                                }
                                            }
                                            else if (janeTally >= 1)
                                            {
                                                cellClickedJane = returnIndex;
                                                previousColumnIndex = returnIndex;
                                                clearBooking();
                                                jane = true;
                                                therapist = 1;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Jane will finish her shift before this booking would finish.\nPlease book an earlier time.");
                                    previousColumnIndex = 99;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Only nails can be booked for the nail bar");
                            clearBooking();
                            returnIndex = -1;
                            therapist = 0;
                            previousColumnIndex = 99;
                            cellClickedJulie = 99;
                            cellClickedJames = 99;
                            cellClickedJoanne = 99;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e) //Save button passing information to the confirmation form
        {
            if (date >= DateTime.Today || GlobalVariables.editEnabled == true && date != null)
            {
                bool validFlag = false;
                if (returnIndex != -1)
                {
                    validFlag = true;
                }
                else
                {
                    validFlag = false;
                }
                if (validFlag == true)
                {
                    Confirmation form = new Confirmation(customerID, therapist, roomNum, date, duration, returnIndex, treatments, bookingID);
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select an avaialble therapist and time", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a date", "Error");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            bookingID = 0;
            if (GlobalVariables.editEnabled == false)
            {
                GlobalVariables.editEnabled = true;
                this.Hide();
                BookingDateTime form = new BookingDateTime();
                form.ShowDialog();
                this.Close();
            }
            else
            {
                GlobalVariables.editEnabled = false;
                this.Hide();
                BookingDateTime form = new BookingDateTime();
                form.ShowDialog();
                this.Close();
            }
        }


        private void calDate_DateSelected(object sender, DateRangeEventArgs e) //Runs all updates to GUI needed if date is changed
        {
            DateTime today = DateTime.Today;
            date = calDate.SelectionRange.Start;
            previousDGV = null;
            clearBooking();
            calDateFlag = true;
            if(GlobalVariables.editEnabled == true)
            {
                foreach(Booking booking in BookingList)
                {
                    if(booking.BookingID == bookingID && booking.DateTime == date)
                    {
                        returnIndex = booking.ColumnIndex;
                        columnIndex = booking.ColumnIndex;
                        therapist = booking.EmployeeID;
                        if(therapist == 1)
                        {
                            jane = true;
                        }
                        else if (therapist == 2)
                        {
                            james = true;
                        }
                        else if(therapist == 3)
                        {
                            joanne = true;
                        }
                        else if(therapist == 4)
                        {
                            julie = true;
                        }
                        clearBooking();
                    }
                }
            }
            if(GlobalVariables.editEnabled == true && date < today)
            {
                dgvJames.Enabled = true;
                dgvJane.Enabled = true;
                dgvJulie.Enabled = true;
                dgvJoanne.Enabled = true;
            }
            else if (GlobalVariables.editEnabled == true && date >= today)
            {
                dgvJames.Enabled = true;
                dgvJane.Enabled = true;
                dgvJulie.Enabled = true;
                dgvJoanne.Enabled = true;
                clearComboBox();
            }
            else
            {
                dgvJames.Enabled = true;
                dgvJane.Enabled = true;
                dgvJulie.Enabled = true;
                dgvJoanne.Enabled = true;
                clearComboBox();
            }
            cellClickedJoanne = 99;
            cellClickedJames = 99;
            cellClickedJane = 99;
            cellClickedJulie = 99;

            if (date.DayOfWeek == DayOfWeek.Friday)
            {
                friday = true;
                clearBooking();
            }
            else
            {
                friday = false;
                clearBooking();
            }
        }


        private void dgvJames_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) //This will register that the appearance of the data grid view needs changed
        {
            dgv = insertBooking(duration, columnIndex, e, james, joanne, julie, jane, dgv);
            james = false;
        }
        private void dgvJoanne_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv = insertBooking(duration, columnIndex, e, james, joanne, julie, jane, dgv);
            joanne = false;
        }
        private void dgvJulie_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv = insertBooking(duration, columnIndex, e, james, joanne, julie, jane, dgv);
            julie = false;
        }
        private void dgvJane_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv = insertBooking(duration, columnIndex, e, james, joanne, julie, jane, dgv);
            jane = false;
        }


        private void setColumns(DataGridView dgv, int columns, int schedule, bool friday) //Sets the data grid views properly
        {
            dgv.DefaultCellStyle.BackColor = Color.Green;
            dgv.RowHeadersVisible = false;
            dgv.ColumnCount = columns;
            dgv.RowCount = 1;
            if (friday == true)
            {
                if (columns != 0 && schedule == 1)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    dgv.CurrentCell.Selected = false;
                }
                else if (columns != 0 && schedule == 2)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Rows[0].Cells[i].Style.BackColor = Color.Gray;
                    }
                    dgv.CurrentCell.Selected = false;
                }
                else if (columns != 0 && schedule == 3)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Rows[0].Cells[i].Style.BackColor = Color.Gray;
                    }
                    dgv.CurrentCell.Selected = false;
                }
            }
            else
            {
                if (columns != 0 && schedule == 1)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    dgv.CurrentCell.Selected = false;
                }
                else if (columns != 0 && schedule == 2)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    for (int i = 16; i < 32; i++)
                    {
                        dgv.Rows[0].Cells[i].Style.BackColor = Color.Gray;
                    }
                    dgv.CurrentCell.Selected = false;
                }
                else if (columns != 0 && schedule == 3)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        dgv.Rows[0].Cells[i].Style.BackColor = Color.Gray;
                    }
                    dgv.CurrentCell.Selected = false;
                }
            }
        }
        public int getDuration(int treatmentID) //Will get the duration of a specific treatment
        {
            int durationGot = 0;
            foreach (Treatment Treat in TreatmentList)
            {
                if (Treat.TreatmentID == treatmentID)
                {
                    durationGot = Treat.Duration;
                }
            }
            return durationGot;
        }
        public int getCost(int treatmentID) //Will get the cost of a specific treatment
        {
            int costGot = 0;
            foreach (Treatment Treat in TreatmentList)
            {
                if (Treat.TreatmentID == treatmentID)
                {
                    costGot = Treat.Cost;
                }
            }
            return costGot;
        }
        public DataGridView insertBooking(int durationGot, int columnIndex, DataGridViewCellPaintingEventArgs e, bool james, bool joanne, bool julie, bool jane, DataGridView dgv) //This will insert the booking into the data grid view
        {
            if (james == true)
            {
                dgv = dgvJames;
                int index = (durationGot / 15);
                for (int i = 0; i < index; i++)
                {
                    if (dgvJames.CurrentCell.RowIndex == e.RowIndex && dgvJames.CurrentCell.ColumnIndex == e.ColumnIndex)
                    {
                        e.CellStyle.SelectionBackColor = Color.Blue;
                        dgvJames.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                    }
                }
                return dgv;
            }
            if (joanne == true)
            {
                dgv = dgvJoanne;
                int index = (durationGot / 15);
                for (int i = 0; i < index; i++)
                {
                    if (dgvJoanne.CurrentCell.RowIndex == e.RowIndex && dgvJoanne.CurrentCell.ColumnIndex == e.ColumnIndex)
                    {
                        e.CellStyle.SelectionBackColor = Color.Blue;
                        dgvJoanne.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                    }
                }
                return dgv;
            }
            if (julie == true)
            {
                dgv = dgvJulie;
                int index = (durationGot / 15);
                for (int i = 0; i < index; i++)
                {
                    if (dgvJulie.CurrentCell.RowIndex == e.RowIndex && dgvJulie.CurrentCell.ColumnIndex == e.ColumnIndex)
                    {
                        e.CellStyle.SelectionBackColor = Color.Blue;
                        dgvJulie.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                    }
                }
                return dgv;
            }
            if (jane == true)
            {
                dgv = dgvJane;
                int index = (durationGot / 15);
                for (int i = 0; i < index; i++)
                {
                    if (dgvJane.CurrentCell.RowIndex == e.RowIndex && dgvJane.CurrentCell.ColumnIndex == e.ColumnIndex)
                    {
                        e.CellStyle.SelectionBackColor = Color.Blue;
                        dgvJane.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                    }
                }
            }
            return dgv;
        }
        private void updateDuration_Tick(object sender, EventArgs e) //Every tick event will be registering 
        {
            lblTotalCost.Text = "£" + cost;
            if (checkBoxTally > 0)
            {
                pbTreatmentComplete.BackColor = Color.Green;
            }
            else
            {
                pbTreatmentComplete.BackColor = Color.Red;
            }
            if (calDateFlag == true)
            {
                pbDateComplete.BackColor = Color.Green;
            }
            else
            {
                pbDateComplete.BackColor = Color.Red;
            }
            if (checkBoxTally > 0 && calDateFlag == true && therapist != 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
            foreach (Booking booking in BookingList) //This is constantly keeping the bookings displayed on the GUI
            {
                if (booking.DateTime == date && bookingID != booking.BookingID)
                {
                    columnIndex = booking.ColumnIndex;
                    durationGot = booking.Duration;
                    int index = (durationGot / 15);
                    if(GlobalVariables.editEnabled == false)
                    {
                        if (booking.EmployeeID == 2 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJames.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 4 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJulie.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 3 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJoanne.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 1 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJane.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 2 && booking.CustomerID == customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJames.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 4 && booking.CustomerID == customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJulie.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 3 && booking.CustomerID == customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJoanne.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 1 && booking.CustomerID == customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJane.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else
                    {
                        if (booking.EmployeeID == 2 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJames.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 4 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJulie.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 3 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJoanne.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 1 && booking.CustomerID != customerID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJane.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 2 && booking.CustomerID == customerID && booking.BookingID == bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJames.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 4 && booking.CustomerID == customerID && booking.BookingID == bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJulie.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 3 && booking.CustomerID == customerID && booking.BookingID == bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJoanne.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 1 && booking.CustomerID == customerID && booking.BookingID == bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJane.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Blue;
                            }
                        }
                        if (booking.EmployeeID == 2 && booking.CustomerID == customerID && booking.BookingID != bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJames.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 4 && booking.CustomerID == customerID && booking.BookingID != bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJulie.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 3 && booking.CustomerID == customerID && booking.BookingID != bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJoanne.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                        if (booking.EmployeeID == 1 && booking.CustomerID == customerID && booking.BookingID != bookingID)
                        {
                            for (int i = 0; i < index; i++)
                            {
                                dgvJane.Rows[0].Cells[columnIndex + i].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
            var data = checkboxChecked(cbHairCut, Hair, 1, returnIndex, duration, cost, bookingID); //These check if a checkbox is checked or unchecked
            Hair = data.Item1;
            duration = data.Item2;
            cost = data.Item3;
            columnIndex = data.Item4;
            var data2 = checkboxUnchecked(cbHairCut, Hair, 1, returnIndex, duration, cost);
            Hair = data2.Item1;
            duration = data2.Item2;
            cost = data2.Item3;
            columnIndex = data2.Item4;
            var data3 = checkboxChecked(cbNails, Nails, 2, returnIndex, duration, cost, bookingID);
            Nails = data3.Item1;
            duration = data3.Item2;
            cost = data3.Item3;
            columnIndex = data3.Item4;
            var data4 = checkboxUnchecked(cbNails, Nails, 2, returnIndex, duration, cost);
            Nails = data4.Item1;
            duration = data4.Item2;
            cost = data4.Item3;
            columnIndex = data4.Item4;
            var data5 = checkboxChecked(cbMakeup, Makeup, 3, returnIndex, duration, cost, bookingID);
            Makeup = data5.Item1;
            duration = data5.Item2;
            cost = data5.Item3;
            columnIndex = data5.Item4;
            var data6 = checkboxUnchecked(cbMakeup, Makeup, 3, returnIndex, duration, cost);
            Makeup = data6.Item1;
            duration = data6.Item2;
            cost = data6.Item3;
            columnIndex = data6.Item4;
            var data7 = checkboxChecked(cbHairDye, HairDye, 4, returnIndex, duration, cost, bookingID);
            HairDye = data7.Item1;
            duration = data7.Item2;
            cost = data7.Item3;
            columnIndex = data7.Item4;
            var data8 = checkboxUnchecked(cbHairDye, HairDye, 4, returnIndex, duration, cost);
            HairDye = data8.Item1;
            duration = data8.Item2;
            cost = data8.Item3;
            columnIndex = data8.Item4;
            var data9 = checkboxChecked(cbWaxing, Waxing, 5, returnIndex, duration, cost, bookingID);
            Waxing = data9.Item1;
            duration = data9.Item2;
            cost = data9.Item3;
            columnIndex = data9.Item4;
            var data10 = checkboxUnchecked(cbWaxing, Waxing, 5, returnIndex, duration, cost);
            Waxing = data10.Item1;
            duration = data10.Item2;
            cost = data10.Item3;
            columnIndex = data10.Item4;
            var data11 = checkboxChecked(cbLashesAndBrows, LashesAndBrows, 6, returnIndex, duration, cost, bookingID);
            LashesAndBrows = data11.Item1;
            duration = data11.Item2;
            cost = data11.Item3;
            columnIndex = data11.Item4;
            var data12 = checkboxUnchecked(cbLashesAndBrows, LashesAndBrows, 6, returnIndex, duration, cost);
            LashesAndBrows = data12.Item1;
            duration = data12.Item2;
            cost = data12.Item3;
            columnIndex = data12.Item4;
            if (duration != 0)
            {
                dgvAppointment.DefaultCellStyle.BackColor = Color.Blue;
                dgvAppointment.RowCount = 1;
                dgvAppointment.ColumnCount = (duration / 15);
                for (int i = 0; i < duration / 15; i++)
                {
                    DataGridViewColumn column = dgvAppointment.Columns[i];
                    column.Width = 15;
                }
                dgvAppointment.CurrentCell.Selected = false;
            }
            else
            {
                cellClickedJames = 99;
                cellClickedJane = 99;
                cellClickedJoanne = 99;
                cellClickedJulie = 99;
                dgvAppointment.ColumnCount = 0;
            }
            if (therapist == 1)
            {
                cbHairCut.Enabled = false;
                cbHairDye.Enabled = false;
                cbWaxing.Enabled = false;
                cbMakeup.Enabled = false;
                cbLashesAndBrows.Enabled = false;
            }
            else if(currentBookingArchive != 1)
            {
                if (hairCutFlag != true)
                {
                    cbHairCut.Enabled = true;
                }
                if(hairDyeFlag != true)
                {
                    cbHairDye.Enabled = true;
                }
                if(waxingFlag != true)
                {
                    cbWaxing.Enabled = true;
                }
                if(makeupFlag != true)
                {
                    cbMakeup.Enabled = true;
                }
                if(lashesAndBrowsFlag != true)
                {
                    cbLashesAndBrows.Enabled = true;
                }
            }
            foreach (Booking booking in BookingList) //This will prevent double booking of treatments
            {
                if (customerID == booking.CustomerID && booking.DateTime == date && GlobalVariables.editEnabled == false)
                {
                    foreach (BookingTreatment bookingTreatment in BookingTreatmentList)
                    {
                        if (bookingTreatment.BookingID == booking.BookingID)
                        {
                            if (bookingTreatment.TreatmentID == 1)
                            {
                                hairCutFlag = true;
                                cbHairCut.Enabled = false;
                                cbHairCut.Checked = false;
                            }
                            else if (bookingTreatment.TreatmentID == 2)
                            {
                                nailsFlag = true;
                                cbNails.Enabled = false;
                                cbNails.Checked = false;
                            }
                            else if (bookingTreatment.TreatmentID == 3)
                            {
                                makeupFlag = true;
                                cbMakeup.Enabled = false;
                                cbMakeup.Checked = false;
                            }
                            else if (bookingTreatment.TreatmentID == 4)
                            {
                                hairDyeFlag = true;
                                cbHairDye.Enabled = false;
                                cbHairDye.Checked = false;
                            }
                            else if (bookingTreatment.TreatmentID == 5)
                            {
                                waxingFlag = true;
                                cbWaxing.Enabled = false;
                                cbWaxing.Checked = false;
                            }
                            else if (bookingTreatment.TreatmentID == 6)
                            {
                                lashesAndBrowsFlag = true;
                                cbLashesAndBrows.Enabled = false;
                                cbLashesAndBrows.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        private Tuple<bool, int, int, int> checkboxChecked(CheckBox cb, bool boolName, int treatmentID, int returnIndex, int duration, int cost, int bookingID) //Checkbox checked event. This will run a significant amount of validation and update the GUI
        {
            bool tempFlag = false;
            bool endFlag = false;
            bool joanneFlag = false;
            if (cb.Checked == true && boolName == false)
            {
                checkBoxTally += 1;
                duration += durationGot = getDuration(treatmentID);
                cost += costGot = getCost(treatmentID);
                boolName = true;
                if (returnIndex != 0 && returnIndex != -1 && previousDGV != null)
                {
                    if (previousDGV == "jane")
                    {
                        returnIndex = cellClickedJane;
                        jane = true;
                    }
                    else if (previousDGV == "james")
                    {
                        returnIndex = cellClickedJames;
                        james = true;
                    }
                    else if (previousDGV == "joanne")
                    {
                        returnIndex = cellClickedJoanne;
                        joanne = true;
                    }
                    else if (previousDGV == "julie")
                    {
                        returnIndex = cellClickedJulie;
                        julie = true;
                    }
                    if (returnIndex < 99)
                    {
                        foreach (Booking booking in BookingList)
                        {
                            if (booking.DateTime == date && booking.CustomerID == customerID)
                            {
                                
                                foreach (BookingTreatment bt in BookingTreatmentList)
                                {
                                    if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)) && booking.BookingID != bookingID)
                                    {
                                        cb.Checked = false;
                                        tempFlag = true;
                                        break;
                                    }
                                    if (tempFlag == true)
                                    {
                                        break;
                                    }
                                }
                                if (tempFlag == true)
                                {
                                    break;
                                }
                            }
                            if(booking.DateTime == date && booking.EmployeeID == therapist)
                            {
                                foreach (BookingTreatment bt in BookingTreatmentList)
                                {
                                    if ((returnIndex + (duration / 15)) > booking.ColumnIndex && returnIndex < (booking.ColumnIndex + (booking.Duration / 15)) && booking.BookingID != bookingID)
                                    {
                                        cb.Checked = false;
                                        tempFlag = true;
                                        break;
                                    }
                                    if (tempFlag == true)
                                    {
                                        break;
                                    }
                                }
                                if (tempFlag == true)
                                {
                                    break;
                                }
                            }
                            if (tempFlag == true)
                            {
                                break;
                            }
                        }
                    }
                    if(returnIndex + duration/15 >= 32)
                    {
                        endFlag = true;
                        tempFlag = true;
                        cb.Checked = false;
                    }
                    if(returnIndex + duration/15 >= 16 && therapist == 3)
                    {
                        joanneFlag = true;
                        tempFlag = true;
                        cb.Checked = false;
                    }
                    clearBooking();
                }
                if(tempFlag != true)
                {
                    treatments.Add(treatmentID);
                }
                else
                {
                    if(endFlag == true)
                    {
                        MessageBox.Show("Overlaping after closing time");
                    }
                    else if(joanneFlag == true)
                    {
                        MessageBox.Show("Joanne's shift will be finished, select an earlier time");
                    }
                    else
                    {
                        MessageBox.Show("This will overlap with a current booking");
                    }
                }
            }
            return Tuple.Create(boolName, duration, cost, returnIndex);
        }
        private Tuple<bool, int, int, int> checkboxUnchecked(CheckBox cb, bool boolName, int treatmentID, int returnIndex, int duration, int cost)//Checkbox unchecked event. This will run a significant amount of validation and update the GUI
        {
            if (cb.Checked == false && boolName == true)
            {
                checkBoxTally -= 1;
                duration -= durationGot = getDuration(treatmentID);
                cost -= costGot = getCost(treatmentID);
                boolName = false;
                if (checkBoxTally == 0)
                {
                    previousDGV = null;
                    therapist = 0;
                }
                if (returnIndex != 0 && returnIndex != -1)
                {
                    if (previousDGV == "jane")
                    {
                        returnIndex = cellClickedJane;
                        jane = true;
                    }
                    else if (previousDGV == "james")
                    {
                        returnIndex = cellClickedJames;
                        james = true;
                    }
                    else if (previousDGV == "joanne")
                    {
                        returnIndex = cellClickedJoanne;
                        joanne = true;
                    }
                    else if (previousDGV == "julie")
                    {
                        returnIndex = cellClickedJulie;
                        julie = true;
                    }
                    clearBooking();
                }
                treatments.Remove(treatmentID);
            }
            return Tuple.Create(boolName, duration, cost, returnIndex);
        }
        private void clearBooking() //This is what will clear the GUI, a reset of sorts
        {
            setColumns(dgvJane, 0, 1, friday);
            setColumns(dgvJane, 32, 1, friday);
            setColumns(dgvJulie, 0, 3, friday);
            setColumns(dgvJulie, 32, 3, friday);
            setColumns(dgvJoanne, 0, 2, friday);
            setColumns(dgvJoanne, 32, 2, friday);
            setColumns(dgvJames, 0, 1, friday);
            setColumns(dgvJames, 32, 1, friday);
        }
        private int returnColumnIndex(DataGridView dgv)
        {
            columnIndex = dgv.CurrentCell.ColumnIndex;
            return columnIndex;
        }
        public void populatelb() //Populates the list box with customers
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

        public void populatelbEdit()//Populates the list box with bookings
        {
            BookingsDBAccess bdba = new BookingsDBAccess(db);
            CustomerDBAccess cdba = new CustomerDBAccess(db);
            BookingList = bdba.getAllBookings();
            CustomerList = cdba.getAllCustomers();
            BookingList.Reverse();
            CustomerList.Reverse();
            string customerName = "";
            DataTable displayBooking = new DataTable();

            displayBooking.Columns.Add("Book Id");
            displayBooking.Columns.Add("Booking");

            foreach (Booking booking in BookingList)
            {
                foreach (Customer customer in CustomerList)
                {
                    if (booking.CustomerID == customer.CustomerID)
                    {
                        customerName = customer.CustomerForename + " " + customer.CustomerSurname;
                    }
                }
                displayBooking.Rows.Add(booking.BookingID, booking.BookingID + " | " + customerName);
                displayBooking.Rows.Add(booking.BookingID, booking.BookingID + " | " + booking.DateTime.AddHours(9).AddMinutes((booking.ColumnIndex) * 15));
            }
            lbCustomers.ValueMember = "Book Id";
            lbCustomers.DisplayMember = "Booking";
            lbCustomers.DataSource = displayBooking;
        }

        public void bookingInfo(DataGridView dgv, int employee, DataGridViewCellFormattingEventArgs e) //This will create the small tool tip when hovering over a booking
        {
            string customerName = "";
            string displayTreatments = "";
            int costTemp = 0;
            foreach (Booking booking in BookingList)
            {
                if (booking.DateTime == date && booking.EmployeeID == employee)
                {
                    DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.ColumnIndex < (booking.ColumnIndex + (booking.Duration / 15)) && cell.ColumnIndex >= booking.ColumnIndex)
                    {
                        foreach (Customer customer in CustomerList)
                        {
                            if (customer.CustomerID == booking.CustomerID)
                            {
                                customerName = customer.CustomerForename + " " + customer.CustomerSurname;
                            }
                            foreach (BookingTreatment bookingTreatment in BookingTreatmentList)
                            {
                                if (booking.BookingID == bookingTreatment.BookingID && booking.DateTime == date)
                                {
                                    foreach (Treatment treatment in TreatmentList)
                                    {
                                        if (bookingTreatment.TreatmentID == treatment.TreatmentID)
                                        {
                                            if (displayTreatments.Contains(treatment.TreatmentName))
                                            {

                                            }
                                            else
                                            {
                                                displayTreatments += " " + treatment.TreatmentName;
                                                costTemp += treatment.Cost;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        cell.ToolTipText = Convert.ToString(booking.BookingID) + " | " + customerName + " | " + displayTreatments + " | " + "£" + costTemp;
                    }
                }
            }
        }
        private void clearComboBox() //Re-enables all comboboxes
        {
            cbHairCut.Enabled = true;
            cbNails.Enabled = true;
            cbMakeup.Enabled = true;
            cbHairDye.Enabled = true;
            cbWaxing.Enabled = true;
            cbLashesAndBrows.Enabled = true;
        }
    }
}
