using System;
using System.Collections.Generic;
using shakespeare.core.dtos;

namespace shakespeare.core {
	public interface ITodoManager {
		void SaveItem( string description );
		void DeleteItem( Guid id );
		IEnumerable<TodoItem> GetItems();
	}
}