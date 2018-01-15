using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public abstract class ProcesatorComandaGeneric<T>: ProcesatorComanda where T:Comanda
	{
		public abstract void Proceseaza(T comanda);

		public override void Proceseaza(Comanda comanda)
		{
			Proceseaza((T)comanda);
		}
	}
}
