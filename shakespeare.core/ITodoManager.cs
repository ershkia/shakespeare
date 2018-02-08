using System;
using System.Collections.Generic;
using shakespeare.core.dto;

namespace shakespeare.core
{
    public interface ITodoManager
    {
        void CreateItem(User user, string description);
        void DeleteItem(User user, Guid id);
        void CheckItem(User user, Guid id);
        void UncheckItem(User user, Guid id);
        IList<User> GetItems(User user);
    }
}