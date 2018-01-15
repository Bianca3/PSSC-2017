using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;

namespace DDD.Comenzi
{
    public class ProcesatorImprumutare : ProcesatorComandaGeneric<ComandaImprumutare>
    {
        public override void Proceseaza(ComandaImprumutare comanda)
        {
            Carte carte = new Carte();
            carte.Imprumuta(comanda.carte);
        }
    }
}
