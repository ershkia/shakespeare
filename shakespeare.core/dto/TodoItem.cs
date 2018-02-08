using System;

namespace shakespeare.core.todo
{
    public class TodoItem
    {
        public TodoItem(Guid id, Guid userId, string description)
        {
            Id = id;
            UserId = userId;
            Description = description;
        }

        public Guid Id { private set; get; }
        public Guid UserId { private set; get; }
        public string Description { private set; get; }
        public bool Checked { private set; get; }
        public bool Deleted { private set; get; }
    }
}