using Scor.Comenzi;
using Scor.Evenimente;
using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor
{
	class Program
	{
		static void Main(string[] args)
		{
			//configurare infrastructura
			MagistralaComenzi.Instanta.Value.InregistreazaProcesatoareStandard();
			MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
			MagistralaEvenimente.Instanta.Value.InchideInregistrarea();

			//-----------------------------------------------------
			//Programarea meciului face parte din alt domeniu, aici este doar simulata
			var writeRepository = new WriteRepository();
			var meciNouDto = new MeciDto()
			{
				Id = Guid.NewGuid(),
				Data = DateTime.Now,
				Echipa1 = "Poli",
				Echipa2 = "FCSB"
			};
			writeRepository.ProgrameazaMeci(meciNouDto);

			//---------------------------------------------------
			// selectare meci
			var readRepository = new ReadRepository();
			var meciSelectat = readRepository.ObtineMeciuri().FirstOrDefault();
			Console.WriteLine(meciSelectat);

			// marcheaza meci ca inceput
			var comandaStartMeci = new ComandaStartMeci() { Meci = meciSelectat };
			MagistralaComenzi.Instanta.Value.Trimite(comandaStartMeci);

			//inscrie gol
			var comandaGolMarcat = new ComandaGolMarcat() {
										IdMeci = meciSelectat.Id,
										GolMarcat = new GolMarcatDto() { NumeEchipa = "Poli" }
			};
			MagistralaComenzi.Instanta.Value.Trimite(comandaGolMarcat);

			//afisare sumar meci
			var meciActualizat = readRepository.CautMeci(meciSelectat.Id);
			Console.WriteLine(meciActualizat);

			var terminareMeci = new ComandaTerminareMeci() { IdMeci = meciSelectat.Id};
			MagistralaComenzi.Instanta.Value.Trimite(terminareMeci);

			//incheie meci
			//TODO implementare terminare meci si actualizare clasament

			Console.ReadLine();
		}
	}
}
