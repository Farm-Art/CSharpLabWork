using System;

namespace Lab_4
{
    public abstract class AbstractMatrix<T>
    {
        protected T[,] data = null;
        private int _rows = 0, _cols = 0;
        public int Rows => _rows;
        public int Cols => _cols;
        
        protected AbstractMatrix(int sizeA, int sizeB)
        {
            data = new T[sizeA, sizeB];
            _rows = sizeA;
            _cols = sizeB;
        }

        public abstract void Input();
        public virtual void Print()
        {
            Console.WriteLine(new string('-', 30));
            for (int i = 0; i < Rows; i++)
            {
                Console.Write("{");
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write(this[i, j] + (j < Cols - 1 ? "; " : ""));
                }
                Console.WriteLine("}");
            }
            Console.WriteLine(new string('-', 30));
        }

        public T this[int indexA, int indexB]
        {
            get => data[indexA, indexB];
            set => data[indexA, indexB] = value;
        }
    }
}