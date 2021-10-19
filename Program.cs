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
            double[][] matrix = new double[N][];
            for (int i = 0; i < N; i++)
            {
                matrix[i] = new double[N];
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i][j] = rnd.Next(10);
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

    public class HeapSort
    {
        public void sort(int[] arr)
        {
            int n = arr.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                heapify(arr, i, 0);
            }
        }

        void heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && arr[l] > arr[largest])
                largest = l;

            if (r < n && arr[r] > arr[largest])
                largest = r;

            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                heapify(arr, n, largest);
            }
        }
        public int[] ArrGenerate(int n)
        {
            Random rnd = new Random();
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(30);
            }
            return arr;
        }
    }

    class Laplace
    {
        public double[,] GetMinor(double[,] matrix, int row, int column)
        {
            double[,] buf = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i != row) || (j != column))
                    {
                        if (i > row && j < column) buf[i - 1, j] = matrix[i, j];
                        if (i < row && j > column) buf[i, j - 1] = matrix[i, j];
                        if (i > row && j > column) buf[i - 1, j - 1] = matrix[i, j];
                        if (i < row && j < column) buf[i, j] = matrix[i, j];
                    }
                }
            return buf;
        }
        public double Determ(double[,] matrix)
        {
            double det = 0;
            int Rank = matrix.GetLength(0);
            if (Rank == 1) det = matrix[0, 0];
            if (Rank == 2) det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            if (Rank > 2)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    det += Math.Pow(-1, 0 + j) * matrix[0, j] * Determ(GetMinor(matrix, 0, j));
                }
            }
            return det;
        }
        public double[,] MatrixGenerate(int n)
        {
            Random rnd = new Random();
            double[,] matr = new double[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matr[i,j] = rnd.Next(10);
                }
            }
            return matr;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GetData1();
        }

        static void GetData3()
        {
            string Path = @"..\..\DataLU.csv";
            int N = 300;
            for (int i = 2; i < N; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                double[][] matrix = LU.MatrixGenerate(i);
                double result = LU.MatrixDeterminant(matrix);
                stopwatch.Stop();
                string time = (stopwatch.ElapsedTicks).ToString();
                File.AppendAllText(Path, time + ";");
            }
        }
        static void GetData4()
        {
            string Path = @"..\..\DataHS.csv";
            int N = 1000;
            for (int i = 2; i < N; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                HeapSort s = new HeapSort();
                s.sort(s.ArrGenerate(i));
                stopwatch.Stop();
                string time = (stopwatch.ElapsedTicks).ToString();
                File.AppendAllText(Path, time + ";");
            }
        }
        static void GetData1()
        {
            string Path = @"..\..\DataLP.csv";
            int N = 12;
            for (int i = 2; i < N; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Laplace l = new Laplace();
                l.Determ(l.MatrixGenerate(i));
                stopwatch.Stop();
                string time = (stopwatch.ElapsedTicks).ToString();
                File.AppendAllText(Path, time + ";");
            }
        }
    }
    
}
