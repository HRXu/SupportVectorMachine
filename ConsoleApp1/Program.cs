using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SimplifiedSmo s = new SimplifiedSmo();
            //trainingSet;
            s.InitTrainSet("train.txt").Train();
            //testingSet
            s.InitTestSet("evaluate.txt", out double[][] x2, out int[] y2);

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
