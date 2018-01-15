using Newtonsoft.Json.Linq;
using System;

namespace Scor.Evenimente
{
	public class Eveniment
	{
		public Guid Id { get; private set; }
		public Guid IdRadacina { get; private set; }
		public TipEveniment Tip;
		public object Detalii { get; private set; }

		public Eveniment(Guid idRadacina, TipEveniment tipEveniment, object detalii)
		{
			Tip = tipEveniment;
			IdRadacina = idRadacina;
			Detalii = detalii;
			Id = Guid.NewGuid();
		}

		public EvenimentGeneric<T> ToGeneric<T>()
		{
			EvenimentGeneric<T> eveniment = null;
			if (Detalii is T)
			{
				eveniment = new EvenimentGeneric<T>(this.IdRadacina, this.Tip, (T)Detalii);
			}
			else if (Detalii is JObject)
			{
				var detalii = ((JObject)this.Detalii).ToObject<T>();
				eveniment = new EvenimentGeneric<T>(this.IdRadacina, this.Tip, detalii);
			}
			else
			{
				throw new InvalidCastException();
			}
			return eveniment;
		}
	}

	public class EvenimentGeneric<T>: Eveniment 
	{
		public EvenimentGeneric(Guid idRadacina, TipEveniment tipEveniment, T detalii)
			: base(idRadacina, tipEveniment, detalii)
		{
		}

		public new T Detalii { get => (T)base.Detalii; }
	}
}