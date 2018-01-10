using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;

namespace DDD.Comenzi
{
    public class ProcesatorCumparare : ProcesatorComandaGeneric<ComandaCumparare>
    {
        public override void Proceseaza(ComandaCumparare comanda)
        {
            Carte carte = new Carte();
            carte.Cumpara(comanda.carte);
        }
    }
}
