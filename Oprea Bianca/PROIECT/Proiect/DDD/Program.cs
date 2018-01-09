using DDD.Comenzi;
using DDD.Evenimente;
using DDD.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD
{
    public class Program
    {
        static void Main(string[] args)
        {
            //configurare infrastructura
            MagistralaComenzi.Instanta.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InchideInregistrarea();

            var carte = new Carte(new Text("1-a"), new ISSN("12"),new Text("titlu1"), new Text("a1"), 1980, Gen_tip.epic, Gen_continut.Aventură, new Utilizator());
            var cmdAdauga = new ComandaAdaugare();
            cmdAdauga.carte = carte;
            MagistralaComenzi.Instanta.Value.Trimite(cmdAdauga);
            var cmdCauta = new ComandaCautare();
            cmdCauta.carte = carte;
            MagistralaComenzi.Instanta.Value.Trimite(cmdCauta);
            var cmdImprumut = new ComandaImprumutare();
            MagistralaComenzi.Instanta.Value.Trimite(cmdImprumut);
            Console.ReadLine();
        }
    }
}
