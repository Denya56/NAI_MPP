using System;
using System.Collections.Generic;
using System.Text;

namespace NAI_MPP_1.SourceFiles
{
    class Menu
    {
        public string currentState { get; set; }
        public Menu()
        {
            //currentState = "mainMenu";
        }

        private void Run(int option)
        {
            Console.WriteLine("Enter k: ");
            int k = int.Parse(Console.ReadLine());
            KNN_AI ai = new KNN_AI(k, "Data/iris.data", "Data/iris.test.data");

            switch (option)
            {
                case 1:
                    ai.Run(1);
                    for (int i = 0; i < ai.results.Count; i++)
                    {
                        Console.WriteLine(ai.results[i] + "\t" + ai.testAnswers[i]);
                    }
                    break;

                case 2:
                    ai.Run(2);
                    break;
            }

            /*for (int i = 0; i < ai.results.Count; i++)
            {
                Console.WriteLine(ai.results[i]);
            }*/
        }

        public void DisplayMenu(int menuState)
        {
            switch (menuState)
            {
                case 0:
                    Console.WriteLine("1. Start\n" +
                                  "2. Demo\n" +
                                  "3. Exit");
                    break;
                case 1:
                    Console.WriteLine("1. Run test file\n" +
                                      "2. Enter vector manually");
                    break;
                case 2:
                    Console.WriteLine("1. Run again\n" +
                                      "2. Go back to main menu");
                    break;
            }
        }

        private int GetChoice(int menuState)
        {
            DisplayMenu(menuState);
            string choice = Console.ReadLine();
            while (choice.Equals(""))
            {
                Console.WriteLine("Invalid input");
                DisplayMenu(menuState);
            }
            return int.Parse(choice);
        }


        // Unique menus
        public void MainMenu()
        {
            Console.Clear();
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(0);

                switch (choice)
                {
                    case 1:
                        StartMenu();
                        break;
                    case 2:
                        // do demo option
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public void StartMenu()
        {
            Console.Clear();
            for (int choice = -1; choice !=0;)
            {
                choice = GetChoice(1);

                if (choice == 1 || choice == 2)
                {
                    Run(choice);
                    EndMenu();
                }
                else
                    Console.WriteLine("Invalid input");
            }
        }

        public void EndMenu()
        {
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(2);

                switch (choice)
                {
                    case 1:
                        StartMenu();
                        break;
                    case 2:
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }
            }
        }
    }
}
