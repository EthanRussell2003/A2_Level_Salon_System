using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Booking_System_21_09_21.Objects
{
    class Booking
    {
        private int customerID;
        private int employeeID;
        private int roomNum;
        private DateTime dateTime;
        private int bookingID;
        private int duration;
        private int columnIndex;
        private int archive;

        public Booking()
        {

        }

        public Booking (int customerID, int employeeID, int roomNum, DateTime dateTime, int bookingID, int duration, int columnIndex, int archive)
        {
            CustomerID = customerID;
            EmployeeID = employeeID;
            RoomNum = roomNum;
            DateTime = dateTime;
            BookingID = bookingID;
            Duration = duration;
            ColumnIndex = columnIndex;
            Archive = archive;
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public int RoomNum
        {
            get { return roomNum; }
            set { roomNum = value; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        public int BookingID
        {
            get { return bookingID; }
            set { bookingID = value; }
        }
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public int ColumnIndex
        {
            get { return columnIndex; }
            set { columnIndex = value; }
        }
        public int Archive
        {
            get { return archive; }
            set { archive = value; }
        }
    }
}
