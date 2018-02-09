using System;
using System.Collections.Generic;
using shakespeare.core.dtos;

namespace shakespeare.core
{
    public interface ITodoManager
    {
        void SaveItem(string description);
        void DeleteItem(Guid id);
        void CheckItem( Guid id);
        void UncheckItem(Guid id);
        IEnumerable<TodoItem> GetItems();
    }
}