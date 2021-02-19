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


        private HashSet<int> BoundAlphaMap = new HashSet<int>();//boundAlpha表示x点处于边界上所对应的拉格朗日乘子a的集合
        private Random random = new Random();

        public double[][] X;

        double[][] kernel; // 内积的矩阵

        double[] a;//拉格朗日乘子
        public int[] Y;
        double b = 0.0;


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

    }

}
