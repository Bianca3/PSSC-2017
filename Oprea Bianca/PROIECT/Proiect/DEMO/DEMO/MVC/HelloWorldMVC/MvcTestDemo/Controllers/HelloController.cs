using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTestDemo.Models;

namespace MvcTestDemo.Controllers
{
    public class HelloController : Controller
    {
        //
        // GET: /Hello/

        public ActionResult Index()
        {
            return View(new HelloMessage());
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            HelloMessage message = new HelloMessage();
            UpdateModel(message);
            message.Message = string.Format("Hello {0}", message.Name);
            return View(message);
        }

    }
}
