using System;
using System.Collections.Generic;

namespace Lab_1
{
    public static class Menu
    {
        // Keys for menu selection options
        public enum Item
        {
            Add = 1, Remove, Print, Sort, Search, Help, Quit
        }

        // Static variables for search functions
        private static string _modelSample, _materialSample;
        private static decimal _priceSample;
        private static bool _printerSample, _matched;
        
        // Dict of sorting lambdas for each field
        private static Dictionary<ComputerTable.Key, Comparison<ComputerTable>>
            _sortFuncs = new Dictionary<ComputerTable.Key, Comparison<ComputerTable>>
            {
                [ComputerTable.Key.Model] =
                    (a, b) => String.CompareOrdinal(a.Model, b.Model),
                [ComputerTable.Key.Printer] =
                    (a, b) => (a.HasPrinter != b.HasPrinter ? (!a.HasPrinter ? 1 : -1) : 0),
                [ComputerTable.Key.Price] =
                    (a, b) => (int) (a.Price - b.Price),
                [ComputerTable.Key.Material] =
                    (a, b) => String.CompareOrdinal(a.Material, b.Material)
            };

        // Dict of search lambdas, print items if they match the query
        private static Dictionary<ComputerTable.Key, Action<ComputerTable>>
            _searchFuncs = new Dictionary<ComputerTable.Key, Action<ComputerTable>>
            {
                [ComputerTable.Key.Model] =
                    (t) => PrintIfTrue(t.Model == _modelSample, t),
                [ComputerTable.Key.Printer] =
                    (t) => PrintIfTrue(t.HasPrinter == _printerSample, t),
                [ComputerTable.Key.Price] =
                    (t) => PrintIfTrue(t.Price == _priceSample, t),
                [ComputerTable.Key.Material] =
                    (t) => PrintIfTrue(t.Material == _materialSample, t)
            };

        private static void PrintIfTrue(bool cond, object item)
        {
            if (!cond) return;
            Console.WriteLine(item);
            _matched = true;
        }

        public static void AddTable(List<ComputerTable> list)
        {
            Console.WriteLine("Enter new table model:");
            var model = Console.ReadLine();

            bool hasPrinter;
            Console.WriteLine("Does the table have a printer stand? (true/false)");
            // while (!Type.TryParse(input, out variable)) {} is the trivial way to keep
            // asking for input until the user provides a correct value.
            while (!Boolean.TryParse(Console.ReadLine(), out hasPrinter))
                Console.WriteLine("Failed to read value, try again.");

            decimal price;
            Console.WriteLine("Enter the price of the table:");
            while (!Decimal.TryParse(Console.ReadLine(), out price))
                Console.WriteLine("Failed to read value, try again.");

            Console.WriteLine("Enter the material this table is made of:");
            var material = Console.ReadLine();

            list.Add(new ComputerTable(model, hasPrinter, price, material));
        }

        public static void RemoveTable(List<ComputerTable> list)
        {
            if (list.IsEmpty)
            {
                Console.WriteLine("The list is empty.");
                return;
            }
            
            Console.WriteLine("Enter index of table to remove from list:");
            int index = 0;
            while (!Int32.TryParse(Console.ReadLine(), out index) || !list.IsValidIndex(index-1))
                Console.WriteLine("The value entered is not an integer or is out of range, try again.");

            list.Remove(index-1);
        }

        public static void PrintList(List<ComputerTable> list)
        {
            Console.WriteLine(new string('=', 30));
            if (list.IsEmpty)
                Console.WriteLine("The list is empty.");
            else
                for (int i = 0; i < list.Length; i++)
                    Console.WriteLine($"{i + 1}: {list[i]}");
            Console.WriteLine(new string('=', 30));
        }

        public static void SortList(List<ComputerTable> list)
        {
            Console.WriteLine("What criteria would you like to sort the list by?");
            Console.WriteLine("Choose one of the following: model (0), printer (1), price (2), material (3).");
            ComputerTable.Key key = GetKey();
            
            bool reverse;
            Console.WriteLine("Would you like to reverse the sort? (true/false)");
            while (!Boolean.TryParse(Console.ReadLine(), out reverse))
                Console.WriteLine("Failed to read input. Try again.");

            list.Sort(_sortFuncs[key], reverse);
        }

        public static void SearchList(List<ComputerTable> list)
        {
            _matched = false;
            Console.WriteLine("What field would you like to search the list by?");
            Console.WriteLine("Choose one of the following: model (0), printer (1), price (2), material (3).");
            ComputerTable.Key key = GetKey();

            Console.WriteLine("Enter search data:");
            // Get and set query data for searching
            switch (key)
            {
                case ComputerTable.Key.Model :
                    _modelSample = Console.ReadLine();
                    break;
                case ComputerTable.Key.Printer :
                    while (!Boolean.TryParse(Console.ReadLine(), out _printerSample))
                        Console.WriteLine("Failed to read input, try again.");
                    break;
                case ComputerTable.Key.Price :
                    while (!Decimal.TryParse(Console.ReadLine(), out _priceSample))
                        Console.WriteLine("Failed to read input, try again.");
                    break;
                case ComputerTable.Key.Material :
                    _materialSample = Console.ReadLine();
                    break;
            }
            
            // Apply func to each item
            list.Iter(_searchFuncs[key]);
            if (!_matched)
                Console.WriteLine("No matches found!");
        }

        public static void PrintMenu()
        {
            Console.WriteLine(new string('=', 30));
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add item to list;");
            Console.WriteLine("2. Remove item from list;");
            Console.WriteLine("3. Print the list;");
            Console.WriteLine("4. Sort the list;");
            Console.WriteLine("5. Search the list;");
            Console.WriteLine("6. Print menu;");
            Console.WriteLine("7. Quit.");
            Console.WriteLine(new string('=', 30));
        }

        private static ComputerTable.Key GetKey()
        {
            ComputerTable.Key input;
            while (!Enum.TryParse(Console.ReadLine(), true, out input))
            {
                Console.WriteLine("That's not a correct key.");
            }
            return input;
        }
        
        public static Item GetInput()
        {
            Console.Write(">>> ");
            int result;
            while (!Int32.TryParse(Console.ReadLine(), out result))
                Console.WriteLine("Incorrect input, please enter an integer corresponding to your choice.");

            return (Item)result;
        }
    }
}