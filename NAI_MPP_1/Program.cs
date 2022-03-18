using System;
using System.Collections.Generic;

namespace NAI_MPP_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter k: ");
            int k = int.Parse(Console.ReadLine());

            KNN_AI ai = new KNN_AI(k, "Data/iris.data", "Data/iris.test.data");

            var l = ai.learnData.Keys;

            foreach (double[] d in l)
            {
                foreach (double a in d)
                {
                    Console.Write(a + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTest\n");

            var l1 = ai.testData.Keys;

            foreach (double[] d in l1)
            {
                foreach (double a in d)
                {
                    Console.Write(a + "\t");
                }
                Console.WriteLine();
            }

            ai.Run();

            for (int i = 0; i < ai.testResults.Count; i++)
            {
                Console.WriteLine(ai.testResults[i] + "\t" + ai.testAnswers[i]);

            }
        }
    }
}
