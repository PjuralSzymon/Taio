using System;

namespace AlgorithmsComputabilityProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSort();
            RunAlgorithm();
            //TestSwap();
        }

        static void RunAlgorithm()
        {
            (Matrix m1, Matrix m2) = FileReader.Read("../../../matrix_4.txt");
            m1.Print();
            m2.Print();
            Console.WriteLine("Maximal sub graph: ");
            Matrix MaxSubGraph = Algorithm.FindMaximalSubGraph(m1, m2);
            MaxSubGraph.Print();

            Console.WriteLine("Maximal Approximate sub graph: ");
            Matrix AppMaxSubGraph = Algorithm.FindMaximalSubGraphApproximate(m1, m2);
            AppMaxSubGraph.Print();

            Console.WriteLine("Minimal super graph: ");
            Matrix MinSuperGraph = Algorithm.FindMinimalSuperGraph(m1, m2);
            MinSuperGraph.Print();

            Console.WriteLine("Minimal Approximate super graph: ");
            Matrix AppMinSuperGraph = Algorithm.FindMinimalSuperGraph(m1, m2);
            AppMinSuperGraph.Print();
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

        static void TestSort()
        {
            (Matrix m1, Matrix m2) = FileReader.Read("../../../matrix_3.txt");
            m1.Print();
            m1.TransformToSortedForm();
            m1.Print();
        }
    }
}
