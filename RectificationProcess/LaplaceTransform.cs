using System;
using System.Collections.Generic;
using System.Text;

namespace ComportMath
{
    class Laplace
    {
        public delegate double FunctionDelegate(double t);
        static double[] V;       //  Stehfest coefficients
        static double ln2;       //  log of 2
        const int DefaultStehfestN = 14;

        static Laplace()
        {
            InitStehfest(DefaultStehfestN);
        }

        public static double Transform(FunctionDelegate F, double s)
        {
            const int DefaultIntegralN = 5000;
            double du = 0.5 / (double)DefaultIntegralN;
            double y = -F(0) / 2.0;
            double u = 0;
            double limit = 1.0 - 1e-10;
            while (u < limit)
            {
                u += du;
                y += 2.0 * Math.Pow(u, s - 1) * F(-Math.Log(u));
                u += du;
                y += Math.Pow(u, s - 1) * F(-Math.Log(u));
            }
            return 2.0 * y * du / 3.0;
        }

        public static double InverseTransform(FunctionDelegate f, double t)
        {
            double ln2t = ln2 / t;
            double x = 0;
            double y = 0;
            for (int i = 0; i < V.Length; i++)
            {
                x += ln2t;
                y += V[i] * f(x);
            }
            return ln2t * y;
        }

        public static double Factorial(int N)
        {
            double x = 1;
            if (N > 1)
            {
                for (int i = 2; i <= N; i++)
                    x = i * x;
            }
            return x;
        }

        //public static double Integrate(FunctionDelegate f, double Min, double Max)
        //{
        //    return Integrate(f, Min, Max, 100);
        //}

        //public static double Integrate(FunctionDelegate f, double XMin, double XMax, int N)
        //{
        //    double dx = (XMax - XMin) / (double)N / 2.0;
        //    double y = (f(XMin) - f(XMax))/2.0;
        //    double x = XMin;
        //    double limit = XMax - 1e-10;
        //    while (x < limit)
        //    {
        //        x += dx;
        //        y += 2.0*f(x);
        //        x += dx;
        //        y += f(x);
        //    }
        //    return 2.0 * y * dx / 3.0;
        //}

        public static void InitStehfest()
        {
            InitStehfest(DefaultStehfestN);
        }

        public static void InitStehfest(int N)
        {
            ln2 = Math.Log(2.0);
            int N2 = N / 2;
            int NV = 2 * N2;
            V = new double[NV];
            int sign = 1;
            if ((N2 % 2) != 0)
                sign = -1;
            for (int i = 0; i < NV; i++)
            {
                int kmin = (i + 2) / 2;
                int kmax = i + 1;
                if (kmax > N2)
                    kmax = N2;
                V[i] = 0;
                sign = -sign;
                for (int k = kmin; k <= kmax; k++)
                {
                    V[i] = V[i] + (Math.Pow(k, N2) / Factorial(k)) * (Factorial(2 * k)
                        / Factorial(2 * k - i - 1)) / Factorial(N2 - k) / Factorial(k - 1)
                        / Factorial(i + 1 - k);
                }
                V[i] = sign * V[i];
            }
        }
        public static void drawStepResponse(FunctionDelegate W, object chart, int time)
        {
            var plot = chart as System.Windows.Forms.DataVisualization.Charting.Chart;
            plot.ChartAreas[0].AxisY.Minimum = Double.NaN;
            plot.ChartAreas[0].AxisY.Maximum = Double.NaN;
            foreach (var series in plot.Series)
            {
                series.Points.Clear();
                series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
            }
            plot.ChartAreas[0].AxisX.Title = "t, сек.";
            plot.ChartAreas[0].AxisY.Title = "h(t)";
            int _time = (int)time;
            for (double i = 0; i < _time; i+=0.1)
            {
                double invCalc = Laplace.InverseTransform(W, i);
                plot.Series[0].Points.AddXY(i, invCalc);
            }

        }
    }
}
