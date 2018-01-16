using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Evenimente;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace DDD
{
    public class WriteRepository
    {
        public void SalvareEvenimente(Eveniment evenimenteNoi)
        {
            string id = evenimenteNoi.Id.ToString();
            string detalii = JsonConvert.SerializeObject(evenimenteNoi);
            var tipEveniment = evenimenteNoi.Tip;
            var idRadacina = evenimenteNoi.IdRadacina.ToString();
            using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\"+
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                string sql = @"INSERT INTO [dbo].[Evenimente] VALUES (@id,@detalii,@tipEveniment,@idRad)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@detalii",detalii);
                cmd.Parameters.AddWithValue("@tipEveniment", tipEveniment.ToString());
                cmd.Parameters.AddWithValue("@idRad", idRadacina);
                con.Open();
                int nrinreg = cmd.ExecuteNonQuery();             
            }
        }
        public void ActualizareEvenimente(Eveniment evenimenteNoi)
        {

        }

        public void StergeEvenimente(Eveniment evenimenteNoi)
        {

        }
    }
}
