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
    class EmployeeDBAccess
    {
        Database db;
        public EmployeeDBAccess(Database db)
        {
            this.db = db;
        }
        public List<Employee> getAllEmployees()
        {
            List<Employee> resultEmployees = new List<Employee>();

            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT * FROM Employees";
            db.Rdr = db.Cmd.ExecuteReader();

            while (db.Rdr.Read())
            {
                resultEmployees.Add(getEmplyoeesFromReader(db.Rdr));
            }
            db.Rdr.Close();
            return resultEmployees;

        }
        public Employee getEmplyoeesFromReader(SqlDataReader reader)
        {
            Employee resultEmployee = new Employee();

            resultEmployee.EmployeeID = db.Rdr.GetInt32(0);
            resultEmployee.EmployeeForename = db.Rdr.GetString(1);
            //resultEmployee.EmployeeSurname = db.Rdr.GetString(2);
            //resultEmployee.EmployeeEmail = db.Rdr.GetString(3);
            //resultEmployee.EmployeeSchedule = db.Rdr.GetInt16(4);
            //resultEmployee.EmployeeArchive = db.Rdr.GetInt16(5);

            return resultEmployee;
        }
    }
}
