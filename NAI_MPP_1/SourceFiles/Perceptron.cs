using System;
using System.Collections.Generic;
using System.Linq;

namespace NAI_MPP_1.SourceFiles
{
    class Perceptron : Base
    {
        public Dictionary<double[], string> learnData { get; set; }
        public Dictionary<double[], string> testData { get; set; }
        public List<string> learnVectorAnswers { get; set; }
        public List<double> testResults { get; set; }
        public List<string> testVectorAnswers { get; set; }
        public List<double> learnResults { get; set; }
        public double[] weightVector { get; set; }
        public double bias { get; set; }
        public double alpha { get; set; } // learning rate
        public double threshold { get; set; }
        public Perceptron(string learnFilePath, string testFilePath, double inBias, double learningRate, double inThershold)
        {
            learnData = ReadData(learnFilePath);
            testData = ReadData(testFilePath);
            learnVectorAnswers = learnData.Values.ToList();
            testVectorAnswers = testData.Values.ToList();
            testResults = new List<double>();
            learnResults = new List<double>();

            weightVector = new double[learnData.Keys.Count];
            var rand = new Random();
            foreach (int i in weightVector)
            {
                weightVector[i] = rand.NextDouble() * 10;
                //Console.WriteLine(weightVector[i]);
            }
            bias = inBias;
            alpha = learningRate;
            threshold = inThershold;
        }

        public void Run(int option)
        {
            Learn();
            switch (option)
            {
                case 1:
                    foreach (var testVector in testData)
                    {
                        testResults.Add((DotProduct(testVector.Key) - bias) >= 1 ? 1 : 0);
                    }
                    /*foreach (var r in testResults)
                        Console.WriteLine(r);*/
                    List<string> sResults = new List<string>();
                    foreach (var v in testResults)
                        sResults.Add(v == 1 ? "Iris-versicolor" : "Iris-virginica");
                    double countCorrectAnswers = 0;
                    //Console.WriteLine(sResults.Count + "\t" + p.testVectorAnswers.Count);
                    for (int i = 0; i < sResults.Count; i++)
                    {
                        if (sResults[i].Equals(testVectorAnswers[i]))
                            countCorrectAnswers++;
                        Console.WriteLine(sResults[i] + "\t" + testVectorAnswers[i]);
                    }
                    Console.WriteLine(Math.Round(countCorrectAnswers / testResults.Count * 100, 3) + "%");
                    break;
                case 2:
                    int dataSize = learnData.Keys.ToArray()[0].Length - 1;
                    Console.WriteLine("Input data in format: {0}{1}",
                                      String.Concat(Enumerable.Repeat("double,", dataSize)), "double");
                    double d;
                    string[] s = Console.ReadLine().Split(",");
                    double[] inputVector = new double[s.Length];
                    if (s.Length != dataSize && !s.All(n => Double.TryParse(n, out d)))
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
                    else inputVector = (Array.ConvertAll<string, double>(s, Convert.ToDouble));

                    double output = (DotProduct(inputVector) - bias) >= 1 ? 1 : 0;
                    Console.WriteLine(output == 1 ? "Iris-versicolor" : "Iris-virginica") ;
                    break;
            }
        }
        private void Learn()
        {
            for (int i = 0; i < 100; i++)
            {
                learnResults.Clear();
                foreach (var learnVector in learnData)
                {

                    double output = (DotProduct(learnVector.Key) - bias) >= 1 ? 1 : 0;
                    learnResults.Add(output);
                    DeltaRuleCalc(learnVector, output);
                    /*foreach (var v in weightVector)
                        if (v != 0)
                            Console.WriteLine(v);
                    Console.WriteLine(" ");*/
                }
                var e = IterationErrorCalc();
                //Console.WriteLine(e);
                if (e < threshold)
                    return;
            }
        }
        private double DotProduct(double[] inputVector)
        {
            double product = 0;

            for (int i = 0; i < inputVector.Length; i++)
                product = product + weightVector[i] * inputVector[i];
            return product;
        }
        private void DeltaRuleCalc(KeyValuePair<double[], string> inputVector, double output)
        {
            for (int i = 0; i < inputVector.Key.Length; i++)
                weightVector[i] += inputVector.Key[i] * alpha * ((inputVector.Value.Equals("Iris-versicolor") ? 1 : 0) - output);
            bias -= alpha * ((inputVector.Value.Equals("Iris-versicolor") ? 1 : 0) - output);
        }
        private double IterationErrorCalc()
        {
            double sum = 0;
            for (int i = 0; i < learnVectorAnswers.Count(); i++)
            {
                var t = (learnVectorAnswers.ElementAt(i).Equals("Iris-versicolor") ? 1 : 0) - learnResults.ElementAt(i);
                sum += Math.Pow(t, 2);
            }
            return sum / learnVectorAnswers.Count;
        }
    }
}
