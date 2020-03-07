using System;

namespace Lab_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new LinkedList();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            list.Insert(2, 10);
            list.Remove(5);
            list.RemoveAt(3);
            Console.WriteLine(list.Contains(8));
            Console.WriteLine(list.IndexOf(7));
            Console.WriteLine(list);
            var arr = new int[list.Count];
            list.CopyTo(arr, 0);
            foreach (int i in arr)
                Console.WriteLine(i);
            list.Clear();
            Console.WriteLine(list);

            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
                list[0] = i;
            }

            list.IsFixedSize = true;
            list[5] = 0;
            Console.WriteLine(list);
        }
    }
}