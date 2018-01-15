using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scor.Model
{
	public class Goluri
	{
		public int NumarGoluri { get; private set;}

		public Goluri()
		{
			NumarGoluri = 0;
		}

		public Goluri(int numarGoluri)
		{
			NumarGoluri = numarGoluri;
		}

		public override bool Equals(object obj)
		{
			var goluri = obj as Goluri;
			return goluri!=null && goluri.NumarGoluri == NumarGoluri;
		}

		public override int GetHashCode()
		{
			return 492230404 + NumarGoluri.GetHashCode();
		}
	}
}
