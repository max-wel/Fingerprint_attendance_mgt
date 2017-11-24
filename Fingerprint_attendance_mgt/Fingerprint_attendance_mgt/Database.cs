using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace Fingerprint_attendance_mgt
{
    class Database
    {
        private string path, dbName;
        private SqlConnection conn;
        public Database()
        {
            path = Path.GetFullPath(Environment.CurrentDirectory);
            dbName = "Attendance_mgt.mdf";
            
        }

     
        public void dbEnroll(string id,string fullname, string dept, string gender, string pos, string phone, string email, byte[] photo, string fprint)
        {
            using (conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\" + dbName + ";Integrated Security=True;Connect Timeout=30"))
            {
                var cmd = new SqlCommand("INSERT INTO [Staff_Enroll] VALUES (@id, @name, @dept, @gender, @position,@phone, @email, @photo, @fprint)", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", fullname);
                cmd.Parameters.AddWithValue("dept", dept);
                cmd.Parameters.AddWithValue("gender", gender);
                cmd.Parameters.AddWithValue("position", pos);
                cmd.Parameters.AddWithValue("phone", phone);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("photo", photo);
                cmd.Parameters.Add("fprint", SqlDbType.VarChar).Value = fprint;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
