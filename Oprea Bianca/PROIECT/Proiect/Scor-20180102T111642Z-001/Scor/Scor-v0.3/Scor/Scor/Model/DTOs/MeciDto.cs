using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Model.DTOs
{
	public class MeciDto
	{
		public Guid Id { get; set; }
		public string Echipa1 { get; set; }
		public string Echipa2 { get; set; }
		public DateTime Data { get; set; }
		public int GoluriEchipa1 { get; set; }
		public int GoluriEchipa2 { get; set; }

		public override string ToString()
		{
			return string.Format("{0}({2}) - {1}({3})", Echipa1, Echipa2, GoluriEchipa1, GoluriEchipa2);
		}
	}
}
