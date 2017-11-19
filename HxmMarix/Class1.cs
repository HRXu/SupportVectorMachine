using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxmMarix
{
    public class Matrix
    {

        /// <summary>
        /// ATA
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double[,] AtA(double[] a)
        {
            int len = a.Length;
            double[,] res = new double[len, len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    res[i, j] = a[i] * a[j];
                }
            }
            return res;
        }

        /// <summary>
        /// 向量内积
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double? innerProductV(double[] a, double[] b)
        {
            //不等长
            if (a.Length != b.Length) return null;
            double res = 0;
            for (int i = 0; i < a.Length; i++)
            {
                res += a[i] * b[i];
            }
            return res;
        }

        /// <summary>
        /// 矩阵乘法
        /// 竖为第0维，横为第二维
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[,] mx(double[,] a, double[,] b)
        {

            //不符合乘法条件
            if (a.GetLength(1) != b.GetLength(0)) return null;

            int ColA = a.GetLength(1);
            int RowA = a.GetLength(0);
            int ColB = b.GetLength(1);

            double[,] res = new double[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < ColB; i++)
            {
                for (int j = 0; j < RowA; j++)
                {
                    for (int k = 0; k < ColA; k++)
                    {
                        res[j, i] += a[j, k] * b[k, i];
                    }
                }
            }
            return res;
        }
        public static double[,] mx(double[,] a, double[] b)
        {
            //不符合乘法条件
            if (a.GetLength(1) != b.Length) return null;

            int ColA = a.GetLength(1);
            int RowA = a.GetLength(0);

            double[,] res = new double[a.GetLength(0), 1];

            for (int i = 0; i < RowA; i++)
            {
                for (int j = 0; j < ColA; j++)
                {
                    res[i, 0] = a[i, j] * b[j];
                }
            }
            return res;
        }

    }
}
