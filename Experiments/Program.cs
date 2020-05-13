using System;
using Python;
using static Python.Stuff;

namespace Experiments
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var rand = new Random();
            var arr = new int[15];
            for (int i=0; i < 15; i++) {
                arr[i] = rand.Next(0, 10);
            }
            foreach (int i in arr) {
                Console.Write(i + " ");
            }

            int count = 0;
            var items = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                bool unique = true;
                for (int j = 0; j < count; j++)
                    if (items[j] == arr[i])
                    {
                        unique = false;
                        break;
                    }

                if (unique)
                    items[count++] = arr[i];
            }

Console.WriteLine(count);
        }
    }
}