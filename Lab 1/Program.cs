using System;
using static Lab_1.Menu;

namespace Lab_1
{
    static class Program
    {
        static void Main(string[] args)
        {
            var list = new List<ComputerTable>();
            PrintMenu();
            bool running = true;
            while (running)
            {
                switch (GetInput())
                {
                    case Menu.Item.Add :
                        AddTable(list);
                        break;
                    case Menu.Item.Remove :
                        RemoveTable(list);
                        break;
                    case Menu.Item.Print :
                        PrintList(list);
                        break;
                    case Menu.Item.Sort :
                        SortList(list);
                        break;
                    case Menu.Item.Search :
                        SearchList(list);
                        break;
                    case Menu.Item.Help :
                        PrintMenu();
                        break;
                    case Menu.Item.Quit :
                        running = false;
                        break;
                    default:
                        Console.WriteLine("That's not a valid command.");
                        PrintMenu();
                        break;
                }
            }
        }
    }
}