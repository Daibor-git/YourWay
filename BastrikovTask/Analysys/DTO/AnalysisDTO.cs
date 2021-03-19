using BastrikovTask.Analysys.TSTLang;
using BastrikovTask.Methods.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BastrikovTask.Analysys.DTO
{
    class AnalysisDTO
    {
        private string firstMethodTitle;
        private string secondMethodTitle;

        private int matrixSize;
        private int countOfMatrix;

        private List<int> firstMethodAllMatrixSum;
        private List<int> secondMethodAllMatrixSum;
        private List<int> firstAndSecondMethodsDelta;

        private int secondMethodSumEqualsFirstMethodSum;
        private int secondMethodSumBestThenFirstMethodSum;
        private int secondMethodSumLessThenFirstMethodSum;

        public AnalysisDTO(string firstMethodTitle, string secondMethodTitle
            , int matrixSize, int countOfMatrix
            , List<int> firstMethodAllMatrixSum, List<int> secondMethodAllMatrixSum)
        {
            this.firstMethodTitle = firstMethodTitle;
            this.secondMethodTitle = secondMethodTitle;
            this.matrixSize = matrixSize;
            this.countOfMatrix = countOfMatrix;
            this.firstMethodAllMatrixSum = firstMethodAllMatrixSum;
            this.secondMethodAllMatrixSum = secondMethodAllMatrixSum;

            CompareMethods();
        }

        // Think about refactoring...
        public AnalysisDTO(TstLangCommand tstLangCommand, List<AnswerDTO> answersForFirstMethod, List<AnswerDTO> answersForSecondMethod) {
            this.FirstMethodTitle = tstLangCommand.FirstMethod.ToString();
            this.SecondMethodTitle = tstLangCommand.SecondMethod.ToString();
            this.MatrixSize = tstLangCommand.SizeOfMatrix;
            this.CountOfMatrix = tstLangCommand.CountOfMatrices;
            this.FirstMethodAllMatrixSum = answersForFirstMethod.Select(x => x.Sum).ToList();
            this.SecondMethodAllMatrixSum = answersForSecondMethod.Select(x => x.Sum).ToList();

            CompareMethods();
        }

        public string FirstMethodTitle { get => firstMethodTitle; set => firstMethodTitle = value; }
        public string SecondMethodTitle { get => secondMethodTitle; set => secondMethodTitle = value; }

        public int MatrixSize { get => matrixSize; set => matrixSize = value; }
        public int CountOfMatrix { get => countOfMatrix; set => countOfMatrix = value; }

        public List<int> FirstMethodAllMatrixSum { get => firstMethodAllMatrixSum; set => firstMethodAllMatrixSum = value; }
        public List<int> SecondMethodAllMatrixSum { get => secondMethodAllMatrixSum; set => secondMethodAllMatrixSum = value; }
        public List<int> FirstAndSecondMethodsDelta { get => firstAndSecondMethodsDelta; set => firstAndSecondMethodsDelta = value; }

        public int SecondMethodSumEqualsFirstMethodSum { get => secondMethodSumEqualsFirstMethodSum; set => secondMethodSumEqualsFirstMethodSum = value; }
        public int SecondMethodSumBestThenFirstMethodSum { get => secondMethodSumBestThenFirstMethodSum; set => secondMethodSumBestThenFirstMethodSum = value; }
        public int SecondMethodSumLessThenFirstMethodSum { get => secondMethodSumLessThenFirstMethodSum; set => secondMethodSumLessThenFirstMethodSum = value; }

        private void CompareMethods()
        {
            FirstAndSecondMethodsDelta = new List<int>();

            for (int i = 0; i < FirstMethodAllMatrixSum.Count; i++)
                FirstAndSecondMethodsDelta.Add(SecondMethodAllMatrixSum[i] - FirstMethodAllMatrixSum[i]);

            secondMethodSumEqualsFirstMethodSum = FirstAndSecondMethodsDelta.Where(x => x == 0).Count();
            SecondMethodSumBestThenFirstMethodSum = FirstAndSecondMethodsDelta.Where(x => x < 0).Count();
            SecondMethodSumLessThenFirstMethodSum = FirstAndSecondMethodsDelta.Where(x => x > 0).Count();
        }
    }
}