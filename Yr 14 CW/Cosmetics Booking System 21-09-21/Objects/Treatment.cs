using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Booking_System_21_09_21.Objects
{
    class Treatment
    {
        private int treatmentID;
        private string treatmentName;
        private int duration;
        private int cost;

        public Treatment()
        {

        }

        public Treatment(int treatmentID, string treatmentName, int duration, int cost)
        {
            TreatmentID = treatmentID;
            TreatmentName = treatmentName;
            Duration = duration;
            Cost = cost;
        }

        public int TreatmentID
        {
            get { return treatmentID; }
            set { treatmentID = value; }
        }
        public string TreatmentName
        {
            get { return treatmentName; }
            set { treatmentName = value; }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
    }
}
