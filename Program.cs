using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace alg2
{
    class LU
    {
        public static double[][] MatrixDecompose(double[][] matrix,
            out int[] perm, out int toggle)
        {
            // Разложение LUP Дулитла. Предполагается,
            // что матрица квадратная.
            int n = matrix.Length; // для удобства
            double[][] result = MatrixDuplicate(matrix);
            perm = new int[n];
            for (int i = 0; i < n; ++i) { perm[i] = i; }
            toggle = 1;
            for (int j = 0; j < n - 1; ++j) // каждый столбец
            {
                double colMax = Math.Abs(result[j][j]); // Наибольшее значение в столбце j
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) // перестановка строк
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = perm[pRow]; // Меняем информацию о перестановке
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; // переключатель перестановки строк
                }
                if (Math.Abs(result[j][j]) < 1.0E-20)
                    return null;
                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                        result[i][k] -= result[i][j] * result[j][k];
                }
            } // основной цикл по столбцу j
            return result;
        }
        public static double[][] MatrixDuplicate(double[][] matrix)
        {
            // Предполагается, что матрица не нулевая
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // Копирование значений
                for (int j = 0; j < matrix[i].Length; ++j)
                    result[i][j] = matrix[i][j];
            return result;
        }
        public static double MatrixDeterminant(double[][] matrix)
        {
            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute MatrixDeterminant");
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
                result *= lum[i][i];
            return result;
        }
        public static double[][] MatrixGenerate(int N)
        {
            Random rnd = new Random();
            double[][] matrix = new double[N, N];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    matrix[i, j] = rnd.Next(10);
                }
            }
            return matrix;
        }
        public static double[][] MatrixCreate(int rows, int cols)
        {
            // Создаем матрицу, полностью инициализированную
            // значениями 0.0. Проверка входных параметров опущена.
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols]; // автоинициализация в 0.0
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GetData3();
        }

        public static void GetData3()
        {
            string PathSelSort = @"..\..\DataSelSort.csv";
            Stopwatch stopwatch = new Stopwatch();
            int N = 10;
            for (int i = 1; i <= N; i++)
            {
                string Path = @"..\..\DataSelSort.csv";
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Srart();
                double[][] matrix = LU.MatrixGenerate(N);
                matrix = LU.MatrixDeterminant(matrix);
                stopwatch.Stop();
                string time = (stopwatch.ElapsedTicks).ToString();
                File.AppendAllText(Path, time + ";");
            }
        }
    }
    
}
