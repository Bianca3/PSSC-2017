using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Scor.Model;
using Scor.Evenimente;

namespace Scor
{
	public class WriteRepository:GenericFileRepository
	{
		public WriteRepository()
			: base("")
		{

		}

		public WriteRepository(string rootPath)
			:base(rootPath)
		{
		}

		public Meci ProgrameazaMeci(MeciDto meci)
		{
			var meciNou = new Meci(meci);
			SalvareEvenimente(meciNou);
			SalavareMeciInListaMeciuri(meci);
			return meciNou;
		}

		public void SalvareEvenimente(Meci meci)
		{
			SalvareEvenimente(meci.EvenimenteNoi);
		}

		public Meci GasesteMeci(Guid idMeci)
		{
			//load events
			var evenimenteMeci = IncarcaListaDeEvenimente()
									.Where(e => e.IdRadacina == idMeci);
			
			//creare meci din evenimente
			return new Meci(evenimenteMeci);
		}

		public MeciDto GasesteSumarMeci(Guid meciId)
		{
			var lista = IncarcaListaDeMeciuri();
			var meciInLista = lista.Where(m => m.Id == meciId).First();
			return meciInLista;
		}

		public void ActualizareMeciInLista(MeciDto meci)
		{
			var lista = IncarcaListaDeMeciuri();
			var meciInLista = lista.Where(m => m.Id == meci.Id).First();
			meciInLista.GoluriEchipa1 = meci.GoluriEchipa1;
			meciInLista.GoluriEchipa2 = meci.GoluriEchipa2;
			SalvareListaMeciuri(lista);
		}

		private void SalavareMeciInListaMeciuri(MeciDto meci)
		{
			List<MeciDto> toateMeciurile = IncarcaListaDeMeciuri();
			toateMeciurile.Add(meci);
			SalvareListaMeciuri(toateMeciurile);
		}

		private void SalvareListaMeciuri(List<MeciDto> toateMeciurile)
		{
			ScrieContinutFisier("meciuri.json", JsonConvert.SerializeObject(toateMeciurile));
		}

		private List<MeciDto> IncarcaListaDeMeciuri()
		{
			List<MeciDto> toateMeciurile = new List<MeciDto>();
			if (ExistaFisier("meciuri.json"))
			{
				toateMeciurile = JsonConvert.DeserializeObject<List<MeciDto>>(CitesteContinutFisier("meciuri.json"));
			}
			return toateMeciurile;
		}

		private void SalvareEvenimente(ReadOnlyCollection<Eveniment> evenimenteNoi)
		{
			List<Eveniment> toateEvenimentele = IncarcaListaDeEvenimente();
			toateEvenimentele.AddRange(evenimenteNoi);
			ScrieContinutFisier("log.json", JsonConvert.SerializeObject(toateEvenimentele));
		}

		private List<Eveniment> IncarcaListaDeEvenimente()
		{
			List<Eveniment> toateEvenimentele = new List<Eveniment>();
			if (ExistaFisier("log.json"))
			{
				toateEvenimentele = JsonConvert.DeserializeObject<List<Eveniment>>(CitesteContinutFisier("log.json"));
			}
			return toateEvenimentele;
		}
	}
}
