using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public class ProcesatorTerminareMeci : ProcesatorComandaGeneric<ComandaTerminareMeci>
	{
		public override void Proceseaza(ComandaTerminareMeci comanda)
		{
			var repo = new WriteRepository();
			var meci = repo.GasesteMeci(comanda.IdMeci);
			meci.TerminareMeci();
			repo.SalvareEvenimente(meci);
		}
	}
}
