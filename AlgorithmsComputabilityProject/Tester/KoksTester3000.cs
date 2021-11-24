using AlgorithmsComputabilityProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject.Tester
{
    public class KoksTester3000
    {
        public static void RunTests()
        {
            //SpeedTester.RunSpeedTestsForExactAlgorithms(4, 9);
            //SpeedTester.RunSpeedTestsForApproximateAlgorithms(10, 30);
            //QualityTester.ApproximateVersusExact(4, 9);
            QualityTester.ApproximateSortedVersusUnsorted(60, 90);
        }

        public static List<(Matrix, Matrix)> GimmieSomeMatrices(int sizeOfMatrices, int numberOfMatrices = 10)
        {
            List<(Matrix, Matrix)> matrices = new List<(Matrix, Matrix)>();
            for (int i = 0; i < numberOfMatrices; i++)
            {
                matrices.Add((GraphGenerator.GetRandomMatrix(sizeOfMatrices), GraphGenerator.GetRandomMatrix(sizeOfMatrices)));
            }
            return matrices;
        }
    }
}
