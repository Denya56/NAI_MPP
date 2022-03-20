using System;
using NAI_MPP_1.SourceFiles;

namespace NAI_MPP_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.MainMenu();
            

            /*while (true)
            {
                
                    switch (Console.ReadLine())
                    {
                        case "1":
                            menu.currentState = "SM";
                            break;
                        case "2":
                            menu.currentState = "Graph";
                            break;
                    }
            }*/

            /*Console.WriteLine("Enter k: ");
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

            }*/
        }
    }
}
