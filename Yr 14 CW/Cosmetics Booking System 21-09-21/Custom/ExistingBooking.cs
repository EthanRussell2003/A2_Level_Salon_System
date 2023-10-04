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


namespace Cosmetics_Booking_System_21_09_21.Custom
{
    class ExistingBooking : Exception
    {
        public ExistingBooking()
        {

        }
        public ExistingBooking(string message) : base(message)
        {

        }
        public void BookingOverlap()
        {
            MessageBox.Show("This booking will overlap with another booking.\nPlease choose a new time.");
        }
    }
}