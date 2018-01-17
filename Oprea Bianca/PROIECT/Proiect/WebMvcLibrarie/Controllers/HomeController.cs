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
            titluri = titluri.Distinct().ToList(); 

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
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<SelectListItem> listItems2 = new List<SelectListItem>();
            foreach (Gen_tip gen in Enum.GetValues(typeof(Gen_tip)))
            {
                SelectListItem _sli = new SelectListItem()
                {
                    Text = gen.ToString()
                };
                listItems.Add(_sli);
            }
            foreach (Gen_continut gen in Enum.GetValues(typeof(Gen_continut)))
            {
                SelectListItem _sli = new SelectListItem()
                {
                    Text = gen.ToString()
                };
                listItems2.Add(_sli);
            }
            ViewBag.GenTOpt = listItems;
            ViewBag.GenCOpt = listItems2;
            if (carte.Id == null)
            {
                return View("VAdaugareCarte");
            }
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
            Carte carte = new Carte();
           
            string NumeCarte = Request["NumeCarte"];
            carte.titlu = new Text(NumeCarte);
            var cmdCauta = new ComandaCautare();
            cmdCauta.carte = carte;
            MagistralaComenzi.Instanta.Value.Trimite(cmdCauta);
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
            mc.Id = carte.Id.Nume;
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

        public ActionResult Cumpara()
        {
            //if ()
            //    ViewBag.Message = "Autentif.";
            //    return View("VInreg");
            //else
            string titlu = Request["titlu"];
            string autor = Request["autor"];
            string id = Request["id"];
            string an = Request["an"];
            string nr = Request["nr"];
            string gent = Request["gent"];
            string genc = Request["genc"];
            string st1 = Request["st1"];
            string st2 = Request["st2"];
            Gen_tip Gent = (Gen_tip)Enum.Parse(typeof(Gen_tip), gent);            
            Gen_continut Genc = (Gen_continut)Enum.Parse(typeof(Gen_tip), gent);
            Carte c = new Carte(new Text(id), new ISSN(nr), new Text(titlu),
                new Text(autor), new Text(an), Gent, Genc, new Utilizator());
            c.stare1 = (Stare)Enum.Parse(typeof(Stare),st1);
            c.stare2 = (Stare)Enum.Parse(typeof(Stare), st2);
            var cmdCumpar = new ComandaCumparare();
            cmdCumpar.carte = c;
            MagistralaComenzi.Instanta.Value.Trimite(cmdCumpar);
            Receiver recv = new Receiver();
            string msg = recv.Citeste();
            ViewBag.EvenimentMsg = msg;

            //        ViewBag.stare1 = c.stare1;
            return View("VEventMsg");
        }

        public ActionResult Imprumuta()
        {
            string titlu = Request["titlu"];
            string autor = Request["autor"];
            string id = Request["id"];
            string an = Request["an"];
            string nr = Request["nr"];
            string gent = Request["gent"];
            string genc = Request["genc"];
            string st1 = Request["st1"];
            string st2 = Request["st2"];
            Gen_tip Gent = (Gen_tip)Enum.Parse(typeof(Gen_tip), gent);
            Gen_continut Genc = (Gen_continut)Enum.Parse(typeof(Gen_tip), gent);
            Carte c = new Carte(new Text(id), new ISSN(nr), new Text(titlu),
                new Text(autor), new Text(an), Gent, Genc, new Utilizator());
            c.stare1 = (Stare)Enum.Parse(typeof(Stare), st1);
            c.stare2 = (Stare)Enum.Parse(typeof(Stare), st2);
            var cmdImprumut = new ComandaImprumutare();
            cmdImprumut.carte = c;
            MagistralaComenzi.Instanta.Value.Trimite(cmdImprumut);
            //       ViewBag.stare2 = c.stare2;
            Receiver recv = new Receiver();
            string msg = recv.Citeste();
            ViewBag.EvenimentMsg = msg;
            return View("VEventMsg");
        }
        public ActionResult Restituie()
        {
            string titlu = Request["titlu"];
            string autor = Request["autor"];
            string id = Request["id"];
            string an = Request["an"];
            string nr = Request["nr"];
            string gent = Request["gent"];
            string genc = Request["genc"];
            string st1 = Request["st1"];
            string st2 = Request["st2"];
            Gen_tip Gent = (Gen_tip)Enum.Parse(typeof(Gen_tip), gent);
            Gen_continut Genc = (Gen_continut)Enum.Parse(typeof(Gen_tip), gent);
            Carte c = new Carte(new Text(id), new ISSN(nr), new Text(titlu),
                new Text(autor), new Text(an), Gent, Genc, new Utilizator());
            c.stare1 = (Stare)Enum.Parse(typeof(Stare), st1);
            c.stare2 = (Stare)Enum.Parse(typeof(Stare), st2);
            var cmdRestituie = new ComandaRestituire();
            cmdRestituie.carte = c;
            MagistralaComenzi.Instanta.Value.Trimite(cmdRestituie);
            Receiver recv = new Receiver();
            string msg = recv.Citeste();
            ViewBag.EvenimentMsg = msg;
            return View("VEventMsg");
        }

        public ActionResult Contact()
        {
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