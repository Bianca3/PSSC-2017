using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Comenzi
{
    public static class MagistralaProcComenzi
    {
        public static void InregistreazaProcesatoareStandard(this MagistralaComenzi magistrala)
        {
            //magistrala.InregistreazaProcesator(new ProcesatorCumparare());
            //magistrala.InregistreazaProcesator(new ProcesatorCautare());
            //magistrala.InregistreazaProcesator(new ProcesatorImprumutare());
            magistrala.InregistreazaProcesator(new ProcesatorAdaugare());
        }
    }
}
