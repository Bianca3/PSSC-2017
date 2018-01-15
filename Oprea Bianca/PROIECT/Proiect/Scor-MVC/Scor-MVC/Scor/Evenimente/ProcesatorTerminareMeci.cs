using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Evenimente
{
	public class ProcesatorTerminareMeci : ProcesatorEveniment
	{
		public override void Proceseaza(Eveniment e)
		{
			var ev = e.ToGeneric<MeciTerminatDto>();
			//actualizare clasament
		}
	}
}
