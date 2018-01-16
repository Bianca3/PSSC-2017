using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;


namespace DDD.Comenzi
{
    public class ComandaCumparare : Comanda
    {
        public Carte carte { get; set; }
    }
}
