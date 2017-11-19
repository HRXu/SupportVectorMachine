using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using matrixCalculator;

namespace matrixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] a = new double[3] { 1, 2, 3 };
            double[,]b =Matrix.AtA(a);
            var c = Matrix.mx(b, a);
        }

    }
}
