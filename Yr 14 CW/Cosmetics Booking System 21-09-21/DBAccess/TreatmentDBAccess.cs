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
    class TreatmentDBAccess
    {
        Database db;

        public TreatmentDBAccess(Database db)
        {
            this.db = db;
        }

        public Treatment getTreatmentsFromReader(SqlDataReader reader)
        {
            Treatment resultTreatment = new Treatment();

            resultTreatment.TreatmentID = db.Rdr.GetInt32(0);
            resultTreatment.TreatmentName = db.Rdr.GetString(1);
            resultTreatment.Duration = db.Rdr.GetInt32(2);
            resultTreatment.Cost = db.Rdr.GetInt32(3);

            return resultTreatment;
        }

        public List<Treatment> getAllTreatments()
        {
            List<Treatment> resultTreatments = new List<Treatment>();

            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT * FROM Treatment";
            db.Rdr = db.Cmd.ExecuteReader();

            while (db.Rdr.Read())
            {
                resultTreatments.Add(getTreatmentsFromReader(db.Rdr));
            }
            db.Rdr.Close();
            return resultTreatments;

        }
    }
}
