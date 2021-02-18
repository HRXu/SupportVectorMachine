using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SimplifiedSmo s = new SimplifiedSmo();//构造函数里内嵌训练数据
            s.InitTrainSet().train();
            //testSet
            double[][] x2 = new double[20][];
            x2[0] = new double[2] {9.015509,0.345019 };
            x2[1] = new double[2] {8.266085,-0.23098 };
            x2[2] = new double[2] { 8.54562,2.788799};
            x2[3] = new double[2] { 9.295969,1.346332};
            x2[4] = new double[2] { 2.404234,0.57278};
            x2[5] = new double[2] { 2.037772,0.021919};
            x2[6] = new double[2] { 1.727631,-0.45314};
            x2[7] = new double[2] { 1.979395,-0.05077};
            x2[8] = new double[2] { 8.092288,-1.37243};
            x2[9] = new double[2] { 1.667645,0.239204};

            x2[10] = new double[2] { 9.854303,1.365116};
            x2[11] = new double[2] { 7.921057,-1.32759};
            x2[12] = new double[2] { 8.500757,1.492372};
            x2[13] = new double[2] { 1.339746,-0.29118};
            x2[14] = new double[2] { 3.107511,0.758367};
            x2[15] = new double[2] { 2.609525,0.902979};
            x2[16] = new double[2] { 3.263585,1.367898};
            x2[17] = new double[2] { 2.912122,-0.20236};
            x2[18] = new double[2] { 1.731786,0.589096};
            x2[19] = new double[2] { 2.387003,1.573131};

            int[] y2 = new int[20]
            {
                1,1,1,1,-1,-1,-1,-1,1,-1,
                1,1,1,-1,-1,-1,-1,-1,-1,-1
            };        
            Console.WriteLine("训练完成\n预测集数据正确率：{0}", s.Predict(x2, y2));
            while (true)
            {
                Console.WriteLine("输入你要预测的数据：（输一个然后回车）");
                try
                {
                    var first=Convert.ToDouble(Console.ReadLine());
                    var sec =Convert.ToDouble(Console.ReadLine());
                    s.Predict(first, sec);
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("格式错误！");
                    continue;
                }
            }
        }
    }
}
