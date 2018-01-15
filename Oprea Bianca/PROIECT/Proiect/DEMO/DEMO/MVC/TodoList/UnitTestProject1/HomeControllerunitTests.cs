using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoList.Controllers;
using TodoList.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class HomeControllerunitTests
    {
        [TestMethod]
        public void CheckThatItemIsAdded()
        {
            var controller = new HomeController();
            var item = new TodoItem() { Description= "test", DeadLine=DateTime.Now};
            controller.AddTodoItem(item);

            var model = ((System.Web.Mvc.ViewResult)controller.Index()).Model as TodoItemCollection;
            Assert.AreEqual(model.AllItems.Count, 1);
        }
    }
}
