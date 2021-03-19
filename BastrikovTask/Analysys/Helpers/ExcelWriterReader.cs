using BastrikovTask.Analysys.DTO;
using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BastrikovTask.Analysys.Helpers
{
    class ExcelWriterReader<T> : IWriterReader<T>
    {
        public T ReadFromFile(string path)
        {
            throw new NotImplementedException();
        }

        public bool WriteToFile(string nameOfFile, string directoryPath, T objectToSave)
        {
            AnalysisDTO analysysDTO = null;

            if (objectToSave is AnalysisDTO)
                analysysDTO = objectToSave as AnalysisDTO;
            else
                throw new InvalidCastException("This is object not instatnce of AnalysysDTO");

            #region Работа с директорией 

            if (directoryPath == null || directoryPath.Length == 0)
                directoryPath = @"c:\Parsed_Data";  // Директория для сохранения файлов Excel

            // Проверяем , чтобы директория существовала. Если её нет — создаем.
            try
            {
                // Проверяем, существует ли директория
                if (!Directory.Exists(directoryPath))
                {
                    // Пытаемся создать директорию
                    DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            #endregion

            #region Работа с файлом

            if (nameOfFile == null || nameOfFile.Length == 0)
                nameOfFile = "Count - " + analysysDTO.CountOfMatrix + "__Size - " + analysysDTO.MatrixSize;

            // Создание файла
            string file = "C:\\Parsed_Data\\" + nameOfFile + ".xls";

            // Проверка на существование файла
            if (File.Exists(file))
            {
                int i = 1;
                do
                {
                    file = "C:\\Parsed_Data\\" + nameOfFile + "-" + i + ".xls";
                    ++i;
                } while (File.Exists(file));
            }
            #endregion

            #region Запись в Excel(.xls)
            // Создание книжки
            Workbook workbook = new Workbook();
            // Создание таблицы
            Worksheet worksheet = new Worksheet("First List");

            // Указываем ширину столбцов
            worksheet.Cells.ColumnWidth[0, 0] = 7000;
            worksheet.Cells.ColumnWidth[0, 1] = 7000;
            worksheet.Cells.ColumnWidth[0, 2] = 7000;
            worksheet.Cells.ColumnWidth[0, 3] = 7000;

            worksheet.Cells[0, 0] = new Cell("Количество матриц: " + analysysDTO.CountOfMatrix);
            worksheet.Cells[1, 0] = new Cell("Размерность матриц: " + analysysDTO.MatrixSize);
            worksheet.Cells[2, 0] = new Cell("Совпало решений: " + analysysDTO.SecondMethodSumEqualsFirstMethodSum);
            worksheet.Cells[3, 0] = new Cell("PLUS лучше Classic: " + analysysDTO.SecondMethodSumBestThenFirstMethodSum);
            worksheet.Cells[4, 0] = new Cell("Classic лучше PLUS: " + analysysDTO.SecondMethodSumLessThenFirstMethodSum);

            // Делаем «Шапку» для таблицы
            worksheet.Cells[6, 0] = new Cell("Матрица №");
            worksheet.Cells[6, 1] = new Cell(analysysDTO.FirstMethodTitle);
            worksheet.Cells[6, 2] = new Cell(analysysDTO.SecondMethodTitle);
            worksheet.Cells[6, 3] = new Cell("Delta (SUM(plus) - SUM(classic))");

            int startIndex = 7;

            // Указываем , в какую ячейку писать информацию, которая пишется в таблицу
            for (int i = 0; i < analysysDTO.FirstMethodAllMatrixSum.Count; i++)
            {
                worksheet.Cells[i + startIndex, 0] = new Cell(i + 1);
                worksheet.Cells[i + startIndex, 1] = new Cell(analysysDTO.FirstMethodAllMatrixSum[i]);
                worksheet.Cells[i + startIndex, 2] = new Cell(analysysDTO.SecondMethodAllMatrixSum[i]);
                worksheet.Cells[i + startIndex, 3] = new Cell(analysysDTO.FirstAndSecondMethodsDelta[i]);
            }

            // Добавляем таблицу в таблицы файла
            workbook.Worksheets.Add(worksheet);
            // Сохраняем файл
            workbook.Save(file);
            #endregion

            return true;
        }

        public void Test()
        {
            string file = "C:\\Parsed_Data\\newdoc.xls";
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("First Sheet");
            worksheet.Cells[0, 1] = new Cell((short)1);
            worksheet.Cells[2, 0] = new Cell(9999999);
            worksheet.Cells[3, 3] = new Cell((decimal)3.45);
            worksheet.Cells[2, 2] = new Cell("Text string");
            worksheet.Cells[2, 4] = new Cell("Second string");
            worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD"); worksheet.Cells.ColumnWidth[0, 1] = 3000;
            workbook.Worksheets.Add(worksheet); workbook.Save(file);
        }
    }
}