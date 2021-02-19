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

            //每一项的拉格朗日乘子
            double[] a = new double[X.Length];//拉格朗日乘子
            this.a = a;

            //将乘子初始化为0
            Array.ForEach(a, (t) => t = 0);

            int passes = 0;

            while (passes < maxPasses)
            {
                int numChangedAlphas = 0; //表示改变乘子的次数
                for (int i = 0; i < X.Length; i++)
                {
                    //表示特定阶段由a和b所决定的输出与真实yi的误差
                    //参照公式(7)
                    double Ei = GetError(i);

                    /*
                     把违背KKT条件的ai作为第一个

                     满足KKT条件的情况是：
                     yi*f(i) >= 1 and alpha == 0 (正确分类)
                     yi*f(i) == 1 and 0 < alpha < C (在边界上的支持向量)
                     yi*f(i) <= 1 and alpha == C (在边界之间)
                     
                     令 ri = y[i] * Ei = y[i] * f(i) - y[i]^2 >= 0
                     如果ri < 0并且alpha < C 则违反了KKT条件
                     因为原本ri < 0 应该对应的是alpha = C
                     同理，ri > 0并且alpha > 0则违反了KKT条件
                     因为原本ri > 0对应的应该是alpha =0
                     */

                    //如果当前的ai 违反kkt条件
                    if ((Y[i] * Ei < -tol && a[i] < C) ||
                        (Y[i] * Ei > tol && a[i] > 0))
                    {

                        //在f(i)*yi=1边界上的点 0 < a[i] < C
                        //找MAX|E1 - E2|                         
                        int j;
                        if (this.BoundAlphaMap.Count > 0)
                        {
                            j = FindMax(Ei, this.BoundAlphaMap);
                        }
                        else
                        {
                            j = RandomSelect(i);//如果边界上没有，就随便选一个j != i的aj
                        }
                        double Ej = GetError(j);

                        //保存当前的ai和aj
                        double oldAi = a[i];
                        double oldAj = a[j];

                        //计算乘子的范围L, H
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
                        if (0 < a[j] && a[j] < C) BoundAlphaMap.Add(j);

                        if (a[j] < L)
                        {
                            a[j] = L;
                        }
                        else if (a[j] > H)
                        {
                            a[j] = H;
                        }

                        //变化太小就不动
                        if (Math.Abs(a[j] - oldAj) < 1e-5) continue;
                        a[i] = a[i] + Y[i] * Y[j] * (oldAj - a[j]);
                        if (0 < a[i] && a[i] < C) BoundAlphaMap.Add(i);

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

                        numChangedAlphas += 1;
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
        private int FindMax(double Ei, HashSet<int> boundAlpha2)
        {
            double max = 0;
            int maxIndex = -1;
            foreach (var item in boundAlpha2)
            {
                double Ej = GetError(item);
                if (Math.Abs(Ei - Ej) > max)
                {
                    max = Math.Abs(Ei - Ej);
                    maxIndex = item;
                }
            }
            return maxIndex;
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


        //simple kernel(i, j) = (x_i)T(x_j)
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

        /// <summary>
        /// 分类函数
        /// \sum{a_i Y_i x_i^T x_j}+b
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        double F(int j)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += a[i] * Y[i] * kernel[i][j];
            }

            return sum + b;
        }

        /// <summary>
        /// 误差
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private double GetError(int i)
        {
            return F(i) - Y[i];
        }
    }

}
