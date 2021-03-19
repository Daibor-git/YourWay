using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BastrikovTask
{
    class BranchOleg
    {

        public static string Start(int[,] matrix, string processName)
        {
            string matrixString = GetStringMatrix(matrix);
            string answer = ExecuteCppProcess(processName, matrixString);
            return answer;
        }

        private static string GetStringMatrix(int[,] matrix)
        {
            string matixString = matrix.GetLength(0).ToString();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matixString += "|" + matrix[i, j]; 
                }
            }

            return matixString;
        }

        private static string ExecuteCppProcess(string processName, string matrixString)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;

            // Перехватываем вывод
            p.StartInfo.RedirectStandardOutput = true;
            // Запускаемое приложение
            p.StartInfo.FileName = processName;

            // Передаем необходимые аргументы
            // p.Arguments = "example.txt";

            p.StartInfo.Arguments = matrixString;
            p.Start();

            // Результат работы консольного приложения
            string output = p.StandardOutput.ReadToEnd();

            // Дождаться завершения запущенного приложения
           // p.WaitForExit();

            return output;
        }
    }
}