using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Evenimente
{
	public class MagistralaEvenimente
	{
		public static readonly Lazy<MagistralaEvenimente> Instanta = new Lazy<MagistralaEvenimente>(()=>new MagistralaEvenimente());
		private readonly Dictionary<TipEveniment, List<ProcesatorEveniment>> registru = new Dictionary<TipEveniment, List<ProcesatorEveniment>>();
		private bool inregistrareDeschisa = true;

		public MagistralaEvenimente()
		{

		}

		public void Trimite(Eveniment eveniment) 
		{
			if (!inregistrareDeschisa)
			{
				if (registru.ContainsKey(eveniment.Tip))
				{
					var lista = registru[eveniment.Tip];
					lista.ForEach(p => p.Proceseaza(eveniment));
				}
				else
				{
					Trace.TraceInformation("Nu exista procesator pentru {0}", eveniment.Tip);
				}
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public void InregistreazaProcesator(TipEveniment tip, ProcesatorEveniment procesator)
		{
			if(inregistrareDeschisa)
			{
				List<ProcesatorEveniment> lista = null;
				if (registru.ContainsKey(tip))
				{
					lista = registru[tip];
				}
				else
				{
					lista = new List<ProcesatorEveniment>();
					registru.Add(tip, lista);
				}
				lista.Add(procesator);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public void InchideInregistrarea()
		{
			inregistrareDeschisa = false;
		}
	}
}
