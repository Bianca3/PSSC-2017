using DDD.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Comenzi
{
    public class ProcesatorAdaugare : ProcesatorComandaGeneric<ComandaAdaugare>
    {
        public override void Proceseaza(ComandaAdaugare comanda)
        {
            Carte carte = new Carte();
            carte.Adauga(comanda.carte);
        }
    }
}
