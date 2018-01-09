using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Evenimente;
using System.Collections.ObjectModel;
using static DDD.Evenimente.Eveniment;
using System.Diagnostics.Contracts;

namespace DDD.Modele
{
    public class Carte
    {
        public Text Id { get; set; }
        public ISSN Nr { get; set; }
        public Text titlu { get; set; }
        public Text autor { get; set; }
        public int an { get; set; }
        public Stare stare { get; set; }
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
        public Carte(Text Id, ISSN Nr, Text titlu, Text autor, int an, Gen_tip gent, Gen_continut genc, Utilizator utiliz)
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

        public void Adauga(Carte carte)
        {
            var e = new EvenimentGeneric<Carte>(Id, TipEveniment.AdaugareCarte, carte);
            AplicaAdaug(e);
            PublicaEveniment(e);
        }
        
        public void Imprumuta(Carte carte)
        {

        }
        public void Restituie(Carte carte)
        {

        }
        public void AplicaAdaug(EvenimentGeneric<Carte> e)
        {
            stare = Stare.InStoc;
        }
        private void AplicaCumpar(EvenimentGeneric<Carte> e)
        {
            if (stare != Stare.InStoc)
                throw new InvalidOperationException("Cartea nu este in stoc");

            stare = Stare.InStoc;
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
