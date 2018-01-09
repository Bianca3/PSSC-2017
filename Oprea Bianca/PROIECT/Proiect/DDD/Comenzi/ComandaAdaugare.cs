using DDD.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Comenzi
{
    public class ComandaAdaugare : Comanda
    {
        public Carte carte { get; set; }
    }
}
