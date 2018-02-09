using System;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(Guid todoItemId)
    {
        TodoItemId = todoItemId;
    }

    public Guid TodoItemId { private set; get; }
}