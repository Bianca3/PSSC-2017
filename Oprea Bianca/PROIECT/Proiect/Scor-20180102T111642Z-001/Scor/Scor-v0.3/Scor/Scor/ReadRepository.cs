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
	public class ReadRepository
	{
		public IEnumerable<MeciDto> ObtineMeciuri()
		{
			List<MeciDto> toateMeciurile = new List<MeciDto>();
			if (File.Exists("meciuri.json"))
			{
				toateMeciurile = JsonConvert.DeserializeObject<List<MeciDto>>(File.ReadAllText("meciuri.json"));
			}
			return toateMeciurile.AsEnumerable();
		}

		public MeciDto CautMeci(Guid id)
		{
			return ObtineMeciuri().Where(m => m.Id == id).FirstOrDefault();
		}
	}
}
