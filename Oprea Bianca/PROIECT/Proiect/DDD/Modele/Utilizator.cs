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

        public Utilizator()
        { }
        public Utilizator(Text nume, string parola, AdresaContact adr)
        {
            Contract.Requires(nume != null, "Numele utilizatorului necesar nenul");
            Contract.Requires(adr != null, "Adresa de contact necesar nenula");
            this.nume = nume;
            this.parola = parola;
            adresa_contact = adr;
        }
        public override int GetHashCode()
        {
            return parola.GetHashCode();
        }
    }
}
