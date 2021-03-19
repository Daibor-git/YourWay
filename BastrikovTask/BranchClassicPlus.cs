using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BastrikovTask
{
    class BranchClassicPlus
    {
        #region private-поля
        static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        Dictionary<int, int> way_func;
        private static int INF = Int32.MaxValue;

        private int iteration = 0;

        // Основная матрица
        private int[,] MainMatrix;
        // Левая ветка (город выбран)
        private int[,] matrix_left_branch;
        // Правая ветка (город исключен)
        private int[,] matrix_right_branch;

        // Минимальные элементы в строке
        private int[] row_min_element;
        // Индекс минимальных элементов в строке
        private int[] row_min_index;

        // Минимальные элементы в столбце
        private int[] column_min_element;
        // Индекс минимальных элементов с столбце
        private int[] column_min_index;
        // Лимит основной матрицы
        private int MainMatrixMinLimit = 0;
        // Лимиь для левой ветки
        private int LeftBranchLimit = 0;
        // Лимит для правой ветки
        private int RightBranchLimit = 0;

        // Максимальный сумма элементов относительно индекса нулевого элемента
        private int sum_of_elements_for_zero_index_max = INF;
        // Сумма элементов относительно индекса нулевого элемена
        private int sum_of_elements_for_zero_index = 0;
        // Индекс строки искомого нулевого элемента
        private int zero_row_index = -1;
        // Индекс столбца искомого нулевого элемента
        private int zero_column_index = -1;

        #endregion

        #region private-методы
        // Инициализирует массивы для поиска минимальных элементов - РАБОТАЕТ
        private void InitializeArraysWithMinElements()
        {
            row_min_element = new int[MainMatrix.GetLength(0)];
            column_min_element = new int[MainMatrix.GetLength(0)];

            row_min_index = new int[MainMatrix.GetLength(0)];
            column_min_index = new int[MainMatrix.GetLength(0)];

            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                row_min_element[i] = INF;
                column_min_element[i] = INF;
            }
        }
        // Проверяем все ли элементы равны нулям - РАБОТАЕТ
        private bool AllElementsIsZeroMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    // Если бесконечность, то продолжить проверку
                    if (MainMatrix[i, j] == INF) continue;

                    if (MainMatrix[i, j] != 0) return false;
                }
            }
            return true;
        }
        // Если все элементы равны нулю - то получаем путь - РАБОТАЕТ
        private void GetWay()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (MainMatrix[i, j] == INF) continue;

                    if (MainMatrix[i, j] == 0)
                    {
                        way_func.Add(i + 1, j + 1);
                        MainMatrix[j, i] = INF;

                        for (int k = 0; k < MainMatrix.GetLength(1); k++)
                        {
                            MainMatrix[i, k] = INF;
                            MainMatrix[k, j] = INF;
                        }
                    }
                }
            }
        }

        private void SortWay()
        {
            Dictionary<int, int> SortedWay = new Dictionary<int, int>();

            int previous_town = 1;

            for (int i = 0; i < way_func.Count; i++)
            {
                for (int j = 0; j < way_func.Count; j++)
                {
                    if (way_func.ElementAt(j).Key == previous_town)
                    {
                        previous_town = way_func.ElementAt(j).Value;
                        SortedWay.Add(way_func.ElementAt(j).Key, way_func.ElementAt(j).Value);
                        break;
                    }
                }
            }

            way_func = SortedWay;
        }

        // Находит минимальные элементы по-строчно в основной матрице - РАБОТАЕТ
        private void GetMinElementsRowMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (MainMatrix[i, j] == INF) continue;

                    if (MainMatrix[i, j] <= row_min_element[i])
                    {
                        row_min_element[i] = MainMatrix[i, j];

                        row_min_index[i] = j;
                    }
                }
            }
        }
        // Отнимает минимальные элементы по-строчно в основной матрице - РАБОТАЕТ
        private void RowSubMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (row_min_element[i] == INF) continue;

                    if (MainMatrix[i, j] == INF || MainMatrix[i, j] == 0) continue;
                    MainMatrix[i, j] -= row_min_element[i];
                }
            }
        }
        // Находит минимальные элементы по-столбцам в основной матрице - РАБОТАЕТ
        private void GetMinElementsColumnMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (iteration == 1 && i == 3)
                    {
                    }
                    if (MainMatrix[j, i] == INF) continue;

                    if (MainMatrix[j, i] < column_min_element[i])
                    {
                        column_min_element[i] = MainMatrix[j, i];
                        column_min_index[i] = j;
                    }
                }
            }
        }
        // Отнимает минимальные элементы по столбцам в основной матрице - РАБОТАЕТ
        private void ColumnSubMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (column_min_element[i] == INF) continue;

                    if (MainMatrix[j, i] == INF || MainMatrix[j, i] == 0) continue;
                    MainMatrix[j, i] -= column_min_element[i];
                }
            }
        }
        // Изменяет минимальный лимит для основной матрицы - РАБОТАЕТ
        private void SetMainMatrixMinLimit()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                if (row_min_element[i] == INF && column_min_element[i] == INF) continue;

                else if (row_min_element[i] == INF)
                {
                    MainMatrixMinLimit += column_min_element[i];
                    continue;
                }

                else if (column_min_element[i] == INF)
                {
                    MainMatrixMinLimit += row_min_element[i];
                    continue;
                }

                MainMatrixMinLimit += row_min_element[i] + column_min_element[i];
            }
        }
        // Находит индекс "оптимального" нулевого элемента - РАБОТАЕТ - ЭТОМ МЕТОД ИЗМЕНЯЕТСЯ В КЛАССИКА+
        private void WorkWithZeroElementsInMainMatrix()
        {
            sum_of_elements_for_zero_index_max = Int32.MinValue;
            zero_row_index = -1;
            zero_column_index = -1;

            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    sum_of_elements_for_zero_index = 0;
                    if (MainMatrix[i, j] == 0)
                    {
                        // Минимальный в строке
                        int min_row = INF;
                        // Минимальный в столбце
                        int min_column = INF;
                        // Минимальный элемент строки диагонального элемента
                        int min_row_diagonal = INF;
                        // Минимальный элемент столбца диагонального элемента
                        int min_column_diagonal = INF;

                        for (int k = 0; k < MainMatrix.GetLength(1); k++)
                        {
                            // Играемся со строкой

                            // Если мы прошли всю строку, а в ней только бесконечности, то : 
                            if (k == (MainMatrix.GetLength(1) - 1) && min_row == INF)
                            {
                                min_row = 0;
                                continue;
                            }

                            // Ибо бесконечность
                            if (i == k) continue;
                            // Ибо не следует смотреть на нулевой элемент заданный
                            if (k == j) continue;
                            // Ну, на всякий случай
                            if (MainMatrix[i, k] == INF) continue;

                            if (MainMatrix[i, k] <= min_row) min_row = MainMatrix[i, k];
                        }

                        for (int k = 0; k < MainMatrix.GetLength(1); k++)
                        {
                            // Играемся со столбцом

                            // Если мы прошли всю строку, а в ней только бесконечности, то : 
                            if (k == (MainMatrix.GetLength(1) - 1) && min_column == INF)
                            {
                                min_column = 0;
                                continue;
                            }
                            // Ибо бесконечность
                            if (j == k) continue;
                            // Ибо не следует смотреть на нулевой элемент заданный
                            if (k == i) continue;
                            // Ну, на всякий случай
                            if (MainMatrix[k, j] == INF) continue;

                            if (MainMatrix[k, j] <= min_column) min_column = MainMatrix[k, j];
                        }

                        for (int k = 0; k < MainMatrix.GetLength(0); k++)
                        {
                            // Играемся со строкой диагонального элемента

                            // Если мы прошли всю строку, а в ней только бесконечности, то : 
                            if (k == (MainMatrix.GetLength(1) - 1) && min_row_diagonal == INF)
                            {
                                min_row_diagonal = 0;
                                continue;
                            }

                            // Ибо бесконечность
                            if (j == k) continue;
                            // Ибо не следует смотреть на нулевой элемент заданный
                            if (k == i) continue;
                            // Ну, на всякий случай
                            if (MainMatrix[j, k] == INF) continue;

                            if (MainMatrix[j, k] <= min_row_diagonal) min_row_diagonal = MainMatrix[j, k];

                        }

                        for (int k = 0; k < MainMatrix.GetLength(1); k++)
                        {
                            // Играемся со столбцом

                            // Если мы прошли весь столбец, а в нем только бесконечности, то : 
                            if (k == (MainMatrix.GetLength(1) - 1) && min_column_diagonal == INF)
                            {
                                min_column_diagonal = 0;
                                continue;
                            }
                            // Ибо бесконечность
                            if (i == k) continue;
                            // Ибо не следует смотреть на нулевой элемент заданный
                             if (k == j) continue;
                            // Ну, на всякий случай
                            if (MainMatrix[k, i] == INF) continue;

                            if (MainMatrix[k, i] <= min_column_diagonal) min_column_diagonal = MainMatrix[k, i];
                        }

                        sum_of_elements_for_zero_index = min_row + min_column - min_row_diagonal - min_column_diagonal;

                        if (sum_of_elements_for_zero_index >= sum_of_elements_for_zero_index_max)
                        {
                            sum_of_elements_for_zero_index_max = sum_of_elements_for_zero_index;
                            zero_row_index = i;
                            zero_column_index = j;
                        }
                    }
                }
            }
        }
        // Создаём матрицу для левой ветки -  РАБОТАЕТ
        private void CreateLeftBranch()
        {
            matrix_left_branch = MainMatrix.Clone() as int[,];
            // Этот путь нам уже не нужен
            matrix_left_branch[zero_column_index, zero_row_index] = INF;


            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (i == zero_row_index || j == zero_column_index)
                        matrix_left_branch[i, j] = INF;
                }
            }
        }
        // Создаём матрицу для правой ветки - НУЖНО ТЕСТИРОВАНИЕ
        private void CreateRightBranch()
        {
            matrix_right_branch = MainMatrix.Clone() as int[,];
            matrix_right_branch[zero_row_index, zero_column_index] = INF;
        }

        #region Работаем с левой веткой

        // Находит минимальные элементы по-строчно в матрице левой ветки - РАБОТАЕТ
        private void GetMinElementsRowMatrixLeftBranch()
        {
            InitializeArraysWithMinElements();

            for (int i = 0; i < matrix_left_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_left_branch.GetLength(1); j++)
                {
                    if (matrix_left_branch[i, j] == INF) continue;

                    if (matrix_left_branch[i, j] <= row_min_element[i])
                    {
                        row_min_element[i] = matrix_left_branch[i, j];

                        row_min_index[i] = j;
                    }
                }
            }
        }
        // Отнимает минимальные элементы по-строчно в матрице левой ветки - РАБОТАЕТ
        private void RowSubMatrixLeftBranch()
        {
            for (int i = 0; i < matrix_left_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_left_branch.GetLength(1); j++)
                {
                    if (row_min_element[i] == INF) continue;

                    if (matrix_left_branch[i, j] == INF || matrix_left_branch[i, j] == 0) continue;
                    matrix_left_branch[i, j] -= row_min_element[i];
                }
            }
        }
        // Находит минимальные элементы по-столбцам в матрице левой ветки - РАБОТАЕТ
        private void GetMinElementsColumnMatrixLeftBranch()
        {
            for (int i = 0; i < matrix_left_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_left_branch.GetLength(1); j++)
                {
                    if (matrix_left_branch[j, i] == INF) continue;

                    if (matrix_left_branch[j, i] < column_min_element[i])
                    {
                        column_min_element[i] = matrix_left_branch[j, i];
                        column_min_index[i] = j;
                    }
                }
            }
        }
        // Отнимает минимальные элементы по столбцам в матрице левой ветки - РАБОТАЕТ
        private void ColumnSubMatrixLeftBranch()
        {
            for (int i = 0; i < matrix_left_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_left_branch.GetLength(1); j++)
                {
                    if (column_min_element[i] == INF) continue;

                    if (matrix_left_branch[j, i] == INF || matrix_left_branch[j, i] == 0) continue;
                    matrix_left_branch[j, i] -= column_min_element[i];
                }
            }
        }
        // Изменяет минимальный лимит для матрицы левой ветки  - РАБОТАЕТ
        private void SetMatrixLeftBranchMinLimit()
        {
            for (int i = 0; i < matrix_left_branch.GetLength(0); i++)
            {
                if (row_min_element[i] == INF && column_min_element[i] == INF) continue;

                else if (row_min_element[i] == INF)
                {
                    LeftBranchLimit += column_min_element[i];
                    continue;
                }

                else if (column_min_element[i] == INF)
                {
                    LeftBranchLimit += row_min_element[i];
                    continue;
                }

                LeftBranchLimit += row_min_element[i] + column_min_element[i];
            }
        }

        #endregion

        #region Работаем с правой веткой

        // Находит минимальные элементы по-строчно в матрице правой ветки - РАБОТАЕТ
        private void GetMinElementsRowMatrixRightBranch()
        {
            InitializeArraysWithMinElements();

            for (int i = 0; i < matrix_right_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_right_branch.GetLength(1); j++)
                {
                    if (matrix_right_branch[i, j] == INF) continue;

                    if (matrix_right_branch[i, j] <= row_min_element[i])
                    {
                        row_min_element[i] = matrix_right_branch[i, j];

                        row_min_index[i] = j;
                    }
                }
            }
        }
        // Отнимает минимальные элементы по-строчно в матрице правой ветки - РАБОТАЕТ
        private void RowSubMatrixRightBranch()
        {
            for (int i = 0; i < matrix_right_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_right_branch.GetLength(1); j++)
                {
                    if (row_min_element[i] == INF) continue;

                    if (matrix_right_branch[i, j] == INF || matrix_right_branch[i, j] == 0) continue;
                    matrix_right_branch[i, j] -= row_min_element[i];
                }
            }
        }
        // Находит минимальные элементы по-столбцам в матрице правой ветки - РАБОТАЕТ
        private void GetMinElementsColumnMatrixRightBranch()
        {
            for (int i = 0; i < matrix_right_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_right_branch.GetLength(1); j++)
                {
                    if (matrix_right_branch[j, i] == INF) continue;

                    if (matrix_right_branch[j, i] < column_min_element[i])
                    {
                        column_min_element[i] = matrix_right_branch[j, i];
                        column_min_index[i] = j;
                    }
                }
            }
        }
        // Отнимает минимальные элементы по столбцам в матрице правой ветки - РАБОТАЕТ
        private void ColumnSubMatrixRightBranch()
        {
            for (int i = 0; i < matrix_right_branch.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_right_branch.GetLength(1); j++)
                {
                    if (column_min_element[i] == INF) continue;

                    if (matrix_right_branch[j, i] == INF || matrix_right_branch[j, i] == 0) continue;
                    matrix_right_branch[j, i] -= column_min_element[i];
                }
            }
        }
        // Изменяет минимальный лимит для матрицы правой ветки  - РАБОТАЕТ
        private void SetMatrixRightBranchMinLimit()
        {
            for (int i = 0; i < matrix_right_branch.GetLength(0); i++)
            {
                if (row_min_element[i] == INF && column_min_element[i] == INF) continue;

                else if (row_min_element[i] == INF)
                {
                    RightBranchLimit += column_min_element[i];
                    continue;
                }

                else if (column_min_element[i] == INF)
                {
                    RightBranchLimit += row_min_element[i];
                    continue;
                }

                RightBranchLimit += row_min_element[i] + column_min_element[i];
            }
        }

        #endregion

        // Выбираем ветку - НУЖНО ТЕСТИРОВАНИЕ
        private void SetMainMatrix()
        {
            if (LeftBranchLimit <= RightBranchLimit)
            {
                MainMatrix = matrix_left_branch.Clone() as int[,];
                way_func.Add(zero_row_index + 1, zero_column_index + 1);
                MainMatrixMinLimit += LeftBranchLimit;
            }
            else
            {
                MainMatrix = matrix_right_branch.Clone() as int[,];
                MainMatrixMinLimit += RightBranchLimit;
            }

        }
        // "Обнуляем" переменные - РАБОТАЕТ
        private void ClearVariables()
        {
            LeftBranchLimit = 0;
            RightBranchLimit = 0;
            sum_of_elements_for_zero_index_max = 0;
            sum_of_elements_for_zero_index = 0;
            zero_row_index = -1;
            zero_column_index = -1;
            iteration = 0;
        }
        // Основной алгоритм - РАБОТАЕТ
        private void MainAlgoritm()
        {
            while (true)
            {
                InitializeArraysWithMinElements();
                GetMinElementsRowMainMatrix();
                RowSubMainMatrix();
                GetMinElementsColumnMainMatrix();
                ColumnSubMainMatrix();
                SetMainMatrixMinLimit();

                // Если все элементы нули, то составить путь
                if (AllElementsIsZeroMainMatrix())
                {
                    GetWay();
                    break;
                }

                // Находим оптимальный элемент
                WorkWithZeroElementsInMainMatrix();

                // Создаём ветки
                CreateLeftBranch();
                CreateRightBranch();

                // Работаем с левой веткой
                GetMinElementsRowMatrixLeftBranch();
                RowSubMatrixLeftBranch();
                GetMinElementsColumnMatrixLeftBranch();
                ColumnSubMatrixLeftBranch();
                SetMatrixLeftBranchMinLimit();

                // Работаем с правой веткой
                GetMinElementsRowMatrixRightBranch();
                RowSubMatrixRightBranch();
                GetMinElementsColumnMatrixRightBranch();
                ColumnSubMatrixRightBranch();
                SetMatrixRightBranchMinLimit();

                // Выбираем ветку
                SetMainMatrix();

                ClearVariables();
                ++iteration;
            }

            ClearVariables();
        }
        #endregion

        // Основной магический метод, с которого всё начинается - НУЖНО ТЕСТИРОВАНИЕ
        /// <summary>
        /// Решение задачи Коммивояжера методом ветвей и границ
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="sum"></param>
        /// <param name="way"></param>
        /// <param name="_time"></param>
        public void Start(int[,] matrix,
            out int sum, out Dictionary<int, int> way, out long _time,
            bool sort)
        {
            sw.Start();

            sum = 0;
            way_func = new Dictionary<int, int>();

            MainMatrix = matrix.Clone() as int[,];
            MainMatrixMinLimit = 0;

            _time = sw.ElapsedMilliseconds;
            sw.Reset();

            // Запускаем основной алгоритм, и искренне молимся, что он будет работать
            MainAlgoritm();

            //if (sort)
            //{
            //    SortWay();
            //}

            // Если чудо случилось, то здесь будет толковый путь для заданной матрицы
            way = way_func;
            // Если предыдущие чудо случилось, то здесь будет сумма расстояний для заданного решения
            sum = MainMatrixMinLimit;

            // Обнуляем лимит для основной матрицы
            MainMatrixMinLimit = 0;

            ClearVariables();
        }
    }
}
