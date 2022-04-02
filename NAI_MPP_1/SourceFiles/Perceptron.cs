using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace NAI_MPP_1.SourceFiles
{
    class Perceptron : Base
    {
        public Dictionary<double[], string> learnData { get; set; }
        public Dictionary<double[], string> testData { get; set; }
        public List<double> output { get; set; }
        public double[] weightVector { get; set; }
        public double bias { get; set; }
        public double alpha { get; set; } // learning rate
        public Perceptron(string learnFilePath, string testFilePath, double _bias, double learningRate)
        {
            learnData = ReadData(learnFilePath);
            testData = ReadData(testFilePath);
            output = new List<double>();

            weightVector = new double[learnData.Keys.Count];
            var rand = new Random();
            foreach (int i in weightVector)
            {
                weightVector[i] = rand.NextDouble() * 10;
                //Console.WriteLine(weightVector[i]);
            }
            bias = _bias;
            alpha = learningRate;
        }

        public void Run()
        {
            foreach (var learnVector in learnData)
            {
                double output = (DotProduct(learnVector.Key) - bias) >= 1 ? 1 : 0;

                DeltaRuleCalc(learnVector, output);
                foreach (var v in weightVector)
                    Console.WriteLine(v);
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
            for (int i = 0; i < inputVector.Key.Count(); i++)
                weightVector[i] += inputVector.Key[i] * alpha * ((inputVector.Value.Equals("Iris-versicolor") ? 1 : 0) - output);
            bias -= alpha * ((inputVector.Value.Equals("Iris-versicolor") ? 1 : 0) - output);
        }
    }
}
