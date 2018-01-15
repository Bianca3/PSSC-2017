using Scor.Evenimente;
using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scor.Model
{
	public class Meci
	{
		public Guid Id { get; private set; }
		public string Echipa1 { get; private set; }
		public string Echipa2 { get; private set; }
		public DateTime Data { get; private set; }
		public DateTime DataStart { get; private set; }
		public StareMeci Stare { get; private set; }
		public Goluri GoluriEchipa1 { get; private set; }
		public Goluri GoluriEchipa2 { get; private set; }
		//public event EventHandler<Eveniment> EvenimentMeci;
		private readonly List<Eveniment> _evenimenteNoi = new List<Eveniment>();
		public ReadOnlyCollection<Eveniment> EvenimenteNoi { get { return _evenimenteNoi.AsReadOnly(); }
        }

		private MagistralaEvenimente _magistralaEveniment;

		public Meci(MeciDto meciDto, MagistralaEvenimente magistrala = null)
		{
			_magistralaEveniment = magistrala;
			if (meciDto.Id == Guid.Empty) throw new Exception("Id meci invalid");
			var e = new EvenimentGeneric<MeciDto>(meciDto.Id, TipEveniment.ProgramareMeci, meciDto);
			Aplica(e);
			PublicaEveniment(e);
		}

		public Meci(IEnumerable<Eveniment> evenimente)
		{
			foreach(var e in evenimente)
			{
				RedaEveniment(e);
			}
		}

		public void Start()
		{
			var e = new EvenimentGeneric<DateTime>(Id, TipEveniment.StartMeci, DateTime.Now);
			Aplica(e);
			PublicaEveniment(e);
		}

		public void Marcheaza(GolMarcatDto golMarcat)
		{
			golMarcat.Minut = (byte)Math.Ceiling((DateTime.Now - DataStart).TotalMinutes);
			var eveniment = new EvenimentGeneric<GolMarcatDto>(Id, TipEveniment.GolMarcat, golMarcat);
			Aplica(eveniment);
			PublicaEveniment(eveniment);
		}

		#region procesare evenimente
		private void Aplica(EvenimentGeneric<MeciDto> e)
		{
			Id = e.Detalii.Id;
			Echipa1 = e.Detalii.Echipa1;
			Echipa2 = e.Detalii.Echipa2;
			Data = e.Detalii.Data;
			GoluriEchipa1 = new Goluri();
			GoluriEchipa2 = new Goluri();
			Stare = StareMeci.Programat;
		}

		private void Aplica(EvenimentGeneric<DateTime> e)
		{
			if (Stare != StareMeci.Programat) throw new InvalidOperationException("Meciul nu este in starea corecta");
			DataStart = e.Detalii;
			Stare = StareMeci.InDesfasurare;
		}

		private void Aplica(EvenimentGeneric<GolMarcatDto> e)
		{
			if (Stare != StareMeci.InDesfasurare) throw new InvalidOperationException("Nu se poate marca intr-un meci care nu e in desfasurare.");

			if (Echipa1 == e.Detalii.NumeEchipa)
			{
				GoluriEchipa1 = new Goluri(GoluriEchipa1.NumarGoluri + 1);
			}
			else if (Echipa2 == e.Detalii.NumeEchipa)
			{
				GoluriEchipa2 = new Goluri(GoluriEchipa2.NumarGoluri + 1);
			}
		}
		#endregion procesare evenimente

		private void RedaEveniment(Eveniment e)
		{
			switch (e.Tip)
			{
				case TipEveniment.ProgramareMeci:
					Aplica(e.ToGeneric<MeciDto>());
					break;
				case TipEveniment.StartMeci:
					Aplica(e.ToGeneric<DateTime>());
					break;
				case TipEveniment.GolMarcat:
					Aplica(e.ToGeneric<GolMarcatDto>());
					break;
				case TipEveniment.TerminareMeci:
					Aplica(e.ToGeneric<MeciTerminatDto>());
					break;
				default:
					throw new EvenimentNecunoscutException();
			}
		}

		public void TerminareMeci()
		{
			var meciTerminat = new MeciTerminatDto();
			meciTerminat.DataTerminare = DateTime.Now;
			meciTerminat.Echipa1 = Echipa1;
			meciTerminat.Echipa2 = Echipa2;
			if (GoluriEchipa1.NumarGoluri > GoluriEchipa2.NumarGoluri)
			{
				meciTerminat.Invingator = Echipa1;
			}
			else if (GoluriEchipa2.NumarGoluri > GoluriEchipa1.NumarGoluri)
			{
				meciTerminat.Invingator = Echipa2;
			}
			var e = new EvenimentGeneric<MeciTerminatDto>(Id, TipEveniment.TerminareMeci, meciTerminat);
			Aplica(e);
			PublicaEveniment(e);
		}

		private void Aplica(EvenimentGeneric<MeciTerminatDto> e)
		{
			if (Stare != StareMeci.InDesfasurare) throw new InvalidOperationException();
			Stare = StareMeci.Terminat;
		}

		protected void PublicaEveniment(Eveniment eveniment)
		{
			
			_evenimenteNoi.Add(eveniment);
			//EvenimentMeci?.Invoke(this, eveniment);

			if (_magistralaEveniment != null)
			{
				_magistralaEveniment.Trimite(eveniment);
			}
			else
			{
				MagistralaEvenimente.Instanta.Value.Trimite(eveniment);
			}
		}
	}
}