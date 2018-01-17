using DDD.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Comenzi
{
    public class ProcesatorRestituire : ProcesatorComandaGeneric<ComandaRestituire>
    {
        public override void Proceseaza(ComandaRestituire comanda)
        {
            Carte carte = new Carte();
            carte.Restituie(comanda.carte);
        }
    }
}
