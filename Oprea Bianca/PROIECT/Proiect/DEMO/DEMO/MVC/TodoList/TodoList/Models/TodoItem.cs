using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    public class TodoItem
    {
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsDone { get; set; }
    }
}