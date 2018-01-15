using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoList2.Entities;

namespace TodoList2.ViewModel
{
    public class TodoItemViewModel:ViewModelBase
    {
        private TodoItem _item;

        public TodoItem Item
        {
            get { return _item; }
            set { _item = value; }
        }
        private TodoItemsDataStore _store;

        public TodoItemViewModel(TodoItemsDataStore store)
        {
            _store = store;
            _item = new TodoItem();
        }

        //public TodoItemViewModel(TodoItemsDataStore store, TodoItem item)
        //{
        //    _store = store;
        //    _item = item;
        //}

        public string Description 
        {
            get
            {
                return _item.Description;
            }
            set
            {
                if (value != _item.Description)
                {
                    _item.Description = value;
                    
                    base.RaisePropertyChanged(() => Description);
                }
            }
        }

        private ICommand _command;
        public ICommand AddNewItem 
        {
            get
            {
                return _command ?? (_command = new RelayCommand(OnAddNewItem, ()=>Description!=null && Description.Length>1));
            }
        }

        private void OnAddNewItem()
        {
            var newItem = new TodoItem();
            newItem.Description = _item.Description;

            _store.AddItem(newItem);
            Description = "";
        }

    }
}
