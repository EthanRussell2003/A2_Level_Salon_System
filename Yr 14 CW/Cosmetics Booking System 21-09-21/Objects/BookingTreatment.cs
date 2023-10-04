using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Booking_System_21_09_21.Objects
{
    class BookingTreatment
    {
        private int bookingTreatmentID;
        private int bookingID;
        private int treatmentID;

        public BookingTreatment()
        {

        }

        public BookingTreatment(int bookingTreatmentID, int bookingID, int treatmentID)
        {
            BookingTreatmentID = bookingTreatmentID;
            BookingID = bookingID;
            TreatmentID = treatmentID;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             TreatmentID = treatmentID;
        }

        public int BookingTreatmentID
        {
            get { return bookingTreatmentID; }
            set { bookingTreatmentID = value; }
        }

        public int BookingID
        {
            get { return bookingID; }
            set { bookingID = value; }
        }
        public int TreatmentID
        {
            get { return treatmentID; }
            set { treatmentID = value; }
        }
    }
}
