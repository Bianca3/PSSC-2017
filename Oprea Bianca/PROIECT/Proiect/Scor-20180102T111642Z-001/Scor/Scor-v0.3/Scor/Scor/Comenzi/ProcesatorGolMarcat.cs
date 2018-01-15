using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public class ProcesatorGolMarcat:ProcesatorComandaGeneric<ComandaGolMarcat>
	{
		public override void Proceseaza(ComandaGolMarcat comanda)
		{
			var repo = new WriteRepository();
			var meci = repo.GasesteMeci(comanda.IdMeci);
			meci.Marcheaza(comanda.GolMarcat);
			repo.SalvareEvenimente(meci);
		}
	}
}
