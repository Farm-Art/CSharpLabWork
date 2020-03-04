using System;
using System.Data.SqlTypes;

namespace Lab_4
{
    public static class Program
    {
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
            var mat = SquareMatrix.GetItem(GetIndex());
            Console.WriteLine("Fill in the matrix:");
            mat.Input();
        }

        public static int GetIndex()
        {
            Console.WriteLine($"Enter index of item (0-{SquareMatrix.ItemsAmount - 1}");
            int input = -1;
            while (!Int32.TryParse(Console.ReadLine(), out input) ||
                   input < 0 || input >= SquareMatrix.ItemsAmount)
                Console.WriteLine("The value entered is not an integer or isn't a valid index.");
            return input;
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
        
        public static void Main(string[] args)
        {
            
        }
    }
}