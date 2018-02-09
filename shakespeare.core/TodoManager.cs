using System;
using System.Collections.Generic;
using System.Linq;
using shakespeare.core.dtos;

namespace shakespeare.core
{
    public class TodoManager : ITodoManager
    {
        private Dictionary<Guid, TodoItem> m_todoItems;

        public TodoManager()
        {
            m_todoItems = new Dictionary<Guid, TodoItem>();
        }

        void ITodoManager.CheckItem(Guid id)
        {
            if (m_todoItems.ContainsKey(id))
            {
                m_todoItems[id].Checked = true;
                return;
            }
            throw new ItemNotFoundException(id);
        }

        void ITodoManager.UncheckItem(Guid id)
        {
            if (m_todoItems.ContainsKey(id))
            {
                m_todoItems[id].Checked = false;
                return;
            }
            throw new ItemNotFoundException(id);
        }

        void ITodoManager.SaveItem(string description)
        {
            TodoItem item = new TodoItem(Guid.NewGuid(), description, DateTime.UtcNow);
            m_todoItems.Add(item.Id, item);
        }

        void ITodoManager.DeleteItem(Guid id)
        {
            if (m_todoItems.ContainsKey(id))
            {
                m_todoItems[id].Deleted = true;
                return;
            }
            throw new ItemNotFoundException(id);
        }

        IEnumerable<TodoItem> ITodoManager.GetItems()
        {
            return m_todoItems.Values.Where(x => x.Deleted == false);
        }
    }
}