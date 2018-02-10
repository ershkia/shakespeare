using CommandLine;

namespace shakespeare.ui {

	[Verb( "add", HelpText = "Add new todo items." )]
	class AddOptions {

		[Option( 'i', "item", Required = true, HelpText = "Todo item description." )]
		public string Description { get; set; }
		
	}

	[Verb( "delete", HelpText = "Delete existing todo items." )]
	class DeleteOptions {
		//normal options here
	}

	[Verb( "list", HelpText = "List all todo items." )]
	class ListOptions {
		//normal options here
	}

	[Verb( "exit", HelpText = "Exit Shakespeare." )]
	class ExitOptions {
		//normal options here
	}

	[Verb( "helpme", HelpText = "Help option." )]
	class HelpOptions {
		//normal options here
	}

}
