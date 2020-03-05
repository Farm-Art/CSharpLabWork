using System;
using System.Collections.Generic;

namespace Lab_4
{
    public class Menu
    {
        public enum Item
        {
            Add = 1, Remove, Edit, Print, Calculate, Help, Quit
        }

        private static bool _running;

        private static Dictionary<Item, Action> _options = new Dictionary<Item, Action>()
        {
            [Item.Add] = AddMatrix,
            [Item.Remove] = RemoveMatrix,
            [Item.Edit] = EditMatrix,
            [Item.Print] = SquareMatrix.PrintArr,
            [Item.Calculate] = CalculateDeterminant,
            [Item.Help] = PrintHelp,
            [Item.Quit] = () => _running = false
        };

        public static void InitArray()
        {
            Console.WriteLine("Enter array length:");
            Console.Write(">>> ");
            int input;
            while (!Int32.TryParse(Console.ReadLine(), out input) || input < 1)
            {
                Console.WriteLine("The value entered is not an integer or isn't a valid array length.");
                Console.Write(">>> ");
            }
            SquareMatrix.InitArray(input);
        }

        public static void AddMatrix()
        {
            if (SquareMatrix.ItemsAmount >= SquareMatrix.ArrLen)
            {
                Console.WriteLine("The array is full.");
                return;
            }
            Console.WriteLine("Enter matrix size:");
            Console.Write(">>> ");
            int input = 0;
            while (!Int32.TryParse(Console.ReadLine(), out input) ||
                   input < 1)
            {
                Console.WriteLine("Value entered is not an integer or a valid matrix size. Try again.");
                Console.Write(">>> ");
            }
            var mat = new SquareMatrix(input);
            SquareMatrix.AddItem(mat);
        }

        public static void RemoveMatrix()
        {
            if (SquareMatrix.ItemsAmount == 0)
            {
                Console.WriteLine("The array is empty.");
                return;
            }

            Console.WriteLine("Enter index of matrix to remove.");
            SquareMatrix.RemoveItem(GetIndex());
        }

        public static void EditMatrix()
        {
            if (SquareMatrix.ItemsAmount == 0)
            {
                Console.WriteLine("The array is empty.");
                return;
            }

            Console.WriteLine("Enter index of matrix to edit.");
            var mat = SquareMatrix.GetItem(GetIndex());
            Console.WriteLine("Fill in the matrix:");
            mat.Input();
        }

        public static void CalculateDeterminant()
        {
            if (SquareMatrix.ItemsAmount == 0)
            {
                Console.WriteLine("The array is empty.");
                return;
            }
            Console.WriteLine("Enter index of matrix to calculate the determinant:");
            if (SquareMatrix.GetItem(GetIndex()) is SquareMatrix mat)
            {
                mat.Print();
                Console.WriteLine($"The determinant of this matrix is: {mat.Determinant()}");
            }
        }

        public static void PrintHelp()
        {
            Console.WriteLine(new string('=', 30));
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. ADD a new matrix to the list;");
            Console.WriteLine("2. REMOVE an existing matrix from the list;");
            Console.WriteLine("3. EDIT an existing matrix in the list;");
            Console.WriteLine("4. PRINT all existing matrices;");
            Console.WriteLine("5. CALCULATE a determinant;");
            Console.WriteLine("6. Print the HELP menu again;");
            Console.WriteLine("7. QUIT.");
            Console.WriteLine(new string('=', 30));
        }

        public static void Run()
        {
            _running = true;
            while (_running)
            {
                _options[GetInput()]();
                Console.WriteLine(new string('=', 30));
            }
        }

        private static Item GetInput()
        {
            Item input;
            Console.Write(">>> ");
            while (!ProperParse(Console.ReadLine(), true, out input))
            {
                Console.WriteLine("That's not a valid menu item.");
                Console.Write(">>> ");
            }

            return input;
        }

        private static int GetIndex()
        {
            Console.WriteLine($"Appropriate range: [0, {SquareMatrix.ItemsAmount - 1}]");
            Console.Write(">>> ");
            int input = -1;
            while (!Int32.TryParse(Console.ReadLine(), out input) ||
                   input < 0 || input >= SquareMatrix.ItemsAmount)
            {
                Console.WriteLine("The value entered is not an integer or isn't a valid index.");
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
    }
}