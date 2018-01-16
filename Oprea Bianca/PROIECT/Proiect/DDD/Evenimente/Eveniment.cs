using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Modele;

namespace DDD.Evenimente
{
    public class Eveniment
    {
        public Guid Id { get; private set; }
        public Text IdRadacina { get; private set; }
        public TipEveniment Tip;
        public object Detalii { get; private set; }

        public Eveniment(Text idRadacina, TipEveniment tipEveniment, object detalii)
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
        public class EvenimentGeneric<T> : Eveniment
        {
            public EvenimentGeneric(Text idRadacina, TipEveniment tipEveniment, T detalii)
                : base(idRadacina, tipEveniment, detalii)
            {
            }

            public new T Detalii { get { return (T)base.Detalii; } }
        }
    }
}
