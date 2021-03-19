
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BastrikovTask
{
    class TestWorker
    {
        static public bool BranchClassicTest(int size, int matrixCount) {
            List<int[,]> arrays = new List<int[,]>(matrixCount);
            BranchClassic branchClassic = new BranchClassic();

            Random random = new Random();

            for (int i = 0; i < arrays.Count; i++)
            {
                int[,] matrix = new int[12, 12];

                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (j == k)
                        {
                            matrix[j, k] = int.MaxValue;
                        }
                        else {
                            matrix[j, k] = random.Next(0 , 100);
                        }
                    }
                }

                arrays[i] = matrix;
            }

            int sum;
            long time;
            int counter = 0;

            Dictionary<int, int> ways;
            foreach (var item in arrays)
            {
                branchClassic.Start(item, out sum, out ways, out time, true);

                if (ways.Count == size)
                {
                    ++counter;
                    Debug.WriteLine("Матрица {0} посчитана корректно", counter);
                }
                else {
                    Debug.WriteLine("Ошибка теста");
                    return false ;
                }
            }

            return true;
        }
    }
}