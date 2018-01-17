using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMvcLibrarie;
using WebMvcLibrarie.Controllers;
using Moq;
using DDD;
using WebMvcLibrarie.Models;

namespace WebMvcLibrarie.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            
            ViewResult result = controller.Index() as ViewResult;
            
            Assert.IsNotNull(result);
        }        

        [TestMethod]
        public void Contact()
        {            
            HomeController controller = new HomeController();
           
            ViewResult result = controller.Contact() as ViewResult;
           
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VerifMvcTitluri()
        {
            List<string> titluri = new List<string>();
            titluri.Add("Ion");
            titluri.Add("Mara");
            Mock<ReadRepository> mockR = new Mock<ReadRepository>();
            Mock<WriteRepository> mockW = new Mock<WriteRepository>();
            mockR.Setup(_ => _.TitluCarti()).Returns(titluri);
            var controller = new HomeController(mockR.Object, mockW.Object);
            var model = (MCarte)(((ViewResult)controller.Index()).Model);
            Assert.AreEqual("Ion", controller.ViewBag.titlu);
            Assert.AreEqual("Mara", controller.ViewBag.titlu);
        }
    }
}
