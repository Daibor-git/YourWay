using BastrikovTask.Methods;
using BastrikovTask.Methods.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastrikovTask
{
    public class Matrix
    {
        public int[,] TargetMatrix;

        // Классы для решения задачи Коммивояжера
        private static BruteForce bruteForce = new BruteForce();
        private static BranchClassic branchClassic = new BranchClassic();
        private static BranchClassicPlus branchClassicPlus = new BranchClassicPlus();
        private static BranchOleg branchOleg = new BranchOleg();
        private static BCPlus bcPlus = new BCPlus();

        // Путь для каждого из методов
        Dictionary<int, int> BruteForceWay;
        Dictionary<int, int> BranchClassicWay;
        Dictionary<int, int> BranchClassicPlusWay;
        Dictionary<int, int> BranchOlegWay;
        Dictionary<int, int> BCPWay;


        // Время для каждого из методов
        long BruteForceTime;
        long BranchClassicTime;
        long BranchClassicPlusTime;
        long BranchOlegTime;
        long bcTime;


        // Сумма расстояний для каждого из методов
        int BruteForceSum;
        int BranchClassicSum;
        int BranchClassicPlusSum;
        int BranchOlegSum;
        int BCSum;


        public Matrix() { }

        public int[,] setTargetMatrix(int[,] _some_matrix)
        {
            TargetMatrix = _some_matrix;

            return TargetMatrix.Clone() as int [,];
        }

        private void BruteForceSolution()
        {
            bruteForce.BruteForceStart(TargetMatrix, 0, out BruteForceSum, out BruteForceWay, out BruteForceTime);
        }

        private void BranchClassicSolution()
        {
            branchClassic.Start(TargetMatrix, out BranchClassicSum, out BranchClassicWay, out BranchClassicTime, true);
        }

        private void BranchClassicPlusSolution()
        {
            branchClassicPlus.Start(TargetMatrix, out BranchClassicPlusSum, out BranchClassicPlusWay, out BranchClassicPlusTime, true);
        }

        private void BranchOlegSolution()
        {
            //  branchOleg.StartBranchBuild();
        }

        private void BCPlusSolution()
        {
            bcPlus.Start(TargetMatrix, out BCSum, out BCPWay, out bcTime, true);
        }

        public string getBruteForceSolutionString()
        {
            BruteForceSolution();

            string solution = "Полный перебор для матрицы " + TargetMatrix.GetLength(0) + "x" + TargetMatrix.GetLength(0) +
                " ,путь : ";

            foreach (var item in BruteForceWay)
            {
                string temp = "(" + item.Key + "," + item.Value + ");";
                solution += temp;
            }

            solution += " сумма расстояний : " + BruteForceSum;
            solution += ", время работы алгоритма : " + BruteForceTime + "ms. .";
            return solution;
        }

        public string getBranchClassicSolutionString(int mode)
        {
            BranchClassicSolution();
            string solution = "";

            if (mode == 0)
            {
                solution = "Метод ветвей и границ для матрицы " + TargetMatrix.GetLength(0) + "x" + TargetMatrix.GetLength(0) +
                   " ,путь : ";
            }
            else
            {
                solution = "Метод с объеденением " + TargetMatrix.GetLength(0) + "x" + TargetMatrix.GetLength(0) +
                   " ,путь : ";
            }

            foreach (var item in BranchClassicWay)
            {
                string temp = "(" + item.Key + "," + item.Value + ");";
                solution += temp;
            }

            solution += " сумма расстояний : " + BranchClassicSum;
            solution += ", время работы алгоритма : " + BranchClassicTime + "ms. .";

            return solution;
        }

        public string getBranchClassicPlusSolutionString()
        {
            BranchClassicPlusSolution();

            string solution = "Метод ветвей и границ+ для матрицы " + TargetMatrix.GetLength(0) + "x" + TargetMatrix.GetLength(0) +
                " ,путь : ";

            foreach (var item in BranchClassicPlusWay)
            {
                string temp = "(" + item.Key + "," + item.Value + ");";
                solution += temp;
            }

            solution += " сумма расстояний : " + BranchClassicPlusSum;
            solution += ", время работы алгоритма : " + BranchClassicPlusTime + "ms. .";
            return solution;
        }

        public string getBCPlusSolutionString(int mode)
        {
            BCPlusSolution();
            string solution = "";

            if (mode == 0)
            {
                solution = "Метод ветвей и границ + для матрицы " + TargetMatrix.GetLength(0) + "x" + TargetMatrix.GetLength(0) +
                   " ,путь : ";
            }
            else
            {
                solution = "Метод с объеденением " + TargetMatrix.GetLength(0) + "x" + TargetMatrix.GetLength(0) +
                   " ,путь : ";
            }

            foreach (var item in BCPWay)
            {
                string temp = "(" + item.Key + "," + item.Value + ");";
                solution += temp;
            }

            solution += " сумма расстояний : " + BCSum;
            solution += ", время работы алгоритма : " + bcTime + "ms. .";

            return solution;
        }

        public static string GetSolutionByBranchSharpMethod(int[,] matrix, bool isSort)
        {
            string answerString = "";
            AnswerDTO answerDTO = null;

            if (matrix != null)
                answerDTO = BaBSharp.Start(matrix, isSort);
            else
                return "Matrix is null...";

            if (answerDTO != null)
                return answerDTO.ToString();
            else
                return "AnswerDTO is null...";
        }
    }
}
