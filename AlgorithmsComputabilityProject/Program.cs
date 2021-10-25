using System;

namespace AlgorithmsComputabilityProject
{
    class Program
    {
        static void Main(string[] args)
        {
            (Matrix m1, Matrix m2) = FileReader.Read("../../../matrix_2.txt");
            m1.Print();
            m2.Print();
            Matrix MaxSubGraph = Algorithm.FindMaximalSubGraph(m1, m2);
            MaxSubGraph.Print();
        }
    }
}
