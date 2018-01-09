using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;

namespace DDD.Comenzi
{
    public class ProcesatorCautare : ProcesatorComandaGeneric<ComandaCautare>
    {
        public override void Proceseaza(ComandaCautare comanda)
        {
            Carte carte = new Carte();
            var repo = new ReadRepository();
            repo.Cauta(carte.Id.ToString());
        }
    }
}
