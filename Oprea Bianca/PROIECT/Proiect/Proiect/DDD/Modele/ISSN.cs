using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Modele
{
    public class ISSN
    {
        private string nr;
        public string Numar
        {
            get { return nr; }
        }
        public ISSN(string nr)
        {
            Contract.Requires(nr != null, "Numarul necesar nenul");
            Contract.Requires(!string.IsNullOrEmpty(nr), "Numarul necesita caractere");
            this.nr = nr;
        }
        public override string ToString()
        {
            return nr.ToString();
        }
        public override int GetHashCode()
        {
            return Numar.GetHashCode();
        }
    }
}
