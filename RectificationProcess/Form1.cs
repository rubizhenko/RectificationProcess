using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RectificationProcess
{
    using ComportMath;
    public partial class Form1 : Form
    {
        string[] tabsForDeflegmator = { "Tb2(Tb1)", "Tb2(Fb1)", "Tb2(Fv)", "Tb2(Tv)" };
        string[] tabsForBoiler = { "Tr2(Fr1)", "Tr2(Tr1)", "Tr2(Fp)" };
        string[] tabsForRectifCol = { "Tr1(Fdist)", "Tr1(Tdist)", "Tr1(Tr2)", "Tr1(Ffleg)", "Tr1(Fb)", "Tr1(Tb)" };
        string[] tabsForDynamicDeflegmator = { "Tb2(Tb1)", "Tb2(Fb1)", "Tb2(Fv)"};
        string[] tabsForDynamicBoiler = { "Tr2(Tr1)", "Tr2(Fr1)", "Tr2(Fp)"};
        double[] staticT2otTbinar1 = new double[9];
        double[] staticT2otFbinar1 = new double[9];
        double[] staticT2otFvod = new double[9];
        double[] staticT2otTvod = new double[9];
        double[] staticTp2otFp = new double[9];
        double[] staticTp2otTp1 = new double[9];
        double[] staticTp2otFpara = new double[9];
        double[] staticTp1opFd = new double[9];
        double[] staticTp1opTd = new double[9];
        double[] staticTp1opTp2 = new double[9];
        double[] staticTp1opFf = new double[9];
        double[] staticTp1opFb = new double[9];
        double[] staticTp1opTb = new double[9];
        double K1, K2, K3, K4, K5, K6, K7, K8, K9, K10, K11, K12, K13;

        

        bool staticFuncs = false;
        bool dynamicFuncs = false;

        RectificationProcess process = new RectificationProcess();
        
        public Form1()
        {
            InitializeComponent();
            string progPath = Environment.CurrentDirectory;
            
            for (int i = 0; i < 9; i++)
            {
                staticT2otTbinar1[i] = process.StaticT2otTbinar1().Item1[i];
                staticT2otFbinar1[i] = process.StaticT2otFbinar1().Item1[i];
                staticT2otFvod[i] = process.StaticT2otFvod().Item1[i];
                staticT2otTvod[i] = process.StaticT2otTvod().Item1[i];
                staticTp2otFp[i] = process.StaticTp2otFp().Item1[i];
                staticTp2otTp1[i] = process.StaticTp2otTp1().Item1[i];
                staticTp2otFpara[i] = process.StaticTp2otFpara().Item1[i];
                staticTp1opFd[i] = process.StaticTp1opFd().Item1[i];
                staticTp1opTd[i] = process.StaticTp1opTd().Item1[i];
                staticTp1opTp2[i] = process.StaticTp1opTp2().Item1[i];
                staticTp1opFf[i] = process.StaticTp1opFf().Item1[i];
                staticTp1opFb[i] = process.StaticTp1opFb().Item1[i];
                staticTp1opTb[i] = process.StaticTp1opTb().Item1[i];
            }
            K1 = process.StaticT2otTbinar1().Item2;
            K2 = process.StaticT2otFbinar1().Item2;
            K3 = process.StaticT2otFvod().Item2;
            K4 = process.StaticT2otTvod().Item2;
            K5 = process.StaticTp2otFp().Item2;
            K6 = process.StaticTp2otTp1().Item2;
            K7 = process.StaticTp2otFpara().Item2;
            K8 = process.StaticTp1opFd().Item2;
            K9 = process.StaticTp1opTd().Item2;
            K10 = process.StaticTp1opTp2().Item2;
            K11 = process.StaticTp1opFf().Item2;
            K12 = process.StaticTp1opFb().Item2;
            K13 = process.StaticTp1opTb().Item2;
            Laplace.InitStehfest(14);
        }
        public class RectificationProcess
        {
            
            //Параметри дистиляту
            private double Fdist = 1530, Cdist = 3.670, Tdist = 293;
            //Параметри бінарної суміші
            private double Fbinar1 = 540, Cbinar1 = 3.260, Tbinar1 = 342;
            //Параметри дистиляту
            private double Fvoda = 650, Cvoda = 4.200, Tvoda = 293;
            //Параметри конденсату
            private double Fkondvoda = 650, Tkondvoda = 4.200, Ckondvoda = 308;
            //Параметри бінарної суміші
            private double Fbinar2 = 540, Cbinar2 = 3.260, Tbinar2 = 312;
            //Параметри флегми
            private double Cflegmy = 3.260, Tflegmy = 312, Fflegmy = 320;
            //Параметри кубового залишку
            private double Fcub = 1110, Ccub = 2.900, Tcub = 359;
            //Параметри рециркуляту
            private double Frecur1 = 960, Crecur1 = 2.900, Trecur1 = 359;
            //Параметри рециркуляту
            private double Frecur2 = 960, Crecur2 = 2.900, Trecur2 = 383;
            //Параметри пари
            private double Fpara = 141, Ppara = 20.101, Ipara = 2800;
            //Параметри конденсованої пари
            private double Fkondpara = 2834, Ckondpara = 4.200, Tkondpara = 297;

            private double q = 0.225;   //для дефлегматора
            private double q1 = 2861;   //для кип'ятильника
            private double k = 512;     //для дефлегматора
            private double s = 20;      //для дефлегматора
            private double k1 = 1308;   //для кип'ятильника
            private double s1 = 25;     //для кип'ятильника
            private double k2 = 168.143;   //для колони
            private double s2 = 35;     //для колони
            private double q2 = 0.393;  //для колони

            //для дефлегматора
            double[] T_binar2_1 = new double[9];
            double[] T_binar2_2 = new double[9];
            double[] T_binar2_3 = new double[9];
            double[] T_binar2_4 = new double[9];

            //для кип'ятильника
            double[] T_recur2_1 = new double[9];
            double[] T_recur2_2 = new double[9];
            double[] T_recur2_3 = new double[9];

            //для колони
            double[] T_recur1_1 = new double[9];
            double[] T_recur1_2 = new double[9];
            double[] T_recur1_3 = new double[9];
            double[] T_recur1_4 = new double[9];
            double[] T_recur1_5 = new double[9];
            double[] T_recur1_6 = new double[9];
           
            double K1 = 0, K2 = 0, K3 = 0, K4 = 0, K5 = 0, K6 = 0, K7 = 0, K8 = 0, K9 = 0, K10 = 0, K11 = 0, K12 = 0, K13 = 0;

            public double Fdist1 { get => Fdist; set => Fdist = value; }
            public double Cdist1 { get => Cdist; set => Cdist = value; }
            public double Tdist1 { get => Tdist; set => Tdist = value; }
            public double Fbinar11 { get => Fbinar1; set => Fbinar1 = value; }
            public double Cbinar11 { get => Cbinar1; set => Cbinar1 = value; }
            public double Tbinar11 { get => Tbinar1; set => Tbinar1 = value; }
            public double Fvoda1 { get => Fvoda; set => Fvoda = value; }
            public double Cvoda1 { get => Cvoda; set => Cvoda = value; }
            public double Tvoda1 { get => Tvoda; set => Tvoda = value; }
            public double Fkondvoda1 { get => Fkondvoda; set => Fkondvoda = value; }
            public double Tkondvoda1 { get => Tkondvoda; set => Tkondvoda = value; }
            public double Ckondvoda1 { get => Ckondvoda; set => Ckondvoda = value; }
            public double Fbinar21 { get => Fbinar2; set => Fbinar2 = value; }
            public double Cbinar21 { get => Cbinar2; set => Cbinar2 = value; }
            public double Tbinar21 { get => Tbinar2; set => Tbinar2 = value; }
            public double Cflegmy1 { get => Cflegmy; set => Cflegmy = value; }
            public double Tflegmy1 { get => Tflegmy; set => Tflegmy = value; }
            public double Fflegmy1 { get => Fflegmy; set => Fflegmy = value; }
            public double Fcub1 { get => Fcub; set => Fcub = value; }
            public double Ccub1 { get => Ccub; set => Ccub = value; }
            public double Tcub1 { get => Tcub; set => Tcub = value; }
            public double Frecur11 { get => Frecur1; set => Frecur1 = value; }
            public double Crecur11 { get => Crecur1; set => Crecur1 = value; }
            public double Trecur11 { get => Trecur1; set => Trecur1 = value; }
            public double Frecur21 { get => Frecur2; set => Frecur2 = value; }
            public double Crecur21 { get => Crecur2; set => Crecur2 = value; }
            public double Trecur21 { get => Trecur2; set => Trecur2 = value; }
            public double Fpara1 { get => Fpara; set => Fpara = value; }
            public double Fkondpara1 { get => Fkondpara; set => Fkondpara = value; }

            //Формули для розрахунку статичних характеристик дефлегматора
            public Tuple<double[], double> StaticT2otTbinar1()
            {
                Tbinar1 = 332;
                for (int i = 1; i <= 9; i++)
                {
                    Tbinar1 = Tbinar1 + 2;
                    T_binar2_1[i - 1] = (0.8 * Fbinar1 * Tbinar1 * Cbinar1 + Fvoda * Tvoda * Cvoda - Fkondvoda * Ckondvoda * Tkondvoda + 0.8 * Fbinar1 * Tbinar1 * Cbinar1 * q) / (Fbinar1 * Cbinar1);
                    T_binar2_1[i - 1] = Math.Round(T_binar2_1[i - 1], 2);
                }
                K1 = (T_binar2_1[1] - T_binar2_1[2]) / -2;
                Tbinar1 = 342;
                return Tuple.Create(T_binar2_1, K1);
            }
            public Tuple<double[], double> StaticT2otFbinar1()
            {
                Fbinar1 = 530;
                for (int i = 1; i <= 9; i++)
                {
                    Fbinar1 = Fbinar1 + 2;
                    T_binar2_2[i - 1] = (0.8 * Fbinar1 * Tbinar1 * Cbinar1 + Fvoda * Tvoda * Cvoda - Fkondvoda * Ckondvoda * Tkondvoda + 0.8 * Fbinar1 * Tbinar1 * Cbinar1 * q) / (Fbinar1 * Cbinar1);
                    T_binar2_2[i - 1] = Math.Round(T_binar2_2[i - 1], 2);
                }
                K2 = (T_binar2_2[1] - T_binar2_2[2]) / -2;
                Fbinar1 = 540;
                return Tuple.Create(T_binar2_2, K2);
            }
            public Tuple<double[], double> StaticT2otFvod()
            {
                Fvoda = 640;
                for (int i = 1; i <= 9; i++)
                {
                    Fvoda = Fvoda + 2;
                    T_binar2_3[i - 1] = (Fbinar1 * Tbinar1 * Cbinar1 + k * s * ((Fvoda * Tvoda * Cvoda) / (Fvoda * Cvoda + k * s)) - 0.2 * (Fbinar1 * Tbinar1 * Cbinar1 * q + Fbinar1 * Tbinar1 * Cbinar1) + Fbinar1 * Tbinar1 * Cbinar1 * q) / (Fbinar1 * Cbinar1 + k * s - k * s * ((k * s) / (Fvoda * Cvoda + k * s)));
                    T_binar2_3[i - 1] = Math.Round(T_binar2_3[i - 1], 2);
                }
                K3 = (T_binar2_3[1] - T_binar2_3[2]) / -2;
                Fvoda = 650;
                return Tuple.Create(T_binar2_3, K3);
            }
            public Tuple<double[], double> StaticT2otTvod()
            {
                Tvoda = 283;
                for (int i = 1; i <= 9; i++)
                {
                    Tvoda = Tvoda + 2;
                    T_binar2_4[i - 1] = (Fbinar1 * Tbinar1 * Cbinar1 + k * s * ((Fvoda * Tvoda * Cvoda) / (Fvoda * Cvoda + k * s)) - 0.2 * (Fbinar1 * Tbinar1 * Cbinar1 * q + Fbinar1 * Tbinar1 * Cbinar1) + Fbinar1 * Tbinar1 * Cbinar1 * q) / (Fbinar1 * Cbinar1 + k * s - k * s * ((k * s) / (Fvoda * Cvoda + k * s)));
                    T_binar2_4[i - 1] = Math.Round(T_binar2_4[i - 1], 2);
                }
                K4 = (T_binar2_4[1] - T_binar2_4[2]) / -2;
                Tvoda = 293;
                return Tuple.Create(T_binar2_4, K4);
            }

            //Формули для розрахунку статичних характеристик кип'ятильника
            public Tuple<double[], double> StaticTp2otFp()
            {
                Frecur1 = 950;
                for (int i = 1; i <= 9; i++)
                {
                    Frecur1 = Frecur1 + 2;
                    T_recur2_1[i - 1] = (Frecur1 * Crecur1 * Trecur1 + 0.8 * (Fpara1 * Ipara * Ppara) - Fpara1 * Ppara * Tkondpara * Ckondpara - Frecur1 * q1) / (Frecur1 * Crecur1);
                    T_recur2_1[i - 1] = Math.Round(T_recur2_1[i - 1], 2);
                }
                //Перевірити
                K5 = (T_recur2_1[1] - T_recur2_1[2]) / -2;
                Frecur1 = 960;
                return Tuple.Create(T_recur2_1, K5);
            }
            public Tuple<double[], double> StaticTp2otTp1()
            {
                Trecur1 = 349;
                for (int i = 1; i <= 9; i++)
                {
                    Trecur1 += 2;
                    T_recur2_2[i - 1] = (Frecur1 * Crecur1 * Trecur1 + 0.8 * (Fpara1 * Ipara * Ppara) - Fpara1 * Ppara * Tkondpara * Ckondpara - Frecur1 * q1) / (Frecur1 * Crecur1);
                    T_recur2_2[i - 1] = Math.Round(T_recur2_2[i - 1], 2);
                }
                K6 = (T_recur2_2[1] - T_recur2_2[2]) / -2;
                Trecur1 = 359;
                return Tuple.Create(T_recur2_2, K6);
            }
            public Tuple<double[], double> StaticTp2otFpara()
            {
                Fpara1 = 131;
                for (int i = 1; i <= 9; i++)
                {
                    Fpara1 += 2;
                    T_recur2_3[i - 1] = (-(Frecur1 * Crecur1 * Trecur1 - Frecur1 * q1 - k1 * s1 * (-0.8 * Fpara1 * Ipara * Ppara / (k1 * s1 - Fpara1 * Ppara * Ckondpara))) / (-Frecur1 * Crecur1 + k1 * s1 - k1 * s1 * (k1 * s1 / (k1 * s1 - Fpara1 * Ppara * Ckondpara))));
                    T_recur2_3[i - 1] = Math.Round(T_recur2_3[i - 1], 2);
                }
                //перевірити
                K7 = (T_recur2_3[1] - T_recur2_3[2]) / -2;
                Fpara1 = 141;
                return Tuple.Create(T_recur2_3, K7);
            }

            //Формули для розрахунку статичних характеристик колони
            public Tuple<double[], double> StaticTp1opFd()
            {
                Fdist = 1505;
                for (int i = 1; i <= 9; i++)
                {
                    Fdist = Fdist + 5;
                    T_recur1_1[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_1[i - 1] = Math.Round(T_recur1_1[i - 1], 2);
                }
                K8 = (T_recur1_1[1] - T_recur1_1[2]) / -5;
                Fdist = 1530;
                return Tuple.Create(T_recur1_1, K8);
            }
            public Tuple<double[], double> StaticTp1opTd()
            {
                Tdist = 278;
                for (int i = 1; i <= 9; i++)
                {
                    Tdist = Tdist + 3;
                    T_recur1_2[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_2[i - 1] = Math.Round(T_recur1_2[i - 1], 2);
                }
                K9 = (T_recur1_2[1] - T_recur1_2[2]) / -3;
                Tdist = 293;
                return Tuple.Create(T_recur1_2, K9);
            }
            public Tuple<double[], double> StaticTp1opTp2()
            {
                Trecur2 = 378;
                for (int i = 1; i <= 9; i++)
                {
                    Trecur2 = Trecur2 + 3;
                    T_recur1_3[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_3[i - 1] = Math.Round(T_recur1_3[i - 1], 2);
                }
                K10 = (T_recur1_3[1] - T_recur1_3[2]) / -3;
                Trecur2 = 383;
                return Tuple.Create(T_recur1_3, K10);
            }
            public Tuple<double[], double> StaticTp1opFf()
            {
                Fflegmy = 295;
                for (int i = 1; i <= 9; i++)
                {
                    Fflegmy = Fflegmy + 5;
                    T_recur1_4[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_4[i - 1] = Math.Round(T_recur1_4[i - 1], 2);
                }
                K11 = (T_recur1_4[1] - T_recur1_4[2]) / -5;
                Fflegmy = 320;
                return Tuple.Create(T_recur1_4, K11);
            }
            public Tuple<double[], double> StaticTp1opFb()
            {
                Fbinar1 = 515;
                for (int i = 1; i <= 9; i++)
                {
                    Fbinar1 = Fbinar1 + 5;
                    T_recur1_5[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_5[i - 1] = Math.Round(T_recur1_5[i - 1], 2);
                }
                K12 = (T_recur1_5[1] - T_recur1_5[2]) / -5;
                Fbinar1 = 540;
                return Tuple.Create(T_recur1_5, K12);
            }
            public Tuple<double[], double> StaticTp1opTb()
            {
                Tbinar1 = 327;
                for (int i = 1; i <= 9; i++)
                {
                    Tbinar1 = Tbinar1 + 3;
                    T_recur1_6[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_6[i - 1] = Math.Round(T_recur1_6[i - 1], 2);
                }
                K13 = (T_recur1_6[1] - T_recur1_6[2]) / -3;
                Tbinar1 = 342;
                return Tuple.Create(T_recur1_6, K13);
            }


            public void DrawStaticFunction(object chart, double XValueCenter, double[] YValues, string XTitle, string YTitle)
            {
                var myChart = chart as System.Windows.Forms.DataVisualization.Charting.Chart;
              
                foreach (var series in myChart.Series)
                {
                    series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    series.Points.Clear();
                }
                myChart.ChartAreas[0].AxisX.Title = XTitle;
                myChart.ChartAreas[0].AxisY.Title = YTitle;
                myChart.ChartAreas[0].AxisY.Minimum = YValues.Min();
                myChart.ChartAreas[0].AxisY.Maximum = YValues.Max();

                int xStart = getRangeFromCenterValue(XValueCenter)[0];
                int xEnd = getRangeFromCenterValue(XValueCenter)[1];
                int xStep = getRangeFromCenterValue(XValueCenter)[2];
                for (int i = 0; i < 9; i++)
                {
                     myChart.Series[0].Points.AddXY(xStart, YValues[i]);
                     xStart += xStep;
                }
            }
           
            public void AddTabsForTabControl(object tabControl, string[] tabsNames)
            {
                var myTabs = tabControl as TabControl;
                myTabs.TabPages.Clear();
                int tabNamesCount = tabsNames.Length;
                while (tabNamesCount > 0)
                {
                    TabPage newTabPage = new TabPage(tabsNames[tabNamesCount - 1]);
                    myTabs.TabPages.Add(newTabPage);
                    tabNamesCount--;
                }

            }
            public int[] getRangeFromCenterValue(double XValueCenter)
            {
                int[] result = new int[3];
                int xStart = (int)(XValueCenter - XValueCenter * 0.05);
                int xEnd = (int)(XValueCenter + XValueCenter * 0.05);
                int xStep = (int)(xEnd - xStart) / 9;
                result[0] = xStart;
                result[1] = xEnd;
                result[2] = xStep;
                return result;
            }
            public void printResult(object formLabel, string title, double value)
            {
                var label = formLabel as System.Windows.Forms.Label;
                if (title!="")
                {
                    label.Text = title;
                    int xStart = getRangeFromCenterValue(value)[0];
                    int xStep = getRangeFromCenterValue(value)[2];
                    for (int i = 0; i < 9; i++)
                    {
                        label.Text += "\n" + xStart.ToString("N");
                        xStart += xStep;
                    }
                }
                else
                {
                    label.Text = "K = " + value.ToString("N");
                }
            }
            public void printResult(object formLabel, string title, double[] value)
            {
                var label = formLabel as System.Windows.Forms.Label;
                label.Text = title;
                for (int i = 0; i < 9; i++)
                {
                    label.Text += "\n" + value[i].ToString("N");
                }

            }    
        }
        
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab!=null)
            {
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Clear();
                if (staticFuncs)
                {
                    switch (tabControl1.SelectedTab.Text)
                    {
                        //deflegmator tabs
                        case "Tb2(Tb1)":
                            process.DrawStaticFunction(chart1, process.Tbinar11, staticT2otTbinar1, "Tbinar1, K", "Tbinar2, K");
                            process.printResult(labelX, "Tb1, K", process.Tbinar11);
                            process.printResult(labelY, "Tb2, K", staticT2otTbinar1);
                            process.printResult(labelK, "", K1);
                            break;
                        case "Tb2(Fb1)":
                            process.DrawStaticFunction(chart1, process.Fbinar11, staticT2otFbinar1, "Fbinar1, m^3/c", "Tbinar2, K");
                            process.printResult(labelX, "Fb1, m^3/c", process.Fbinar11);
                            process.printResult(labelY, "Tb2, K", staticT2otFbinar1);
                            process.printResult(labelK, "", K2);
                            break;
                        case "Tb2(Fv)":
                            process.DrawStaticFunction(chart1, process.Fvoda1, staticT2otFvod, "Fvoda, m^3/c", "Tbinar2, K");
                            process.printResult(labelX, "Fv, m^3/c", process.Fvoda1);
                            process.printResult(labelY, "Tb2, K", staticT2otFvod);
                            process.printResult(labelK, "", K3);
                            break;
                        case "Tb2(Tv)":
                            process.DrawStaticFunction(chart1, process.Tvoda1, staticT2otTvod, "Tvoda, K", "Tbinar2, K");
                            process.printResult(labelX, "Tv, K", process.Tvoda1);
                            process.printResult(labelY, "Tb2, K", staticT2otTvod);
                            process.printResult(labelK, "", K4);
                            break;

                        //boiler tabs
                        case "Tr2(Fr1)":
                            process.DrawStaticFunction(chart1, process.Frecur11, staticTp2otFp, "Frecur1, m^3/c", "Trecur2, K");
                            process.printResult(labelX, "Fr1, m^3/c", process.Frecur11);
                            process.printResult(labelY, "Tr2, K", staticTp2otFp);
                            process.printResult(labelK, "", K5);
                            break;
                        case "Tr2(Tr1)":
                            process.DrawStaticFunction(chart1, process.Trecur11, staticTp2otTp1, "Trecur1, K", "Trecur2, K");
                            process.printResult(labelX, "Tr1, K", process.Trecur11);
                            process.printResult(labelY, "Tr2, K", staticTp2otTp1);
                            process.printResult(labelK, "", K6);
                            break;
                        case "Tr2(Fp)":
                            process.DrawStaticFunction(chart1, process.Fpara1, staticTp2otFp, "Fpara, m^3/c", "Trecur2, K");
                            process.printResult(labelX, "Fp, m^3/c", process.Fpara1);
                            process.printResult(labelY, "Tr2, K", staticTp2otFp);
                            process.printResult(labelK, "", K7);
                            break;

                        //rectification column tabs
                        case "Tr1(Fdist)":
                            process.DrawStaticFunction(chart1, process.Fdist1, staticTp1opFd, "Fdist, m^3/c", "Trecur1, K");
                            process.printResult(labelX, "Fd, m^3/c", process.Fdist1);
                            process.printResult(labelY, "Tr1, K", staticTp1opFd);
                            process.printResult(labelK, "", K8);
                            break;
                        case "Tr1(Tdist)":
                            process.DrawStaticFunction(chart1, process.Tdist1, staticTp1opTd, "Tdist, K", "Trecur1, K");
                            process.printResult(labelX, "Td, K", process.Tdist1);
                            process.printResult(labelY, "Tr1, K", staticTp1opTd);
                            process.printResult(labelK, "", K9);
                            break;
                        case "Tr1(Tr2)":
                            process.DrawStaticFunction(chart1, process.Trecur21, staticTp1opTp2, "Trecur2, K", "Trecur1, K");
                            process.printResult(labelX, "Tr2, K", process.Trecur21);
                            process.printResult(labelY, "Tr1, K", staticTp1opTp2);
                            process.printResult(labelK, "", K10);
                            break;
                        case "Tr1(Ffleg)":
                            process.DrawStaticFunction(chart1, process.Fflegmy1, staticTp1opFf, "Fflegmy, m^3/c", "Trecur1, K");
                            process.printResult(labelX, "Tfl, m^3/c", process.Fflegmy1);
                            process.printResult(labelY, "Tr1, K", staticTp1opFf);
                            process.printResult(labelK, "", K11);
                            break;
                        case "Tr1(Fb)":
                            process.DrawStaticFunction(chart1, process.Fbinar11, staticTp1opFb, "Fbinar, m^3/c", "Trecur1, K");
                            process.printResult(labelX, "Fb, m^3/c", process.Fbinar11);
                            process.printResult(labelY, "Tr1, K", staticTp1opFb);
                            process.printResult(labelK, "", K12);
                            break;
                        case "Tr1(Tb)":
                            process.DrawStaticFunction(chart1, process.Tbinar11, staticTp1opTb, "Tbinar, K", "Trecur1, K");
                            process.printResult(labelX, "Tb, K", process.Tbinar11);
                            process.printResult(labelY, "Tr1, K", staticTp1opTb);
                            process.printResult(labelK, "", K13);
                            break;

                        //default chart for deflegmator
                        default:
                            process.DrawStaticFunction(chart1, process.Tbinar11, staticT2otTbinar1, "Tbinar1, K", "Tbinar2, K");
                            process.printResult(labelX, "Tb, K", process.Tbinar11);
                            process.printResult(labelY, "Tb2, K", staticT2otTbinar1);
                            process.printResult(labelK, "", K1);
                            break;
                    }
                }
                if (dynamicFuncs)
                {
                    switch (tabControl1.SelectedTab.Text)
                    {
                        //deflegmator tabs
                        case "Tb2(Tb1)":
                            double W1(double s)
                            {
                                return 0.98 / (6.944 * s * s + 1.0 * s);
                            }
                            Laplace.drawStepResponse(W1, chart1, 100);
                            break;
                        case "Tb2(Fb1)":
                            double W2(double s)
                            {
                                return 0.043 / (6.9 * s * s + 1.0 * s);
                            }
                            Laplace.drawStepResponse(W2, chart1, 100);
                            break;
                        case "Tb2(Fv)":
                            double W3(double s)
                            {
                                return -0.013 / (7.077 * s * s * s + 6.716 * s * s + 1 * s);
                            }
                            Laplace.drawStepResponse(W3, chart1, 100);
                            break;

                        //boiler tabs
                        case "Tr2(Fr1)":
                            double W5(double s)
                            {
                                return -2.27 / (7.08 * s * s + 1 * s);
                            }
                            Laplace.drawStepResponse(W5, chart1, 100);
                            break;
                        case "Tr2(Tr1)":
                            double W4(double s)
                            {
                                return 1 / (7.08 * s * s + 1 * s);
                            }
                            Laplace.drawStepResponse(W4, chart1, 100);
                            break;
                        case "Tr2(Fp)":
                            double W6(double s)
                            {
                                return 3.024 / (0.413 * s * s * s + 1.153 * s * s + 1 * s);
                            }
                            Laplace.drawStepResponse(W6, chart1, 10);
                            break;

                        //rectification column tabs
                        case "Tr1(Fdist)":
                           
                            break;
                        case "Tr1(Tdist)":
                            
                            break;
                        case "Tr1(Tr2)":
                            
                            break;
                        case "Tr1(Ffleg)":
                            
                            break;
                        case "Tr1(Fb)":
                            
                            break;
                        case "Tr1(Tb)":
                            
                            break;

                        //default chart for deflegmator
                        default:
                            Laplace.drawStepResponse(W3, chart1, 200);
                            break;
                    }
                }
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
            }
            
        }

        private void дефлегматорToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staticFuncs = true;
            dynamicFuncs = false;
            process.AddTabsForTabControl(tabControl1, tabsForDeflegmator);
            process.DrawStaticFunction(chart1, process.Tvoda1, staticT2otTvod, "Tvoda, K", "Tbinar2, K");
            process.printResult(labelX, "Tv, K", process.Tvoda1);
            process.printResult(labelY, "Tb2, K", staticT2otTvod);
            process.printResult(labelK, "", K4);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }

        private void кипятильникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staticFuncs = true;
            dynamicFuncs = false;
            process.AddTabsForTabControl(tabControl1, tabsForBoiler);
            process.DrawStaticFunction(chart1, process.Fpara1, staticTp2otFp, "Fpara, m^3/c", "Trecur2, K");
            process.printResult(labelX, "Fp, m^3/c", process.Fpara1);
            process.printResult(labelY, "Tr2, K", staticTp2otFp);
            process.printResult(labelK, "", K5);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }

        private void ректифікаційнаКолонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staticFuncs = true;
            dynamicFuncs = false;
            process.AddTabsForTabControl(tabControl1, tabsForRectifCol);
            process.DrawStaticFunction(chart1, process.Tbinar11, staticTp1opTb, "Tbinar, K", "Trecur1, K");
            process.printResult(labelX, "Tb, K", process.Tbinar11);
            process.printResult(labelY, "Tr1, K", staticTp1opTb);
            process.printResult(labelK, "", K13);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void дефлегматорToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            staticFuncs = false;
            dynamicFuncs = true;
            process.AddTabsForTabControl(tabControl1, tabsForDynamicDeflegmator);
            double W3(double s)
            {
                return -0.013 / (7.077 * s * s * s + 6.716 * s * s + 1 * s);
            }
            Laplace.drawStepResponse(W3, chart1, 100);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void кипятильникToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            staticFuncs = false;
            dynamicFuncs = true;
            process.AddTabsForTabControl(tabControl1, tabsForDynamicBoiler);
            double W6(double s)
            {
                return 3.024 / (0.413 * s * s * s + 1.153 * s * s + 1 * s);
            }
            Laplace.drawStepResponse(W6, chart1, 10);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }



    }
}
