using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Cosmetics_Booking_System_21_09_21.Objects
{
    public class Database
    {
        private SqlCommand cmd;
        private SqlConnection conn;
        private SqlDataReader rdr;

        public Database() { }

        public bool connect()
        {
            SqlConnectionStringBuilder scStrBuilder = new SqlConnectionStringBuilder();

            string str = Application.StartupPath;
            str = str.Remove((Convert.ToInt16(str.Length) - 9));

            scStrBuilder.DataSource = "(LocalDB)\\MSSQLLocalDB";
            scStrBuilder.AttachDBFilename = str + "Bookings.mdf";
            scStrBuilder.IntegratedSecurity = true;

            conn = new SqlConnection(scStrBuilder.ToString());
            try
            {
                conn.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            if (conn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SqlCommand Cmd
        {
            get { return cmd; }
            set { cmd = value; }
        }

        public SqlConnection Conn
        {
            get { return conn; }
            set { conn = value; }
        }

        public SqlDataReader Rdr
        {
            get { return rdr; }
            set { rdr = value; }
        }
    }
}

