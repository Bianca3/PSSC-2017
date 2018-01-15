using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
	public class MeciModel
	{
		public Guid MeciId { get; set; }

		public string Echipa1 { get; set; }
		public string Echipa2 { get; set; }

		public int Goluri1 { get; set; }
		public int Goluri2 { get; set; }
	}
}