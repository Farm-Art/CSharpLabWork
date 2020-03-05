using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

        public static void InitArray()
        {
            int input;
            Console.Write("Enter array length: ");
            while (!Int32.TryParse(Console.ReadLine(), out input) || input < 1)
                Console.WriteLine("The value entered is not an integer or isn't a valid array length.");
            SquareMatrix.InitArray(input);
        }
        
        public static void AddMatrix()
        {
            if (SquareMatrix.ArrLen <= SquareMatrix.ItemsAmount)
            {
                Console.WriteLine("The array is full.");
                return;
            }
            Console.Write("Enter matrix size: ");
            int input = 0;
            while (!Int32.TryParse(Console.ReadLine(), out input) ||
                   input < 1)
                Console.WriteLine("Value entered is not an integer or a valid matrix size. Try again.");
            var mat = new SquareMatrix(input);
            SquareMatrix.AddItem(mat);
        }

        public static void EditMatrix()
        {
            if (SquareMatrix.ItemsAmount == 0)
            {
                Console.WriteLine("The array is empty.");
                return;
            }
            var mat = SquareMatrix.GetItem(GetIndex());
            Console.WriteLine("Fill in the matrix:");
            mat.Input();
        }

        public static int GetIndex()
        {
            Console.WriteLine($"Enter index of item (0-{SquareMatrix.ItemsAmount - 1}):");
            int input = -1;
            while (!Int32.TryParse(Console.ReadLine(), out input) ||
                   input < 0 || input >= SquareMatrix.ItemsAmount)
                Console.WriteLine("The value entered is not an integer or isn't a valid index.");
            return input;
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
            else
                Console.WriteLine("You'll quite literally never end up here.");
        }

        public static void RemoveMatrix()
        {
            if (SquareMatrix.ItemsAmount == 0)
            {
                Console.WriteLine("The array is empty.");
                return;
            }
            SquareMatrix.RemoveItem(GetIndex());
        }

        public static Item GetInput()
        {
            Item input;
            Console.Write(">>> ");
            while (!Enum.TryParse(Console.ReadLine(), true, out input) ||
                   (int) input < 1 || (int) input > 7)
            {
                Console.WriteLine("That's not a valid menu item.");
                Console.Write(">>> ");
            }

            return input;
        }

        public static void Run()
        {
            _running = true;
            while (_running)
                _options[GetInput()]();
        }
    }
}