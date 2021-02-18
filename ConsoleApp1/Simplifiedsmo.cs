using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public partial class SimplifiedSmo
    {


        private HashSet<int> boundAlpha = new HashSet<int>();//boundAlpha表示x点处于边界上所对应的拉格朗日乘子a的集合
        private Random random = new Random();

        public double[][] X;
        public double[][] kernel; // 核

        public double[] a;//拉格朗日乘子
        public int[] Y;
        double b = 0.0;

        public void Train()
        {
            kernel = new double[X.Length][];
            for (int i = 0; i < kernel.Length; i++)
            {
                kernel[i] = new double[X.Length];
            }
            InitiateKernel(X.Length);

            double C = 1; //对不在界内的惩罚因子
            double tol = 0.0125;//容忍极限值
            int maxPasses = 6; //没有改变拉格朗日乘子的最多迭代次数

            double[] a = new double[X.Length];//拉格朗日乘子
            this.a = a;

            //将乘子初始化为0
            for (int i = 0; i < X.Length; i++)
            {
                a[i] = 0;
            }
            int passes = 0;

            while (passes < maxPasses)
            {
                int numChangedAlphas = 0; //表示改变乘子的次数
                for (int i = 0; i < X.Length; i++)
                {
                    //表示特定阶段由a和b所决定的输出与真实yi的误差
                    //参照公式(7)
                    double Ei = getE(i);
                    /*
                     把违背KKT条件的ai作为第一个
                    满足KKT条件的情况是：
                     yi*f(i) >= 1 and alpha == 0 (正确分类)
                     yi*f(i) == 1 and 0<alpha < C (在边界上的支持向量)
                     yi*f(i) <= 1 and alpha == C (在边界之间)
                     ri = y[i] * Ei = y[i] * f(i) - y[i]^2 >= 0
                     如果ri < 0并且alpha < C 则违反了KKT条件
                     因为原本ri < 0 应该对应的是alpha = C
                     同理，ri > 0并且alpha > 0则违反了KKT条件
                     因为原本ri > 0对应的应该是alpha =0
                     */
                    if ((Y[i] * Ei < -tol && a[i] < C) ||
                        (Y[i] * Ei > tol && a[i] > 0))
                    {

                        //ui*yi=1边界上的点 0 < a[i] < C
                        //找MAX|E1 - E2|                         
                        int j;
                        if (this.boundAlpha.Count > 0)
                        {
                            j = findMax(Ei, this.boundAlpha);
                        }
                        else
                        {
                            j = RandomSelect(i);//如果边界上没有，就随便选一个j != i的aj
                        }
                        double Ej = getE(j);

                        //保存当前的ai和aj
                        double oldAi = a[i];
                        double oldAj = a[j];

                        //计算乘子的范围U, V
                        double L, H;
                        if (Y[i] != Y[j])
                        {
                            L = Math.Max(0, a[j] - a[i]);
                            H = Math.Min(C, C - a[i] + a[j]);
                        }
                        else
                        {
                            L = Math.Max(0, a[i] + a[j] - C);
                            H = Math.Min(C, a[i] + a[j]);
                        }

                        //如果eta等于0或者大于0 则表明a最优值应该在L或者U上
                        double eta = 2 * k(i, j) - k(i, i) - k(j, j);

                        if (eta >= 0) continue;

                        a[j] = a[j] - Y[j] * (Ei - Ej) / eta;
                        if (0 < a[j] && a[j] < C) boundAlpha.Add(j);

                        if (a[j] < L)
                        {
                            a[j] = L;
                        }
                        else if (a[j] > H)
                        {
                            a[j] = H;
                        }

                        if (Math.Abs(a[j] - oldAj) < 1e-5) continue;
                        a[i] = a[i] + Y[i] * Y[j] * (oldAj - a[j]);
                        if (0 < a[i] && a[i] < C) boundAlpha.Add(i);

                        //计算b1， b2
                        double b1 = b - Ei - Y[i] * (a[i] - oldAi) * k(i, i) - Y[j] * (a[j] - oldAj) * k(i, j);
                        double b2 = b - Ej - Y[i] * (a[i] - oldAi) * k(i, j) - Y[j] * (a[j] - oldAj) * k(j, j);

                        if (0 < a[i] && a[i] < C)
                        {
                            b = b1;
                        }
                        else if (0 < a[j] && a[j] < C)
                        {
                            b = b2;
                        }
                        else
                        {
                            b = (b1 + b2) / 2;
                        }
                        numChangedAlphas = numChangedAlphas + 1;
                    }
                }
                if (numChangedAlphas == 0)
                {
                    passes++;
                }
                else
                    passes = 0;
            }
        }

        private int findMax(double Ei, HashSet<int> boundAlpha2)
        {
            double max = 0;
            int maxIndex = -1;
            foreach (var item in boundAlpha2)
            {
                double Ej = getE(item);
                if (Math.Abs(Ei - Ej) > max)
                {
                    max = Math.Abs(Ei - Ej);
                    maxIndex = item;
                }
            }
            return maxIndex;
        }

        public double Predict(double[][] x2, int[] y2)
        {
            double probability = 0;
            int correctCnt = 0;
            int total = y2.Length;

            for (int i = 0; i < total; i++)
            {
                //原来训练矩阵的维数（长度）
                int len = Y.Length;
                double sum = 0;
                for (int j = 0; j < len; j++)
                {
                    sum += Y[j] * a[j] * k2(i, j, x2);
                }
                sum += b;
                Console.Write("{0}", sum);
                if ((sum > 0 && y2[i] > 0) || (sum < 0 && y2[i] < 0))
                {
                    Console.Write(" 正确\n");
                    correctCnt++;
                }
                else
                {
                    Console.Write("\n");
                }
            }
            probability = (double)correctCnt / (double)total;
            return probability;
        }

        public void Predict(double x1, double x2)
        {
            int len = Y.Length;
            double sum = 0;
            double[][] vec = new double[1][];
            vec[0] = new double[2] { x1, x2 };
            for (int j = 0; j < len; j++)
            {
                sum += Y[j] * a[j] * k2(0, j, vec);
            }
            sum += b;
            if (sum > 0)
            {
                Console.WriteLine("1");
            }
            else
            {
                Console.WriteLine("-1");
            }

        }

        private void InitiateKernel(int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    kernel[i][j] = k(i, j);
                }
            }
        }

        //simple kernel(i, j) = xTx
        private double k(int i, int j)
        {
            double sum = 0.0;
            for (int t = 0; t < X[i].Length; t++)
            {
                sum += X[i][t] * X[j][t];
            }
            return sum;
        }
        //计算预测数据的内积
        private double k2(int i, int j, double[][] x2)
        {
            double sum = 0.0;
            for (int t = 0; t < 2; t++)
            {
                sum += x2[i][t] * X[j][t];
            }
            return sum;
        }

        //随机选一个非i的整数
        private int RandomSelect(int i)
        {
            int j;
            do
            {
                j = random.Next(X.Length);
            } while (i == j);
            return j;
        }


        private double f(int j)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += a[i] * Y[i] * kernel[i][j];
            }

            return sum + b;
        }

        private double getE(int i)
        {
            return f(i) - Y[i];
        }
    }

}
