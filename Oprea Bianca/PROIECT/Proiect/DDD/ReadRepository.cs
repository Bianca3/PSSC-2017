using DDD.Evenimente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;
using System.Text.RegularExpressions;
using System.Data;

namespace DDD
{
    public class ReadRepository
    {
        Carte carte = new Carte();
        static List<string> Titluri = new List<string>();
        static List<string> ListId = new List<string>();
  //      List<Carte> carti = new List<Carte>();
        public string Cauta(string nume)
        {
            var titlu = Titluri.Find(t => t.Equals(nume));
            return titlu;
        }
        public List<string> TitluCarti()
        {
            string detalii, titlu="";
            string[] detaliiEv;
            using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\" +
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
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
                    titlu = detaliiEv[21];
                    Titluri.Add(titlu);                   
                }
                return Titluri;
            }
        }

        public Stare Stoc()
        {
            return carte.stare1;
        }

        public List<string> GetId()
        {
            string ID = "";
            using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
               @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\" +
               @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                string sql_select = @"SELECT * FROM [dbo].[Evenimente]";
                var cmds = new SqlCommand(sql_select, con);
                con.Open();
                SqlDataReader reader = cmds.ExecuteReader();
                while (reader.Read())
                {
                    ID = reader.GetString(3);
                    ListId.Add(ID);
                }
                ListId = ListId.Distinct().ToList();
                reader.Close();
                con.Close();
                return ListId;
            }
        }
        public Carte CartiDetalii(string titlu)
        {
            string detalii;
            string[] detaliiEv;
            List<string> ListID = new List<string>();
            ListID = GetId();
            using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Bianca\Documents\Proiectarea sistemelor software complexe\Proiect\Proiect\" +
                @"WebMvcLibrarie\App_Data\DatabaseCarti.mdf';Integrated Security=True"))
            {
                //         string sql_select = @"SELECT * FROM [dbo].[Evenimente] where [TipEveniment]=@tip";
                //         var cmds = new SqlCommand(sql_select, con);

                //         cmds.Parameters.AddWithValue("@tip", TipEveniment.AdaugareCarte.ToString());
                //         con.Open();
                //         SqlDataReader reader = cmds.ExecuteReader();
                //         while (reader.Read())
                //         {
                //             detalii = reader.GetString(1);
                //             detalii = JsonConvert.DeserializeObject(detalii).ToString();
                ////             carte = JsonConvert.DeserializeAnonymousType<Carte>(detalii, new Carte());
                //             detaliiEv = detalii.Split('"');
                //             carte.titlu = new Text(detaliiEv[21]);
                //             if(titlu.Equals(carte.titlu.Nume))
                //             {
                //                 carte.Id = new Text(detaliiEv[9]);
                //                 carte.Nr = new ISSN(detaliiEv[15]);
                //                 carte.autor = new Text(detaliiEv[27]);
                //                 carte.an = new Text(detaliiEv[33]);
                //                 string stare1 = Regex.Match(detaliiEv[36], @"\d+").Value;
                //                 string stare2 = Regex.Match(detaliiEv[38], @"\d+").Value;
                //                 string gent = Regex.Match(detaliiEv[40], @"\d+").Value;
                //                 string genc = Regex.Match(detaliiEv[42], @"\d+").Value;
                //                 carte.stare1 = (Stare)Enum.ToObject(typeof(Stare), Convert.ToInt32(stare1));
                //                 carte.stare2 = (Stare)Enum.ToObject(typeof(Stare), Convert.ToInt32(stare2));
                //                 carte.gent = (Gen_tip)Enum.ToObject(typeof(Gen_tip), Convert.ToInt32(gent));
                //                 carte.genc = (Gen_continut)Enum.ToObject(typeof(Gen_continut), Convert.ToInt32(genc));
                //             }
                //         }
                //         return carte;
                foreach (string id in ListID)
                {
                    string sql_select = @"select row_number() over (partition by IdRadacina order by IdRadacina) 
                    as nr, Detalii from Evenimente where IdRadacina = @idRad order by nr desc";
                    var cmds = new SqlCommand(sql_select, con);
                    cmds.Parameters.AddWithValue("@idRad", id);
                    if (con.State.Equals(ConnectionState.Closed))
                        con.Open();
                    SqlDataReader reader = cmds.ExecuteReader();
                    if (reader.Read())
                    {
                        detalii = reader.GetString(1);
                        detalii = JsonConvert.DeserializeObject(detalii).ToString();
                        detaliiEv = detalii.Split('"');
                        carte.titlu = new Text(detaliiEv[21]);
                        if (titlu.Equals(carte.titlu.Nume))
                        {
                            carte.Id = new Text(detaliiEv[9]);
                            carte.Nr = new ISSN(detaliiEv[15]);
                            carte.autor = new Text(detaliiEv[27]);
                            carte.an = new Text(detaliiEv[33]);
                            string stare1 = Regex.Match(detaliiEv[36], @"\d+").Value;
                            string stare2 = Regex.Match(detaliiEv[38], @"\d+").Value;
                            string gent = Regex.Match(detaliiEv[40], @"\d+").Value;
                            string genc = Regex.Match(detaliiEv[42], @"\d+").Value;
                            carte.stare1 = (Stare)Enum.ToObject(typeof(Stare), Convert.ToInt32(stare1));
                            carte.stare2 = (Stare)Enum.ToObject(typeof(Stare), Convert.ToInt32(stare2));
                            carte.gent = (Gen_tip)Enum.ToObject(typeof(Gen_tip), Convert.ToInt32(gent));
                            carte.genc = (Gen_continut)Enum.ToObject(typeof(Gen_continut), Convert.ToInt32(genc));
                            break;
                        }
                    }
                    reader.Close();                  
                }
                con.Close();
            }
            return carte;
        }
    }
}