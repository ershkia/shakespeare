using System;
using CommandLine;
using shakespeare.core;

namespace shakespeare.ui
{
    class Program
    {

        private static bool m_exit = false;
        private static ITodoManager m_todoManager = new TodoManager();

        static void Main(string[] args)
        {
			Console.Clear();
            PrintHeader();

            while (!m_exit)
            {

                Console.Write("\n> ");

                string inputStr = Console.ReadLine();

                if (string.IsNullOrEmpty(inputStr))
                {
                    continue;
                }

                string[] input = inputStr.Split(' ');

                Console.Clear();

                PrintHeader();

                var parser = new Parser();

                parser.ParseArguments
                    <AddOptions,
                    DeleteOptions,
                    ListOptions,
					HelpOptions,
                    ExitOptions>(input)
                        .WithParsed<AddOptions>(opts => RunAdd(opts))
                        .WithParsed<DeleteOptions>(opts => RunDelete(opts))
                        .WithParsed<ListOptions>(opts => RunList(opts))
                        .WithParsed<ExitOptions>(opts => RunExit(opts))
						.WithParsed<HelpOptions>(opts => RunHelp(opts))
                        .WithNotParsed(errs =>
                        {
							PrintErrorMessage("Could not process your command.");
                        });

            }
        }

        private static void PrintHeader()
        {
            Console.WriteLine("Welcome to Shakespeare! Todo or not Todo that is the question.");
			Console.WriteLine("At any moment type helpme for instructions.\n");
        }

		private static object RunHelp(HelpOptions opts){
			Console.WriteLine("add -i my-item		Adds a new item to the Todo list.");
			Console.WriteLine("list          		Lists existing items.");
			return 0;
		}

        private static object RunExit(ExitOptions opts)
        {
            m_exit = true;
            PrintSucessMessage("Farewell ......\n");
            return 0;
        }

        private static object RunAdd(AddOptions opts)
        {

            m_todoManager.SaveItem(opts.Description);

            PrintSucessMessage("Item sucessfully added.");
            return 0;
        }

        private static object RunDelete(DeleteOptions opts)
        {
            PrintSucessMessage("Item deleted.");
            return 0;
        }

        private static object RunList(ListOptions opts)
        {
            Console.WriteLine("List of Todo items:\n");

            foreach (var item in m_todoManager.GetItems())
            {
                Console.WriteLine(item.Description);
            }
            return 0;
        }

		private static void PrintSucessMessage(string message){
			var ink = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ForegroundColor = ink;
		}

		private static void PrintErrorMessage(string message){
			var ink = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ForegroundColor = ink;
		}

    }
}
