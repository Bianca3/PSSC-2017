using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public class ProcesatorStartMeci:ProcesatorComandaGeneric<ComandaStartMeci>
	{
		public override void Proceseaza(ComandaStartMeci comanda)
		{
			var repo = new WriteRepository();
			var meci = repo.GasesteMeci(comanda.Meci.Id);
			meci.Start();
			repo.SalvareEvenimente(meci);
		}
	}
}
