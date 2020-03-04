using System;

namespace Lab_4
{
    public class SquareMatrix : AbstractMatrix<int>
    {
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
                for (int i = 0; i < _size; i++)
                {
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
                    while (!Int32.TryParse(Console.ReadLine(), out input))
                        Console.WriteLine("Failed to parse value.");
                    this[i, j] = input;
                }
            }
        }
    }
}