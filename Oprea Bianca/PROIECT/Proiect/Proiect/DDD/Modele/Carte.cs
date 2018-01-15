using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Evenimente;
using System.Collections.ObjectModel;
using static DDD.Evenimente.Eveniment;
using System.Diagnostics.Contracts;
using DDD.Modele.Evenimente;

namespace DDD.Modele
{
    public class Carte
    {
        public Text Id { get; set; }
        public ISSN Nr { get; set; }
        public Text titlu { get; set; }
        public Text autor { get; set; }
        public Text an { get; set; }
        public Stare stare1 { get; set; }
        public Stare stare2 { get; set; }
        public Gen_tip gent { get; set; }
        public Gen_continut genc { get; set; }
        public Utilizator utiliz { get; set; }

        private readonly List<Eveniment> _evenimenteNoi = new List<Eveniment>();
        public ReadOnlyCollection<Eveniment> EvenimenteNoi
        {
            get { return _evenimenteNoi.AsReadOnly(); }
        }

        private MagistralaEvenimente _magistralaEveniment;
        public Carte() { }
        public Carte(Text Id, ISSN Nr, Text titlu, Text autor, Text an, Gen_tip gent, Gen_continut genc, Utilizator utiliz)
        {
            this.Id = Id;
            this.Nr = Nr;
            this.titlu = titlu;
            this.autor = autor;
            this.an = an;
            this.gent = gent;
            this.genc = genc;
            this.utiliz = utiliz;
        }
        public Carte(IEnumerable<Eveniment> evenimente)
        {
            foreach (var e in evenimente)
            {
                RedaEveniment(e);
            }
        }
        public void Adauga(Carte carte)
        {
            var e = new EvenimentGeneric<Carte>(carte.Id, TipEveniment.AdaugareCarte, carte);
            AplicaAdaug(e);
            PublicaEveniment(e);
        }
        public void Cumpara(Carte carte)
        {
            var e = new EvenimentGeneric<Carte>(carte.Id, TipEveniment.CumparareCarte, carte);
            AplicaCumpar(e);
            PublicaEveniment(e);
        }
        public void Imprumuta(Carte carte)
        {
            var e = new EvenimentGeneric<Carte>(carte.Id, TipEveniment.ImprumutareCarte, carte);
            AplicaImprumut(e);
            PublicaEveniment(e);
        }
        public void Restituie(Carte carte)
        {
            var e = new EvenimentGeneric<Carte>(Id, TipEveniment.RestituireCarte, carte);
            AplicaRestituie(e);
            PublicaEveniment(e);
        }
        public void AplicaAdaug(EvenimentGeneric<Carte> e)
        {
            stare1 = Stare.InStoc;
            stare2 = Stare.Disponibila;
        }
        private void AplicaCumpar(EvenimentGeneric<Carte> e)
        {
            if (stare1 != Stare.InStoc)
                throw new InvalidOperationException("Cartea nu este in stoc");
            stare1 = Stare.InStoc;
        }
        public void AplicaImprumut(EvenimentGeneric<Carte> e)
        {
            if (stare2 != Stare.Imprumutata)
                stare2 = Stare.Imprumutata;
            else
                throw new InvalidOperationException("Cartea nu este disponibila");
        }
        public void AplicaRestituie(EvenimentGeneric<Carte> e)
        {
            stare2 = Stare.Disponibila;
        }
        private void RedaEveniment(Eveniment e)
        {
            switch (e.Tip)
            {
                case TipEveniment.AdaugareCarte:
                    AplicaAdaug(e.ToGeneric<Carte>());
                    break;
                case TipEveniment.CumparareCarte:
                    AplicaCumpar(e.ToGeneric<Carte>());
                    break;
                case TipEveniment.ImprumutareCarte:
                    AplicaImprumut(e.ToGeneric<Carte>());
                    break;
                case TipEveniment.RestituireCarte:
                    AplicaRestituie(e.ToGeneric<Carte>());
                    break;
                default:
                    throw new EvenimentNecunoscutExceptie();
            }
        }
        protected void PublicaEveniment(Eveniment eveniment)
        {

            _evenimenteNoi.Add(eveniment);
            //EvenimentMeci?.Invoke(this, eveniment);
            if (_magistralaEveniment != null)
            {
                _magistralaEveniment.Trimite(eveniment);
            }
            else
            {
                MagistralaEvenimente.Instanta.Value.Trimite(eveniment);
            }
        }
    }
}