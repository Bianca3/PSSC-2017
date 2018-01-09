using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebMvcLibrarie.Models
{
    public class MUtilizator
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsValid(string _username, string _password)
        {
  //          SHA1 hash = SHA1.Create();
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
           'C: \Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\
            WebMvcLibrarie\App_Data\Database.mdf' ;Integrated Security=True"))
            {
                string _sql = @"SELECT [Username] FROM [dbo].[System_Users] " +
                       @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, conn);
                cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar)).Value = _username;
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar)).Value =
                                    _password;
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
    }
}