using System;
using System.Collections.Generic;
using System.Linq;
using shakespeare.core.dtos;
using shakespeare.core.exceptions;
using shakespeare.core.utilities;

namespace shakespeare.core {
	public class TodoManager : ITodoManager {
		private Dictionary<Guid, TodoItem> m_todoItems;
		private readonly INowProvider m_nowProvider;

		public TodoManager( INowProvider nowProvider ) {
			m_todoItems = new Dictionary<Guid, TodoItem>();
			m_nowProvider = nowProvider;
		}

		void ITodoManager.SaveItem( string description, int priority ) {
			TodoItem item = new TodoItem( Guid.NewGuid(), description, priority, m_nowProvider.Now );
			m_todoItems.Add( item.Id, item );
		}

		void ITodoManager.DeleteItem( Guid id ) {
			if( m_todoItems.ContainsKey( id ) ) {
				m_todoItems[id].Deleted = true;
				m_todoItems[id].DeletedAt = m_nowProvider.Now;
				return;
			}
			throw new ItemNotFoundException( id );
		}

		IEnumerable<TodoItem> ITodoManager.GetItems() {
			return m_todoItems.Values.Where( x => x.Deleted == false ).OrderBy( x => x.Priority );
		}

		IOrderedEnumerable<KeyValuePair<int, int>> ITodoManager.GetPrioirtyReport() {
			Dictionary<int, int> report = new Dictionary<int, int>();

			foreach( TodoItem item in m_todoItems.Values ) {
				if( report.ContainsKey( item.Priority ) ) {
					report[item.Priority]++;
				} else {
					report[item.Priority] = 1;
				}
			}
			if( report.Keys.Any() ) {
				var sorted = report.Keys.OrderByDescending( x => x );
				int maxPriority = sorted.FirstOrDefault();
				int minPriority = sorted.Last();

				for(int i = minPriority; i < maxPriority; i++ ) {
					if( !report.ContainsKey( i ) ) {
						report[i] = 0;
					}
				}
			}

			return report.OrderBy( x => x.Key );
		}
	}
}