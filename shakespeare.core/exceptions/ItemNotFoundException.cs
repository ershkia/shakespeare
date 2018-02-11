using System;

namespace shakespeare.core.exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(Guid todoItemId)
        {
            TodoItemId = todoItemId;
        }

        public Guid TodoItemId { private set; get; }
    }
}