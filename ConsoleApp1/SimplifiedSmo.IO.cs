using System;
using System.Collections.Generic;
using System.IO;


namespace ConsoleApp1
{
    public partial class SimplifiedSmo
    {

        public SimplifiedSmo InitTrainSet(string path)
        {
            LoadDataSet(path, out this.X, out this.Y);
            return this;
        }

        public SimplifiedSmo InitTestSet(string path, out double[][] x, out int[] y)
        {
            LoadDataSet(path, out x, out y);
            return this;
        }

        void LoadDataSet(string path, out double[][] x, out int[] y)
        {
            StreamReader sw = new StreamReader(new FileStream(path, FileMode.Open));

            string s = sw.ReadLine();
            int count = Convert.ToInt32(s);
            x = new double[count][];
            y = new int[count];

            for (int i = 0; i < count; i++)
            {
                var tmp = sw.ReadLine().Split('\t', ' ',',');
                x[i] = new double[2];
                x[i][0] = Convert.ToDouble(tmp[0]);
                x[i][1] = Convert.ToDouble(tmp[1]);
                y[i] = Convert.ToInt32(tmp[2]);
            }
            sw.Close();
        }
    }
}
