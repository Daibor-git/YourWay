using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BastrikovTask
{
    // Задача коммивояжера - полный обход графа
    class BruteForce
    {
        private int[,] TargetMatrix;
        private int Sum;
        private long Time;

        static int INF = Int32.MaxValue;

        private int[] M;               // Отметка пройденных "городов"
        private int[] W;               // Текущая последовательность обхода
        private int[] Wmin;            // Оптимальная последовательность обхода
        private int minlnt = -1;       // Длина минимального пути

        //public int way_[];
        public int sum = 0;
        public int minSum = 0;
        public Dictionary<int, int> Way;

        private void Step(int n, int k, int lnt)
        {        // n - номер шага, k - номер "города"
            if (n == TargetMatrix.GetLength(0))
            {                                   // lnt - длина пройденого пути
                if (minlnt == -1 || lnt < minlnt)          // Обход закончен - фиксировать минимум
                {
                    minlnt = lnt;                     // Запомнить длину и последовательность
                    for (int i = 0; i < TargetMatrix.GetLength(0); i++)         // обхода
                        Wmin[i] = W[i];
                }
                return;
            }
            if (M[k] == 1) return;                                 // Повторное прохождение
            W[n] = k;                                              // Дополнить последовательность обхода
            M[k] = 1;                                              // Отметить прохождение
            for (int i = 0; i < TargetMatrix.GetLength(0); i++)
            {                                                      // Просмотр соседей
                if (TargetMatrix[k, i] == INF) continue;             // Соседи не связаны - пропустить
                Step(n + 1, i, lnt + TargetMatrix[k, i]);          // Рекурсивный вызов для соседнего
            }                                                      // "города" с учетом расстояния до него
            M[k] = INF;                                              // Сбросить отметку
        }

        public void BruteForceStart(int[,] _matrix,
            int start_town, out int _sum, out Dictionary<int, int> _way,
            out long _time)
        {
            this.TargetMatrix = _matrix.Clone() as int[,];
            _sum = 0;
            _way = new Dictionary<int, int>();
            _time = 0;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            M = new int[TargetMatrix.GetLength(0)];

            for (int i = 0; i < TargetMatrix.GetLength(0); i++)
            {
                M[i] = INF;
            }


            W = new int[TargetMatrix.GetLength(0)];
            Wmin = new int[TargetMatrix.GetLength(0)];
            minlnt = -1;

            Step(0, start_town, 0);
            sw.Stop();

            // Костыляра=============================
            int prev = (Wmin[0]);
            int sum = 0;
            for (int i = 1; i < Wmin.GetLength(0); i++)
            {
                sum += TargetMatrix[prev, Wmin[i]];
                prev = Wmin[i];
            }
            sum += TargetMatrix[prev, start_town];
            // --------------------------------------

            int previous = (Wmin[0] + 1);
            for (int i = 1; i < Wmin.GetLength(0); i++)
            {
                _way.Add(previous, Wmin[i] + 1);
                previous = Wmin[i] + 1;
            }

            _way.Add(previous, (start_town + 1));
            _sum = minlnt;
            _time = sw.ElapsedMilliseconds;

            // if (Way == null)
            // {
            //     Way = new Dictionary<int, int>();
            // }
            // else
            //     Way.Clear();
            //
            // switch (TargetMatrix.GetLength(0))
            // {
            //     case 3:
            //             BruteForceFor3();
            //             break;
            //     case 4:
            //             BruteForceFor4();
            //             break;
            //     case 5:
            //         BruteForceFor5();
            //         break;
            //     case 6: 
            //         BruteForceFor6();
            //         break;
            //     case 7:
            //         BruteForceFor7();
            //         break;
            //     default:
            //         break;
            // }
            //
            // _sum = minSum;
            // // Тут бы чекнуть потом объекты
            // _way = Way;

        }

        //private void BruteForceFor3()
        //{
        //    minSum = BruteForce.INF;

        //    // Следующий не равен предыдущему
        //    for (int i = 1; i < 3; i++)
        //    {
        //        for (int j = 1; j < 3; j++)
        //        {
        //            if (j == i) continue;

        //            sum = TargetMatrix[0, i] + TargetMatrix[i, j] + TargetMatrix[j, 0];

        //            if (sum < minSum)
        //            {
        //                Way.Clear();
        //                minSum = sum;
        //                Way.Add(1, i + 1);
        //                Way.Add(i + 1, j + 1);
        //                Way.Add(j + 1, 1);
        //            }
        //        }
        //    }
        //}

        //private void BruteForceFor4()
        //{
        //    minSum = BruteForce.INF;

        //    for (int i = 1; i < 4; i++)
        //    {
        //        for (int j = 1; j < 4; j++)
        //        {
        //            if (i == j) continue;

        //            for (int k = 1; k < 4; k++)
        //            {
        //                if (j == k || i == k) continue;

        //                sum = TargetMatrix[0, i] + TargetMatrix[i, j] + TargetMatrix[j, k] + TargetMatrix[k, 0];

        //                if (sum < minSum)
        //                {
        //                    Way.Clear();
        //                    minSum = sum;
        //                    Way.Add(1, i + 1);
        //                    Way.Add(i + 1, j + 1);
        //                    Way.Add(j + 1, k + 1);
        //                    Way.Add(k + 1, 1);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void BruteForceFor5()
        //{
        //    minSum = BruteForce.INF;

        //    for (int i = 1; i < 5; i++)
        //    {
        //        for (int j = 1; j < 5; j++)
        //        {
        //            if (i == j) continue;

        //            for (int k = 1; k < 5; k++)
        //            {
        //                if (j == k || i == k) continue;

        //                for (int l = 1; l < 5; l++)
        //                {
        //                    if (l == i || l == j || l == k) continue;

        //                    sum = TargetMatrix[0, i] + TargetMatrix[i, j] + TargetMatrix[j, k] + TargetMatrix[k, l] + TargetMatrix[l, 0];

        //                    if (sum < minSum)
        //                    {
        //                        Way.Clear();
        //                        minSum = sum;
        //                        Way.Add(1, i + 1);
        //                        Way.Add(i + 1, j + 1);
        //                        Way.Add(j + 1, k + 1);
        //                        Way.Add(k + 1, l + 1);
        //                        Way.Add(l + 1, 1);

        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

        //private void BruteForceFor6()
        //{
        //    minSum = BruteForce.INF;

        //    for (int i = 1; i < TargetMatrix.GetLength(0); i++)
        //    {
        //        for (int j = 1; j < TargetMatrix.GetLength(0); j++)
        //        {
        //            if (i == j) continue;

        //            for (int k = 1; k < TargetMatrix.GetLength(0); k++)
        //            {
        //                if (j == k || i == k) continue;

        //                for (int l = 1; l < TargetMatrix.GetLength(0); l++)
        //                {
        //                    if (l == i || l == j || l == k) continue;

        //                    for (int q = 1; q < TargetMatrix.GetLength(0); q++)
        //                    {
        //                        if (q == i || q == j || q == k || q == k || q==l) continue;

        //                        sum = TargetMatrix[0, i] + TargetMatrix[i, j] + TargetMatrix[j, k] +
        //                            TargetMatrix[k, l] + TargetMatrix[l, q] + TargetMatrix[q,0];

        //                        if (sum < minSum)
        //                        {
        //                            Way.Clear();
        //                            minSum = sum;
        //                            Way.Add(1, i + 1);
        //                            Way.Add(i + 1, j + 1);
        //                            Way.Add(j + 1, k + 1);
        //                            Way.Add(k + 1, l + 1);
        //                            Way.Add(l + 1, q + 1);
        //                            Way.Add(q + 1, 1);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void BruteForceFor7()
        //{
        //    minSum = BruteForce.INF;

        //    for (int i = 1; i < TargetMatrix.GetLength(0); i++)
        //    {
        //        for (int j = 1; j < TargetMatrix.GetLength(0); j++)
        //        {
        //            if (i == j) continue;

        //            for (int k = 1; k < TargetMatrix.GetLength(0); k++)
        //            {
        //                if (j == k || i == k) continue;

        //                for (int l = 1; l < TargetMatrix.GetLength(0); l++)
        //                {
        //                    if (l == i || l == j || l == k) continue;

        //                    for (int q = 1; q < TargetMatrix.GetLength(0); q++)
        //                    {
        //                        if (q == i || q == j || q == k || q == k || q == l) continue;

        //                        for (int w = 1; w < TargetMatrix.GetLength(0); w++)
        //                        {
        //                            if (w == i || w == j || w == k || w == k || w == l || w == q) continue;

        //                            sum = TargetMatrix[0, i] + TargetMatrix[i, j] + TargetMatrix[j, k] +
        //                                TargetMatrix[k, l] + TargetMatrix[l, q] + TargetMatrix[q, w] + 
        //                                TargetMatrix[w,0];

        //                            if (sum < minSum)
        //                            {
        //                                Way.Clear();
        //                                minSum = sum;
        //                                Way.Add(1, i + 1);
        //                                Way.Add(i + 1, j + 1);
        //                                Way.Add(j + 1, k + 1);
        //                                Way.Add(k + 1, l + 1);
        //                                Way.Add(l + 1, q + 1);
        //                                Way.Add(q + 1, w+1);
        //                                Way.Add(w + 1, 1);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
