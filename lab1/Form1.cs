using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    

        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label8.Text = "";
            if (numericUpDown4.Value < numericUpDown5.Value)
            {
                label8.Text = "Макс значение не м.б. меньше минзначения!"; return;
            }
            int count, current = 0;
            count = (Convert.ToInt32(numericUpDown2.Value) -
            Convert.ToInt32(numericUpDown1.Value)) /
            Convert.ToInt32(numericUpDown3.Value) + 1;
            for (int n = Convert.ToInt32(numericUpDown1.Value); n <=
            Convert.ToInt32(numericUpDown2.Value); n +=
            Convert.ToInt32(numericUpDown3.Value))
            {
                int[] vptr = new int[n];
                Random rand = new Random();
                for (int j = 0; j < n; j++)
                {
                    vptr[j] =
                    rand.Next(Convert.ToInt32(numericUpDown5.Value),
                    Convert.ToInt32(numericUpDown4.Value));
                }
                if (checkBox1.Checked)
                {
                    dataGridView1.ColumnCount = n + 1;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = "Исходный массив";
                    for (int j = 0; j < n; j++)
                    {
                        dataGridView1.Rows[i].Cells[j + 1].Value =
                    vptr[j];
                    }
                    i++;
                }
                sort(vptr, n);
                current += 1;
                progressBar1.Value = 100 * current / count;
            }
        }
        private void sort(int[] p, int n)
        {
            int k = 0, sr = 0, obm = 0, m = 0;
            for (int j = 0; j < n; j++)
            {
                if (p[j] == 0) k++;
                else p[j - k] = p[j];
            }
            n -= k;
            sr += n;
            if (n == 0)
            {
                label8.Text = "В массиве одни нули"; return;
            }
            for (m = 0; m < n - 1; m++)
                for (int j = m + 1; j < n; j++)
                {
                    if (p[m] > 0 && p[j] > 0 && p[m] < p[j])
                    {
                        swap(ref p[m], ref p[j]); obm++;
                    }
                    if (p[m] < 0 && p[j] < 0 && p[m] > p[j])
                    {
                        swap(ref p[m], ref p[j]); obm++;
                    }
                    sr += 6;
                }
            if (checkBox1.Checked)
            {
                dataGridView1.AutoResizeColumns();
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = "Получен массив";
                for (int j = 0; j < n; j++)
                { dataGridView1.Rows[i].Cells[j + 1].Value = p[j]; }
                i++;
            }
            if (Convert.ToInt32(numericUpDown1.Value) ==
Convert.ToInt32(numericUpDown2.Value))
            {
                label8.Text = "Количество сравнений=" +
            Convert.ToString(sr) + " Количество обменов=" + Convert.ToString(obm);
            }
            if (checkBox2.Checked)
            {
                chart1.Series[0].Points.AddXY(n, sr);
                chart2.Series[0].Points.AddXY(n, obm);
            }
        }

        void swap(ref int x, ref int y)
        { int z = x; x = y; y = z; }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            i = 0;
            button2.Enabled = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        int n, m;
        private void button3_Click(object sender, EventArgs e)
            {
            int i, j;
            button3.Enabled = false;

            if (numericUpDown7.Value < numericUpDown8.Value)
            {
                label9.Text = "Макс значение не м.б. меньше минзначения!";
                return;
            }

            n = Convert.ToInt32(numericUpDown6.Value);
            m = Convert.ToInt32(numericUpDown6.Value);
            int[,] ptr = new int[n, m];
            Random rand = new Random();
            dataGridView2.AutoResizeColumns();
            dataGridView2.ColumnCount = m;

            // Заполнение матрицы случайными значениями
            for (i = 0; i < n; i++)
            {
                dataGridView2.Rows.Add();
                for (j = 0; j < m; j++)
                {
                    ptr[i, j] = rand.Next(Convert.ToInt32(numericUpDown8.Value), Convert.ToInt32(numericUpDown7.Value));
                    dataGridView2.Rows[i].Cells[j].Value = ptr[i, j];
                }
            }

            // Удаление строк с отрицательными последними элементами
            List<int[]> newMatrix = new List<int[]>();
            for (i = 0; i < n; i++)
            {
                if (ptr[i, m - 1] >= 0)
                {
                    int[] row = new int[m];
                    for (j = 0; j < m; j++)
                    {
                        row[j] = ptr[i, j];
                    }
                    newMatrix.Add(row);
                }
            }


            // Добавление строки в начало из массива, заданного в textBox1
            string[] textBoxValues = textBox1.Text.Split(',');
            if (textBoxValues.Length == m) // Проверка на совпадение количества столбцов
            {
                int[] arrayFromTextBox = new int[m];
                for (i = 0; i < m; i++)
                {
                    arrayFromTextBox[i] = Convert.ToInt32(textBoxValues[i]);
                }
                newMatrix.Insert(0, arrayFromTextBox);
            }
            else
            {
                label9.Text = "Количество элементов в массиве из textBox1 должно совпадать с количеством столбцов!";
                return;
            }

            // Обновление DataGridView
            dataGridView3.AutoResizeColumns();
            dataGridView3.ColumnCount = m;
            dataGridView3.Rows.Clear();
            for (i = 0; i < newMatrix.Count; i++)
            {
                dataGridView3.Rows.Add();
                for (j = 0; j < newMatrix[i].Length; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = newMatrix[i][j];
                }
            }

            label9.Text = $"Удалено строк: {n - newMatrix.Count + 1}"; // +1 потому что добавили новую строку из textBox1
        }



        private void button4_Click(object sender, EventArgs e)
             {
             dataGridView2.Rows.Clear();
             dataGridView2.Columns.Clear();
             dataGridView3.Rows.Clear();
             dataGridView3.Columns.Clear();
             button3.Enabled = true;
             label9.Text = "";
             }


    }
}