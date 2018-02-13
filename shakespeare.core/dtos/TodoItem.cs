using System;

namespace shakespeare.core.dtos {
	public class TodoItem {
		public TodoItem( Guid id, string description, int priority, DateTime createdAt ) {
			Id = id;
			Description = description;
			CreatedAt = createdAt;
			Priority = priority;
		}

		public Guid Id { private set; get; }
		public string Description { private set; get; }
		public int Priority { private set; get; }
		public DateTime CreatedAt { private set; get; }
		public DateTime DeletedAt { set; get; }
		public bool Deleted { set; get; }
	}
}