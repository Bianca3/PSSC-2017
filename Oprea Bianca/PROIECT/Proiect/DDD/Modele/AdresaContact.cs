using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Modele
{
    public class AdresaContact
    {
        private string adresa;
        public string Adresa
        {
            get { return adresa; }
        }
        public AdresaContact(string adresa)
        {
            Contract.Requires<ArgumentNullException>(adresa != null, "Adresa de contact nu poate fi null");
            Contract.Requires<ArgumentException>(adresa.Length >= 5, "Adresa trebuie sa contina minim 5 caractere");
            this.adresa = adresa;
        }
        //public override bool Equals(object obj)
        //{
        //    //    return base.Equals(obj);
        //}
        public override string ToString()
        {
            return adresa;
        }
        public override int GetHashCode()
        {
            return Adresa.GetHashCode();
        }
    }
}
