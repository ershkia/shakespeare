using System;

namespace shakespeare.core.dtos
{
    public class TodoItem
    {
        public TodoItem(Guid id, string description, DateTime createdAt )
        {
            Id = id;
            Description = description;
            CreatedAt = createdAt;
        }

        public Guid Id { private set; get; }
        public string Description { private set; get; }
        public DateTime CreatedAt { private set; get; }
        public DateTime DeletedAt { set; get; }
        public bool Deleted { set; get; }
    }
}