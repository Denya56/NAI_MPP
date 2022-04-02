using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;

namespace NAI_MPP_1.SourceFiles
{
    class Base
    {
        protected Dictionary<double[], string> ReadData(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var data = new Dictionary<double[], string>();

            using (StreamReader sr = new StreamReader(fs))
            {
                double d;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(",");

                    if (s.Take(s.Length - 1).All(n => Double.TryParse(n, out d)))
                    {
                        data.Add(Array.ConvertAll<string, double>(s.Take(s.Length - 1).ToArray(), Convert.ToDouble), s[s.Length - 1]);
                    }
                }
            }
            return data;
        }
    }
}
