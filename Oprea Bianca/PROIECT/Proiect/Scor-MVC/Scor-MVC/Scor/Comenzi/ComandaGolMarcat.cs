using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Comenzi
{
	public class ComandaGolMarcat:Comanda
	{
		public Guid IdMeci { get; set; }
		public GolMarcatDto GolMarcat { get; set; }
	}
}
