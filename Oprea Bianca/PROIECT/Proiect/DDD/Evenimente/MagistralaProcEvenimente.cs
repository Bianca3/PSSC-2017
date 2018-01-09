using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Evenimente
{
    public static class MagistralaProcEvenimente
    {
        public static void InregistreazaProcesatoareStandard(this MagistralaEvenimente magistrala)
        {
            magistrala.InregistreazaProcesator(TipEveniment.AdaugareCarte, new ProcesatorAdaugare());
            magistrala.InregistreazaProcesator(TipEveniment.CumparareCarte, new ProcesatorCumparare());
            magistrala.InregistreazaProcesator(TipEveniment.ImprumutareCarte, new ProcesatorImprumutare());

        }
    }
}
