﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsComputabilityProject
{
    public class ConsoleMenu
    {
        private List<int> ChosenSizes { get; set; } = new List<int>();
        private List<Matrix> GeneratedMatrices { get; set; } = new List<Matrix>();
        private Dictionary<string, Matrix> CalculatedMatrices { get; set; } = new Dictionary<string, Matrix>();
        private bool IsExactAlgorithmApplicable
        {
            get
            {
                return ChosenSizes[0] < 11 && ChosenSizes[1] < 11;
            }
        }

        public void RunConsoleMenu(string[] args)
        {
            try
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Press a key to choose the action:");
                    Console.WriteLine("1 - Run algorithm on random graphs");
                    Console.WriteLine("2 - Run algorithm on specified graphs");
                    Console.WriteLine("q - Quit the application");
                    char key = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    switch (key)
                    {
                        case '1':
                            DisplayOptionsForRandomGraphs();
                            break;
                        case '2':
                            DisplayOptionsForSpecifiedGraphs();
                            break;
                        case 'q':
                            flag = false;
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("\nInvalid option selected.\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }

        private void DisplayOptionsForRandomGraphs()
        {
            bool flag = true;
            while (flag)
            {
                if (ChosenSizes.Count < 2)
                {
                    Console.WriteLine("Note: If size of a graph is bigger than 10, only approximate algorithms will be available.");
                    Console.WriteLine("Please enter the size of the first graph and confirm by pressing enter.");

                    ChosenSizes.Add(ReadInteger());
                    Console.WriteLine("Please enter the size of the second graph and confirm by pressing enter.");
                    ChosenSizes.Add(ReadInteger());
                    Console.WriteLine("Generating graphs...");

                    GeneratedMatrices.Add(GraphGenerator.GetRandomMatrix(ChosenSizes[0]));
                    GeneratedMatrices.Add(GraphGenerator.GetRandomMatrix(ChosenSizes[1]));
                    Console.Write("Graphs generated. ");
                }

                Console.WriteLine("Please press a key to choose an option:");
                Console.WriteLine("1 - Print the first graph");
                Console.WriteLine("2 - Print the second graph");
                Console.WriteLine("3 - Show the Approximate Maximal Common Subgraph");
                Console.WriteLine("4 - Show the Approximate Minimal Common Supergraph");
                if (ChosenSizes[0] < 11 && ChosenSizes[1] < 11)
                {
                    Console.WriteLine("5 - Show the Exact Maximal Common Subgraph");
                    Console.WriteLine("6 - Show the Exact Minimal Common Supergraph");
                }
                Console.WriteLine("r - Reset the graphs and choose new sizes");
                Console.WriteLine("b - Go back");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (key)
                {
                    case '1':
                        GeneratedMatrices[0].Print();
                        break;
                    case '2':
                        GeneratedMatrices[1].Print();
                        break;
                    case '3':
                        if (!CalculatedMatrices.ContainsKey("ApproximateMaximalSubGraph"))
                        {
                            Console.WriteLine("Calculating the solution...");
                            CalculatedMatrices.Add("ApproximateMaximalSubGraph",
                                Algorithm.FindMaximalSubGraphApproximate(GeneratedMatrices[0], GeneratedMatrices[1]));
                        }
                        CalculatedMatrices["ApproximateMaximalSubGraph"].Print();
                        break;
                    case '4':
                        if (!CalculatedMatrices.ContainsKey("ApproximateMinimalSuperGraph"))
                        {
                            Console.WriteLine("Calculating the solution...");
                            CalculatedMatrices.Add("ApproximateMinimalSuperGraph",
                                Algorithm.FindMinimalSuperGraphApproximate(GeneratedMatrices[0], GeneratedMatrices[1]));
                        }
                        CalculatedMatrices["ApproximateMinimalSuperGraph"].Print();
                        break;
                    case '5':
                        if (IsExactAlgorithmApplicable)
                        {
                            if (!CalculatedMatrices.ContainsKey("ExactMaximalSubGraph"))
                            {
                                Console.WriteLine("Calculating the solution...");
                                CalculatedMatrices.Add("ExactMaximalSubGraph",
                                    Algorithm.FindMaximalSubGraph(GeneratedMatrices[0], GeneratedMatrices[1]));
                            }
                            CalculatedMatrices["ExactMaximalSubGraph"].Print();
                        }
                        else Console.WriteLine("Not applicable for graphs bigger than 10 vertices");
                        break;
                    case '6':
                        if (IsExactAlgorithmApplicable)
                        {
                            if (!CalculatedMatrices.ContainsKey("ExactMinimalSuperGraph"))
                            {
                                Console.WriteLine("Calculating the solution...");
                                CalculatedMatrices.Add("ExactMinimalSuperGraph",
                                    Algorithm.FindMinimalSuperGraph(GeneratedMatrices[0], GeneratedMatrices[1]));
                            }
                            CalculatedMatrices["ExactMinimalSuperGraph"].Print();
                        }
                        else Console.WriteLine("Not applicable for graphs bigger than 10 vertices");
                        break;
                    case 'r':
                        ChosenSizes.Clear();
                        GeneratedMatrices.Clear();
                        CalculatedMatrices.Clear();
                        Console.WriteLine();
                        break;
                    case 'b':
                        flag = false;
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("\nInvalid option selected.\n");
                        break;
                }
            }
        }

        private static int ReadInteger()
        {
            int size = 0;
            while (size < 2)
            {
                string input = Console.ReadLine();
                while (!Int32.TryParse(input, out size))
                {
                    Console.WriteLine("Please provide a valid value for size:");
                    input = Console.ReadLine();
                }
                if (size < 2)
                {
                    Console.WriteLine("The graph should have at least 2 vertices. Please provide a valid value for size:");
                }
            }
            return size;
        }

        private void DisplayOptionsForSpecifiedGraphs()
        {
            Console.WriteLine("Note: If size of a graph is bigger than 10, only approximate algorithms will be available.");
            Console.WriteLine("Please write global path to the file with matrices:");

            string input = Console.ReadLine();

            while (!File.Exists(input))
            {
                Console.WriteLine("File wasn't found! Please supply correct global path or press 1 to go back.");
                input = Console.ReadLine();
                if (input == "1")
                {
                    return;
                }
            }

            try
            {
                (Matrix m1, Matrix m2) = FileReader.Read(input);
                GeneratedMatrices.Add(m1);
                GeneratedMatrices.Add(m2);
                ChosenSizes.Add(m1.VerticesNumber);
                ChosenSizes.Add(m2.VerticesNumber);
            }
            catch
            {
                Console.WriteLine("File deserialization went wrong. Going back to main menu");
                return;
            }
            Console.WriteLine();

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Graphs deserialized! Please press a key to choose an option:");
                Console.WriteLine("1 - Print the first graph");
                Console.WriteLine("2 - Print the second graph");
                Console.WriteLine("3 - Show the Approximate Maximal Common Subgraph");
                Console.WriteLine("4 - Show the Approximate Minimal Common Supergraph");
                if (ChosenSizes[0] < 11 && ChosenSizes[1] < 11)
                {
                    Console.WriteLine("5 - Show the Exact Maximal Common Subgraph");
                    Console.WriteLine("6 - Show the Exact Minimal Common Supergraph");
                }
                Console.WriteLine("b - Go back - this option will reset the graphs");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (key)
                {
                    case '1':
                        GeneratedMatrices[0].Print();
                        break;
                    case '2':
                        GeneratedMatrices[1].Print();
                        break;
                    case '3':
                        if (!CalculatedMatrices.ContainsKey("ApproximateMaximalSubGraph"))
                        {
                            Console.WriteLine("Calculating the solution...");
                            CalculatedMatrices.Add("ApproximateMaximalSubGraph",
                                Algorithm.FindMaximalSubGraphApproximate(GeneratedMatrices[0], GeneratedMatrices[1]));
                        }
                        CalculatedMatrices["ApproximateMaximalSubGraph"].Print();
                        break;
                    case '4':
                        if (!CalculatedMatrices.ContainsKey("ApproximateMinimalSuperGraph"))
                        {
                            Console.WriteLine("Calculating the solution...");
                            CalculatedMatrices.Add("ApproximateMinimalSuperGraph",
                                Algorithm.FindMinimalSuperGraphApproximate(GeneratedMatrices[0], GeneratedMatrices[1]));
                        }
                        CalculatedMatrices["ApproximateMinimalSuperGraph"].Print();
                        break;
                    case '5':
                        if (IsExactAlgorithmApplicable)
                        {
                            if (!CalculatedMatrices.ContainsKey("ExactMaximalSubGraph"))
                            {
                                Console.WriteLine("Calculating the solution...");
                                CalculatedMatrices.Add("ExactMaximalSubGraph",
                                    Algorithm.FindMaximalSubGraph(GeneratedMatrices[0], GeneratedMatrices[1]));
                            }
                            CalculatedMatrices["ExactMaximalSubGraph"].Print();
                        }
                        else Console.WriteLine("Not applicable for graphs bigger than 10 vertices");
                        break;
                    case '6':
                        if (IsExactAlgorithmApplicable)
                        {
                            if (!CalculatedMatrices.ContainsKey("ExactMinimalSuperGraph"))
                            {
                                Console.WriteLine("Calculating the solution...");
                                CalculatedMatrices.Add("ExactMinimalSuperGraph",
                                    Algorithm.FindMinimalSuperGraph(GeneratedMatrices[0], GeneratedMatrices[1]));
                            }
                            CalculatedMatrices["ExactMinimalSuperGraph"].Print();
                        }
                        else Console.WriteLine("Not applicable for graphs bigger than 10 vertices");
                        break;
                    case 'b':
                        ChosenSizes.Clear();
                        GeneratedMatrices.Clear();
                        CalculatedMatrices.Clear();
                        flag = false;
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("\nInvalid option selected.\n");
                        break;
                }
            }
        }

        private static void DisplayException(Exception ex)
        {
            Console.WriteLine("The application terminated with an error.");
            Console.WriteLine(ex.Message);
            while (ex.InnerException != null)
            {
                Console.WriteLine("\t* {0}", ex.InnerException.Message);
                ex = ex.InnerException;
            }
        }
    }
}
