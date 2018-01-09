using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcLibrarie
{
    public class DDDConfig
    {
        public static void Config()
        {
            //configurare infrastructura
            //MagistralaComenzi.Instanta.Value.InregistreazaProcesatoareStandard();
            //---------------------------------------------------------------------
            //var procesatorStartMeci = new ProcesatorStartMeci(RootPath);
            //MagistralaComenzi.Instanta.Value.InregistreazaProcesator<ComandaStartMeci>(procesatorStartMeci);
            //var procesatorGolMarcat = new Scor.Comenzi.ProcesatorGolMarcat(RootPath);
            //MagistralaComenzi.Instanta.Value.InregistreazaProcesator(procesatorGolMarcat);
            //var procesatorEvenimentGolMarcat = new Scor.Evenimente.ProcesatorGolMarcat(RootPath);
            //MagistralaEvenimente.Instanta.Value.InregistreazaProcesator(TipEveniment.GolMarcat, procesatorEvenimentGolMarcat);//;InregistreazaProcesatoareStandard();
            //MagistralaEvenimente.Instanta.Value.InchideInregistrarea();
        }
    }
}