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
    class CustomerDBAccess
    {
        Database db;

        public CustomerDBAccess(Database db)
        {
            this.db = db;
        }
        public void insertCustomer(int CustomerID, string Forename, string Surname, string Address, string TelNo, DateTime DateOfBirth, int Archive, string Town, string Postcode, string Email)
        {
            //try
            //{
                db.Cmd = db.Conn.CreateCommand();
                db.Cmd.CommandText = "INSERT INTO Customer (CustomerId, Forename, Surname, Address, TelNo, DateOfBirth, Archive, Town, Postcode, Email) Values (" + CustomerID + ", '" + Forename + "', '" + Surname + "', '" + Address + "', '" + TelNo + "', '" + DateOfBirth.ToString("yyyy-MM-dd") + "', " + Archive + ",'" + Town + "','" + Postcode + "','"+ Email +"')";
                db.Cmd.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Customer Already Exists");
            //}
        }
        public Customer getCustomersFromReader(SqlDataReader reader)
        {
            Customer resultCustomer = new Customer();

            resultCustomer.CustomerID = db.Rdr.GetInt32(0);
            resultCustomer.CustomerForename = db.Rdr.GetString(1);
            resultCustomer.CustomerSurname = db.Rdr.GetString(2);
            resultCustomer.CustomerAddress = db.Rdr.GetString(3);
            resultCustomer.CustomerTelNo = db.Rdr.GetString(4);
            resultCustomer.CustomerDateOfBirth = db.Rdr.GetDateTime(5);
            resultCustomer.CustomerArchive = db.Rdr.GetInt32(6);
            resultCustomer.CustomerTown = db.Rdr.GetString(7);
            resultCustomer.CustomerPostcode = db.Rdr.GetString(8);
            resultCustomer.CustomerEmail = db.Rdr.GetString(9);

            return resultCustomer;
        }
        public List<Customer> getAllCustomers()
        {
            List<Customer> resultCustomers = new List<Customer>();

            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "SELECT * FROM Customer";
            db.Rdr = db.Cmd.ExecuteReader();

            while (db.Rdr.Read())
            {
                resultCustomers.Add(getCustomersFromReader(db.Rdr));
            }
            db.Rdr.Close();
            return resultCustomers;

        }
        public void updateCustomer(Customer updateCustomer)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = String.Format("UPDATE Customer SET Forename = '{0}', Surname = '{1}', Address = '{2}', TelNo = '{3}', DateOfBirth = '{4}', Archive = '{5}', Town = '{6}', Postcode = '{7}', Email = '{8}' WHERE CustomerID = '{9}'", updateCustomer.CustomerForename, updateCustomer.CustomerSurname, updateCustomer.CustomerAddress, updateCustomer.CustomerTelNo, updateCustomer.CustomerDateOfBirth.ToString("yyyy-MM-dd"), updateCustomer.CustomerArchive, updateCustomer.CustomerTown, updateCustomer.CustomerPostcode, updateCustomer.CustomerEmail, updateCustomer.CustomerID);
            db.Cmd.ExecuteNonQuery();
        }
        public void deleteCustomer(int id)
        {
            db.Cmd = db.Conn.CreateCommand();
            db.Cmd.CommandText = "DELETE FROM Customer WHERE CustomerID= '" + id + "'";
            db.Cmd.ExecuteNonQuery();
        }
    }
}
