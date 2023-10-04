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
    public partial class Confirmation : Form
    {
        Database db;
        DataTable table = new DataTable();
        List<Customer> CustomerList = new List<Customer>();
        List<Booking> BookingList = new List<Booking>();
        List<Treatment> TreatmentList = new List<Treatment>();
        List<BookingTreatment> BookingTreatmentList = new List<BookingTreatment>();
        int customerID, therapist, roomNum, duration, returnIndex, cost, bookingID;
        DateTime date, dateTime;
        List<Int32> treatments = new List<Int32>();
        public Confirmation(int CustomerID, int Therapist, int RoomNum, DateTime Date, int Duration, int ReturnIndex, List<Int32> Treatments, int BookingID ) //Recieving passed data
        {
            InitializeComponent();
            customerID = CustomerID;
            therapist = Therapist;
            roomNum = RoomNum;
            date = Date.AddMinutes(returnIndex * 15).AddHours(9);
            dateTime = Date;
            duration = Duration;
            returnIndex = ReturnIndex;
            treatments = Treatments;
            bookingID = BookingID;
            db = new Database();
            if (db.connect())
            {
            }
            else
            {
                MessageBox.Show("Database Connection Unsuccessful.", "Error");
                Application.Exit();
            }
            BookingsDBAccess bdba = new BookingsDBAccess(db);
            TreatmentDBAccess tdba = new TreatmentDBAccess(db);
            CustomerDBAccess cdba = new CustomerDBAccess(db);
            BookingTreatmentDBAccess btdba = new BookingTreatmentDBAccess(db);
            BookingList = bdba.getAllBookings();
            CustomerList = cdba.getAllCustomers();
            TreatmentList = tdba.getAllTreatments();
            BookingTreatmentList = btdba.getAllBookingTreatments();
            lblShowTreatments.Text = "";
            foreach(Customer customer in CustomerList)
            {
                if(CustomerID == customer.CustomerID)
                {
                    lblShowName.Text = customer.CustomerForename + " " + customer.CustomerSurname;
                }
            }
            foreach(Treatment treatment in TreatmentList)
            {
                for (int i = 0; i < treatments.Count; i++)
                {
                    if (treatment.TreatmentID == treatments[i])
                    {
                        if(lblShowTreatments.Text == "")
                        {
                            lblShowTreatments.Text = lblShowTreatments.Text + treatment.TreatmentName;
                            cost += treatment.Cost;
                        }
                        else
                        {
                            lblShowTreatments.Text = lblShowTreatments.Text + " | " + treatment.TreatmentName;
                            cost += treatment.Cost;
                        }
                    }
                }
            }
            lblShowDate.Text = Convert.ToString(date.ToLongDateString());
            lblShowStart.Text = Convert.ToString(date.AddMinutes(returnIndex * 15).ToLongTimeString());
            lblShowEnd.Text = Convert.ToString(date.AddMinutes(duration + returnIndex * 15).ToLongTimeString());
            lblShowCost.Text = "£" + Convert.ToString(cost);
        }
        private void btnConfirm_Click(object sender, EventArgs e) //Saves a booking
        {
            BookingTreatmentDBAccess btdba = new BookingTreatmentDBAccess(db);
            BookingsDBAccess bdba = new BookingsDBAccess(db);
            int bookingTreatmentID = 0;
            if (GlobalVariables.editEnabled != true)
            {
                int highestID = bdba.newID();
                int CustomerID = Convert.ToInt32(customerID);
                int EmployeeID = therapist;
                int RoomNum = roomNum;
                DateTime DateTime = dateTime;
                int BookingID = bdba.newID();
                int Duration = duration;
                int ColumnIndex = returnIndex;
                int Archive = 0;
                bdba.insertBooking(CustomerID, EmployeeID, RoomNum, DateTime, BookingID, Duration, ColumnIndex, Archive);
                for (int i = 0; i < treatments.Count; i++)
                {
                    bookingTreatmentID = btdba.newID();
                    btdba.insertBookingTreatment(bookingTreatmentID, highestID, treatments[i]);
                }
            }
            else
            {
                foreach(Booking booking in BookingList)
                {
                    if(booking.BookingID == bookingID)
                    {
                        booking.EmployeeID = therapist;
                        booking.RoomNum = roomNum;
                        booking.DateTime = dateTime;
                        booking.Duration = duration;
                        booking.ColumnIndex = returnIndex;
                        btdba.deleteBookingTreatment(bookingID);
                        bdba.updateBooking(booking);
                        for (int i = 0; i < treatments.Count; i++)
                        {
                            bookingTreatmentID = btdba.newID();
                            btdba.insertBookingTreatment(bookingTreatmentID, bookingID, treatments[i]);
                        }
                    }
                }
            }
            GlobalVariables.dateReturn = dateTime;
            foreach (Form f in Application.OpenForms)
            {
                f.Hide();
            }
            BookingDateTime form = new BookingDateTime();
            form.ShowDialog();
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e) //Closes only the confirmation form and not the booking form
        {
            this.Hide();
        }
    }
}
