using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Modele
{
    public class Utilizator
    {
        public Text nume { get; private set; }
        public string parola { get; set; }
        public AdresaContact adresa_contact { get; private set; }
        int imprumut;

        public Utilizator()
        { }
        public Utilizator(Text nume, AdresaContact adr)
        {
            Contract.Requires(nume != null, "Numele utilizatorului necesar nenul");
            Contract.Requires(adr != null, "Adresa de contact necesar nenula");
            this.nume = nume;
            adresa_contact = adr;
        }
    }
}
