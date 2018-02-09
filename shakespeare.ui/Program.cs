using System;
using CommandLine;
using shakespeare.core;

namespace shakespeare.ui {
	class Program {

		private static bool m_exit = false;
		private static ITodoManager m_todoManager = new TodoManager();

		static void Main( string[] args ) {

			PrintHeader();

			while( !m_exit ) {

				Console.Write("> ");
				string inputStr = Console.ReadLine();

				if( string.IsNullOrEmpty( inputStr ) ) {
					continue;
				}

				string[] input = inputStr.Split( ' ' );

				Console.Clear();

				PrintHeader();

				Parser.Default.ParseArguments<AddOptions, DeleteOptions, ListOptions>( input )
					.WithParsed<AddOptions>( opts => RunAddAndReturnExitCode( opts ) )
					.WithParsed<DeleteOptions>( opts => RunDeleteAndReturnExitCode( opts ) )
					.WithParsed<ListOptions>( opts => RunListAndReturnExitCode( opts ) )
					.WithParsed<ExitOptions>( opts => RunExitAndReturnExitCode( opts ) )
					.WithNotParsed( errs => {
						Console.WriteLine( "Invalid command!" );
					} );

			}			
		}

		private static void PrintHeader() {
			Console.WriteLine( "Welcome to Shakespeare!" );
			Console.WriteLine( "Todo or not Todo. That is the question." );
			Console.WriteLine( "To see a list of all available commands type help." );
			Console.WriteLine( "-----------------------------------------------------\n" );
		
		}

		private static object RunExitAndReturnExitCode( ExitOptions opts ) {
			m_exit = true;
			Console.WriteLine( "Farewell ......" );
			return 0;
		}

		private static object RunAddAndReturnExitCode( AddOptions opts ) {

			m_todoManager.SaveItem( opts.Description );

			Console.WriteLine("Item sucessfully added.");
			return 0;
		}

		private static object RunDeleteAndReturnExitCode( DeleteOptions opts ) {
			Console.WriteLine( "Item deleted." );
			return 0;
		}

		private static object RunListAndReturnExitCode( ListOptions opts ) {
			Console.WriteLine( "List of Todo items:\n" );

			foreach( var item in m_todoManager.GetItems() ) {
				Console.WriteLine(item.Description);
			}
			return 0;
		}

	}
}
