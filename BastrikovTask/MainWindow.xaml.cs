using BastrikovTask.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BastrikovTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Переменные
        TextBox[,] TextBoxMatrix = new TextBox[12, 12];//поле для матрицы

        static Matrix MainMatrix;//сама матрица

        Label[] LabelArray = new Label[12];

        int mode = 3;
        #endregion



        private void InitializeComboBox()
        {
            comboBox.Items.Add("3");
            comboBox.Items.Add("4");
            comboBox.Items.Add("5");
            comboBox.Items.Add("6");
            comboBox.Items.Add("7");
            comboBox.Items.Add("8");
            comboBox.Items.Add("9");
            comboBox.Items.Add("10");
            comboBox.Items.Add("11");
            comboBox.Items.Add("12");

            comboBox.SelectedIndex = 0;
        }

        private void InitializeTextBoxMatrix()
        {
            TextBoxMatrix[0, 0] = element0;
            TextBoxMatrix[0, 1] = tbMatrix0x1;
            TextBoxMatrix[0, 2] = tbMatrix0x2;
            TextBoxMatrix[0, 3] = tbMatrix0x3;
            TextBoxMatrix[0, 4] = tbMatrix0x4;
            TextBoxMatrix[0, 5] = tbMatrix0x5;
            TextBoxMatrix[0, 6] = tbMatrix0x6;
            TextBoxMatrix[0, 7] = tbMatrix0x7;
            TextBoxMatrix[0, 8] = tbMatrix0x8;
            TextBoxMatrix[0, 9] = tbMatrix0x9;
            TextBoxMatrix[0, 10] = tbMatrix0x10;
            TextBoxMatrix[0, 11] = tbMatrix0x11;

            TextBoxMatrix[1, 0] = tbMatrix1x0;
            TextBoxMatrix[1, 1] = element1;
            TextBoxMatrix[1, 2] = tbMatrix1x2;
            TextBoxMatrix[1, 3] = tbMatrix1x3;
            TextBoxMatrix[1, 4] = tbMatrix1x4;
            TextBoxMatrix[1, 5] = tbMatrix1x5;
            TextBoxMatrix[1, 6] = tbMatrix1x6;
            TextBoxMatrix[1, 7] = tbMatrix1x7;
            TextBoxMatrix[1, 8] = tbMatrix1x8;
            TextBoxMatrix[1, 9] = tbMatrix1x9;
            TextBoxMatrix[1, 10] = tbMatrix1x10;
            TextBoxMatrix[1, 11] = tbMatrix1x11;

            TextBoxMatrix[2, 0] = tbMatrix2x0;
            TextBoxMatrix[2, 1] = tbMatrix2x1;
            TextBoxMatrix[2, 2] = element2;
            TextBoxMatrix[2, 3] = tbMatrix2x3;
            TextBoxMatrix[2, 4] = tbMatrix2x4;
            TextBoxMatrix[2, 5] = tbMatrix2x5;
            TextBoxMatrix[2, 6] = tbMatrix2x6;
            TextBoxMatrix[2, 7] = tbMatrix2x7;
            TextBoxMatrix[2, 8] = tbMatrix2x8;
            TextBoxMatrix[2, 9] = tbMatrix2x9;
            TextBoxMatrix[2, 10] = tbMatrix2x10;
            TextBoxMatrix[2, 11] = tbMatrix2x11;

            TextBoxMatrix[3, 0] = tbMatrix3x0;
            TextBoxMatrix[3, 1] = tbMatrix3x1;
            TextBoxMatrix[3, 2] = tbMatrix3x2;
            TextBoxMatrix[3, 3] = element3;
            TextBoxMatrix[3, 4] = tbMatrix3x4;
            TextBoxMatrix[3, 5] = tbMatrix3x5;
            TextBoxMatrix[3, 6] = tbMatrix3x6;
            TextBoxMatrix[3, 7] = tbMatrix3x7;
            TextBoxMatrix[3, 8] = tbMatrix3x8;
            TextBoxMatrix[3, 9] = tbMatrix3x9;
            TextBoxMatrix[3, 10] = tbMatrix3x10;
            TextBoxMatrix[3, 11] = tbMatrix3x11;

            TextBoxMatrix[4, 0] = tbMatrix4x0;
            TextBoxMatrix[4, 1] = tbMatrix4x1;
            TextBoxMatrix[4, 2] = tbMatrix4x2;
            TextBoxMatrix[4, 3] = tbMatrix4x3;
            TextBoxMatrix[4, 4] = element4;
            TextBoxMatrix[4, 5] = tbMatrix4x5;
            TextBoxMatrix[4, 6] = tbMatrix4x6;
            TextBoxMatrix[4, 7] = tbMatrix4x7;
            TextBoxMatrix[4, 8] = tbMatrix4x8;
            TextBoxMatrix[4, 9] = tbMatrix4x9;
            TextBoxMatrix[4, 10] = tbMatrix4x10;
            TextBoxMatrix[4, 11] = tbMatrix4x11;

            TextBoxMatrix[5, 0] = tbMatrix5x0;
            TextBoxMatrix[5, 1] = tbMatrix5x1;
            TextBoxMatrix[5, 2] = tbMatrix5x2;
            TextBoxMatrix[5, 3] = tbMatrix5x3;
            TextBoxMatrix[5, 4] = tbMatrix5x4;
            TextBoxMatrix[5, 5] = element5;
            TextBoxMatrix[5, 6] = tbMatrix5x6;
            TextBoxMatrix[5, 7] = tbMatrix5x7;
            TextBoxMatrix[5, 8] = tbMatrix5x8;
            TextBoxMatrix[5, 9] = tbMatrix5x9;
            TextBoxMatrix[5, 10] = tbMatrix5x10;
            TextBoxMatrix[5, 11] = tbMatrix5x11;

            TextBoxMatrix[6, 0] = tbMatrix6x0;
            TextBoxMatrix[6, 1] = tbMatrix6x1;
            TextBoxMatrix[6, 2] = tbMatrix6x2;
            TextBoxMatrix[6, 3] = tbMatrix6x3;
            TextBoxMatrix[6, 4] = tbMatrix6x4;
            TextBoxMatrix[6, 5] = tbMatrix6x5;
            TextBoxMatrix[6, 6] = element6;
            TextBoxMatrix[6, 7] = tbMatrix6x7;
            TextBoxMatrix[6, 8] = tbMatrix6x8;
            TextBoxMatrix[6, 9] = tbMatrix6x9;
            TextBoxMatrix[6, 10] = tbMatrix6x10;
            TextBoxMatrix[6, 11] = tbMatrix6x11;

            TextBoxMatrix[7, 0] = tbMatrix7x0;
            TextBoxMatrix[7, 1] = tbMatrix7x1;
            TextBoxMatrix[7, 2] = tbMatrix7x2;
            TextBoxMatrix[7, 3] = tbMatrix7x3;
            TextBoxMatrix[7, 4] = tbMatrix7x4;
            TextBoxMatrix[7, 5] = tbMatrix7x5;
            TextBoxMatrix[7, 6] = tbMatrix7x6;
            TextBoxMatrix[7, 7] = element7;
            TextBoxMatrix[7, 8] = tbMatrix7x8;
            TextBoxMatrix[7, 9] = tbMatrix7x9;
            TextBoxMatrix[7, 10] = tbMatrix7x10;
            TextBoxMatrix[7, 11] = tbMatrix7x11;

            TextBoxMatrix[8, 0] = tbMatrix8x0;
            TextBoxMatrix[8, 1] = tbMatrix8x1;
            TextBoxMatrix[8, 2] = tbMatrix8x2;
            TextBoxMatrix[8, 3] = tbMatrix8x3;
            TextBoxMatrix[8, 4] = tbMatrix8x4;
            TextBoxMatrix[8, 5] = tbMatrix8x5;
            TextBoxMatrix[8, 6] = tbMatrix8x6;
            TextBoxMatrix[8, 7] = tbMatrix8x7;
            TextBoxMatrix[8, 8] = element8;
            TextBoxMatrix[8, 9] = tbMatrix8x9;
            TextBoxMatrix[8, 10] = tbMatrix8x10;
            TextBoxMatrix[8, 11] = tbMatrix8x11;

            TextBoxMatrix[9, 0] = tbMatrix9x0;
            TextBoxMatrix[9, 1] = tbMatrix9x1;
            TextBoxMatrix[9, 2] = tbMatrix9x2;
            TextBoxMatrix[9, 3] = tbMatrix9x3;
            TextBoxMatrix[9, 4] = tbMatrix9x4;
            TextBoxMatrix[9, 5] = tbMatrix9x5;
            TextBoxMatrix[9, 6] = tbMatrix9x6;
            TextBoxMatrix[9, 7] = tbMatrix9x7;
            TextBoxMatrix[9, 8] = tbMatrix9x8;
            TextBoxMatrix[9, 9] = element9;
            TextBoxMatrix[9, 10] = tbMatrix9x10;
            TextBoxMatrix[9, 11] = tbMatrix9x11;

            TextBoxMatrix[10, 0] = tbMatrix10x0;
            TextBoxMatrix[10, 1] = tbMatrix10x1;
            TextBoxMatrix[10, 2] = tbMatrix10x2;
            TextBoxMatrix[10, 3] = tbMatrix10x3;
            TextBoxMatrix[10, 4] = tbMatrix10x4;
            TextBoxMatrix[10, 5] = tbMatrix10x5;
            TextBoxMatrix[10, 6] = tbMatrix10x6;
            TextBoxMatrix[10, 7] = tbMatrix10x7;
            TextBoxMatrix[10, 8] = tbMatrix10x8;
            TextBoxMatrix[10, 9] = tbMatrix10x9;
            TextBoxMatrix[10, 10] = element10;
            TextBoxMatrix[10, 11] = tbMatrix10x11;

            TextBoxMatrix[11, 0] = tbMatrix11x0;
            TextBoxMatrix[11, 1] = tbMatrix11x1;
            TextBoxMatrix[11, 2] = tbMatrix11x2;
            TextBoxMatrix[11, 3] = tbMatrix11x3;
            TextBoxMatrix[11, 4] = tbMatrix11x4;
            TextBoxMatrix[11, 5] = tbMatrix11x5;
            TextBoxMatrix[11, 6] = tbMatrix11x6;
            TextBoxMatrix[11, 7] = tbMatrix11x7;
            TextBoxMatrix[11, 8] = tbMatrix11x8;
            TextBoxMatrix[11, 9] = tbMatrix11x9;
            TextBoxMatrix[11, 10] = tbMatrix11x10;
            TextBoxMatrix[11, 11] = element11;
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeTextBoxMatrix();
            InitializeComboBox();

            MainMatrix = new Matrix();

            if (TestWorker.BranchClassicTest(100, 1000))
            {
                Debug.WriteLine("Тест BranchClassicTest - пройден");
            }
            else
            {
                Debug.WriteLine("Тест BranchClassicTest - не пройден");
            }
        }

        private void DisableTextBoxMatrixElements(int index)
        {
            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < index; j++)
                {
                    TextBoxMatrix[i, j].IsEnabled = false;
                    TextBoxMatrix[i, j].Visibility = Visibility.Hidden;
                }
            }
        }

        private void EnableTextBoxMatrixElements(int index)
        {
            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < index; j++)
                {
                    if (i == j)
                    {
                        TextBoxMatrix[i, j].Visibility = Visibility.Visible;
                        continue;
                    }
                    TextBoxMatrix[i, j].IsEnabled = true;
                    TextBoxMatrix[i, j].Visibility = Visibility.Visible;
                }
            }
        }

        private void comboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            mode = Convert.ToInt32(comboBox.SelectedItem.ToString());

            DisableTextBoxMatrixElements(12);
            EnableTextBoxMatrixElements(mode);
        }

        private void btRandomValue_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < mode; i++)
            {
                for (int j = 0; j < mode; j++)
                {
                    if (i == j) continue;

                    TextBoxMatrix[i, j].Text = random.Next(1, 100).ToString();
                }
            }
        }

        private void btConstantValue_Click(object sender, RoutedEventArgs e)
        {

            double constant = 0;

            try
            {
                constant = Convert.ToDouble(tbConstant.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте правильность ввода константы", "Ошибка в поле ввода");
                return;
            }

            string str_const = "";
            str_const += constant;

            for (int i = 0; i < mode; i++)
            {
                for (int j = 0; j < mode; j++)
                {
                    if (i == j) continue;

                    TextBoxMatrix[i, j].Text = str_const;
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btBranchAndBound_Click(object sender, RoutedEventArgs e)
        {
            if (BuildMatix() != null)
            {
                listboxResult.Items.Add(MainMatrix.getBranchClassicSolutionString(0));
            }
        }

        private void btBranchAndBoundPlus_Click(object sender, RoutedEventArgs e)
        {
            if (BuildMatix() != null)
            {
                listboxResult.Items.Add(MainMatrix.getBCPlusSolutionString(0));
            }
        }

        private void btBruteForce_Click(object sender, RoutedEventArgs e)
        {
            if (BuildMatix() != null)
            {
                listboxResult.Items.Add(MainMatrix.getBruteForceSolutionString());
            }
        }



        // добавить обработчик на кнопку, саму кнопку

        private int[,] BuildMatix()
        {
            int[,] matrix = new int[mode, mode];

            int opr = 0;

            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = Int32.MaxValue; //continue;
                        continue;
                    }

                    try
                    {
                        opr = Convert.ToInt32(TextBoxMatrix[i, j].Text);
                        matrix[i, j] = opr;
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Ошибка в поле " + (i + 1) + ";" + (j + 1), "Ошибка");
                        return null;
                    }
                }
            }

            MainMatrix.setTargetMatrix(matrix);
            return matrix;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listboxResult.Items.Clear();
        }

        private void btRandomSemiValue_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < mode; i++)
            {
                for (int j = i + 1; j < mode; j++)
                {
                    if (i == j) continue;

                    string temp = random.Next(1, 100).ToString();

                    TextBoxMatrix[i, j].Text = temp;
                    TextBoxMatrix[j, i].Text = temp;
                }
            }
        }

        private void btGetAnalysis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int countOfMatrix = 0;
                int sizeOfMatrix = 0;

                int generateMode = Analysis.RANDOM_MATRIX;

                bool brute_force_method = false;
                bool branch_method = false;
                bool branch_plus_method = false;
                bool isBbSharpMethodEnabled = false;

                try
                {
                    countOfMatrix = Convert.ToInt32(tbCountOfMatrix.Text);
                    sizeOfMatrix = Convert.ToInt32(tbSizeOfMatrix.Text);
                }
                catch
                {
                    MessageBox.Show("Проверьте правильность ввода размерности и количества матриц", "Ошибка");
                    return;
                }

                if ((bool)rbGenerateRandomMx.IsChecked)
                    generateMode = Analysis.RANDOM_MATRIX;
                else
                    generateMode = Analysis.SYMMETRIC_MATRIX;

                if ((bool)rbGenerateTestMatrix.IsChecked)
                    generateMode = Analysis.TEST_MATRIX;

                brute_force_method = (bool)cbBruteForceMode.IsChecked;
                branch_method = (bool)cbBranchMode.IsChecked;
                branch_plus_method = (bool)cbBranchPlus.IsChecked;
                isBbSharpMethodEnabled = (bool)cbBbSharpEnabled.IsChecked;

                if (sizeOfMatrix > 12 && brute_force_method)
                {
                    MessageBox.Show("Выбран метод полного перебора, для матрицы размерности больше чем 12, что вызовет зависание программы",
                        "Возможно зависание программы");
                    return;
                }

                listboxResult.Items.Add(
                    Analysis.Start(sizeOfMatrix, countOfMatrix, generateMode, brute_force_method, branch_method, branch_plus_method, isBbSharpMethodEnabled));
            }

            catch (Exception ex)
            {
                //  MessageBox.Show("Неизвестная ошибка", "Ошибка");

                MessageBox.Show(ex.StackTrace);
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void btBrandAndOleg_Click(object sender, RoutedEventArgs e)
        {
            int[,] matrix = BuildMatix();
            string answer = "";

            if (matrix != null)
            {
                answer = Matrix.GetSolutionByBranchSharpMethod(matrix, true);
            }

            listboxResult.Items.Add(answer);
        }

        private void btTstTerminal_Click(object sender, RoutedEventArgs e)
        {
            TstLangTerminal tstLangTerminal = new TstLangTerminal();
            tstLangTerminal.ShowDialog();
        }
    }
}