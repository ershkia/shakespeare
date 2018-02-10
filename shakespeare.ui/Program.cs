using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using shakespeare.core;
using shakespeare.core.dtos;

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
                            PrintErrorMessage("Cannot fathom the intricacies of your command my lord.");
                        });

            }
        }

        private static void PrintHeader()
        {
            Console.WriteLine("Welcome to Shakespeare! Todo or not Todo that is the question.");
            Console.WriteLine("At any moment type helpme for your truely's assistance.\n");
        }

        private static object RunHelp(HelpOptions opts)
        {
            Console.WriteLine("add -i my-item		Adds a new item to the Todo list.");
            Console.WriteLine("list          		Lists existing items.");
			Console.WriteLine("delete i      		Delete item with index i in the list.");
            return 0;
        }

        private static object RunExit(ExitOptions opts)
        {
            m_exit = true;
            PrintSucessMessage("Farewell your majesty, farewell ...\n");
            return 0;
        }

        private static object RunAdd(AddOptions opts)
        {

            m_todoManager.SaveItem(opts.Description);

            PrintSucessMessage("Your command was instantly executed your honor.");
            return 0;
        }

        private static object RunDelete(DeleteOptions opts)
        {
			PrintSucessMessage("Please type the item number you want to delete or type 0 to move to go to main menu.\n");
			
			TodoItem[] itemsArray = m_todoManager.GetItems().ToArray();

			if(itemsArray.Count() == 0)
			{
				Console.WriteLine("Todo list is empty.");
				return 0;
			}
			
			PrintTodoItems(itemsArray);

			while(true)
			{
				Console.Write("\n>");
				
				string input = Console.ReadKey().KeyChar.ToString();
				bool parsed = Int32.TryParse(input, out int index);

				if(!parsed || index<0 || index>itemsArray.Count()){
					
					PrintErrorMessage("\nInvalid input. Try again.");
					continue;
				}

				if(index == 0){
					break;
				}

				m_todoManager.DeleteItem(itemsArray[index-1].Id);
				PrintSucessMessage("\nItem was deleted immediately your majesty.");
				break;
			}
			
            return 0;
        }

        private static object RunList(ListOptions opts)
        {
            PrintSucessMessage("List of Todo items\n");

            PrintTodoItems(m_todoManager.GetItems());
            return 0;
        }

        private static void PrintTodoItems(IEnumerable<TodoItem> items)
        {
			int i = 1;
            foreach (var item in items)
            {
                Console.WriteLine($"{i}. {item.Description}");
				i++;
            }
        }

        private static void PrintSucessMessage(string message)
        {
            var ink = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ink;
        }

        private static void PrintErrorMessage(string message)
        {
            var ink = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ink;
        }

    }
}
