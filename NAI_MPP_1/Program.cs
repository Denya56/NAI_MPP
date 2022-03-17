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

            Data learnData = new Data();
            learnData.ReadData("./iris.data");

            Data testData = new Data();
            testData.ReadData("./iris.test.data");

            List<decimal[]> l = learnData.vectorList;

            foreach (decimal[] d in l)
            {
                foreach (decimal a in d)
                {
                    Console.Write(a + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTest\n");

            List<decimal[]> l1 = testData.vectorList;

            foreach (decimal[] d in l1)
            {
                foreach (decimal a in d)
                {
                    Console.Write(a + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
