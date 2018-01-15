using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public static class MagistralaComenziEx
	{
		public static void InregistreazaProcesatoareStandard(this MagistralaComenzi magistrala)
		{
			magistrala.InregistreazaProcesator(new ProcesatorStartMeci());
			magistrala.InregistreazaProcesator(new ProcesatorGolMarcat());
			magistrala.InregistreazaProcesator(new ProcesatorTerminareMeci());
		}
	}
}
