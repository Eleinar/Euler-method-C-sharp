using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;
using ZedGraph;

namespace Elier_wf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("Xcolumn", "X");

            dataGridView1.Columns.Add("Ycolumn", "Y");


        }
        string Func;
        private void Func_textbox_TextChanged(object sender, EventArgs e)
        {
            Func = Func_textbox.Text;
        }
        double x, y;
        int n;
        double h;
        private void button1_Click(object sender, EventArgs e)
        {
            if (Func_textbox.Text == "")
            {
                MessageBox.Show("Уравнение не может быть пустым", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            try
            {
                h = double.Parse(h_textBox.Text);
                x = double.Parse(x0_textBox.Text);
                y = double.Parse(y0_textBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Некорректное значение h, x0 или y0", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            dataGridView1.Rows.Clear();
            n = (int)numericUpDown1.Value;

           
            Function F = new Function($"F(x,y) = {Func}");

            double[] X = new double[n + 1];
            double[] Y = new double[n + 1];

            X[0] = x;
            Y[0] = y;
            Argument xn = new Argument("xn", X[0]);
            Argument yn = new Argument("yn", Y[0]);
            dataGridView1.Rows.Add(X[0], Y[0]);

            for (int i = 1; i <= n; i++)
            {
                X[i] = x + i * h;
                xn.setArgumentValue(X[i - 1]);
                yn.setArgumentValue(Y[i - 1]);
                Expression Exp = new Expression("F(xn,yn)", F, xn, yn);

                Y[i] = Y[i - 1] + h * Exp.calculate();


                dataGridView1.Rows.Add(X[i], Y[i]);

                DrawGaphic(X, Y);
            }
            
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Выполнил Моисеев Даниил ИС(ПРО)-21");
        }
        
        private void DrawGaphic(double[] X, double[] Y)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            PointPairList list = new PointPairList();
            pane.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = "Ось Y";
            pane.Title.Text = "График";

            for (int i = 0; i <= n; i++)
            {
                list.Add(X[i], Y[i]);
            }
            LineItem Curva = pane.AddCurve("График", list, Color.Blue, SymbolType.None);
        }
    }
}
