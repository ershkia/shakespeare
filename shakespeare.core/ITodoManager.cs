using System;
using System.Collections.Generic;
using System.Linq;
using shakespeare.core.dtos;

namespace shakespeare.core {
	public interface ITodoManager {
		void SaveItem( string description, int priority );
		void DeleteItem( Guid id );
		IEnumerable<TodoItem> GetItems();

		IOrderedEnumerable<KeyValuePair<int, int>> GetPrioirtyReport();
	}
}