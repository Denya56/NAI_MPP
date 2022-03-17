using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NAI_MPP_1
{
    public class Data
    {
        public List<decimal[]> vectorList { get; set; }
        public List<string> answerList { get; set; }
        public Data()
        {
            vectorList = null;
            answerList = null;
        }
        public void ReadData(string testData)
        {
            FileStream fs = new FileStream(testData, FileMode.Open, FileAccess.Read);

            int vectorSize;
            List<decimal[]> _vectorList = new List<decimal[]>();
            List<string> _answersList = new List<string>();

            using (StreamReader sr = new StreamReader(fs))
            {
                decimal d;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(",");

                    if (s.Take(s.Length - 1).ToArray().All(n => Decimal.TryParse(n, out d)))
                    {
                        _vectorList.Add(Array.ConvertAll<string, decimal>(s.Take(s.Length - 1).ToArray(), Convert.ToDecimal));
                    }
                    _answersList.Add(s[s.Length - 1]);
                }
            }
            vectorList = _vectorList;
            answerList = _answersList;
        }
    }
}
