using System;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var mat = new SquareMatrix(3);
            mat.Input();
            mat.Print();
            Console.WriteLine(mat.Determinant());
        }
    }
}