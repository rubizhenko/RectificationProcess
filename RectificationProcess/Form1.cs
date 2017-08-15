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
    public partial class Form1 : Form
    {
        string[] tabsForDeflegmator = { "Tb2(Tb1)", "Tb2(Fb1)", "Tb2(Fv)", "Tb2(Tv)" };
        public Form1()
        {
            InitializeComponent();
            string progPath = Environment.CurrentDirectory;
            RectificationProcess process = new RectificationProcess();
            process.StaticT2otFbinar1();
            process.StaticT2otFvod();
            //double k = process.StaticT2otTbinar1().Item1[0];

            process.StaticT2otTvod();
            process.StaticTp1opFb();
            process.StaticTp1opFd();
            process.StaticTp1opFf();
            process.StaticTp1opTb();
            process.StaticTp1opTd();
            process.StaticTp1opTp2();
            process.StaticTp2otFp();
            process.StaticTp2otFpara();
            process.StaticTp2otTp1();
            process.AddTabsForDeflegCharts(tabControl1, tabsForDeflegmator);
  
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
                    T_recur2_1[i - 1] = (Frecur1 * Crecur1 * Trecur1 + 0.8 * (Fpara * Ipara * Ppara) - Fpara * Ppara * Tkondpara * Ckondpara - Frecur1 * q1) / (Frecur1 * Crecur1);
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
                    T_recur2_2[i - 1] = (Frecur1 * Crecur1 * Trecur1 + 0.8 * (Fpara * Ipara * Ppara) - Fpara * Ppara * Tkondpara * Ckondpara - Frecur1 * q1) / (Frecur1 * Crecur1);
                    T_recur2_2[i - 1] = Math.Round(T_recur2_2[i - 1], 2);
                }
                K6 = (T_recur2_2[1] - T_recur2_2[2]) / -2;
                Trecur1 = 359;
                return Tuple.Create(T_recur2_2, K6);
            }
            public Tuple<double[], double> StaticTp2otFpara()
            {
                Fpara = 131;
                for (int i = 1; i <= 9; i++)
                {
                    Fpara += 2;
                    T_recur2_3[i - 1] = (-(Frecur1 * Crecur1 * Trecur1 - Frecur1 * q1 - k1 * s1 * (-0.8 * Fpara * Ipara * Ppara / (k1 * s1 - Fpara * Ppara * Ckondpara))) / (-Frecur1 * Crecur1 + k1 * s1 - k1 * s1 * (k1 * s1 / (k1 * s1 - Fpara * Ppara * Ckondpara))));
                    T_recur2_3[i - 1] = Math.Round(T_recur2_3[i - 1], 2);
                }
                //перевірити
                K7 = (T_recur2_3[1] - T_recur2_3[2]) / -2;
                Fpara = 141;
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
                    series.Points.Clear();
                }

            }
            public void AddTabsForDeflegCharts(object tabControl, string[] tabsNames)
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
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Clear();
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
       
        }
}
