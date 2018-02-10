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

                Console.Clear();
                PrintHeader();

                if (string.IsNullOrEmpty(inputStr))
                {
                    continue;
                }

				if(string.Equals(
					inputStr.Trim(), "list",
					StringComparison.InvariantCultureIgnoreCase) )
				{
					RunList();
					continue;
				} 
				
				if(string.Equals(
					inputStr.Trim(), "delete",
					StringComparison.InvariantCultureIgnoreCase) )
				{
					RunDelete();
					continue;
				}
				
				if(string.Equals(
					inputStr.Trim(),"help",
					StringComparison.InvariantCultureIgnoreCase) )
				{
					RunHelp();
					continue;
				}
				
				if(string.Equals(
					inputStr.Trim(), "exit",
					StringComparison.InvariantCultureIgnoreCase) )
				{
					RunExit();
					continue;
				}

				if(inputStr.StartsWith("add",true, null))
				{
					string desc = new String(inputStr.SkipWhile(c => c != ' ').ToArray()).Trim();
					
					if(string.IsNullOrEmpty(desc)){
						PrintErrorMessage("I regret to say I am confused your honor. May I ask what you want me to remember.");
						continue;
					}
					RunAdd(desc);
					continue;
				}
				PrintErrorMessage("I cannot fathom the intricacies of your command my lord.");

            }
        }

        private static void PrintHeader()
        {
            PrintSucessMessage("Welcome to Shakespeare! Todo or not Todo that is the question.");
            Console.WriteLine("At any moment type help for royal assistance.\n");
        }

        private static void RunHelp()
        {
			PrintSucessMessage("May this help me to better be at your service my lord.\n");
            
			Console.WriteLine("add  my-item 		Adds my-item to the Todo list.");
            Console.WriteLine("list          		Lists existing Todo items.");
			Console.WriteLine("delete i      		Deletes item with index i in the list.");
        }

        private static void RunExit()
        {
            m_exit = true;
            PrintSucessMessage("Farewell your majesty, Long live the king.\n");
        }

        private static void RunAdd(string item)
        {
            m_todoManager.SaveItem(item);

            PrintSucessMessage("Your command was instantly executed your honor.");
        }

        private static void RunDelete()
        {			
			TodoItem[] itemsArray = m_todoManager.GetItems().ToArray();

			if(itemsArray.Count() == 0)
			{
				PrintSucessMessage("Rest assured there is nothing to delete your honor.");
				return;
			}

			PrintSucessMessage("Select the number to delete with the royal finger or type 0 if the majesty doesn't want to delete anything..\n");	
			PrintTodoItems(itemsArray);

			while(true)
			{
				Console.Write("\n>");
				
				string input = Console.ReadLine().ToString();
				bool parsed = Int32.TryParse(input, out int index);

				if(!parsed || index<0 || index>itemsArray.Count()){
					
					PrintErrorMessage("\nYour commands are always undoubtedly valid but this one is beyond my grasp. May I ask you to try again.");
					continue;
				}

				if(index == 0){
					break;
				}

				m_todoManager.DeleteItem(itemsArray[index-1].Id);
				PrintSucessMessage("\nItem was deleted immediately your majesty.");
				break;
			}			
        }

        private static void RunList()
        {
			var items = m_todoManager.GetItems();
			
			if(!items.Any())
			{
            	PrintSucessMessage("May the majesty be always at peace with no todo items.\n");
			}else
			{
				PrintSucessMessage("I hope I am not disturbing the mood by reminding your majesty of the todo items.\n");
            	PrintTodoItems(items);
			}
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
