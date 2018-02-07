using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace RectificationProcess
{
    using ComportMath;
    public partial class Form1 : Form
    {
        string[] tabsForDeflegmator = { "Tb2(Tb1)", "Tb2(Fb1)", "Tb2(Fv)", "Tb2(Tv)" };
        string[] tabsForBoiler = { "Tr2(Fr1)", "Tr2(Tr1)", "Tr2(Fp)" };
        string[] tabsForRectifCol = { "Tr1(Fdist)", "Tr1(Tdist)", "Tr1(Tr2)", "Tr1(Ffleg)", "Tr1(Fb)", "Tr1(Tb)" };
        string[] tabsForDynamicDeflegmator = { "Tb2(Tb1)", "Tb2(Fb1)", "Tb2(Fv)" };
        string[] tabsForDynamicBoiler = { "Tr2(Tr1)", "Tr2(Fr1)", "Tr2(Fp)" };
        string[] tabsForRectifColumn = { "Tr1(Ffleg)", "Tr1(Fb1)", "Tr1(Tb1)" };
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
        double newTime = 0.0;



        bool staticFuncs = false;
        bool dynamicFuncs = false;
        bool timeChanged = false;
        string progPath = Environment.CurrentDirectory;

        RectificationProcess process = new RectificationProcess();

        public Form1()
        {

            InitializeComponent();

            processPicture.Controls.Add(pictureBoxBoiler);
            pictureBoxBoiler.BackColor = Color.Transparent;
            pictureBoxBoiler.Size = new Size(90, 147);
            pictureBoxBoiler.Location = new Point(656, 367);

            processPicture.Controls.Add(pictureBoxDefleg);
            pictureBoxDefleg.BackColor = Color.Transparent;
            pictureBoxDefleg.Size = new Size(90, 147);
            pictureBoxDefleg.Location = new Point(660, 85);

            processPicture.Controls.Add(pictureBoxColumn);
            pictureBoxColumn.BackColor = Color.Transparent;
            pictureBoxColumn.Size = new Size(120, 340);
            pictureBoxColumn.Location = new Point(242, 103);

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
            showProcess();

            FdistEdit.Text = process.Fdist1.ToString();
            CdistEdit.Text = process.Cdist1.ToString();
            TdistEdit.Text = process.Tdist1.ToString();

            Fbinar1Edit.Text = process.Fbinar11.ToString();
            Cbinar1Edit.Text = process.Cbinar11.ToString();
            Tbinar1Edit.Text = process.Tbinar11.ToString();

            FvodaEdit.Text = process.Fvoda1.ToString();
            TvodaEdit.Text = process.Tvoda1.ToString();
            CvodaEdit.Text = process.Cvoda1.ToString();

            FkondVodaEdit.Text = process.Fkondvoda1.ToString();
            TkondVodaEdit.Text = process.Tkondvoda1.ToString();
            CkondVodaEdit.Text = process.Ckondvoda1.ToString();

            Fbinar2Edit.Text = process.Fbinar21.ToString();
            Tbinar2Edit.Text = process.Tbinar21.ToString();
            Cbinar2Edit.Text = process.Cbinar21.ToString();

            CflegmyEdit.Text = process.Cflegmy1.ToString();
            TflegmyEdit.Text = process.Tflegmy1.ToString();
            FflegmyEdit.Text = process.Fflegmy1.ToString();

            FcubEdit.Text = process.Fcub1.ToString();
            TcubEdit.Text = process.Tcub1.ToString();
            CcubEdit.Text = process.Ccub1.ToString();

            Frecur1Edit.Text = process.Frecur11.ToString();
            Trecur1Edit.Text = process.Trecur11.ToString();
            Crecur1Edit.Text = process.Crecur11.ToString();

            Crecur2Edit.Text = process.Crecur21.ToString();
            Trecur2Edit.Text = process.Trecur21.ToString();
            Frecur2Edit.Text = process.Frecur21.ToString();

            FparaEdit.Text = process.Fpara1.ToString();
            PparaEdit.Text = process.Ppara1.ToString();
            IparaEdit.Text = process.Ipara1.ToString();

            FkondparaEdit.Text = process.Fkondpara1.ToString();
            TkondparaEdit.Text = process.Tkondpara1.ToString();
            CkondparaEdit.Text = process.Ckondpara1.ToString();

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
            public double Ppara1 { get => Ppara; set => Ppara = value; }
            public double Ipara1 { get => Ipara; set => Ipara = value; }
            public double Fkondpara1 { get => Fkondpara; set => Fkondpara = value; }
            public double Tkondpara1 { get => Tkondpara; set => Tkondpara = value; }
            public double Ckondpara1 { get => Ckondpara; set => Ckondpara = value; }

 
            public double Tr2Fp(double Fp, double Fr1, double Tr1)
            {
                double Tr2 = 0;
                double A = -(Fr1 * Crecur1 * Tr1 - Fr1 * q1 - k1 * s1 * (-0.8 * Fp * Ipara * Ppara / (k1 * s1 - Fp * Ppara * Ckondpara)));
                double B = -Fr1 * Crecur1 + k1 * s1 - k1 * s1 * (k1 * s1 / (k1 * s1 - Fp * Ppara * Ckondpara));
                Tr2 = A / B;
                return Tr2;
            }
            public double Tb2Fv(double Fv, double Tb1, double Fb1)
            {
                double Tb2 = 0;
                double A = Fb1 * Tb1 * Cbinar1 + k * s * ((Fv * Tvoda * Cvoda) / (Fv * Cvoda + k * s)) - 0.2 * (Fb1 * Tb1 * Cbinar1 * q + Fb1 * Tb1 * Cbinar1) + Fb1 * Tb1 * Cbinar1 * q;
                double B = Fb1 * Cbinar1 + k * s - k * s * ((k * s) / (Fv * Cvoda + k * s));
                Tb2 = A / B;
                return Tb2;
            }
            public double TcubFf(double Ff, double Td, double Fd)
            {
                double Tcub = 0;
                double A = Fd * Cdist * Td + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fd * Cdist * Td + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Ff * Cflegmy - k2 * s2));
                double B = Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Ff * Cflegmy - k2 * s2));
                Tcub = A / B;
                return Tcub;
            }
            #region Формули для розрахунку статичних характеристик дефлегматора
            public Tuple<double[], double> StaticT2otTbinar1()
            {
                double Tb = getRangeFromCenterValue(Tbinar1)[0];
                double inc = getRangeFromCenterValue(Tbinar1)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_binar2_1[i - 1] = (0.8 * Fbinar1 * Tb * Cbinar1 + Fvoda * Tvoda * Cvoda - Fkondvoda * Ckondvoda * Tkondvoda + 0.8 * Fbinar1 * Tb * Cbinar1 * q) / (Fbinar1 * Cbinar1);
                    T_binar2_1[i - 1] = Math.Round(T_binar2_1[i - 1], 2);
                    Tb += inc;
                }
                K1 = (T_binar2_1[1] - T_binar2_1[2]) / -inc;
                return Tuple.Create(T_binar2_1, K1);
            }
            public Tuple<double[], double> StaticT2otFbinar1()
            {
                double Fb = getRangeFromCenterValue(Fbinar1)[0];
                double inc = getRangeFromCenterValue(Fbinar1)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_binar2_2[i - 1] = (0.8 * Fb * Tbinar1 * Cbinar1 + Fvoda * Tvoda * Cvoda - Fkondvoda * Ckondvoda * Tkondvoda + 0.8 * Fb * Tbinar1 * Cbinar1 * q) / (Fb * Cbinar1);
                    T_binar2_2[i - 1] = Math.Round(T_binar2_2[i - 1], 2);
                    Fb += inc;
                }
                K2 = (T_binar2_2[1] - T_binar2_2[2]) / -inc;
                return Tuple.Create(T_binar2_2, K2);
            }
            public Tuple<double[], double> StaticT2otFvod()
            {
                double Fv = getRangeFromCenterValue(Fvoda)[0];
                double inc = getRangeFromCenterValue(Fvoda)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_binar2_3[i - 1] = (Fbinar1 * Tbinar1 * Cbinar1 + k * s * ((Fv * Tvoda * Cvoda) / (Fv * Cvoda + k * s)) - 0.2 * (Fbinar1 * Tbinar1 * Cbinar1 * q + Fbinar1 * Tbinar1 * Cbinar1) + Fbinar1 * Tbinar1 * Cbinar1 * q) / (Fbinar1 * Cbinar1 + k * s - k * s * ((k * s) / (Fv * Cvoda + k * s)));
                    T_binar2_3[i - 1] = Math.Round(T_binar2_3[i - 1], 2);
                    Fv += inc;
                }
                K3 = (T_binar2_3[1] - T_binar2_3[2]) / -inc;
                return Tuple.Create(T_binar2_3, K3);
            }
            public Tuple<double[], double> StaticT2otTvod()
            {
                double Tv = getRangeFromCenterValue(Tvoda)[0];
                double inc = getRangeFromCenterValue(Tvoda)[2];
                for (int i = 1; i <= 9; i++)
                {

                    T_binar2_4[i - 1] = (Fbinar1 * Tbinar1 * Cbinar1 + k * s * ((Fvoda * Tv * Cvoda) / (Fvoda * Cvoda + k * s)) - 0.2 * (Fbinar1 * Tbinar1 * Cbinar1 * q + Fbinar1 * Tbinar1 * Cbinar1) + Fbinar1 * Tbinar1 * Cbinar1 * q) / (Fbinar1 * Cbinar1 + k * s - k * s * ((k * s) / (Fvoda * Cvoda + k * s)));
                    T_binar2_4[i - 1] = Math.Round(T_binar2_4[i - 1], 2);
                    Tv += inc;
                }
                K4 = (T_binar2_4[1] - T_binar2_4[2]) / -inc;
                return Tuple.Create(T_binar2_4, K4);
            }
            #endregion
            #region Формули для розрахунку статичних характеристик кип'ятильника
            public Tuple<double[], double> StaticTp2otFp()
            {
                double Fr1 = getRangeFromCenterValue(Frecur1)[0];
                double inc = getRangeFromCenterValue(Frecur1)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur2_1[i - 1] = (Fr1 * Crecur1 * Trecur1 + 0.8 * (Fpara1 * Ipara * Ppara) - Fpara1 * Ppara * Tkondpara * Ckondpara - Fr1 * q1) / (Fr1 * Crecur1);
                    T_recur2_1[i - 1] = Math.Round(T_recur2_1[i - 1], 2);
                    Fr1 += inc;
                }
                //Перевірити
                K5 = (T_recur2_1[1] - T_recur2_1[2]) / -inc;
                return Tuple.Create(T_recur2_1, K5);
            }
            public Tuple<double[], double> StaticTp2otTp1()
            {
                double Tr1 = getRangeFromCenterValue(Trecur1)[0];
                double inc = getRangeFromCenterValue(Trecur1)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur2_2[i - 1] = (Frecur1 * Crecur1 * Tr1 + 0.8 * (Fpara1 * Ipara * Ppara) - Fpara1 * Ppara * Tkondpara * Ckondpara - Frecur1 * q1) / (Frecur1 * Crecur1);
                    T_recur2_2[i - 1] = Math.Round(T_recur2_2[i - 1], 2);
                    Tr1 += inc;
                }
                K6 = (T_recur2_2[1] - T_recur2_2[2]) / -inc;
                return Tuple.Create(T_recur2_2, K6);
            }
            public Tuple<double[], double> StaticTp2otFpara()
            {
                double Fp = getRangeFromCenterValue(Fpara1)[0];
                double inc = getRangeFromCenterValue(Fpara1)[2];
                for (int i = 1; i <= 9; i++)
                {

                    T_recur2_3[i - 1] = (-(Frecur1 * Crecur1 * Trecur1 - Frecur1 * q1 - k1 * s1 * (-0.8 * Fp * Ipara * Ppara / (k1 * s1 - Fp * Ppara * Ckondpara))) / (-Frecur1 * Crecur1 + k1 * s1 - k1 * s1 * (k1 * s1 / (k1 * s1 - Fp * Ppara * Ckondpara))));
                    T_recur2_3[i - 1] = Math.Round(T_recur2_3[i - 1], 2);
                    Fp += inc;
                }
                //перевірити
                K7 = (T_recur2_3[1] - T_recur2_3[2]) / -inc;
                return Tuple.Create(T_recur2_3, K7);
            }
            #endregion
            #region Формули для розрахунку статичних характеристик колони
            public Tuple<double[], double> StaticTp1opFd()
            {
                double Fd = getRangeFromCenterValue(Fdist)[0];
                double inc = getRangeFromCenterValue(Fdist)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur1_1[i - 1] = (Fd * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fd * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_1[i - 1] = Math.Round(T_recur1_1[i - 1], 2);
                    Fd += inc;
                }
                K8 = (T_recur1_1[1] - T_recur1_1[2]) / -inc;
                return Tuple.Create(T_recur1_1, K8);
            }
            public Tuple<double[], double> StaticTp1opTd()
            {
                double Td = getRangeFromCenterValue(Tdist)[0];
                double inc = getRangeFromCenterValue(Tdist)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur1_2[i - 1] = (Fdist * Cdist * Td + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Td + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_2[i - 1] = Math.Round(T_recur1_2[i - 1], 2);
                    Td += inc;
                }
                K9 = (T_recur1_2[1] - T_recur1_2[2]) / -inc;
                return Tuple.Create(T_recur1_2, K9);
            }
            public Tuple<double[], double> StaticTp1opTp2()
            {
                double Tr2 = getRangeFromCenterValue(Trecur2)[0];
                double inc = getRangeFromCenterValue(Trecur2)[2];

                for (int i = 1; i <= 9; i++)
                {
                    T_recur1_3[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Tr2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Tr2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_3[i - 1] = Math.Round(T_recur1_3[i - 1], 2);
                    Tr2 += inc;
                }
                K10 = (T_recur1_3[1] - T_recur1_3[2]) / -inc;
                return Tuple.Create(T_recur1_3, K10);
            }
            public Tuple<double[], double> StaticTp1opFf()
            {
                double Ff = getRangeFromCenterValue(Fflegmy)[0];
                double inc = getRangeFromCenterValue(Fflegmy)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur1_4[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tbinar1 * Cbinar1 / (Ff * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Ff * Cflegmy - k2 * s2)));
                    T_recur1_4[i - 1] = Math.Round(T_recur1_4[i - 1], 2);
                    Ff += inc;
                }
                K11 = (T_recur1_4[1] - T_recur1_4[2]) / -inc;
                return Tuple.Create(T_recur1_4, K11);
            }
            public Tuple<double[], double> StaticTp1opFb()
            {
                double Fb1 = getRangeFromCenterValue(Fbinar1)[0];
                double inc = getRangeFromCenterValue(Fbinar1)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur1_5[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fb1 * Tbinar1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_5[i - 1] = Math.Round(T_recur1_5[i - 1], 2);
                    Fb1 += inc;
                }
                K12 = (T_recur1_5[1] - T_recur1_5[2]) / -inc;
                return Tuple.Create(T_recur1_5, K12);
            }
            public Tuple<double[], double> StaticTp1opTb()
            {
                double Tb1 = getRangeFromCenterValue(Tbinar1)[0];
                double inc = getRangeFromCenterValue(Tbinar1)[2];
                for (int i = 1; i <= 9; i++)
                {
                    T_recur1_6[i - 1] = (Fdist * Cdist * Tdist + 0.8 * Frecur1 * Trecur2 * Crecur1 - (Fdist * Cdist * Tdist + Frecur1 * Trecur2 * Crecur1) * q2 + k2 * s2 * (Fbinar1 * Tb1 * Cbinar1 / (Fflegmy * Cflegmy - k2 * s2))) / (Fcub * Ccub + k2 * s2 - k2 * s2 * ((-k2 * s2) / (Fflegmy * Cflegmy - k2 * s2)));
                    T_recur1_6[i - 1] = Math.Round(T_recur1_6[i - 1], 2);
                    Tb1 += inc;
                }
                K13 = (T_recur1_6[1] - T_recur1_6[2]) / -inc;
                return Tuple.Create(T_recur1_6, K13);
            }
            #endregion

            public void DrawStaticFunction(object chart, double XValueCenter, double[] YValues, string XTitle, string YTitle)
            {
                var myChart = chart as System.Windows.Forms.DataVisualization.Charting.Chart;

                foreach (var series in myChart.Series)
                {
                    series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    series.BorderWidth = 2;
                    series.Points.Clear();

                }
                myChart.ChartAreas[0].AxisX.Minimum = Double.NaN;
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
                int xStep = (int)Math.Round((xEnd - xStart) / 9.0);
                result[0] = xStart;
                result[1] = xEnd;
                result[2] = xStep;
                return result;
            }
            public void printResult(object formLabel, string title, double value)
            {
                var label = formLabel as Label;
                if (title != "")
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
                var label = formLabel as Label;
                label.Text = title;
                for (int i = 0; i < 9; i++)
                {
                    label.Text += "\n" + value[i].ToString("N");
                }

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Clear();
                detectRelation();
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
            }

        }
        private void timeEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(timeEdit);
            double editedTime;
            if (Double.TryParse(timeEdit.Text, out editedTime))
            {
                timeChanged = true;
                newTime = editedTime;
                detectRelation();
                timeChanged = false;
            }
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //TODO: Fix Scale 
            //float newRatio = (float)(Width-300) / processPanel.Width;
            //SizeF scale = new SizeF(newRatio, newRatio);
            //processPanel.Scale(scale);
        }
        #region Change parameters values
        private void Fbinar1Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Fbinar1Edit);
            if (Fbinar1Edit.Text.Length != 0)
            {
                process.Fbinar11 = Convert.ToDouble(Fbinar1Edit.Text);
            }

        }

        private void Tbinar1Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Tbinar1Edit);
            if (Tbinar1Edit.Text.Length != 0)
            {
                process.Tbinar11 = Convert.ToDouble(Tbinar1Edit.Text);
            }
        }

        private void Cbinar1Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Cbinar1Edit);
            if (Cbinar1Edit.Text.Length != 0)
            {
                process.Cbinar11 = Convert.ToDouble(Cbinar1Edit.Text);
            }

        }

        private void FdistEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FdistEdit);
            if (FdistEdit.Text.Length != 0)
            {
                process.Fdist1 = Convert.ToDouble(FdistEdit.Text);
            }
        }

        private void TdistEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(TdistEdit);
            if (TdistEdit.Text.Length != 0)
            {
                process.Tdist1 = Convert.ToDouble(TdistEdit.Text);
            }
        }

        private void CdistEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(CdistEdit);
            if (CdistEdit.Text.Length != 0)
            {
                process.Cdist1 = Convert.ToDouble(CdistEdit.Text);
            }
        }

        private void FkondVodaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FkondVodaEdit);
            if (FkondVodaEdit.Text.Length != 0)
            {
                process.Fkondvoda1 = Convert.ToDouble(FkondVodaEdit.Text);
            }
        }

        private void CkondVodaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(CkondVodaEdit);
            if (CkondVodaEdit.Text.Length != 0)
            {
                process.Ckondvoda1 = Convert.ToDouble(CkondVodaEdit.Text);
            }
        }

        private void FflegmyEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FflegmyEdit);
            if (FflegmyEdit.Text.Length != 0)
            {
                process.Fflegmy1 = Convert.ToDouble(FflegmyEdit.Text);
            }
        }

        private void TflegmyEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(TflegmyEdit);
            if (TflegmyEdit.Text.Length != 0)
            {
                process.Tflegmy1 = Convert.ToDouble(TflegmyEdit.Text);
            }
        }

        private void CflegmyEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(CflegmyEdit);
            if (CflegmyEdit.Text.Length != 0)
            {
                process.Cflegmy1 = Convert.ToDouble(CflegmyEdit.Text);
            }
        }

        private void Frecur2Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Frecur2Edit);
            if (Frecur2Edit.Text.Length != 0)
            {
                process.Frecur21 = Convert.ToDouble(Frecur2Edit.Text);
            }
        }

        private void Trecur2Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Trecur2Edit);
            if (Trecur2Edit.Text.Length != 0)
            {
                process.Trecur21 = Convert.ToDouble(Trecur2Edit.Text);
            }
        }

        private void Crecur2Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Crecur2Edit);
            if (Crecur2Edit.Text.Length != 0)
            {
                process.Crecur21 = Convert.ToDouble(Crecur2Edit.Text);
            }
        }

        private void FkondparaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FkondparaEdit);
            if (FkondparaEdit.Text.Length != 0)
            {
                process.Fkondpara1 = Convert.ToDouble(FkondparaEdit.Text);
            }
        }

        private void TkondparaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(TkondparaEdit);
            if (TkondparaEdit.Text.Length != 0)
            {
                process.Tkondpara1 = Convert.ToDouble(TkondparaEdit.Text);
            }
        }

        private void FcubEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FcubEdit);
            if (FcubEdit.Text.Length != 0)
            {
                process.Fcub1 = Convert.ToDouble(FcubEdit.Text);
            }
        }

        private void TcubEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(TcubEdit);
            if (TcubEdit.Text.Length != 0)
            {
                process.Tcub1 = Convert.ToDouble(TcubEdit.Text);
            }
        }

        private void Frecur1Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Frecur1Edit);
            if (Frecur1Edit.Text.Length != 0)
            {
                process.Frecur11 = Convert.ToDouble(Frecur1Edit.Text);
            }
        }

        private void Trecur1Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Trecur1Edit);
            if (Trecur1Edit.Text.Length != 0)
            {
                process.Trecur11 = Convert.ToDouble(Trecur1Edit.Text);
            }
        }

        private void Crecur1Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Crecur1Edit);
            if (Crecur1Edit.Text.Length != 0)
            {
                process.Crecur11 = Convert.ToDouble(Crecur1Edit.Text);
            }
        }

        private void FparaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FparaEdit);
            if (FparaEdit.Text.Length != 0)
            {
                process.Fpara1 = Convert.ToDouble(FparaEdit.Text);
            }
        }

        private void Fbinar2Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Fbinar2Edit);
            if (Fbinar2Edit.Text.Length != 0)
            {
                process.Fbinar21 = Convert.ToDouble(Fbinar2Edit.Text);
            }

        }

        private void Tbinar2Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Tbinar2Edit);
            if (Tbinar2Edit.Text.Length != 0)
            {
                process.Tbinar21 = Convert.ToDouble(Tbinar2Edit.Text);
            }
        }

        private void Cbinar2Edit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(Cbinar2Edit);
            if (Cbinar2Edit.Text.Length != 0)
            {
                process.Cbinar21 = Convert.ToDouble(Cbinar2Edit.Text);
            }
        }

        private void FvodaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(FvodaEdit);
            if (FvodaEdit.Text.Length != 0)
            {
                process.Fvoda1 = Convert.ToDouble(FvodaEdit.Text);
            }
        }

        private void TvodaEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(TvodaEdit);
            if (TvodaEdit.Text.Length != 0)
            {
                process.Tvoda1 = Convert.ToDouble(TvodaEdit.Text);
            }
        }

        private void CcubEdit_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(CcubEdit);
            if (CcubEdit.Text.Length != 0)
            {
                process.Ccub1 = Convert.ToDouble(CcubEdit.Text);
            }
        }
        #endregion

        private void preventTextInput(object input)
        {
            var textInput = input as System.Windows.Forms.TextBox;
            if (Regex.IsMatch(textInput.Text, @"[^\d,]"))
            {
                MessageBox.Show("Тільки цифри і десятковий дільник кома!");
                textInput.Text = Regex.Replace(textInput.Text, @"[^\d,]", String.Empty);
            }
        }

        private void detectRelation()
        {
            if (staticFuncs)
            {
                DrawStaticFunction();
            }
            if (dynamicFuncs)
            {
                DrawDynamicFunction();
            }
        }



        private void DrawDynamicFunction()
        {
            switch (tabControl1.SelectedTab.Text)
            {
                //deflegmator tabs
                case "Tb2(Tb1)":
                    double W1(double s)
                    {
                        return 0.98 / (6.944 * s * s + 1.0 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W1, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W1, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                case "Tb2(Fb1)":
                    double W2(double s)
                    {
                        return 0.043 / (6.9 * s * s + 1.0 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W2, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W2, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                case "Tb2(Fv)":
                    double W3(double s)
                    {
                        return -0.013 / (7.077 * s * s * s + 6.716 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W3, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W3, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;

                //boiler tabs
                case "Tr2(Fr1)":
                    double W5(double s)
                    {
                        return -2.27 / (7.08 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W5, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W5, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                case "Tr2(Tr1)":
                    double W4(double s)
                    {
                        return 1 / (7.08 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W4, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W4, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                case "Tr2(Fp)":
                    double W6(double s)
                    {
                        return 3.024 / (0.413 * s * s * s + 1.153 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W6, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W6, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;

                //rectification column tabs
                case "Tr1(Ffleg)":
                    double W7(double s)
                    {
                        return 0.634 / (24.27 * s * s * s + 21.33 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W7, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W7, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                case "Tr1(Fb1)":
                    double W8(double s)
                    {
                        return -0.695 / (24.27 * s * s * s + 21.33 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W8, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W8, chart1, dynDataGridView);
                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                case "Tr1(Tb1)":
                    double W9(double s)
                    {
                        return -1.097 / (24.27 * s * s * s + 21.33 * s * s + 1 * s);
                    }
                    if (timeChanged)
                    {
                        Laplace.drawStepResponse(W9, chart1, dynDataGridView, (int)newTime);
                    }
                    else
                    {
                        Laplace.drawStepResponse(W9, chart1, dynDataGridView);

                    }
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
                //default chart for deflegmator
                default:
                    Laplace.drawStepResponse(W7, chart1, dynDataGridView);
                    timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
                    break;
            }
        }


        private void DrawStaticFunction()
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
                    process.DrawStaticFunction(chart1, process.Fbinar11, staticT2otFbinar1, "Fbinar1, m^3/год", "Tbinar2, K");
                    process.printResult(labelX, "Fb1, m^3/год", process.Fbinar11);
                    process.printResult(labelY, "Tb2, K", staticT2otFbinar1);
                    process.printResult(labelK, "", K2);
                    break;
                case "Tb2(Fv)":
                    process.DrawStaticFunction(chart1, process.Fvoda1, staticT2otFvod, "Fvoda, m^3/год", "Tbinar2, K");
                    process.printResult(labelX, "Fv, m^3/год", process.Fvoda1);
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
                    process.DrawStaticFunction(chart1, process.Frecur11, staticTp2otFp, "Frecur1, m^3/год", "Trecur2, K");
                    process.printResult(labelX, "Fr1, m^3/год", process.Frecur11);
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
                    process.DrawStaticFunction(chart1, process.Fpara1, staticTp2otFpara, "Fpara, m^3/год", "Trecur2, K");
                    process.printResult(labelX, "Fp, m^3/год", process.Fpara1);
                    process.printResult(labelY, "Tr2, K", staticTp2otFpara);
                    process.printResult(labelK, "", K7);
                    break;

                //rectification column tabs
                case "Tr1(Fdist)":
                    process.DrawStaticFunction(chart1, process.Fdist1, staticTp1opFd, "Fdist, m^3/год", "Trecur1, K");
                    process.printResult(labelX, "Fd, m^3/год", process.Fdist1);
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
                    process.DrawStaticFunction(chart1, process.Fflegmy1, staticTp1opFf, "Fflegmy, m^3/год", "Trecur1, K");
                    process.printResult(labelX, "Tfl, m^3/год", process.Fflegmy1);
                    process.printResult(labelY, "Tr1, K", staticTp1opFf);
                    process.printResult(labelK, "", K11);
                    break;
                case "Tr1(Fb)":
                    process.DrawStaticFunction(chart1, process.Fbinar11, staticTp1opFb, "Fbinar, m^3/год", "Trecur1, K");
                    process.printResult(labelX, "Fb, m^3/год", process.Fbinar11);
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

        private void імітаціяToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showImitation(); 
            chartTr2.ChartAreas[0].AxisY.Minimum = 200;
            chartTr2.ChartAreas[0].AxisY.Maximum = 500;
            chartFpara.ChartAreas[0].AxisY.Minimum = 100;
            chartFpara.ChartAreas[0].AxisY.Maximum = 180;
            chartFr1.ChartAreas[0].AxisY.Minimum = 940;
            chartFr1.ChartAreas[0].AxisY.Maximum = 980;
            chartTr1.ChartAreas[0].AxisY.Minimum = 320;
            chartTr1.ChartAreas[0].AxisY.Maximum = 400;

            chartTb2.ChartAreas[0].AxisY.Minimum = 305;
            chartTb2.ChartAreas[0].AxisY.Maximum = 320;
            chartFv.ChartAreas[0].AxisY.Minimum = 560;
            chartFv.ChartAreas[0].AxisY.Maximum = 750;
            chartFb1.ChartAreas[0].AxisY.Minimum = 490;
            chartFb1.ChartAreas[0].AxisY.Maximum = 600;
            chartTb1.ChartAreas[0].AxisY.Minimum = 320;
            chartTb1.ChartAreas[0].AxisY.Maximum = 360;

            chartTcub.ChartAreas[0].AxisY.Minimum = 270;
            chartTcub.ChartAreas[0].AxisY.Maximum = 440;
            chartFf.ChartAreas[0].AxisY.Minimum = 250;
            chartFf.ChartAreas[0].AxisY.Maximum = 370;
            chartTd.ChartAreas[0].AxisY.Minimum = 280;
            chartTd.ChartAreas[0].AxisY.Maximum = 310;
            chartFd.ChartAreas[0].AxisY.Minimum = 1450;
            chartFd.ChartAreas[0].AxisY.Maximum = 1600;

            radioButton2.Checked = true;
            radioButton6.Checked = true;
            radioButton9.Checked = true;
            excelBtnBoliler.Enabled = true;
            excelBtnDefleg.Enabled = true;
            excelBtnColumn.Enabled = true;
        }
        int N = -1;
        Random rnd = new Random();
        double Tr2Imit = 0;
        double FparaImit = 0;
        double Fr1Imit = 0;
        double Tr1Imit = 0;
        double percentBoiler = 1;
        double[] FparaData = new double[1800];
        double[] Fr1Data = new double[1800];
        double[] Tr1Data = new double[1800];
        double[] Tr2Data = new double[1800];
        string[] currTime = new string[1800];

        double Tb2Imit = 0;
        double FvImit = 0;
        double Tb1Imit = 0;
        double Fb1Imit = 0;
        double percentDefleg = 1;
        double[] FvData = new double[1800];
        double[] Tb2Data = new double[1800];
        double[] Tb1Data = new double[1800];
        double[] Fb1Data = new double[1800];

        double TcubImit = 0;
        double FfImit = 0;
        double TdImit = 0;
        double FdImit = 0;
        double percentColumn = 1;
        double[] FfData = new double[1800];
        double[] TcubData = new double[1800];
        double[] TdData = new double[1800];
        double[] FdData = new double[1800];

        string mode = "work";
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ImitationTabs.SelectedTab.Text == "Кип\'ятильник")
            {
                if (radioButton1.Checked)
                {
                    mode = "min";
                }
                else if (radioButton2.Checked)
                {
                    mode = "work";
                }
                else
                {
                    mode = "max";
                }
                percentBoiler = getWorkMode(percentBoiler, mode, 0.85, 1.15);
                
            }
            
            if (ImitationTabs.SelectedTab.Text == "Дефлегматор")
            {
                if (radioButton4.Checked)
                {
                    mode = "min";
                }
                else if (radioButton6.Checked)
                {
                    mode = "work";
                }
                else
                {
                    mode = "max";
                }
                percentDefleg = getWorkMode(percentDefleg, mode, 0.95, 1.05);
            }

            if (ImitationTabs.SelectedTab.Text == "Ректифікаційна колона")
            {
                if (radioButton7.Checked)
                {
                    mode = "min";
                }
                else if (radioButton9.Checked)
                {
                    mode = "work";
                }
                else
                {
                    mode = "max";
                }
                percentColumn = getWorkMode(percentColumn, mode, 0.9, 1.1);
            }

            RandomBoilerParameter();
            RandomDeflegParameter();
            RandomColumnParameter();

            Tr2Imit = process.Tr2Fp(FparaImit, Fr1Imit, Tr1Imit);
            chartTr2.Series[0].Points.AddXY(N, Tr2Imit);
            chartFpara.Series[0].Points.AddXY(N, FparaImit);
            chartFr1.Series[0].Points.AddXY(N, Fr1Imit);
            chartTr1.Series[0].Points.AddXY(N, Tr1Imit);

            Tb2Imit = process.Tb2Fv(FvImit, Tb1Imit, Fb1Imit);
            chartTb2.Series[0].Points.AddXY(N, Tb2Imit);
            chartFv.Series[0].Points.AddXY(N, FvImit);
            chartTb1.Series[0].Points.AddXY(N, Tb1Imit);
            chartFb1.Series[0].Points.AddXY(N, Fb1Imit);

            TcubImit = process.TcubFf(FfImit, TdImit, FdImit);
            chartTcub.Series[0].Points.AddXY(N, TcubImit);
            chartFf.Series[0].Points.AddXY(N, FfImit);
            chartTd.Series[0].Points.AddXY(N, TdImit);
            chartFd.Series[0].Points.AddXY(N, FdImit);
            if (N >= 0)
            {
                currTime[N] = DateTime.Now.ToLongTimeString();
                FparaData[N] = FparaImit;
                Fr1Data[N] = Fr1Imit;
                Tr1Data[N] = Tr1Imit;
                Tr2Data[N] = Tr2Imit;

                FvData[N] = FvImit;
                Tb2Data[N] = Tb2Imit;
                Fb1Data[N] = Fb1Imit;
                Tb1Data[N] = Tb1Imit;

                FfData[N] = FfImit;
                TcubData[N] = TcubImit;
                FdData[N] = FdImit;
                TdData[N] = TdImit;
            }


            mainParamLabel.Text = "Tr2 = " + Tr2Imit.ToString("F2") + " K";
            FparaLabel.Text = "Fp = " + FparaImit.ToString("F2") + " м^3/год";
            Fr1Label.Text = "Fr1 = " + Fr1Imit.ToString("F2") + " м^3/год";
            Tr1Label.Text = "Tr1 = " + Tr1Imit.ToString("F2") + " K";

            Tb2Label.Text = "Tf = " + Tb2Imit.ToString("F2") + " K";
            FvLabel.Text = "Fv = " + FvImit.ToString("F2") + " м^3/год";
            Tb1Label.Text = "Tb1 = " + Tb1Imit.ToString("F2") + " K";
            Fb1Label.Text = "Fb1 = " + Fb1Imit.ToString("F2") + " м^3/год";

            TcubLabel.Text = "Tкуб = " + TcubImit.ToString("F2") + " K";
            FfLabel.Text = "Fф = " + FfImit.ToString("F2") + " м^3/год";
            TdLabel.Text = "Tд = " + TdImit.ToString("F2") + " K";
            FdLabel.Text = "Fд = " + FdImit.ToString("F2") + " м^3/год";

            ++N;
            if (N < 6)
            {
                chartFpara.ChartAreas[0].AxisX.Minimum = 0;
                chartFpara.ChartAreas[0].AxisX.Maximum = 6;
                chartFr1.ChartAreas[0].AxisX.Minimum = 0;
                chartFr1.ChartAreas[0].AxisX.Maximum = 6;
                chartTr1.ChartAreas[0].AxisX.Minimum = 0;
                chartTr1.ChartAreas[0].AxisX.Maximum = 6;

                chartFv.ChartAreas[0].AxisX.Minimum = 0;
                chartFv.ChartAreas[0].AxisX.Maximum = 6;
                chartTb1.ChartAreas[0].AxisX.Minimum = 0;
                chartTb1.ChartAreas[0].AxisX.Maximum = 6;
                chartFb1.ChartAreas[0].AxisX.Minimum = 0;
                chartFb1.ChartAreas[0].AxisX.Maximum = 6;

                chartFf.ChartAreas[0].AxisX.Minimum = 0;
                chartFf.ChartAreas[0].AxisX.Maximum = 6;
                chartTd.ChartAreas[0].AxisX.Minimum = 0;
                chartTd.ChartAreas[0].AxisX.Maximum = 6;
                chartFd.ChartAreas[0].AxisX.Minimum = 0;
                chartFd.ChartAreas[0].AxisX.Maximum = 6;
            }
            else
            {
                chartFpara.ChartAreas[0].AxisX.Minimum = N - 6;
                chartFpara.ChartAreas[0].AxisX.Maximum = N;
                chartFr1.ChartAreas[0].AxisX.Minimum = N - 6;
                chartFr1.ChartAreas[0].AxisX.Maximum = N;
                chartTr1.ChartAreas[0].AxisX.Minimum = N - 6;
                chartTr1.ChartAreas[0].AxisX.Maximum = N;

                chartFv.ChartAreas[0].AxisX.Minimum = N - 6;
                chartFv.ChartAreas[0].AxisX.Maximum = N;
                chartFb1.ChartAreas[0].AxisX.Minimum = N - 6;
                chartFb1.ChartAreas[0].AxisX.Maximum = N;
                chartTb1.ChartAreas[0].AxisX.Minimum = N - 6;
                chartTb1.ChartAreas[0].AxisX.Maximum = N;

                chartFf.ChartAreas[0].AxisX.Minimum = N - 6;
                chartFf.ChartAreas[0].AxisX.Maximum = N;
                chartFd.ChartAreas[0].AxisX.Minimum = N - 6;
                chartFd.ChartAreas[0].AxisX.Maximum = N;
                chartTd.ChartAreas[0].AxisX.Minimum = N - 6;
                chartTd.ChartAreas[0].AxisX.Maximum = N;
            }
            if (N <= 10)
            {
                chartTr2.ChartAreas[0].AxisX.Minimum = 0;
                chartTr2.ChartAreas[0].AxisX.Maximum = 10;

                chartTb2.ChartAreas[0].AxisX.Minimum = 0;
                chartTb2.ChartAreas[0].AxisX.Maximum = 10;

                chartTcub.ChartAreas[0].AxisX.Minimum = 0;
                chartTcub.ChartAreas[0].AxisX.Maximum = 10;
            }
            else
            {
                chartTr2.ChartAreas[0].AxisX.Minimum = N - 10;
                chartTr2.ChartAreas[0].AxisX.Maximum = N;

                chartTb2.ChartAreas[0].AxisX.Minimum = N - 10;
                chartTb2.ChartAreas[0].AxisX.Maximum = N;

                chartTcub.ChartAreas[0].AxisX.Minimum = N - 10;
                chartTcub.ChartAreas[0].AxisX.Maximum = N;
            }
            
        }
        private void RandomColumnParameter()
        {
            switch (rnd.Next(0, 3))
            {
                case 0:
                    FfImit = process.Fflegmy1 * percentColumn + process.Fflegmy1 * percentColumn * (rnd.Next(-1, 1) / 100.0);
                    FdImit = process.Fdist1;
                    TdImit = process.Tdist1;
                    FfLabel.BackColor = Color.LightCoral;
                    FdLabel.BackColor = Color.Transparent;
                    TdLabel.BackColor = Color.Transparent;
                    break;
                case 1:
                    FfImit = process.Fflegmy1 * percentColumn;
                    FdImit = process.Fdist1 + process.Fdist1 * (rnd.Next(-1, 1) / 100.0);
                    TdImit = process.Tdist1;
                    FfLabel.BackColor = Color.Transparent;
                    FdLabel.BackColor = Color.LightCoral;
                    TdLabel.BackColor = Color.Transparent;
                    break;
                case 2:
                    FfImit = process.Fflegmy1 * percentColumn;
                    FdImit = process.Fdist1;
                    TdImit = process.Tdist1 + process.Tdist1 * (rnd.Next(-1, 1) / 100.0);
                    FfLabel.BackColor = Color.Transparent;
                    FdLabel.BackColor = Color.LightCoral;
                    TdLabel.BackColor = Color.Transparent;
                    break;
            }
        }
        private void RandomDeflegParameter()
        {
            switch (rnd.Next(0, 3))
            {
                case 0:
                    FvImit = process.Fvoda1 * percentDefleg + process.Fvoda1 * percentDefleg * (rnd.Next(-1, 1) / 100.0);
                    Fb1Imit = process.Fbinar11;
                    Tb1Imit = process.Tbinar11;
                    FvLabel.BackColor = Color.LightCoral;
                    Fb1Label.BackColor = Color.Transparent;
                    Tb1Label.BackColor = Color.Transparent;
                    break;
                case 1:
                    FvImit = process.Fvoda1 * percentDefleg;
                    Fb1Imit = process.Fbinar11 + process.Fbinar11 * (rnd.Next(-1, 1) / 100.0);
                    Tb1Imit = process.Tbinar11;
                    FvLabel.BackColor = Color.Transparent;
                    Fb1Label.BackColor = Color.LightCoral;
                    Tb1Label.BackColor = Color.Transparent;
                    break;
                case 2:
                    FvImit = process.Fvoda1 * percentDefleg;
                    Fb1Imit = process.Fbinar11;
                    Tb1Imit = process.Tbinar11 + process.Tbinar11 * (rnd.Next(-1, 1) / 100.0);
                    FvLabel.BackColor = Color.Transparent;
                    Fb1Label.BackColor = Color.LightCoral;
                    Tb1Label.BackColor = Color.Transparent;
                    break;
            }
        }
        private void RandomBoilerParameter()
        {
            switch (rnd.Next(0, 3))
            {
                case 0:
                    FparaImit = process.Fpara1 * percentBoiler + process.Fpara1 * percentBoiler * (rnd.Next(-2, 2) / 100.0);
                    Fr1Imit = process.Frecur11;
                    Tr1Imit = process.Trecur11;
                    FparaLabel.BackColor = Color.LightCoral;
                    Fr1Label.BackColor = Color.Transparent;
                    Tr1Label.BackColor = Color.Transparent;
                    break;
                case 1:
                    FparaImit = process.Fpara1 * percentBoiler;
                    Fr1Imit = process.Frecur11 + process.Frecur11 * (rnd.Next(-1, 1) / 200.0);
                    Tr1Imit = process.Trecur11;
                    FparaLabel.BackColor = Color.Transparent;
                    Fr1Label.BackColor = Color.LightCoral;
                    Tr1Label.BackColor = Color.Transparent;
                    break;
                case 2:
                    FparaImit = process.Fpara1 * percentBoiler;
                    Fr1Imit = process.Frecur11;
                    Tr1Imit = process.Trecur11 + process.Trecur11 * (rnd.Next(-2, 2) / 100.0);
                    FparaLabel.BackColor = Color.Transparent;
                    Fr1Label.BackColor = Color.Transparent;
                    Tr1Label.BackColor = Color.LightCoral;
                    break;
            }
        }

        private double getWorkMode(double currentPercent, string workMode, double min, double max)
        {
            double step = (max - min) / 10;
            //Метод для вибору режиму роботи апарату (Мінімальне навантаження, робочий режим, максимальне навантаження)
            double _percent = currentPercent;
            //Режим мінімального навантаження
            if (workMode == "min")
            {
                if (_percent >= min)
                {
                    _percent -= step;
                }
                else
                {
                    _percent = min;
                }
            }
            //Робочий режим
            if (workMode == "work")
            {
                if (_percent > 1.0)
                {
                    _percent -= step;
                }
                if (_percent < 1.0)
                {
                    _percent += step;
                } 
            }
            //Режим максимального навантаження
            if (workMode == "max")
            {
                if (_percent <= max)
                {
                    _percent += step;
                }
                else
                {
                    _percent = max;
                }
            }
            return _percent;
        }

        private void excelBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //Write to Excel
            Cursor.Current = Cursors.WaitCursor;
            var XL = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook nwb = null;
            Microsoft.Office.Interop.Excel._Worksheet nws = null;
            XL.Visible = false;
            nwb = XL.Workbooks.Add();
            nws = (Microsoft.Office.Interop.Excel._Worksheet)nwb.ActiveSheet;
            //Get a new workbook.
            //double[] noEmptyData = new double[N];
            nws.Cells[1, 1] = "Час";
            nws.Cells[1, 2] = "Fpara, м^3/год";
            nws.Cells[1, 3] = "Fr1, м^3/год";
            nws.Cells[1, 4] = "Tr1, K";
            nws.Cells[1, 5] = "Tr2, K";
            for (int i = 0; i < N; i++)
            {
                nws.Cells[i + 2, 1] = currTime[i];
                nws.Cells[i + 2, 2] = FparaData[i];
                nws.Cells[i + 2, 3] = Fr1Data[i];
                nws.Cells[i + 2, 4] = Tr1Data[i];
                nws.Cells[i + 2, 5] = Tr2Data[i];
                //noEmptyData[i] = Data[i];
            }

            //Add Chart
            var charts = nws.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(300, 30, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartFpara = chartObject.Chart;
            var rangeFpara = nws.get_Range("B2", "B" + (N).ToString());
            chartFpara.SetSourceData(rangeFpara);
            chartFpara.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartFpara.ChartWizard(Source: rangeFpara,
               Title: "Витрата пари",
               CategoryTitle: "Час, сек",
               ValueTitle: "Витрата пари, м.куб/год.");

            var chartObjectFr1 = charts.Add(300, 240, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartFr1 = chartObjectFr1.Chart;
            var rangeFr1 = nws.get_Range("C2", "C" + (N).ToString());
            chartFr1.SetSourceData(rangeFr1);
            chartFr1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartFr1.ChartWizard(Source: rangeFr1,
               Title: "Витрата рециркуляту 1",
               CategoryTitle: "Час, сек",
               ValueTitle: "Витрата рециркуляту, м.куб/год.");

            var chartObjectTr1 = charts.Add(300, 450, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartTr1 = chartObjectTr1.Chart;
            var rangeTr1 = nws.get_Range("D2", "D" + (N).ToString());
            chartTr1.SetSourceData(rangeTr1);
            chartTr1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartTr1.ChartWizard(Source: rangeTr1,
               Title: "Температура рециркуляту 1",
               CategoryTitle: "Час, сек",
               ValueTitle: "Температура рециркуляту, K");

            var chartObjectTr2 = charts.Add(620, 30, 500, 400) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartTr2 = chartObjectTr2.Chart;
            var rangeTr2 = nws.get_Range("E2", "E" + (N).ToString());
            chartTr2.SetSourceData(rangeTr2);
            chartTr2.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartTr2.ChartWizard(Source: rangeTr2,
               Title: "Температура рециркуляту 2",
               CategoryTitle: "Час, сек",
               ValueTitle: "Температура рециркуляту, K");

            nws.SaveAs(progPath + "\\BoilerData.xlsx");
            nwb.Close(false);
            XL.Quit();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Дані записано у файл "+ progPath + "\\BoilerData.xlsx", "Експорт даних до Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Write to Excel
        }

        private void excelBtnDefleg_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //Write to Excel
            Cursor.Current = Cursors.WaitCursor;
            var XL = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook nwb = null;
            Microsoft.Office.Interop.Excel._Worksheet nws = null;
            XL.Visible = false;
            nwb = XL.Workbooks.Add();
            nws = (Microsoft.Office.Interop.Excel._Worksheet)nwb.ActiveSheet;
            //Get a new workbook.
            //double[] noEmptyData = new double[N];
            nws.Cells[1, 1] = "Час";
            nws.Cells[1, 2] = "Fv, м^3/год";
            nws.Cells[1, 3] = "Fb1, м^3/год";
            nws.Cells[1, 4] = "Tb1, K";
            nws.Cells[1, 5] = "Tb2, K";
            for (int i = 0; i < N; i++)
            {
                nws.Cells[i + 2, 1] = currTime[i];
                nws.Cells[i + 2, 2] = FvData[i];
                nws.Cells[i + 2, 3] = Fb1Data[i];
                nws.Cells[i + 2, 4] = Tb1Data[i];
                nws.Cells[i + 2, 5] = Tb2Data[i];
            }

            //Add Chart
            var charts = nws.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(300, 30, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartFv = chartObject.Chart;
            var rangeFvoda = nws.get_Range("B2", "B" + (N).ToString());
            chartFv.SetSourceData(rangeFvoda);
            chartFv.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartFv.ChartWizard(Source: rangeFvoda,
               Title: "Витрата води",
               CategoryTitle: "Час, сек",
               ValueTitle: "Витрата води, м.куб/год.");

            var chartObjectFb1 = charts.Add(300, 240, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartFb1 = chartObjectFb1.Chart;
            var rangeFb1 = nws.get_Range("C2", "C" + (N).ToString());
            chartFb1.SetSourceData(rangeFb1);
            chartFb1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartFb1.ChartWizard(Source: rangeFb1,
               Title: "Витрата бінарної суміші 1",
               CategoryTitle: "Час, сек",
               ValueTitle: "Витрата бінарної суміші, м.куб/год.");

            var chartObjectTb1 = charts.Add(300, 450, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartTb1 = chartObjectTb1.Chart;
            var rangeTb1 = nws.get_Range("D2", "D" + (N).ToString());
            chartTb1.SetSourceData(rangeTb1);
            chartTb1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartTb1.ChartWizard(Source: rangeTb1,
               Title: "Температура бінарної суміші 1",
               CategoryTitle: "Час, сек",
               ValueTitle: "Температура бінарної суміші, K");

            var chartObjectTb2 = charts.Add(620, 30, 500, 400) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartTb2 = chartObjectTb2.Chart;
            var rangeTb2 = nws.get_Range("E2", "E" + (N).ToString());
            chartTb2.SetSourceData(rangeTb2);
            chartTb2.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartTb2.ChartWizard(Source: rangeTb2,
               Title: "Температура бінарної суміші 2",
               CategoryTitle: "Час, сек",
               ValueTitle: "Температура бінарної суміші, K");

            nws.SaveAs(progPath + "\\DeflegmatorData.xlsx");
            nwb.Close(false);
            XL.Quit();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Дані записано у файл " + progPath + "\\DeflegmatorData.xlsx", "Експорт даних до Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Write to Excel
        }

        private void excelBtnColumn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //Write to Excel
            Cursor.Current = Cursors.WaitCursor;
            var XL = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook nwb = null;
            Microsoft.Office.Interop.Excel._Worksheet nws = null;
            XL.Visible = false;
            nwb = XL.Workbooks.Add();
            nws = (Microsoft.Office.Interop.Excel._Worksheet)nwb.ActiveSheet;
            //Get a new workbook.
            //double[] noEmptyData = new double[N];
            nws.Cells[1, 1] = "Час";
            nws.Cells[1, 2] = "Fф, м^3/год";
            nws.Cells[1, 3] = "Fд, м^3/год";
            nws.Cells[1, 4] = "Tд, K";
            nws.Cells[1, 5] = "Tкуб, K";
            for (int i = 0; i < N; i++)
            {
                nws.Cells[i + 2, 1] = currTime[i];
                nws.Cells[i + 2, 2] = FfData[i];
                nws.Cells[i + 2, 3] = FdData[i];
                nws.Cells[i + 2, 4] = TdData[i];
                nws.Cells[i + 2, 5] = TcubData[i];
            }

            //Add Chart
            var charts = nws.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(300, 30, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartFf = chartObject.Chart;
            var rangeFf = nws.get_Range("B2", "B" + (N).ToString());
            chartFf.SetSourceData(rangeFf);
            chartFf.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartFf.ChartWizard(Source: rangeFf,
               Title: "Витрата флегми",
               CategoryTitle: "Час, сек",
               ValueTitle: "Витрата флегми, м.куб/год.");

            var chartObjectFd = charts.Add(300, 240, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartFd = chartObjectFd.Chart;
            var rangeFd = nws.get_Range("C2", "C" + (N).ToString());
            chartFd.SetSourceData(rangeFd);
            chartFd.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartFd.ChartWizard(Source: rangeFd,
               Title: "Витрата дистиляту",
               CategoryTitle: "Час, сек",
               ValueTitle: "Витрата дистиляту, м.куб/год.");

            var chartObjectTd = charts.Add(300, 450, 300, 200) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartTd = chartObjectTd.Chart;
            var rangeTd = nws.get_Range("D2", "D" + (N).ToString());
            chartTd.SetSourceData(rangeTd);
            chartTd.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartTd.ChartWizard(Source: rangeTd,
               Title: "Температура дистиляту",
               CategoryTitle: "Час, сек",
               ValueTitle: "Температура дистиляту, K");

            var chartObjectTcub = charts.Add(620, 30, 500, 400) as Microsoft.Office.Interop.Excel.ChartObject;
            var chartTcub = chartObjectTcub.Chart;
            var rangeTcub = nws.get_Range("E2", "E" + (N).ToString());
            chartTcub.SetSourceData(rangeTcub);
            chartTcub.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
            chartTcub.ChartWizard(Source: rangeTcub,
               Title: "Температура кубового залишку",
               CategoryTitle: "Час, сек",
               ValueTitle: "Температура кубового залишку, K");

            nws.SaveAs(progPath + "\\ColumnData.xlsx");
            nwb.Close(false);
            XL.Quit();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Дані записано у файл " + progPath + "\\ColumnData.xlsx", "Експорт даних до Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Write to Excel
        }

        private void pictureBoxBoiler_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxBoiler.Image = Properties.Resources.boilerInner;
            
            

        }

        private void pictureBoxBoiler_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxBoiler.Image = null;
        }

        private void pictureBoxDefleg_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxDefleg.Image = Properties.Resources.deflegInner;
        }

        private void pictureBoxDefleg_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxDefleg.Image = null;
        }

        private void pictureBoxColumn_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxColumn.Image = Properties.Resources.columnInner;
        }

        private void pictureBoxColumn_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxColumn.Image = null;
        }


        double Kreg = 0.43;
        double Ti = 1.6;
        double Tr2zavd = 383;
        double Tr2prev = 383;
        double delta = 0;
        double Wzam(double s)
        {
            double Wob = 3.024 / (0.413 * s * s + 1.153 * s + 1);
            double WregPI = (Kreg * s + Ti) / s;
            double Wroz = Wob * WregPI;

            return delta*(Wroz / (1 + Wroz)) / s;
        }
        double Wker(double s)
        {
            double Wob = 3.024 / (0.413 * s * s + 1.153 * s + 1);
            double WregPI = (Kreg * s + Ti) / s;
            double Wroz = Wob * WregPI;

            return delta*(WregPI / (1 + Wroz)) / s;
        }
        int timer2Time = 0;
        double responseTime = 0;
        double Ht = 0;
        double Htker = 0;
        double[] controlArray = new double[100];
        bool isDetecting = false;
        bool isDetectingDone = false;
        int controlArrayCounter = 0;
        private void регулюванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            showControl();
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "N0";
            chart2.ChartAreas[0].AxisY.Minimum = Double.NaN;
            chart2.ChartAreas[0].AxisY.Maximum = Double.NaN;

            chart3.ChartAreas[0].AxisX.Interval = 1;
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "N0";

            chart4.ChartAreas[0].AxisX.Interval = 1;
            chart4.ChartAreas[0].AxisX.LabelStyle.Format = "N0";

            //мінімільне та максимальне значення вихідного сигналу регулятра для бойлера
            int boilerMinRegOutput = (int)(process.Trecur21 * 0.85);
            int boilerMaxRegOutput = (int)(process.Trecur21 * 1.15);

            trackBar1.Minimum = boilerMinRegOutput;
            trackBar1.Maximum = boilerMaxRegOutput;

            trackBar1.Value = (int)process.Trecur21;
            textBoxKreg.Text = Kreg.ToString();
            textBoxTi.Text = Ti.ToString();
            label2.Text = Tr2zavd.ToString();

            double boilerRegOut = (trackBar1.Value - boilerMinRegOutput) / (double)(boilerMaxRegOutput - boilerMinRegOutput) * 16.0 + 4.0;
            label6.Text = boilerRegOut.ToString("N1");

            label8.Text = "Tr2 = " + Tr2zavd.ToString("N2") + " K";
            label9.Text = "Fp = " + process.Fpara1.ToString("N2") + " м.куб/год";
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            double interval = timer2Time * timer2.Interval/1000.0;
            if (interval <= 10)
            {
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Maximum = 10;
                chart3.ChartAreas[0].AxisX.Minimum = 0;
                chart3.ChartAreas[0].AxisX.Maximum = 10;
                chart4.ChartAreas[0].AxisX.Minimum = 0;
                chart4.ChartAreas[0].AxisX.Maximum = 10;
            }
            else
            {
                chart2.ChartAreas[0].AxisX.Minimum = timer2Time / 10.0-10;
                chart2.ChartAreas[0].AxisX.Maximum = timer2Time / 10.0;
                chart3.ChartAreas[0].AxisX.Minimum = timer2Time / 10.0 - 10;
                chart3.ChartAreas[0].AxisX.Maximum = timer2Time / 10.0;
                chart4.ChartAreas[0].AxisX.Minimum = timer2Time / 10.0 - 10;
                chart4.ChartAreas[0].AxisX.Maximum = timer2Time / 10.0;
            }
            double invCalc = 0;
            double invCalcker = 0;
            invCalc = Laplace.InverseTransform(Wzam, responseTime + 0.0001);
            invCalcker = Laplace.InverseTransform(Wker, responseTime + 0.0001);
            Ht = invCalc + Tr2zavd;
            Htker = invCalcker;

            double boilerMikOut = Htker * 8 / 110 + 12;

            chart2.Series[0].Points.AddXY(interval - 0.1, Ht);
            chart3.Series[0].Points.AddXY(interval - 0.1, boilerMikOut);
            chart4.Series[0].Points.AddXY(interval - 0.1, Htker+141);
            label3.Text = Ht.ToString("N1");

            label6.Text = boilerMikOut.ToString("N1");

            double FparaKer = process.Fpara1 + Htker;
            
            if (interval % 1 == 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                dataGridView1.Rows.Add((timer2Time/10.0).ToString("N0"), Ht.ToString("N2"), (Htker+141).ToString("N2"), boilerMikOut.ToString("N2"));
                label8.Text = "Tr2 = " + Ht.ToString("N2") + " K";
                label9.Text = "Fp = " + FparaKer.ToString("N2") + " м.куб/год";
            }

            if (isDetecting && controlArrayCounter<100) {
                controlArray[controlArrayCounter] = Ht;
                controlArrayCounter++;
            } else if(controlArrayCounter>=100 && !isDetectingDone)
            {
                double sigma = Math.Abs(controlArray.Max() - trackBar1.Value) / trackBar1.Value;
                double integral = 0;
                for (int i = 0; i < controlArrayCounter; i++)
                {
                    integral += Math.Pow(trackBar1.Value - controlArray[i], 2);
                }
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
                dataGridView2.Rows.Add("Kreg="+Kreg.ToString("N2")+"; Ti="+Ti.ToString("N2"), (sigma*100).ToString("N2") + "%", integral.ToString("N2"));
                isDetectingDone = true;
            }

            timer2Time++;
            
            responseTime += 0.1;
        }
        
        private void дефлегматорToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showStatic();
            process.AddTabsForTabControl(tabControl1, tabsForDeflegmator);
            process.DrawStaticFunction(chart1, process.Tvoda1, staticT2otTvod, "Tvoda, K", "Tbinar2, K");
            process.printResult(labelX, "Tv, K", process.Tvoda1);
            process.printResult(labelY, "Tb2, K", staticT2otTvod);
            process.printResult(labelK, "", K4);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            delta = trackBar1.Value - Tr2prev;
            responseTime = 0.000001;
            Tr2prev = trackBar1.Value;
            Tr2zavd = Ht;
            isDetecting = true;
            controlArrayCounter = 0;
            isDetectingDone = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
            
        }

        private void textBoxKreg_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(textBoxKreg);
            if (textBoxKreg.Text.Length != 0)
            {
                Kreg = Convert.ToDouble(textBoxKreg.Text);
            }
        }

        private void textBoxTi_TextChanged(object sender, EventArgs e)
        {
            preventTextInput(textBoxTi);
            if (textBoxTi.Text.Length != 0)
            {
                Ti = Convert.ToDouble(textBoxTi.Text);
            }
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            isDetecting = false;
        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void кипятильникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showStatic();
            process.AddTabsForTabControl(tabControl1, tabsForBoiler);
            process.DrawStaticFunction(chart1, process.Fpara1, staticTp2otFpara, "Fpara, m^3/год", "Trecur2, K");
            process.printResult(labelX, "Fp, m^3/год", process.Fpara1);
            process.printResult(labelY, "Tr2, K", staticTp2otFpara);
            process.printResult(labelK, "", K7);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void ректифікаційнаКолонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showStatic();
            process.AddTabsForTabControl(tabControl1, tabsForRectifCol);
            process.DrawStaticFunction(chart1, process.Tbinar11, staticTp1opTb, "Tbinar, K", "Trecur1, K");
            process.printResult(labelX, "Tb, K", process.Tbinar11);
            process.printResult(labelY, "Tr1, K", staticTp1opTb);
            process.printResult(labelK, "", K13);
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void дефлегматорToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showDynamic();
            process.AddTabsForTabControl(tabControl1, tabsForDynamicDeflegmator);
            double W3(double s)
            {
                return -0.013 / (7.077 * s * s * s + 6.716 * s * s + 1 * s);
            }
            Laplace.drawStepResponse(W3, chart1, dynDataGridView);
            chart1.ChartAreas[0].RecalculateAxesScale();
            timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void кипятильникToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showDynamic();
            process.AddTabsForTabControl(tabControl1, tabsForDynamicBoiler);
            double W6(double s)
            {
                return 3.024 / (0.413 * s * s * s + 1.153 * s * s + 1 * s);
            }
            Laplace.drawStepResponse(W6, chart1, dynDataGridView);
            chart1.ChartAreas[0].RecalculateAxesScale();
            timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void ректифікаційнаКолонаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showDynamic();
            process.AddTabsForTabControl(tabControl1, tabsForRectifColumn);
            double W7(double s)
            {
                return 0.634 / (24.27 * s * s * s + 21.33 * s * s + 1 * s);
            }
            Laplace.drawStepResponse(W7, chart1, dynDataGridView);
            chart1.ChartAreas[0].RecalculateAxesScale();
            timeEdit.Text = chart1.ChartAreas[0].AxisX.Maximum.ToString("F0");
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(chart1);
        }
        private void процесToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showProcess();
        }
        private void showControl()
        {
            controllerPanel.Visible = true;
            timeEdit.Visible = false;
            dynDataGridView.Visible = false;
            label1.Visible = false;
            tabControl1.Visible = false;
            labelK.Visible = false;
            labelX.Visible = false;
            labelY.Visible = false;
            processPanel.Visible = false;
            ImitationTabs.Visible = false;
            timer1.Enabled = false;
        }
        private void showImitation()
        {
            timer2.Enabled = false;
            controllerPanel.Visible = false;
            timeEdit.Visible = false;
            dynDataGridView.Visible = false;
            label1.Visible = false;
            tabControl1.Visible = false;
            labelK.Visible = false;
            labelX.Visible = false;
            labelY.Visible = false;
            processPanel.Visible = false;
            ImitationTabs.Visible = true;
            timer1.Enabled = true;
        }
        private void showStatic()
        {
            timer2.Enabled = false;
            controllerPanel.Visible = false;
            staticFuncs = true;
            dynamicFuncs = false;
            timeEdit.Visible = false;
            dynDataGridView.Visible = false;
            label1.Visible = false;
            tabControl1.Visible = true;
            labelK.Visible = true;
            labelX.Visible = true;
            labelY.Visible = true;
            labelX.TextAlign = ContentAlignment.TopCenter;
            labelY.TextAlign = ContentAlignment.TopCenter;
            processPanel.Visible = false;
            ImitationTabs.Visible = false;
        }
        private void showDynamic()
        {
            timer2.Enabled = false;
            controllerPanel.Visible = false;
            staticFuncs = false;
            dynamicFuncs = true;
            timeEdit.Visible = true;
            setupDataGridView();
            dynDataGridView.Visible = true;
            label1.Visible = true;
            tabControl1.Visible = true;
            labelK.Visible = false;
            labelX.Visible = false;
            labelY.Visible = false;
            processPanel.Visible = false;
            ImitationTabs.Visible = false;
        }
        private void showProcess()
        {
            timer2.Enabled = false;
            controllerPanel.Visible = false;
            dynDataGridView.Visible = false;
            label1.Visible = false;
            timeEdit.Visible = false;
            tabControl1.Visible = false;
            labelK.Visible = false;
            labelX.Visible = false;
            labelY.Visible = false;
            processPanel.Visible = true;
            ImitationTabs.Visible = false;
        }
        private void setupDataGridView()
        {
            dynDataGridView.ColumnCount = 2;
            dynDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dynDataGridView.Columns[1].Name = "h(t)";
            dynDataGridView.Columns[0].Name = "t, сек.";

        }
    }
}
