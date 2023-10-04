using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Cosmetics_Booking_System_21_09_21.Objects;
using Cosmetics_Booking_System_21_09_21.DBAccess;

namespace Cosmetics_Booking_System_21_09_21.DBAccess
{
    class BookingsDBAccess
    {
        Database db;

        public BookingsDBAccess(Database db)
        {
            this.db = db;
        }
        public void insertBooking(int CustomerID, int EmployeeID, int RoomNum, DateTime DateTime, int BookingID, int Duration, int ColumnIndex, int Archive)
        {
            //try
            //{
                db.Cmd = db.Conn.CreateCommand();
                db.Cmd.CommandText = "INSERT INTO Bookings (CustomerID, EmployeeID, RoomNum, DateTime, BookingID, Duration, ColumnIndex, Archive) Values ( '" + CustomerID + "', '" + EmployeeID + "', '" + RoomNum + "' , '" + DateTime.ToString("s") + "'," + BookingID + "," + Duration + "," + ColumnIndex + "," + Archive + ")";
                db.Cmd.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Error");
            //}
        }
        public Booking getBookingsFromReader(SqlDataReader reader)
        {
            Booking resultBooking = new Booking();

            resultBooking.CustomerID = db.Rdr.GetInt32(0);
            resultBooking.EmployeeID = db.Rdr.GetInt32(1);
            resultBooking.RoomNum = db.Rdr.GetInt32(2);
            resultBooking.DateTime = db.Rdr.GetDateTime(3);
            resultBooking.BookingID = db.Rdr.GetInt32(4);
            resultBooking.Duration = db.Rdr.GetInt32(5);
            resultBooking.ColumnIndex = db.Rdr.GetInt32(6);
            resultBooking.Archive = db.Rdr.GetInt32(7);
            return resultBooking;
        }
        public void updateBooking(Booking updateBooking)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = String.Format("UPDATE Bookings SET CustomerID = '{0}', EmployeeID = '{1}', RoomNum = '{2}', DateTime = '{3}', Duration = '{4}', ColumnIndex = '{5}', Archive = '{6}' WHERE BookingID = '{7}'", updateBooking.CustomerID, updateBooking.EmployeeID, updateBooking.RoomNum, updateBooking.DateTime.ToString("s"), updateBooking.Duration, updateBooking.ColumnIndex, updateBooking.Archive, updateBooking.BookingID);
            db.Cmd.ExecuteNonQuery();
        }
        public void archiveBooking(Booking archiveBooking)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = String.Format("UPDATE Bookings SET Archive = '{0}' WHERE BookingID = '{1}'", archiveBooking.Archive, archiveBooking.BookingID);
            db.Cmd.ExecuteNonQuery();
        }

        public List<Booking> getAllBookings()
        {
            List<Booking> resultBookings = new List<Booking>();

            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT * FROM Bookings";
            db.Rdr = db.Cmd.ExecuteReader();

            while (db.Rdr.Read())
            {
                resultBookings.Add(getBookingsFromReader(db.Rdr));
            }
            db.Rdr.Close();
            return resultBookings;
        }
        public void deleteBooking(int id)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "DELETE FROM Bookings WHERE BookingID= '" + id + "'";
            db.Cmd.ExecuteNonQuery();
        }
        public int newID()
        {
            int result;
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT max(BookingID) FROM BOOKINGS";
            db.Rdr = db.Cmd.ExecuteReader();
            db.Rdr.Read();
            if (db.Rdr.IsDBNull(0))
            {
                result = 0;
            }
            else
            {
                result = db.Rdr.GetInt32(0);
            }
            db.Rdr.Close();
            return result + 1;
        }
    }
}
