using System;
using System.Collections.Generic;
using System.Linq;

namespace NAI_MPP_1.SourceFiles
{
    class KNN_AI : Base
    {
        public Dictionary<double[], string> learnData { get; set; }
        public Dictionary<double[], string> testData { get; set; }
        public List<string> testAnswers { get; set; }
        public List<string> results { get; set; }
        public int k { get; set; }

        public KNN_AI(int kIndex, string learnFilePath, string testFilePath) 
        {
            k = kIndex;
            learnData = ReadData(learnFilePath);
            testData = ReadData(testFilePath);

            testAnswers = testData.Values.ToList();
            results = new List<string>();
        }
        public void Run(int option)
        {
            Dictionary<double[], string> vectorDistanceAndAnswer = new Dictionary<double[], string>();
            List<string> sortedAnswers = new List<string>();
            string most;
            switch (option)
            {
                case 1:
                    foreach (var vectorTest in testData)
                    {
                        foreach (var vectorLearn in learnData)
                        {
                            vectorDistanceAndAnswer.Add(CalculateVectorsDistance(vectorTest.Key, vectorLearn.Key), vectorLearn.Value);
                        }

                        /*foreach (KeyValuePair<double[], string> kvp in vectorDistanceAndAnswer)
                        {
                            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key.ToArray()[0], kvp.Value);
                        }*/



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


                        //Console.WriteLine(most);

                        sortedAnswers = vectorDistanceAndAnswer.OrderBy(a => a.Key[0]).Select(x => x.Value).ToList();

                        /*for (int i = 0; i < sortedAnswers.Count; i++)
                        {
                            string ss = sortedAnswers[i];
                            Console.WriteLine(i + ". " + ss);
                        }*/
                        most = sortedAnswers.GroupBy(i => i).Select(grp => grp.Key).First();
                        results.Add(most);
                        vectorDistanceAndAnswer.Clear();
                    }

                    int countCorrectAnswers = 0;
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].Equals(testAnswers[i]))
                            countCorrectAnswers++;
                        Console.WriteLine(results[i] + "\t" + testAnswers[i]);
                    }
                    Console.WriteLine(countCorrectAnswers / results.Count * 100 + "%");
                    break;
                case 2:
                    int dataSize = learnData.Keys.ToArray()[0].Length - 1;
                    Console.WriteLine("Input data in format: {0}{1}",
                                      String.Concat(Enumerable.Repeat("double,", dataSize)), "double");
                    double d;
                    string[] s = Console.ReadLine().Split(",");
                    if (s.Length != dataSize && !s.All(n => Double.TryParse(n, out d)))
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }

                    foreach (var vectorLearn in learnData)
                    {
                        vectorDistanceAndAnswer.Add(CalculateVectorsDistance((Array.ConvertAll<string, double>(s, Convert.ToDouble)), vectorLearn.Key), vectorLearn.Value);
                    }
                    sortedAnswers = vectorDistanceAndAnswer.OrderBy(a => a.Key[0]).Select(x => x.Value).Take(k).ToList();
                    most = sortedAnswers.GroupBy(i => i).Select(grp => grp.Key).First();
                    results.Add(most);
                    vectorDistanceAndAnswer.Clear();
                    Console.WriteLine(results.ElementAt(0));
                    break;
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
