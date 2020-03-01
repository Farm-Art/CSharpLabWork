using System;

namespace Lab_2
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter array size:");
            int size = -1;
            while (!Int32.TryParse(Console.ReadLine(), out size) ||
                   size < 0)
                Console.WriteLine("The value entered couldn't be parsed or isn't a valid array length.");
            var arr = new Array(size);
            Console.WriteLine("Fill in the array:");
            for (int i = 0; i < arr.Length; i++)
            {
                int item;
                while (!Int32.TryParse(Console.ReadLine(), out item))
                    Console.WriteLine("Failed to parse integer, try again.");
                arr[i] = item;
            }

            Console.WriteLine("Array contents: {0}", arr);
            Console.Write("Even elements: ");
            arr.PrintEven();
        }
    }
}