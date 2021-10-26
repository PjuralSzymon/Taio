using System;

namespace AlgorithmsComputabilityProject
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAlgorithm();
            //TestSwap();
        }

        static void RunAlgorithm()
        {
            (Matrix m1, Matrix m2) = FileReader.Read("../../../matrix_2.txt");
            m1.Print();
            m2.Print();
            Console.WriteLine("Maximal sub graph: ");
            Matrix MaxSubGraph = Algorithm.FindMaximalSubGraph(m1, m2);
            MaxSubGraph.Print();

            Console.WriteLine("Minimal super graph: ");
            Matrix MinSuperGraph = Algorithm.FindMinimalSuperGraph(m1, m2);
            MinSuperGraph.Print();
        }

        static void TestSwap()
        {
            (Matrix m1, Matrix m2) = FileReader.Read("../../../matrix_2.txt");
            m1.Print();
            m1.SwapColumn(1, 2);
            m1.Print();
            m1.SwapRow(2, 4);
            m1.Print();
        }
    }
}
