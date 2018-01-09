using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Modele
{
    public class Text
    {
        private string nume;
        public string Numesimplu
        {
            get { return nume; }
        }
        public Text()
        { }
        public Text(string nume)
        {
            Contract.Requires<ArgumentNullException>(nume != null, "Numele nu poate fi null");
            Contract.Requires(!string.IsNullOrEmpty(nume), "Numele necesita caractere");
            this.nume = nume;
        }

        public override string ToString()
        {
            return nume;
        }
        public override int GetHashCode()
        {
            return Numesimplu.GetHashCode();
        }
    }
}
