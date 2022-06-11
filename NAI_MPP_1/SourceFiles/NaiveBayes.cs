using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI_MPP_1.SourceFiles
{
    class NaiveBayes : Base
    {
        public List<List<string>> data { get; set; }
        public List<List<string>> testData { get; set; }
        public List<int> uniqueVals { get; set; }
        public List<string> answers { get; set; }
        public List<float> result { get; set; }
        public NaiveBayes(string learnDatapath, string testDataPath)
        {
            data = ReadDataNaiveBayes(learnDatapath);
            testData = ReadDataNaiveBayes(testDataPath);

            uniqueVals = new List<int>();
            answers = new List<string>();
            result = new List<float>();

            for (int i = 1; i < data.First().Count(); i++)
            {
                uniqueVals.Add(data.Select(x => x.ElementAt(i)).ToList().Distinct().Count());
            }
        }
        public void Run()
        {
            float totalAmount = data.Count();
            float pAmount = data.Where(x => x.ElementAt(0).Equals("p")).Count();
            float eAmount = data.Where(x => x.ElementAt(0).Equals("e")).Count();
            var pData = data.Where(x => x.ElementAt(0).Equals("p")).ToList();
            var eData = data.Where(x => x.ElementAt(0).Equals("e")).ToList();

            foreach (var item in testData)
            {
                var a = Calc(item, totalAmount, pAmount, eAmount, pData, eData);
                answers.Add(a);
                //Console.WriteLine(a);
            }
            List<string> list = testData.Select(x => x.ElementAt(0)).ToList();
            result = Vals(list, answers);
        }
        private string Calc(List<string> item, float totalAmount, float pAmount, float eAmount,
            List<List<string>> pData, List<List<string>> eData)
        {
            float pCalc = pAmount / totalAmount;
            float eCalc = eAmount / totalAmount;
            for (int i = 1; i < item.Count; i++)
            {
                //Console.WriteLine(pData.Where(x => x.Contains(item.ElementAt(i))).Count());
                if (pData.Where(x => x.Contains(item.ElementAt(i))).Count() == 0)
                {
                    pCalc *= 1 / pAmount + uniqueVals.ElementAt(i);
                }
                else
                {
                    pCalc *= pData.Where(x => x.Contains(item.ElementAt(i))).Count() / pAmount;
                }
                if (eData.Where(x => x.Contains(item.ElementAt(i))).Count() == 0)
                {
                    eCalc *= 1 / eAmount + uniqueVals.ElementAt(i);
                }
                else
                {
                    eCalc *= eData.Where(x => x.Contains(item.ElementAt(i))).Count() / eAmount;
                }
            }
            return pCalc < eCalc ? "e" : "p";
        }
        private List<float> Vals(List<string> list, List<string> answers)
        {
            /*for (int i = 0; i < answers.Count; i++)
            {
                Console.Write(answers.ElementAt(i) + "\t");
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list.ElementAt(i) + "\t");
            }*/
            var res = new List<float>();
            var TP = list.Intersect(answers).Count(x => x.Equals("p"));
            var TN = list.Intersect(answers).Count(x => x.Equals("e"));
            var FP = answers.Except(list).Count(x => x.Equals("p"));
            var FN = answers.Except(list).Count(x => x.Equals("e"));

            // Accuracy
            res.Add(TP + TN / list.Count);
            // Precision
            float P = TP / TP + FP;
            res.Add(P);
            // Recall
            float R = TP / TP + FN;
            res.Add(R);
            // F-Value
            float f = 2 * P * R / (P + R);
            res.Add(f);
            return res;
        }
    }
}
