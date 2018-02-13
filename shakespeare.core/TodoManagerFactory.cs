using shakespeare.core.utilities;

namespace shakespeare.core {
	public static class TodoManagerFactory {
		public static ITodoManager Create() {
			INowProvider nowProvider = new NowProvider();
			ITodoManager todoManager = new TodoManager( nowProvider );
			return todoManager;
		}
	}
}