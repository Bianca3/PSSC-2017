using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Modele.Evenimente
{
    internal class EvenimentNecunoscutExceptie : Exception
    {
        public EvenimentNecunoscutExceptie()
        {
            ToString();
        }
        public override string ToString()
        {
            Exception ex = new Exception("Eveniment necunoscut");
            return ex.ToString();
        }
    }
}
