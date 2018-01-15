using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Evenimente
{
	public class ProcesatorGolMarcat : ProcesatorEveniment
	{
		public override void Proceseaza(Eveniment e)
		{
			var eGolMarcat = e.ToGeneric<GolMarcatDto>();
			var repo = new WriteRepository();
			var sumarMeci = repo.GasesteSumarMeci(eGolMarcat.IdRadacina);
			if(sumarMeci.Echipa1 == eGolMarcat.Detalii.NumeEchipa)
			{
				sumarMeci.GoluriEchipa1 += 1;
			}
			else if (sumarMeci.Echipa2 == eGolMarcat.Detalii.NumeEchipa)
			{
				sumarMeci.GoluriEchipa2 += 1;
			}
			repo.ActualizareMeciInLista(sumarMeci);
		}
	}
}
