using BastrikovTask.NewGeneration.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BastrikovTask
{
    class BCPlus
    {
        #region private-поля
        private static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        Dictionary<int, int> way_func;
        List<Node> wayList;

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

        private int штраф = 0;

        // Максимальный сумма элементов относительно индекса нулевого элемента
        private int sum_of_elements_for_zero_index_max = INF;
        // Сумма элементов относительно индекса нулевого элемена
        private int sum_of_elements_for_zero_index = 0;
        // Индекс строки искомого нулевого элемента
        private int zero_row_index = -1;
        // Индекс столбца искомого нулевого элемента
        private int zero_column_index = -1;

        #region Переменные, для левой ветки
        //private int 
        #endregion

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
        private bool AllElementsIsInfMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (MainMatrix[i, j] != INF) return false;
                }
            }
            return true;
        }

        // Проверяем все ли элементы равны нулям - РАБОТАЕТ
        private bool AllElementsIsZeroMainMatrix()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (MainMatrix[i, j] == INF) continue;

                    if (MainMatrix[i, j] != 0) return false;
                }
            }
            return true;
        }
        // Если все элементы равны нулю - то получаем путь - РАБОТАЕТ
        private void GetWay()
        {
            List<int> тутЕщёНеБыли = new List<int>();
            List<int> сюдаНеЗаходили = new List<int>();
            List<Node> tempNodes = new List<Node>();

            // Смотрим, куда мы ещё не заходили
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                bool isInot = true;
                bool isJnot = true;


                for (int j = 0; j < wayList.Count; j++)
                {
                    // Тут ещё не были
                    if (i == (wayList[j].i - 1))
                    {
                        isInot = false;
                    }

                    // Сюда ещё не заходили
                    if (i == (wayList[j].j - 1))
                    {
                        isJnot = false;
                    }
                }

                if (isJnot)
                    тутЕщёНеБыли.Add(i);

                if (isInot)
                    сюдаНеЗаходили.Add(i);
            }

            // Формируем список всех возможных путей
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(1); j++)
                {
                    if (MainMatrix[i, j] == INF) continue;

                    if (MainMatrix[i, j] == 0)
                    {
                        wayList.Add(new Node(i + 1, j + 1));
                    }
                }
            }

            Console.WriteLine("Новое: ");
            for (int i = 0; i < wayList.Count; i++)
            {
                Debug.WriteLine(wayList[i].i + " " + wayList[i].j);
            }
        }


        private void SortWay()
        {
            Dictionary<int, int> SortedWay = new Dictionary<int, int>();

            int previous_town = wayList[0].i;

            // Сортируем по возрастанию
            wayList.OrderBy(w => w.i);

            for (int i = 0; i < wayList.Count; i++)
            {
                for (int j = 0; j < wayList.Count; j++)
                {
                    if (wayList.ElementAt(j).i == previous_town && !SortedWay.ContainsKey(wayList.ElementAt(j).i))
                    {
                        previous_town = wayList.ElementAt(j).j;
                        SortedWay.Add(wayList.ElementAt(j).i, wayList.ElementAt(j).j);
                   //     break;
                    }
                }
            }

            way_func = SortedWay;
        }

        private bool AllElementInfinity()
        {
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(0); j++)
                {
                    if (MainMatrix[i, j] != INF)
                        return false;
                }
            }

            return true;
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
            if (iteration == 0)
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

        }
        // Находит индекс "оптимального" нулевого элемента - РАБОТАЕТ - ЭТОМ МЕТОД ИЗМЕНЯЕТСЯ В КЛАССИКА+
        // ПЕРЕДЕЛАТЬ
        private void WorkWithZeroElementsInMainMatrix()
        {
            sum_of_elements_for_zero_index_max = 0;
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

                        
                        sum_of_elements_for_zero_index = min_row + min_column;

                        // Ветвей и границ +
                        sum_of_elements_for_zero_index += Support.GetEstimationForBranch(MainMatrix, i, j);

                        // Добавить проверку двух сумм - КАКИХ СУММ, НАДО БЫЛО ПИСАТЬ!!!

                        if (sum_of_elements_for_zero_index >= sum_of_elements_for_zero_index_max)
                        {
                            //    if (wayList.Count != 0)
                            //    {
                            //        Node node = wayList.SingleOrDefault(w => (w.i - 1) == j);
                            //
                            //        if (node != null)
                            //        {
                            //           // if ((iteration + 1) != MainMatrix.GetLength(0))
                            //
                            //            if (AllElementsIsZeroMainMatrix() == false)
                            //            {
                            //                continue;
                            //            }
                            //        }
                            //    }

                            sum_of_elements_for_zero_index_max = sum_of_elements_for_zero_index;
                            штраф = sum_of_elements_for_zero_index_max;
                            zero_row_index = i;
                            zero_column_index = j;
                        }
                    }
                }
            }

            Debug.WriteLine("Выбранный элемент: " + zero_row_index + ", " + zero_column_index);
        }

        private int[] GetDiagonalElement(int i, int j) {
            int[] elementIJ = new int[2];

            // Матрица с наименованиями
            int[,] matrix = 
                new int[MainMatrix.GetLength(0) - wayList.Count, MainMatrix.GetLength(0) - wayList.Count];
            matrix[0, 0] = INF;


            int[] строкиНеВычеркнутые = new int[MainMatrix.GetLength(0) - wayList.Count - 1];
            int[] столбцыНеВычеркнутые = new int[MainMatrix.GetLength(0) - wayList.Count - 1];

            // 
            int counter = 0;
            int indexator = 0;

            // Получаем не вычеркнутые строки
            while (indexator < строкиНеВычеркнутые.Length) {
                Node node = wayList.SingleOrDefault(n => (n.i - 1) == counter);

                if (node == null && counter != i) {
                    строкиНеВычеркнутые[indexator] = counter;
                    ++indexator;
                }

                ++counter;
            }

            counter = 0;
            indexator = 0;

            // Получаем не вычеркнутые столбцы
            while (indexator < столбцыНеВычеркнутые.Length)
            {
                Node node = wayList.SingleOrDefault(n => (n.j - 1) == counter);

                if (node == null && counter != j)
                {
                    столбцыНеВычеркнутые[indexator] = counter;
                    ++indexator;
                }

                ++counter;
            }

            int indexator2 = 0;

            // Инициализируем имена строк и столбцев
            for (int k = 1; k < matrix.GetLength(0); k++)
            {
                matrix[k, 0] = строкиНеВычеркнутые[indexator2];
                matrix[0, k] = столбцыНеВычеркнутые[indexator2];
                ++indexator2;
            }

            // Заполняем матрицу элементами основной матрицы
            for (int k = 1; k < matrix.GetLength(0); k++)
            {
                for (int n = 1; n < matrix.GetLength(0); n++)
                {
                    matrix[k, n] = MainMatrix[matrix[k, 0], matrix[0, n]];
                }
            }

            Console.WriteLine("Матрица сокращения: ");
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                for (int l = 0; l < matrix.GetLength(1); l++)
                {
                    Console.Write(matrix[k, l] + "\t");
                }
                Console.WriteLine();
            }

            // определить в какой город, из всех строк не закрыт поход
            bool[] ways = new bool[matrix.GetLength(0)];
            ways[0] = true;
            int i_result = -1;
            int j_result = -1;

            for (int k = 1; k < matrix.GetLength(0); k++)
            {
                for (int n = 1; n < matrix.GetLength(0); n++)
                {
                    if (matrix[k, n] == INF) {
                        ways[n] = true;
                        break;
                    }

                    if (n == matrix.GetLength(0) - 1) {
                        i_result = k;
                    }
                }
            }

            for (int k = 1; k < ways.Length; k++)
            {
                if (!ways[k])
                {
                    j_result = k;
                }
            }

            if (i_result == -1 || j_result == -1)
            {
                elementIJ[0] = -1;
                elementIJ[1] = -1;
            }
            else
            {
                elementIJ[0] = matrix[i_result, 0];
                elementIJ[1] = matrix[0, j_result];
            }


            //// Находим элемент равный бесконечности
            //// И определяем, он в главной или побочной диагонали
            //int inf_i = 0;
            //int inf_j = 0;
            //bool isMainDiaglonal = false;

            //for (int k = 1; k < matrix.GetLength(0); k++)
            //{
            //    for (int n = 1; n < matrix.GetLength(0); n++)
            //    {
            //        if (matrix[k, n] == INF) {
            //            inf_i = k; // matrix[k, 0];
            //            inf_j = n;   //matrix[0, n];

            //            if (k == n)
            //            {
            //                isMainDiaglonal = true;
            //            }
            //            else {
            //                isMainDiaglonal = false;
            //            }

            //            break;
            //        }
            //    }
            //}

            //// Строим диагональ, и находим элемент
            //int matrixSize = matrix.GetLength(0);

            //int i_min = inf_i;
            //int j_min = inf_j;

            //int i_max = inf_i;
            //int j_max = inf_j;

            //int i_min_s = inf_i;
            //int j_min_s = inf_j;

            //int i_max_s = inf_i;
            //int j_max_s = inf_j;


            //while (true) {
            //    // Для главной диагонали
            //    if (isMainDiaglonal)
            //    {
            //        if (i_max + 1 < matrixSize && j_max + 1 < matrixSize)
            //        {
            //            ++i_max;
            //            ++j_max;
            //            if (matrix[i_max, j_max] != INF)
            //            {
            //                elementIJ[0] = matrix[i_max, 0];
            //                elementIJ[1] = matrix[0, j_max];
            //                break;
            //            }
            //        }

            //        if (i_min - 1 >= 1 && j_max - 1 >= 1)
            //        {
            //            --i_min;
            //            --j_min;
            //            if (matrix[i_min, j_min] != INF)
            //            {
            //                elementIJ[0] = matrix[i_min, 0];
            //                elementIJ[1] = matrix[0, j_min];
            //                break;
            //            }
            //        }
            //    }
            //    // для побочной
            //    else
            //    {
            //        if (i_max_s - 1 >= 1 && j_max_s + 1 < matrixSize)
            //        {
            //            --i_max_s;
            //            ++j_max_s;
            //            if (matrix[i_max_s, j_max_s] != INF)
            //            {
            //                elementIJ[0] = matrix[i_max_s, 0];
            //                elementIJ[1] = matrix[0, j_max_s];
            //                break;
            //            }
            //        }

            //        if (i_min_s + 1 < matrixSize && j_max_s - 1 >= 1)
            //        {
            //            ++i_min_s;
            //            --j_min_s;
            //            if (matrix[i_min_s, j_min_s] != INF)
            //            {
            //                elementIJ[0] = matrix[i_min_s, 0];
            //                elementIJ[1] = matrix[0, j_min_s];
            //                break;
            //            }
            //        }
            //    }
            //}

            // Если остался только один элемент
            if (matrix.GetLength(0) == 2)
            {
                wayList.Add(new Node(matrix[1, 0] + 1, matrix[0, 1] + 1));
            }

            return elementIJ;
        }

        // Создаём матрицу для левой ветки -  РАБОТАЕТ
        private void CreateLeftBranch()
        {
            matrix_left_branch = MainMatrix.Clone() as int[,];
            int size = MainMatrix.GetLength(0);

            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                matrix_left_branch[zero_row_index, i] = INF;
                matrix_left_branch[i, zero_column_index] = INF;
            }

            bool flag = true;

            if (matrix_left_branch[zero_column_index, zero_row_index] != INF)
            {
                matrix_left_branch[zero_column_index, zero_row_index] = INF;
            }
            else {
                    int[] elementIJ = GetDiagonalElement(zero_row_index, zero_column_index);
                if (elementIJ[0] != -1)
                    matrix_left_branch[elementIJ[0], elementIJ[1]] = INF;
            }
           // else
           // {
           //     if (zero_column_index < size - 2)
           //     {
           //         for (int i = zero_column_index; i < size; i++)
           //         {
           //             if (matrix_left_branch[i, zero_row_index] != INF)
           //             {
           //                 matrix_left_branch[i, zero_row_index] = INF;
           //                 goto External;
           //             }
           //         }
           //     }
           //
           //     if (zero_column_index >= 1)
           //     {
           //         for (int i = zero_column_index; i >=0; i--)
           //         {
           //             if (matrix_left_branch[i, zero_row_index] != INF)
           //             {
           //                 matrix_left_branch[i, zero_row_index] = INF;
           //                 goto External;
           //             }
           //         }
           //     }
           //
           //     if (zero_row_index < size - 2)
           //     {
           //         for (int i = zero_row_index; i < size; i++)
           //         {
           //             if (matrix_left_branch[zero_column_index, i] != INF)
           //             {
           //                 matrix_left_branch[zero_column_index, i] = INF;
           //                 goto External;
           //             }
           //         }
           //     }
           //
           //     if (zero_row_index >= 1)
           //     {
           //         for (int i = zero_row_index; i >= 0; i--)
           //         {
           //             if (matrix_left_branch[zero_column_index, i] != INF)
           //             {
           //                 matrix_left_branch[zero_column_index, i] = INF;
           //                 goto External;
           //             }
           //         }
           //     }
           //
           // }

            External:;
            // Этот путь нам уже не нужен
            // if (matrix_left_branch[zero_column_index, zero_row_index] != INF)
            //     matrix_left_branch[zero_column_index, zero_row_index] = INF;
            // else if (zero_column_index == (matrix_left_branch.GetLength(0) - 1)
            //          && zero_row_index == (matrix_left_branch.GetLength(0) - 1))
            // {
            //     matrix_left_branch[zero_column_index - 1, zero_row_index - 1] = INF;
            // }
            // else if (zero_column_index == (matrix_left_branch.GetLength(0) - 1)
            //     && zero_row_index != (matrix_left_branch.GetLength(0) - 1))
            // {
            //     matrix_left_branch[zero_column_index - 1, zero_row_index] = INF;
            // }
            // else if (zero_column_index != (matrix_left_branch.GetLength(0) - 1)
            //    && zero_row_index == (matrix_left_branch.GetLength(0) - 1))
            // {
            //     matrix_left_branch[zero_column_index, zero_row_index - 1] = INF;
            // }

            // Это всё хуйня...
            // Запрещаем поход в первую ветку
            //  if (wayList.Count != 0)
            //  {
            //      for (int i = 0; i < wayList.Count; i++)
            //      {
            //          matrix_left_branch[zero_row_index, (wayList[i].i - 1)] = INF;
            //          matrix_left_branch[wayList[i].j - 1, zero_column_index] = INF;
            //      }
            //  }

            // if (wayList.Count < (MainMatrix.GetLength(0) - 1))
            // {
            // Запрещаем из всех других веток, поход в данную
            //     for (int i = 0; i < matrix_left_branch.GetLength(0); i++)
            //   {
            //  matrix_left_branch[i, zero_column_index] = INF;
            //    }
            //    }

            LeftBranchLimit = MainMatrixMinLimit;

            //// Идем в плюс от диагонального
            //int k = zero_column_index;
            //int f = zero_row_index;
            //int n = MainMatrix.GetLength(0);
            //while (true) {
            //    ++k;
            //    ++f;

            //    if (n == k || f == n) break;

            //    matrix_left_branch[k, f] = INF;
            //}

            //while (true)
            //{
            //    --k;
            //    --f;

            //    if ( k < 0 || f < 0) break;

            //    matrix_left_branch[k, f] = INF;
            //}
        }
        // Создаём матрицу для правой ветки - НУЖНО ТЕСТИРОВАНИЕ
        private void CreateRightBranch()
        {
            matrix_right_branch = MainMatrix.Clone() as int[,];
            matrix_right_branch[zero_row_index, zero_column_index] = INF;
            RightBranchLimit = MainMatrixMinLimit + штраф;
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

            //sum_of_elements_for_zero_index_max = 0;
            //for (int i = 0; i < MainMatrix.GetLength(1); i++)
            //{
            //    for (int j = 0; j < MainMatrix.GetLength(1); j++)
            //    {
            //        sum_of_elements_for_zero_index = 0;
            //        if (matrix_left_branch[i, j] == 0)
            //        {
            //            // Минимальный в строке
            //            int min_row = INF;
            //            // Минимальный в столбце
            //            int min_column = INF;


            //            for (int k = 0; k < matrix_left_branch.GetLength(1); k++)
            //            {
            //                // Играемся со строкой

            //                // Если мы прошли всю строку, а в ней только бесконечности, то : 
            //                if (k == (matrix_left_branch.GetLength(1) - 1) && min_row == INF)
            //                {
            //                    min_row = 0;
            //                    continue;
            //                }

            //                // Ибо бесконечность
            //                if (i == k) continue;
            //                // Ибо не следует смотреть на нулевой элемент заданный
            //                if (k == j) continue;
            //                // Ну, на всякий случай
            //                if (matrix_left_branch[i, k] == INF) continue;

            //                if (matrix_left_branch[i, k] <= min_row) min_row = matrix_left_branch[i, k];
            //            }

            //            for (int k = 0; k < matrix_left_branch.GetLength(1); k++)
            //            {
            //                // Играемся со столбцом

            //                // Если мы прошли всю строку, а в ней только бесконечности, то : 
            //                if (k == (matrix_left_branch.GetLength(1) - 1) && min_column == INF)
            //                {
            //                    min_column = 0;
            //                    continue;
            //                }
            //                // Ибо бесконечность
            //                if (j == k) continue;
            //                // Ибо не следует смотреть на нулевой элемент заданный
            //                if (k == i) continue;
            //                // Ну, на всякий случай
            //                if (matrix_left_branch[k, j] == INF) continue;

            //                if (matrix_left_branch[k, j] <= min_column) min_column = matrix_left_branch[k, j];
            //            }
            //            sum_of_elements_for_zero_index = min_row + min_column;

            //            if (sum_of_elements_for_zero_index >= sum_of_elements_for_zero_index_max)
            //            {
            //                sum_of_elements_for_zero_index_max = sum_of_elements_for_zero_index;
            //            }
            //        }
            //    }
            //}
            //LeftBranchLimit += sum_of_elements_for_zero_index_max;
        }

        #endregion

        /*
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
    */

        // Выбираем ветку - НУЖНО ТЕСТИРОВАНИЕ
        private void SetMainMatrix()
        {
            if (LeftBranchLimit <= RightBranchLimit)
            {
                Debug.WriteLine("Выбрана левая ветка");
                MainMatrix = matrix_left_branch.Clone() as int[,];
                wayList.Add(new Node(zero_row_index + 1, zero_column_index + 1));
                MainMatrixMinLimit = LeftBranchLimit;
            }
            else
            {
                Debug.WriteLine("ВЫбрана правая ветка");
                MainMatrix = matrix_right_branch.Clone() as int[,];
                //   way_func.Add(zero_row_index + 1, zero_column_index + 1);
                MainMatrixMinLimit = RightBranchLimit;
            }
            ShowMatrixInLogs();
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
            //   iteration = 0;
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
                if (AllElementsIsInfMainMatrix() || AllElementsIsZeroMainMatrix())
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
                //  GetMinElementsRowMatrixRightBranch();
                //  RowSubMatrixRightBranch();
                //  GetMinElementsColumnMatrixRightBranch();
                //  ColumnSubMatrixRightBranch();
                //  SetMatrixRightBranchMinLimit();

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
            iteration = 0;
            sw.Start();

            sum = 0;
            way_func = new Dictionary<int, int>();
            wayList = new List<Node>();

            MainMatrix = matrix.Clone() as int[,];
            MainMatrixMinLimit = 0;

            _time = sw.ElapsedMilliseconds;
            sw.Reset();

            // Запускаем основной алгоритм, и искренне молимся, что он будет работать
            MainAlgoritm();

            if (sort)
            {
                SortWay();
            }

            // Если чудо случилось, то здесь будет толковый путь для заданной матрицы
            way = way_func;
            // Если предыдущие чудо случилось, то здесь будет сумма расстояний для заданного решения
            sum = MainMatrixMinLimit;

            // Обнуляем лимит для основной матрицы
            MainMatrixMinLimit = 0;

            ClearVariables();
        }

        private void ShowMatrixInLogs()
        {
            Console.WriteLine("Matrix: ");
            for (int i = 0; i < MainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MainMatrix.GetLength(0); j++)
                {
                    if (MainMatrix[i, j] == INF)
                    {
                        Console.Write("INF\t");
                        continue;
                    }
                    Console.Write(MainMatrix[i, j] + "\t");
                }

                Console.WriteLine();
            }
        }
    }
}
