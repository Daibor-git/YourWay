using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BastrikovTask.NewGeneration.Support
{
   public class Support
    {
        const int INF = Int32.MaxValue;

        public static int GetEstimationForBranch(int[,] _matrix, int index_x, int index_y)
        {
            int[,] matrix = _matrix.Clone() as int[,];
            int estimation = 0;
            int indexByX = index_x;
            int indexByY = index_y;
            int matrixSize = matrix.GetLength(0);

            // Находим минимальный элемент в строке
            int row_min_element = INF;

            for (int i = 0; i < matrixSize; i++)
            {
                int rowElement = matrix[index_x, i];

                if (rowElement == INF)
                    continue;
                if (i == index_y)
                    continue;

                if (rowElement < row_min_element)
                    row_min_element = rowElement;
            }

            // Если не нашли ни одного элемента
            row_min_element = row_min_element == INF ? 0 : row_min_element;

            // Находим минимальный элемент в столбце
            int column_min_element = INF;

            for (int i = 0; i < matrixSize; i++)
            {
                int columnElement = matrix[i, index_y];

                if (columnElement == INF)
                    continue;
                if (i == index_x)
                    continue;

                if (columnElement < column_min_element)
                    column_min_element = columnElement;
            }

            // Считаем оценку
            estimation = row_min_element + column_min_element;

            return estimation;
        }
    }
}
