using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public class ProcesatorStartMeci:ProcesatorComandaGeneric<ComandaStartMeci>
	{
		private string _rootPath;

		public ProcesatorStartMeci()
		{
			_rootPath = "";
		}

		public ProcesatorStartMeci(string rootPath)
		{
			_rootPath = rootPath;
		}

		public override void Proceseaza(ComandaStartMeci comanda)
		{
			var repo = new WriteRepository(_rootPath);
			var meci = repo.GasesteMeci(comanda.Meci.Id);
			meci.Start();
			repo.SalvareEvenimente(meci);
		}
	}
}
