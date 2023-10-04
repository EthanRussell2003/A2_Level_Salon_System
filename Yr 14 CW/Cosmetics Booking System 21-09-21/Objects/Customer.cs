using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Cosmetics_Booking_System_21_09_21.Objects
{
    class Customer
    {
        private int customerID;
        private string customerForename;
        private string customerSurname;
        private string customerAddress;
        private string customerTelNo;
        private DateTime customerDateOfBirth;
        private int customerArchive;
        private string customerTown;
        private string customerPostcode;
        private string customerEmail;

        public Customer()
        {

        }
        public Customer(int customerID, string customerForename, string customerSurname, string customerAddress, string customerTelNo, DateTime customerDateOfBirth, int customerArchive, string customerTown, string customerPostcode, string customerEmail)
        {
            CustomerID = customerID;
            CustomerForename = customerForename;
            CustomerSurname = customerSurname;
            CustomerAddress = customerAddress;
            CustomerTelNo = customerTelNo;
            CustomerDateOfBirth = customerDateOfBirth;
            CustomerArchive = customerArchive;
            CustomerTown = customerTown;
            CustomerPostcode = customerPostcode;
            CustomerEmail = customerEmail;
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string CustomerForename
        {
            get { return customerForename; }
            set { customerForename = value; }
        }

        public string CustomerSurname
        {
            get { return customerSurname; }
            set { customerSurname = value; }
        }

        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        public string CustomerTelNo
        {
            get { return customerTelNo; }
            set { customerTelNo = value; }
        }
        public DateTime CustomerDateOfBirth
        {
            get { return customerDateOfBirth; }
            set { customerDateOfBirth = value; }
        }
        public int CustomerArchive
        {
            get { return customerArchive; }
            set { customerArchive = value; }
        }
        public string CustomerTown
        {
            get { return customerTown; }
            set { customerTown = value; }
        }
        public string CustomerPostcode
        {
            get { return customerPostcode; }
            set { customerPostcode = value; }
        }
        public string CustomerEmail
        {
            get { return customerEmail; }
            set { customerEmail = value; }
        }
    }
}
