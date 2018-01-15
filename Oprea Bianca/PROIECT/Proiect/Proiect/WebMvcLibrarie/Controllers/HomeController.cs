using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcLibrarie.Models;
using DDD;
using DDD.Evenimente;
using DDD.Comenzi;
using DDD.Modele;

namespace WebMvcLibrarie.Controllers
{
    public class HomeController : Controller
    {
        private ReadRepository _readRepo;
        private WriteRepository _writeRepo;
        
        ReadRepository read = new ReadRepository();
        public HomeController()
        {
            _readRepo = new ReadRepository();
            _writeRepo = new WriteRepository();
        }

        public HomeController(ReadRepository readRepo, WriteRepository writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        public ActionResult Index()
        {
            List<string> titluri = new List<string>();
            titluri = read.TitluCarti();
            //List<MCarte> carti = new List<MCarte>();
            //MCarte mcarte;
            //foreach (string t in titluri)
            //{
            //    mcarte = new MCarte();
            //    mcarte.titlu = t;
            //    carti.Add(mcarte);
            //}
            ViewBag.ListaTitluri = titluri;
            return View();
        }
        public ActionResult Adaugare(MCarte carte)
        {
            if (carte.Id == null)
                return View("VAdaugareCarte");
            else
            {
                Carte c = new Carte(new Text(carte.Id), new ISSN(carte.Nr), new Text(carte.titlu), 
                    new Text(carte.autor), new Text(carte.an), carte.gent, carte.genc, new Utilizator());
                var cmdAdauga = new ComandaAdaugare();
                cmdAdauga.carte = c;
                MagistralaComenzi.Instanta.Value.Trimite(cmdAdauga);
                Receiver recv = new Receiver();
                string msg = recv.Citeste();
                ViewBag.EvenimentMsg = msg;
                return View("VAdaugareCarte");
            }           
        }

        [HttpPost]
        public ActionResult Cautare()
        {
            //var cmdCauta = new ComandaCautare();
            //cmdCauta.carte = carte;
            //MagistralaComenzi.Instanta.Value.Trimite(cmdCauta);
            string NumeCarte = Request["NumeCarte"];
            ViewBag.titluCautat = read.Cauta(NumeCarte);
            return View("VCautareCarti");
        }

        public ActionResult DetaliiCarte(string param)
        {
            string titlu = param;
            ReadRepository read = new ReadRepository();
            Carte carte = new Carte();
            carte = read.CartiDetalii(titlu);
            MCarte mc = new MCarte();
            mc.autor = carte.autor.Nume;
            mc.an = carte.an.Nume;
            mc.Nr = carte.Nr.Numar;
            mc.stare1 = carte.stare1;
            mc.stare2 = carte.stare2;
            mc.gent = carte.gent;
            mc.genc = carte.genc;
            ViewBag.Titlu = titlu;
            ViewBag.Detalii = mc;
            return View("VCarteInf");
        }

        public ActionResult Cumpara(MCarte carte)
        {
            //if ()
            //    ViewBag.Message = "Autentif.";
            //    return View("VInreg");
            //else
            Carte c = new Carte(new Text(carte.Id), new ISSN(carte.Nr), new Text(carte.titlu), 
                new Text(carte.autor), new Text(carte.an), carte.gent, carte.genc, new Utilizator());
            var cmdCumpar = new ComandaCumparare();
            cmdCumpar.carte = c;
            MagistralaComenzi.Instanta.Value.Trimite(cmdCumpar);
    //        ViewBag.stare1 = c.stare1;
            return View("VAutentif");
        }

        public ActionResult Imprumuta(MCarte carte)
        {
            Carte c = new Carte(new Text(carte.Id), new ISSN(carte.Nr), new Text(carte.titlu),
                new Text(carte.autor), new Text(carte.an), carte.gent, carte.genc, new Utilizator());
            var cmdImprumut = new ComandaImprumutare();
            cmdImprumut.carte = c;
            MagistralaComenzi.Instanta.Value.Trimite(cmdImprumut);
     //       ViewBag.stare2 = c.stare2;
            return View("VCarteInf");
        }
        public ActionResult Restituie()
        {
            //var cmdImprumut = new ComandaImprumutare();
            //MagistralaComenzi.Instanta.Value.Trimite(cmdImprumut);

            return View("VCarteInf");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";
            return View("VContact");
        }

        [HttpGet]
        public ActionResult Autentificare()
        {
            ViewBag.Message = "Login";

            return View("VAutentif");
        }

        [HttpPost]
        public ActionResult Login(MUtilizator user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
           //         FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Date Login incorecte!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
     //       FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}