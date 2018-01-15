using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private static TodoItemCollection _items = new TodoItemCollection() { 
            AllItems = new List<TodoItem>()
        };

        //
        // GET: /Home/

        public ActionResult Index()
        {
            var model = _items;
            return View(model);
        }

        public ActionResult AddTodoItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTodoItem(TodoItem item)
        {
            _items.AllItems.Add(item);
            return RedirectToAction("Index");
        }
    }
}