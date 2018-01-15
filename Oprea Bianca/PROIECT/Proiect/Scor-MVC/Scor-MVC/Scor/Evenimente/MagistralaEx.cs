using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Evenimente
{
	public static class MagistralaEx
	{
		public static void InregistreazaProcesatoareStandard(this MagistralaEvenimente magistrala)
		{
			magistrala.InregistreazaProcesator(TipEveniment.GolMarcat, new ProcesatorGolMarcat());
			magistrala.InregistreazaProcesator(TipEveniment.TerminareMeci, new ProcesatorTerminareMeci());
		}
	}
}
