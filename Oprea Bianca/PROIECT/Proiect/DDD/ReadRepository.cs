using DDD.Evenimente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;

namespace DDD
{
    public class ReadRepository
    {
        Carte carte = new Carte();
        List<string> Titluri = new List<string>();
        public Carte Cauta(string nume)
        {
            string detalii, tipeveniment;
            using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\" +
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                string sql = @"SELECT count(*) FROM [dbo].[Evenimente] where Id=@id ";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", nume);
                con.Open();
                int nrinreg = cmd.ExecuteNonQuery();
                if (nrinreg > 0)
                {
                    string sql_select = @"SELECT * FROM [dbo].[Evenimente] where Id=@id ";
                    var cmds = new SqlCommand(sql_select, con);
                    cmds.Parameters.AddWithValue("@id", nume);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        detalii = reader.GetString(1);
                        tipeveniment = reader.GetString(2);
                        if (tipeveniment.Equals(TipEveniment.AdaugareCarte))
                            detalii = JsonConvert.DeserializeObject(detalii).ToString();
                    }
                    return carte;
                }
                else
                    return null;
            }
        }
        public List<string> Cauta()
        {
            string detalii, titlu="";
            string[] detaliiEv;
            using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\" +
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                //string sql = @"SELECT count(*) FROM [dbo].[Evenimente] where [TipEveniment]=@tip";
                //var cmd = new SqlCommand(sql, con);
                //cmd.Parameters.AddWithValue("@tip", "AdaugareCarte");
                //con.Open();
                //int nrinreg = cmd.ExecuteNonQuery();
                //if (nrinreg > 0)
                //{
                    string sql_select = @"SELECT * FROM [dbo].[Evenimente] where [TipEveniment]=@tip";
                    var cmds = new SqlCommand(sql_select, con);
                    cmds.Parameters.AddWithValue("@tip", TipEveniment.AdaugareCarte.ToString());
                    con.Open();
                    SqlDataReader reader = cmds.ExecuteReader();
                    while (reader.Read())
                    {
                        detalii = reader.GetString(1);
                        detalii = JsonConvert.DeserializeObject(detalii).ToString();
                        detaliiEv = detalii.Split('"');
                        titlu = detaliiEv[19];
                        Titluri.Add(titlu);
                    }
                    return Titluri;
                //}
                //else
                //    return null;
            }
        }
    }
}