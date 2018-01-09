using DDD.Evenimente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD
{
    public class ReadRepository
    {
        public bool Cauta(string id)
        {
            List<Eveniment> toateEvenimentele = new List<Eveniment>();
            List<Eveniment> evenimenteCitite = new List<Eveniment>();
            //    toateEvenimentele = JsonConvert.DeserializeObject<List<Eveniment>>(detalii);
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\" +
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [dbo].[Evenimente] where Id=@id ";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object detalii = JsonConvert.DeserializeObject<MasinaDes.RootObject>(string.Format("{0}", reader["DetaliiEveniment"]));
                        Eveniment e = new Eveniment(new Guid(), (TipEveniment)Enum.Parse(typeof(TipEveniment), reader["TipEveniment"].ToString()), detalii);
                        evenimenteCitite.Add(e);
                    }
                }
                return toateEvenimentele;
            }
        }
    }
}