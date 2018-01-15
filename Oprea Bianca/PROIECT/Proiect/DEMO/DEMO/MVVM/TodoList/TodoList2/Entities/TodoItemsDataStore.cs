using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList2.Entities
{
    public class TodoItemsDataStore
    {
        private List<TodoItem> _items = new List<TodoItem>();

        public List<TodoItem> GetItems()
        {
            return _items;
        }

        public void AddItem(TodoItem item)
        {
            _items.Add(item);
        }
    }
}
