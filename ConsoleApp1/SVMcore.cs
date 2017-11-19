using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SVMcore
    {
        public SVMcore()
        {
            //training set
            x = new double[80][];
            x[0] = new double[2] { 3.542485, 1.977398 };
            x[1] = new double[2] { 3.018896, 2.556416 };
            x[2] = new double[2] { 7.55151, -1.58003 };
            x[3] = new double[2] { 2.114999, -0.00447 };
            x[4] = new double[2] { 8.127113, 1.274372 };
            x[5] = new double[2] { 7.108772, -0.98691 };
            x[6] = new double[2] { 8.610639, 2.046708 };
            x[7] = new double[2] { 2.326297, 0.265213 };
            x[8] = new double[2] { 3.634009, 1.730537 };
            x[9] = new double[2] { 0.341367, -0.895 };

            x[10] = new double[2] { 3.125951, 0.293251 };
            x[11] = new double[2] { 2.123252, -0.78356 };
            x[12] = new double[2] { 0.887835, -2.79779 };
            x[13] = new double[2] { 7.139979, -2.3299 };
            x[14] = new double[2] { 1.696414, -1.2125 };
            x[15] = new double[2] { 8.117032, 0.623493 };
            x[16] = new double[2] { 8.497162, -0.26665 };
            x[17] = new double[2] { 4.658191, 3.507396 };
            x[18] = new double[2] { 8.197181, 1.545132 };
            x[19] = new double[2] { 1.208047, 0.2131 };

            x[20] = new double[2] { 1.928486, -0.32187 };
            x[21] = new double[2] { 2.175808, -0.01453 };
            x[22] = new double[2] { 7.886608, 0.461755 };
            x[23] = new double[2] { 3.223038, -0.55239 };
            x[24] = new double[2] { 3.628502, 2.19585 };
            x[25] = new double[2] { 7.40786, -0.12196 };
            x[26] = new double[2] { 7.286357, 0.251077 };
            x[27] = new double[2] { 2.301095, -0.53399 };
            x[28] = new double[2] { -0.23254, -0.54769 };
            x[29] = new double[2] { 3.457096, -0.08222 };

            x[30] = new double[2] { 3.023938, -0.057739 };
            x[31] = new double[2] { 8.015003, 0.885325 };
            x[32] = new double[2] { 8.991748, 0.923154 };
            x[33] = new double[2] { 7.916831, -1.78174 };
            x[34] = new double[2] { 7.616862, -0.21796 };
            x[35] = new double[2] { 2.450939, 0.744769 };
            x[36] = new double[2] { 7.270337, -2.50783 };
            x[37] = new double[2] { 1.749721, -0.9619 };
            x[38] = new double[2] { 1.803111, -0.17635 };
            x[39] = new double[2] { 8.804461, 3.044301 };

            x[40] = new double[2] { 1.231257, -0.56857 };
            x[41] = new double[2] { 2.074915, 1.41055 };
            x[42] = new double[2] { -0.74304, -1.7361 };
            x[43] = new double[2] { 3.536555, 3.96496 };
            x[44] = new double[2] { 8.410143, 0.225606 };
            x[45] = new double[2] { 7.382988, -0.47876 };
            x[46] = new double[2] { 6.960661, -0.24535 };
            x[47] = new double[2] { 8.23446, 0.701868 };
            x[48] = new double[2] { 8.168618, -0.90384 };
            x[49] = new double[2] { 1.534187, -0.62249 };

            x[50] = new double[2] { 9.229518, 2.066088 };
            x[51] = new double[2] { 7.886242, 0.191813 };
            x[52] = new double[2] { 2.893743, -1.64347 };
            x[53] = new double[2] { 1.870457, -1.04042 };
            x[54] = new double[2] { 5.286862, -2.35829 };
            x[55] = new double[2] { 6.080573, 0.418886 };
            x[56] = new double[2] { 2.544314, 1.714165 };
            x[57] = new double[2] { 6.016004, -3.75371 };
            x[58] = new double[2] { 0.92631, -0.56436 };
            x[59] = new double[2] { 0.870296, -0.10995 };

            x[60] = new double[2] { 2.369345, 1.375695 };
            x[61] = new double[2] { 1.363782, -0.25408 };
            x[62] = new double[2] { 7.27946, -0.18957 };
            x[63] = new double[2] { 1.896005, 0.51508 };
            x[64] = new double[2] { 8.102154, -0.60388 };
            x[65] = new double[2] { 2.529893, 0.662657 };
            x[66] = new double[2] { 1.963874, -0.36523 };
            x[67] = new double[2] { 8.132048, 0.785914 };
            x[68] = new double[2] { 8.245938, 0.372366 };
            x[69] = new double[2] { 6.543888, 0.433164 };

            x[70] = new double[2] { -0.23671, -5.76672 };
            x[71] = new double[2] { 8.112593, 0.295839 };
            x[72] = new double[2] { 9.803425, 1.495167 };
            x[73] = new double[2] { 1.497407, -0.55282 };
            x[74] = new double[2] { 1.336267, -1.63289 };
            x[75] = new double[2] { 9.205805, -0.58648 };
            x[76] = new double[2] { 1.966279, -1.84044 };
            x[77] = new double[2] { 8.398012, 1.584918 };
            x[78] = new double[2] { 7.239953, -1.76429 };
            x[79] = new double[2] { 7.556201, 0.241185 };

            y = new int[80]
            {
                -1,-1, 1,-1,1, 1, 1,-1,-1,-1,

                -1,-1,-1,1,-1,1,1,-1,1,-1,

                -1,-1, 1,-1,-1,1,1,-1,-1,-1,

                -1, 1, 1, 1, 1,-1, 1,-1,-1,1,

                -1,-1,-1,-1, 1, 1, 1, 1, 1, -1,

                1, 1,-1,-1, 1, 1,-1, 1,-1,-1,

                -1,-1, 1,-1, 1,-1,-1, 1, 1, 1,

                -1, 1, 1,-1,-1, 1,-1, 1, 1, 1
            };
        }

        private HashSet<int> boundAlpha = new HashSet<int>();//boundAlpha表示x点处于边界上所对应的拉格朗日乘子a的集合
        private Random random = new Random();

        public double[][] x;//trainingSet
        private double[][] kernel; // 核

        public double[] a;//拉格朗日乘子
        public int[] y;
        double b = 0.0;

        public void train()
        {
            kernel = new double[80][];
            for (int i = 0; i < kernel.Length; i++)
            {
                kernel[i] = new double[80];
            }
            initiateKernel(x.Length);

            double C = 1; //对不在界内的惩罚因子
            double tol = 0.0125;//容忍极限值
            int maxPasses = 5; //没有改变拉格朗日乘子的最多迭代次数

            double[] a = new double[x.Length];//拉格朗日乘子
            this.a = a;

            //将乘子初始化为0
            for (int i = 0; i < x.Length; i++)
            {
                a[i] = 0;
            }
            int passes = 0;

            while (passes < maxPasses)
            {
                int numChangedAlphas = 0; //表示改变乘子的次数
                for (int i = 0; i < x.Length; i++)
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
                    if ((y[i] * Ei < -tol && a[i] < C) ||
                        (y[i] * Ei > tol && a[i] > 0))
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
                        if (y[i] != y[j])
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

                        a[j] = a[j] - y[j] * (Ei - Ej) / eta;
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
                        a[i] = a[i] + y[i] * y[j] * (oldAj - a[j]);
                        if (0 < a[i] && a[i] < C) boundAlpha.Add(i);

                        //计算b1， b2
                        double b1 = b - Ei - y[i] * (a[i] - oldAi) * k(i, i) - y[j] * (a[j] - oldAj) * k(i, j);
                        double b2 = b - Ej - y[i] * (a[i] - oldAi) * k(i, j) - y[j] * (a[j] - oldAj) * k(j, j);

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

        public double predict(double[][] x2, int[] y2)
        {
            double probability = 0;
            int correctCnt = 0;
            int total = y2.Length;

            for (int i = 0; i < total; i++)
            {
                //原来训练矩阵的维数（长度）
                int len = y.Length;
                double sum = 0;
                for (int j = 0; j < len; j++)
                {
                    sum += y[j] * a[j] * k2(i, j, x2);
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

        public void userPredict(double x1, double x2)
        {
            int len = y.Length;
            double sum = 0;
            double[][] vec = new double[1][];
            vec[0] = new double[2] { x1, x2 };
            for (int j = 0; j < len; j++)
            {
                sum += y[j] * a[j] * k2(0, j, vec);
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

        private void initiateKernel(int length)
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
            for (int t = 0; t < x[i].Length; t++)
            {
                sum += x[i][t] * x[j][t];
            }
            return sum;
        }
        //计算预测数据的内积
        private double k2(int i, int j, double[][] x2)
        {
            double sum = 0.0;
            for (int t = 0; t < 2; t++)
            {
                sum += x2[i][t] * x[j][t];
            }
            return sum;
        }

        //随机选一个非i的整数
        private int RandomSelect(int i)
        {
            int j;
            do
            {
                j = random.Next(x.Length);
            } while (i == j);
            return j;
        }

        private double f(int j)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += a[i] * y[i] * kernel[i][j];
            }

            return sum + b;
        }

        private double getE(int i)
        {
            return f(i) - y[i];
        }

    }
}
