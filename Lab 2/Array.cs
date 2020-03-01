using System;
using System.Collections;

namespace Lab_2
{
    public class Array : IEnumerable
    {
        private int[] _arr = null;
        public int Length => _arr.Length;

        public Array(int size)
        {
            if (size < 0)
                throw new ArgumentException("Array size must be non-negative");
            _arr = new int[size];
        }

        public void PrintEven()
        {
            for (int i = 0; i < Length; i++)
                if (this[i] % 2 == 0)
                    Console.Write(this[i] + (i < Length - 1 ? "; " : ""));
            Console.WriteLine();
    }

        public int this[int index]
        {
            get => _arr[index];
            set
            {
                if (value < 0)
                    Console.WriteLine($"{value} is negative, inverting...");
                _arr[index] = Math.Abs(value);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _arr.GetEnumerator();
        }

        public override string ToString()
        {
            string output = "{";
            for (int i = 0; i < Length; i++)
                output += this[i] + (i < Length - 1 ? "; " : "");
            return output + "}";
        }
    }
}