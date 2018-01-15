using Newtonsoft.Json;
using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor
{
	public class ReadRepository:GenericFileRepository
	{

		public ReadRepository(string rootPath)
			:base (rootPath)
		{
		}

		public ReadRepository()
			:base("")
		{
		}

		public IEnumerable<MeciDto> ObtineMeciuri()
		{
			List<MeciDto> toateMeciurile = new List<MeciDto>();
			if (ExistaFisier("meciuri.json"))
			{
				toateMeciurile = JsonConvert.DeserializeObject<List<MeciDto>>(CitesteContinutFisier("meciuri.json"));
			}
			return toateMeciurile.AsEnumerable();
		}

		public MeciDto CautMeci(Guid id)
		{
			return ObtineMeciuri().Where(m => m.Id == id).FirstOrDefault();
		}
	}
}
