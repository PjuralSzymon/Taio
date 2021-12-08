using AlgorithmsComputabilityProject.Tester;
using System;

namespace AlgorithmsComputabilityProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenu consoleMenu = new ConsoleMenu();
            consoleMenu.RunConsoleMenu(args);
            //MainTester.RunTests();
            //MainTester.GenerateExamplesFolder();
        }
        
        static void Tests()
        {
            //(Matrix, Matrix) m = FileReader.Read(@"..\4_4_iso.txt");

            //Matrix.PrintGraphs(m.Item1, m.Item2);

            //Matrix eee = new Matrix(m.Item2.Graph);
            //eee.SwapColumn(3, 2);
            //eee.SwapRow(3, 2);

            //Matrix.PrintGraphs(m.Item1, eee);

            //foreach (IsomorphicGeneratorEnumeratorData perm in new IsomorphicGenerator(m.Item2))
            //{
            //    Console.WriteLine(String.Join(" ", perm.Permutation));
            //    perm.PermutedMatrix.Print();
            //}

            //for (int i = 1; i < 10; i++)
            //{
            //    var per = new Permutation(i);
            //    Console.WriteLine(per.Permutations.Count);
            //}
        }
    }
}
