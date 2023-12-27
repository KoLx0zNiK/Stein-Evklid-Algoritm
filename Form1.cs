using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Oop2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isFirstTime = true;

        class Algoritm
        {
            Stopwatch stopwatch = new Stopwatch();
            public List<Tuple<int, int, long>> times = new List<Tuple<int, int, long>>();
            int count = 0;
            public int AlgoritmByEuclid(int a, int b)
            {
                a = Math.Abs(a);
                b = Math.Abs(b);
                if (a == b) return a;
                if (a == 0) return b;
                if (b == 0) return a;

                while ((a != 0) && (b != 0))
                {
                    if (a > b)
                        a -= b;
                    else
                        b -= a;
                }
                return Math.Max(a, b);
            }
            public int AlgoritmByEuclid(int a, int b, int c)
            {
                return AlgoritmByEuclid(AlgoritmByEuclid(a, b), c);
            }
            public int AlgoritmByEuclid(int a, int b, int c, int d)
            {
                return AlgoritmByEuclid(AlgoritmByEuclid(AlgoritmByEuclid(a, b), c), d);
            }
            public int AlgoritmByEuclid(int a, int b, int c, int d, int f)
            {
                return AlgoritmByEuclid(AlgoritmByEuclid(AlgoritmByEuclid(AlgoritmByEuclid(a, b), c), d), f);
            }
            public int AlgoritmByEuclid(int a, int b, out long time)
            {
                stopwatch.Start();
                int Gcd = AlgoritmByEuclid(a, b);
                stopwatch.Stop();
                time = stopwatch.ElapsedTicks;
                stopwatch.Reset();
                return Gcd;
            }
            private int AlgoritmByStein(int a, int b)
            {
                stopwatch.Stop();
                long time = stopwatch.ElapsedTicks;
                if (count > 0)
                {
                    times.Add(new Tuple<int, int, long>(a, b, time));
                }
                count++;
                stopwatch.Reset();
                stopwatch.Start();
                a = Math.Abs(a);
                b = Math.Abs(b);
                if (a == b)
                {
                    count = 0;
                    return a;
                }
                if (a == 0)
                {
                    count = 0;
                    return b;
                }
                if (b == 0)
                {
                    count = 0;
                    return a;
                }

                if (a % 2 == 0)
                {
                    if (b % 2 == 0) return 2 * AlgoritmByStein(a / 2, b / 2);
                    return AlgoritmByStein(a / 2, b);
                }
                if (b % 2 == 0) return AlgoritmByStein(a, b / 2);
                if (a > b) return AlgoritmByStein((a - b) / 2, b);
                return AlgoritmByStein(a, (b - a) / 2);
            }
            public int AlgoritmByStein(int a, int b, out long time)
            {
                stopwatch.Start();
                int Gcd = AlgoritmByStein(a, b);
                stopwatch.Stop();
                time = stopwatch.ElapsedTicks;
                stopwatch.Reset();
                return Gcd;
            }
            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
        }        
        private void DrawHistogram(List<Tuple<int, int, long>> times, string orientation = "Vertical")
        {
            Axis axisX = new Axis();
            Axis axisY = new Axis();

            axisX.Title = "время";
            axisY.Title = "Результат вычислений";

            chart1.Series.Clear();
            chart1.Series.Add("Число1");
            chart1.Series.Add("Число2");

            if (orientation == "Horizontal")
            {
                chart1.Series["Число1"].ChartType = SeriesChartType.Bar;
                chart1.Series["Число2"].ChartType = SeriesChartType.Bar;
            }

            foreach (var item in times)
            {
                chart1.Series["Число1"].Points.AddXY(item.Item3, item.Item1);
                chart1.Series["Число2"].Points.AddXY(item.Item3, item.Item2);
            }

            chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 12);
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 12);
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "2";
            chart1.ChartAreas[0].AxisX = axisX;
            chart1.ChartAreas[0].AxisY = axisY;

            chart1.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Algoritm getGcd = new Algoritm();
            if (radioButton1.Checked && textBox1.Text != "" && textBox2.Text != "")
            {
                int a = Convert.ToInt32(textBox1.Text);
                int b = Convert.ToInt32(textBox2.Text);
                textBox9.Text = Convert.ToString(getGcd.AlgoritmByEuclid(a, b, out long timeEuclid));
                textBox6.Text = Convert.ToString(getGcd.AlgoritmByStein(a, b, out long timeStein));
                if (isFirstTime)
                {
                    textBox9.Text = Convert.ToString(getGcd.AlgoritmByEuclid(a, b, out timeEuclid));
                    textBox6.Text = Convert.ToString(getGcd.AlgoritmByStein(a, b, out timeStein));
                    isFirstTime = false;
                }
                textBox7.Text = Convert.ToString(timeEuclid);
                textBox8.Text = Convert.ToString(timeStein);

                DrawHistogram(getGcd.times);

                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
            }
            if (radioButton2.Checked && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                int a = Convert.ToInt32(textBox1.Text);
                int b = Convert.ToInt32(textBox2.Text);
                int c = Convert.ToInt32(textBox3.Text);
                textBox5.Text = Convert.ToString(getGcd.AlgoritmByEuclid(a, b, c));
            }
            if (radioButton3.Checked && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""
                && textBox4.Text != "")
            {
                int a = Convert.ToInt32(textBox1.Text);
                int b = Convert.ToInt32(textBox2.Text);
                int c = Convert.ToInt32(textBox3.Text);
                int d = Convert.ToInt32(textBox4.Text);
                textBox5.Text = Convert.ToString(getGcd.AlgoritmByEuclid(a, b, c, d));
            }
            if (radioButton4.Checked && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""
                && textBox4.Text != "" && textBox5.Text != "")
            {
                int a = Convert.ToInt32(textBox1.Text);
                int b = Convert.ToInt32(textBox2.Text);
                int c = Convert.ToInt32(textBox3.Text);
                int d = Convert.ToInt32(textBox4.Text);
                int f = Convert.ToInt32(textBox5.Text);
                textBox9.Text = Convert.ToString(getGcd.AlgoritmByEuclid(a, b, c, d, f));
            }
            textBox9.Enabled = true;
        }
    }
}