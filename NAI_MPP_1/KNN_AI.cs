using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NAI_MPP_1
{
    class KNN_AI
    {

        public Dictionary<double[], string> learnData { get; set; }
        public Dictionary<double[], string> testData { get; set; }

        public List<string> testAnswers { get; set; }
        public List<string> testResults { get; set; }
        public int k { get; set; }

        public KNN_AI(int kIndex, string learnFilePath, string testFilePath) 
        {
            k = kIndex;
            learnData = ReadData(learnFilePath);
            testData = ReadData(testFilePath);

            testAnswers = testData.Values.ToList();
            testResults = new List<string>();
        }

        private Dictionary<double[], string> ReadData(string filePath)
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

                    if (s.Take(s.Length - 1).ToArray().All(n => Double.TryParse(n, out d)))
                    {
                        data.Add(Array.ConvertAll<string, double>(s.Take(s.Length - 1).ToArray(), Convert.ToDouble), s[s.Length - 1]);
                    }
                }
            }

            return data;
        }

        public void Run() 
        {
            
            
            foreach (var vectorTest in testData)
            {

                Dictionary<double[], string> vectorDistanceAndAnswer = new Dictionary<double[], string>();
                foreach (var vectorLearn in learnData)
                {
                    vectorDistanceAndAnswer.Add(CalculateVectorsDistance(vectorTest.Key, vectorLearn.Key), vectorLearn.Value);
                }

                /*foreach (KeyValuePair<double[], string> kvp in vectorDistance)
                {
                    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key.ToArray()[0], kvp.Value);
                }*/


                List<string> sortedAnswers = vectorDistanceAndAnswer.OrderBy(a => a.Key[0]).Select(x => x.Value).Take(k).ToList();


                /*foreach (var s in vectorDistance.OrderBy(a => a.Key[0]))
                {
                    Console.WriteLine(ii++ + ". Key = {0}, Value = {1}", s.Key[0], s.Value);
                }*/

                //Console.WriteLine("\nTest\n");

                /*for (int i = 0; i < sortedAnswers.Count; i++)
                {
                    string ss = sortedAnswers[i];
                    Console.WriteLine(i + ". " + ss);
                }*/

                var most = sortedAnswers.GroupBy(i => i).Select(grp => grp.Key).First();

                //Console.WriteLine(most);



                testResults.Add(most);
            }
        }

        private double[] CalculateVectorsDistance(double[] vec_1, double[] vec_2)
        {
            int vectorLength = vec_1.Length;
            double[] result = new double[1];

            for (int i = 0; i < vectorLength; i++)
            {
                double pow = Math.Pow((vec_1[i] - vec_2[i]), 2);
                result[0] += pow;
            }

            return result;
        }
    }
}
