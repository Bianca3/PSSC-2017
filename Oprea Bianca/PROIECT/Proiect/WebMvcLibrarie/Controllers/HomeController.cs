using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcLibrarie.Models;
using DDD;
using DDD.Evenimente;
using DDD.Comenzi;

namespace WebMvcLibrarie.Controllers
{
    public class HomeController : Controller
    {
        private const string RootPath = @"C:\\Users\\Bianca\\Desktop\\";
        private ReadRepository _readRepo;
        private WriteRepository _writeRepo;

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
            return View("Index");
        }
        public ActionResult Adaugare()
        {
            MagistralaComenzi.Instanta.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InchideInregistrarea();
            Receiver recv = new Receiver();
            string msg = recv.Citeste();
            ViewBag.Message = msg;
            return View();
        }

        [HttpPost]
        public ActionResult Cautare()
        {

            Receiver recv = new Receiver();
            string msg = recv.Citeste();
            ViewBag.Message = msg;

            return View("VCautareCarti");
        }

        public ActionResult DetaliiCarte()
        {
            ViewBag.Message = "Detalii.";

            return View("VCarteInf");
        }

        public ActionResult Cumpara()
        {

            //if ()
            //    ViewBag.Message = "Autentif.";
            //    return View("VInreg");
            //else
            ViewBag.Message = "Inreg.";
            return View("VAutentif");
        }

        public ActionResult Imprumuta()
        {
            return View("VCarteInf");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }
        public ActionResult Autentificare()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
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
                    ModelState.AddModelError("", "Login data is incorrect!");
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