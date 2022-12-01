using System;
using LibMatrix;

namespace Lib_12
{
    public static class ExtensionMatrix
    {
        /// <summary>
        /// Заполнение матрицы целыми числами
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="min">Иинимум диапазона</param>
        /// <param name="max">Максимум диапазона</param>
        /// <returns name="matrix">Заполненная матрица</returnsname></returns>
        public static Matrix<int> Generate(this Matrix<int> matrix, int min = 1, int max = 100)
        {
            if (min > max)
            {
                Invert(ref min, ref max);
            }
            Random rnd = new();
            for (int i = 0; i < matrix.Rows; i++)
            {
                for(int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i,j] = rnd.Next(min, max);
                }
            }
            return matrix;
        }
        /// <summary>
        /// Нахождение элементов больше 15
        /// </summary>
        /// <param name="matrix">Матрица для нахождения суммы</param>
        /// <returns name="sum">Сумма элементов</returns>
        public static int Sum(this Matrix<int> matrix, int numberOfColumn)
        {
            if(numberOfColumn > matrix.Columns)
            {
                return 0;
            }
            int sum = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                sum += matrix[i, numberOfColumn-1];
            }
            return sum;
        }
        public static int Multiply(this Matrix<int> matrix, int numberOfColumn)
        {
            if (numberOfColumn > matrix.Columns)
            {
                return 0;
            }
            int multiply = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                multiply *= matrix[i, numberOfColumn-1];
            }
            return multiply;
        }

        /// <summary>
        /// Меняет значения двух переменных местами
        /// </summary>
        /// <param name="value1">Переменная 1</param>
        /// <param name="value2">Переменная 2</param>
        private static void Invert(ref int value1, ref int value2)
        {
            int buffer = value1;
            value1 = value2;
            value2 = buffer;
        }
    }
}
