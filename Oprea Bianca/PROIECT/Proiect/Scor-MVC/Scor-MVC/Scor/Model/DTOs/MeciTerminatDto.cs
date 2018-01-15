using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Model.DTOs
{
	public class MeciTerminatDto
	{
		public DateTime DataTerminare { get; set; }
		public string Invingator { get; set; }
		public string Echipa1 { get; set; }
		public string Echipa2 { get; set; }
	}
}
