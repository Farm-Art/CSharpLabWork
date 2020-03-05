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

        // Most used words for bool values
        private enum Confirmation
        {
            No = 0, N = 0, False = 0,
            Yes = 1, Y = 1, True = 1
        }

        // Static variables for search functions
        private static string _modelSample, _materialSample;
        private static decimal _priceSample;
        private static bool _printerSample, _matched;
        
        // Static variables for run control
        private static bool _running;
        private static readonly List<ComputerTable> _list = new List<ComputerTable>();
        
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

        // Helper method for search functions
        private static void PrintIfTrue(bool cond, object item)
        {
            if (!cond) return;
            Console.WriteLine(item);
            _matched = true;
        }

        // Dict of menu options
        private static Dictionary<Menu.Item, Action>
            _options = new Dictionary<Item, Action>
            {
                [Item.Add] = AddTable,
                [Item.Remove] = RemoveTable,
                [Item.Print] = PrintList,
                [Item.Sort] = SortList,
                [Item.Search] = SearchList,
                [Item.Help] = PrintMenu,
                [Item.Quit] = () => _running = false
            };

        public static void Run()
        {
            _running = true;
            while (_running)
            {
                _options[GetInput()]();
                Console.WriteLine(new string('-', 30));
            }
            
        }

        public static void AddTable()
        {
            Console.WriteLine("Enter new table model:");
            Console.Write(">>> ");
            var model = Console.ReadLine();

            Console.WriteLine("Does the table have a printer stand?");
            // while (!Type.TryParse(input, out variable)) {} is the trivial way to keep
            // asking for input until the user provides a correct value.
            var hasPrinter = GetConfirmation();

            decimal price;
            Console.WriteLine("Enter the price of the table:");
            Console.Write(">>> ");
            while (!Decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Failed to read value, try again.");
                Console.Write(">>> ");
            }

            Console.WriteLine("Enter the material this table is made of:");
            Console.Write(">>> ");
            var material = Console.ReadLine();

            var table = new ComputerTable(model, hasPrinter, price, material);
            _list.Add(table);
            Console.WriteLine($"Added {table}.");
        }

        public static void RemoveTable()
        {
            if (_list.IsEmpty)
            {
                Console.WriteLine("The list is empty.");
                return;
            }
            
            Console.WriteLine("Enter index of table to remove from list:");
            Console.Write(">>> ");
            int index = 0;
            while (!Int32.TryParse(Console.ReadLine(), out index) || !_list.IsValidIndex(index-1))
            {
                Console.WriteLine("The value entered is not an integer or is out of range, try again.");
                Console.Write(">>> ");
            }
            _list.Remove(index-1);
        }

        public static void PrintList()
        {
            Console.WriteLine(new string('=', 30));
            if (_list.IsEmpty)
                Console.WriteLine("The list is empty.");
            else
                for (int i = 0; i < _list.Length; i++)
                    Console.WriteLine($"{i + 1}: {_list[i]}");
            Console.WriteLine(new string('=', 30));
        }

        public static void SortList()
        {
            Console.WriteLine("What criteria would you like to sort the list by?");
            Console.WriteLine("Choose one of the following: model (0), printer (1), price (2), material (3).");
            ComputerTable.Key key = GetKey();

            Console.WriteLine("Would you like to reverse the sort?");
            bool reverse = GetConfirmation();

            _list.Sort(_sortFuncs[key], reverse);
        }

        public static void SearchList()
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
                    Console.Write(">>> ");
                    _modelSample = Console.ReadLine();
                    break;
                case ComputerTable.Key.Printer :
                    _printerSample = GetConfirmation();
                    break;
                case ComputerTable.Key.Price :
                    Console.Write(">>> ");
                    while (!Decimal.TryParse(Console.ReadLine(), out _priceSample))
                    {
                        Console.WriteLine("Failed to read input, try again.");
                        Console.Write(">>> ");
                    }
                    break;
                case ComputerTable.Key.Material :
                    Console.Write(">>> ");
                    _materialSample = Console.ReadLine();
                    break;
            }
            
            // Apply func to each item
            _list.Iter(_searchFuncs[key]);
            if (!_matched)
                Console.WriteLine("No matches found!");
        }

        public static void PrintMenu()
        {
            Console.WriteLine(new string('=', 30));
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. ADD an item to the list;");
            Console.WriteLine("2. REMOVE an item from the list;");
            Console.WriteLine("3. PRINT the list;");
            Console.WriteLine("4. SORT the list;");
            Console.WriteLine("5. SEARCH the list;");
            Console.WriteLine("6. Print the HELP menu;");
            Console.WriteLine("7. QUIT.");
            Console.WriteLine(new string('=', 30));
        }

        private static ComputerTable.Key GetKey()
        {
            Console.Write(">>> ");
            ComputerTable.Key input;
            while (!ProperParse(Console.ReadLine(), true, out input))
            {
                Console.WriteLine("That's not a correct key.");
                Console.Write(">>> ");
            }
            return input;
        }

        public static bool ProperParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
        {
            // Regular Enum.TryParse doesn't check if the parsed integer has an associated constant;
            // This version merges parsing with definition checking.
            return Enum.TryParse(value, ignoreCase, out result) && Enum.IsDefined(typeof(TEnum), result);
        }

        public static bool ProperParse<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            return ProperParse(value, false, out result);
        }

        private static Item GetInput()
        {
            Console.Write(">>> ");
            Item selection;
            while (!ProperParse(Console.ReadLine(), true, out selection))
            {
                Console.WriteLine("That's not a valid menu item.");
                Console.Write(">>> ");
            }
            return selection;
        }

        private static bool GetConfirmation()
        {
            Console.Write(">>> ");
            Confirmation result;
            while (!ProperParse(Console.ReadLine(), true, out result))
            {
                Console.WriteLine("Failed to read bool value, try again.");
                Console.Write(">>> ");
            }

            return (int) result == 1;
        }
    }
}