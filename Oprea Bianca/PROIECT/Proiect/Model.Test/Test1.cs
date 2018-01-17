using DDD.Evenimente;
using DDD.Modele;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Test
{
    [TestClass]
    public class Test1
    {
        private Mock<ProcesatorEveniment> mockProcesatorAdaugare;
        private MagistralaEvenimente magistrala;
        private Carte carte;
        public Test1()
        {
            carte = new Carte()
            {
                Id = new Text("238s0r-3d3"),
                Nr = new ISSN("384-9ff4-5g"),
                titlu = new Text("Aventurile lui Tom Sawyer"),
                autor = new Text("Mark Twain"),
                an = new Text("1876"),
                gent = Gen_tip.Liric,
                genc = Gen_continut.Aventură
            };
            magistrala = new MagistralaEvenimente();
            magistrala.InregistreazaProcesatoareStandard();
            mockProcesatorAdaugare = new Mock<ProcesatorEveniment>();
            magistrala.InregistreazaProcesator(TipEveniment.AdaugareCarte, mockProcesatorAdaugare.Object);
            magistrala.InchideInregistrarea();
        }

        [TestMethod]
        public void SetareProprietati()
        {
            var c = new Carte(carte, magistrala);
            Assert.AreEqual("238s0r-3d3", c.Id.Nume);
            Assert.AreEqual("384-9ff4-5g", c.Nr.Numar);
            Assert.AreEqual("Aventurile lui Tom Sawyer", c.titlu.Nume);
            Assert.AreEqual("Mark Twain", c.autor.Nume);
            Assert.AreEqual("1876", c.an.Nume);
            Assert.AreEqual(Gen_tip.Liric, c.gent);
            Assert.AreEqual(Gen_continut.Aventură, c.genc);
            Assert.AreEqual(Stare.InStoc, c.stare1);
            Assert.AreEqual(Stare.Disponibila, c.stare2);
            Assert.IsNull(c.utiliz);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void VerifIdCarte()
        {
            var carte = new Carte()
            {
                Nr = new ISSN("384-9ff4-5g"),
                titlu = new Text("Aventurile lui Tom Sawyer"),
                autor = new Text("Mark Twain"),
                an = new Text("1876"),
                gent = Gen_tip.Liric,
                genc = Gen_continut.Aventură
            };
             new Carte(carte, magistrala);
        }

        [TestMethod]
        public void VerifStareImprumut()
        {
            var carte = new Carte(this.carte, magistrala);
            carte.Carte2(this.carte);
            Assert.AreEqual(Stare.Imprumutata, carte.stare2);
        }
        //[TestMethod]
        //public void VerifStareRestituire()
        //{
        //    var carte = new Carte(this.carte, magistrala);
        //    carte.Carte3(this.carte);
        //    Assert.AreEqual(Stare.Disponibila, carte.stare2);
        //}
    }
}
