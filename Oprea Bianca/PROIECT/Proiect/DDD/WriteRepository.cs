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
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\"+
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                string sql = @"INSERT INTO [dbo].[Evenimente](Id,TipEveniment,Detalii,IdRadacina)" +
                      "VALUES (@id,@tipEveniment,@detalii,@idRad)";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@id", SqlDbType.VarChar))
                    .Value = id;
                cmd.Parameters
                    .Add(new SqlParameter("@tipEveniment", SqlDbType.VarChar))
                    .Value = tipEveniment;
                cmd.Parameters
                    .Add(new SqlParameter("@detalii", SqlDbType.VarChar))
                    .Value = detalii;
                cmd.Parameters
                    .Add(new SqlParameter("@idRad", SqlDbType.VarChar))
                    .Value = idRadacina;
                cn.Open();
                int nrinreg = cmd.ExecuteNonQuery();             
            }
        }
    }
}
