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
    class BookingTreatmentDBAccess
    {
        Database db;
        public BookingTreatmentDBAccess(Database db)
        {
            this.db = db;
        }

        public void insertBookingTreatment(int BookingTreatmentID, int BookingID, int TreatmentID)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "INSERT INTO BOOKINGTREATMENT(BookingTreatmentID, BookingID, TreatmentID) " + "VALUES (" + BookingTreatmentID + ", " + BookingID + ", " + TreatmentID + ")";
            db.Cmd.ExecuteNonQuery();
        }
        public BookingTreatment getBookingTreatmentsFromReader(SqlDataReader reader)
        {
            BookingTreatment resultBookingTreatment = new BookingTreatment();

            resultBookingTreatment.BookingTreatmentID = db.Rdr.GetInt32(0);
            resultBookingTreatment.BookingID = db.Rdr.GetInt32(1);
            resultBookingTreatment.TreatmentID = db.Rdr.GetInt32(2);
            return resultBookingTreatment;
        }
        public List<BookingTreatment> getAllBookingTreatments()
        {
            List<BookingTreatment> resultBookingTreatments = new List<BookingTreatment>();

            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT * FROM BookingTreatment";
            db.Rdr = db.Cmd.ExecuteReader();

            while (db.Rdr.Read())
            {
                resultBookingTreatments.Add(getBookingTreatmentsFromReader(db.Rdr));
            }
            db.Rdr.Close();
            return resultBookingTreatments;
        }

        public int newID()
        {
            int result;
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT max(BookingTreatmentID) FROM BOOKINGTREATMENT";
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

        public void deleteBookingTreatment(int BookingID)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "DELETE FROM BOOKINGTREATMENT WHERE BookingID = " + BookingID;
            db.Cmd.ExecuteNonQuery();
        }
    }
}
