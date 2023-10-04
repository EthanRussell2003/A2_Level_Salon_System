using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Booking_System_21_09_21.Objects
{
    class Employee
    {
        private int employeeID;
        private string employeeForename;
        private string employeeSurname;
        private string employeeEmail;
        private int employeeSchedule;
        private int employeeArchive;

        public Employee()
        {

        }

        public Employee(int employeeID, string employeeForename, string employeeSurname, string employeeEmail, int employeeSchedule, int employeeArchive)
        {
            EmployeeID = employeeID;
            EmployeeForename = employeeForename;
            EmployeeSurname = employeeSurname;
            EmployeeEmail = employeeEmail;
            EmployeeSchedule = employeeSchedule;
            EmployeeArchive = employeeArchive;

        }

        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public string EmployeeForename
        {
            get { return employeeForename; }
            set { employeeForename = value; }
        }
        public string EmployeeSurname
        {
            get { return employeeSurname; }
            set { employeeSurname = value; }
        }
        public string EmployeeEmail
        {
            get { return employeeEmail; }
            set { employeeEmail = value; }
        }
        public int EmployeeSchedule
        {
            get { return employeeSchedule; }
            set { employeeSchedule = value; }
        }
        public int EmployeeArchive
        {
            get { return employeeArchive; }
            set { employeeArchive = value; }
        }
    }
}
