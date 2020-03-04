using System;

namespace Lab_4
{
    public class SquareMatrix : AbstractMatrix<int>
    {
        private static AbstractMatrix<int>[] _matrices = null;
        private static int _arrLen = 0;
        private static int _itemsAmount = 0;

        public static int ArrLen => _arrLen;
        public static int ItemsAmount => _itemsAmount;

        public static void InitArray(int len)
        {
            _arrLen = len;
            _matrices = new AbstractMatrix<int>[_arrLen];
        }
        
        public static void AddItem(AbstractMatrix<int> matrix)
        {
            if (_itemsAmount >= _arrLen) throw new ArgumentException("The array is full!");
            _matrices[_itemsAmount++] = matrix;
        }

        public static void RemoveItem(int index)
        {
            if (index > _itemsAmount) throw new ArgumentException("Index is out of range!");
            _matrices[index] = null;
            while (index < _arrLen - 1 && _matrices[index + 1] != null)
            {
                _matrices[index] = _matrices[index + 1];
                _matrices[++index] = null;
            }

            _itemsAmount--;
        }

        public static AbstractMatrix<int> GetItem(int index)
        {
            return _matrices[index];
        }

        public static void PrintArr()
        {
            foreach (var mat in _matrices)
                mat.Print();
        }

        private int _size = 0;
        public SquareMatrix(int size) : base(size, size)
        {
            _size = size;
        }

        public int Determinant()
        {
            int total = 0;
            if (_size == 1)
                total = this[0, 0];
            else if (_size == 2)
                total = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            else
                // Split matrix by first row
                for (int i = 0; i < _size; i++)
                {
                    // For each column in the first row, create
                    // a lower-rank matrix and fill it excluding
                    // the first row and the current column
                    var mat = new SquareMatrix(_size - 1);
                    for (int j = 1; j < _size; j++)
                    {
                        int col = 0;
                        for (int k = 0; k < _size; k++)
                        {
                            if (k == i)
                                continue;
                            mat[j - 1, col++] = this[j, k];
                        }
                    }
                    // Add (subtract if the sum of item indices is odd) the product
                    // of the current item and its additional matrix determinant.
                    // The recursive nature of this allows calculating determinants
                    // for matrices of any rank.
                    total += mat.Determinant() * this[0, i] * (i % 2 == 0 ? 1 : -1);
                }

            return total;
        }

        public override void Input()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    int input;
                    Console.Write($"Matrix[{i+1}, {j+1}] = ");
                    while (!Int32.TryParse(Console.ReadLine(), out input))
                    {
                        Console.WriteLine("Failed to parse value.");
                        Console.Write($"Matrix[{i+1}, {j+1}] = ");
                    }

                    this[i, j] = input;
                }
            }
        }
    }
}