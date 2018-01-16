using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Evenimente
{
    public class ProcesatorCumparare : ProcesatorEveniment
    {
        public override void Proceseaza(Eveniment e)
        {
            var repo = new WriteRepository();
            repo.SalvareEvenimente(e);
            var send = new Sender();
            bool trimis = send.Trimite(e);
        }
    }
}
