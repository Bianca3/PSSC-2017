using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public class MagistralaComenzi
	{
		public static readonly Lazy<MagistralaComenzi> Instanta = new Lazy<MagistralaComenzi>(() => new MagistralaComenzi());

		private readonly Dictionary<Type, ProcesatorComanda> registru = new Dictionary<Type, ProcesatorComanda>();

		private MagistralaComenzi() { }

		public void InregistreazaProcesator<T>(ProcesatorComandaGeneric<T> procesator) where T:Comanda
		{
			registru.Add(typeof(T), procesator);
		}

		public void Trimite<T>(T comanda) where T : Comanda
		{
			var procesator = registru[typeof(T)];
			procesator.Proceseaza(comanda);
		}
	}
}
