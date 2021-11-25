using AlgorithmsComputabilityProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject.Tester
{
    public class KoksTester3000
    {
        public static void RunTests()
        {
            List<(Matrix, Matrix)> examples = ReadExamplesFromDisk();
            List<(Matrix, Matrix)> examplesForExactAlgorithms = examples.Where(e => e.Item1.VerticesNumber <= 10).ToList();
            List<(Matrix, Matrix)> examplesForApproxAlgorithms = examples.Where(e => e.Item1.VerticesNumber >= 10).ToList();

            //SpeedTester.RunSpeedTestsForExactAlgorithms(examplesForExactAlgorithms);
            //SpeedTester.RunSpeedTestsForApproximateAlgorithms(examplesForApproxAlgorithms);

            QualityTester.ApproximateVersusExact(examplesForExactAlgorithms);
            QualityTester.ApproximateSortedVersusUnsorted(examplesForApproxAlgorithms);

            //SpeedTester.RunSpeedTestsForExactAlgorithms(4, 9);
            //SpeedTester.RunSpeedTestsForApproximateAlgorithms(10, 30);
            //QualityTester.ApproximateVersusExact(4, 9);
            //QualityTester.ApproximateSortedVersusUnsorted(60, 70);
            //Console.ReadKey();
        }

        public static List<(Matrix, Matrix)> ReadExamplesFromDisk()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + Storage.EXAMPLES_DEVELOPMENT_DIRECTORY;
            string[] filenames = Directory.GetFiles(path);

            List<(Matrix, Matrix)> examples = new List<(Matrix, Matrix)>();
            foreach (string filename in filenames)
            {
                examples.Add(FileReader.Read(filename));
            }

            return examples;
        }

        public static List<(Matrix, Matrix)> GimmieSomeMatricesWithRandomizedSize(int maxSize, int numberOfMatrices = 3)
        {
            Random random = new Random();
            List<(Matrix, Matrix)> matrices = new List<(Matrix, Matrix)>();
            for (int i = 0; i < numberOfMatrices; i++)
            {
                int minLowerSize = maxSize > 9 ? (int)Math.Floor(maxSize * 0.8) : (int)Math.Floor(maxSize * 0.9);
                int lowerSize = random.Next(minLowerSize, maxSize + 1);
                matrices.Add((GraphGenerator.GetRandomMatrix(maxSize), GraphGenerator.GetRandomMatrix(lowerSize)));
            }
            return matrices;
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

        // Don't call it unless you want to overwrite the examples!
        private static void SaveExamplesOnDisk()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + Storage.EXAMPLES_DEVELOPMENT_DIRECTORY;
            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (int size in Storage.MATRIX_SIZES)
            {
                List<(Matrix, Matrix)> generatedMatrices = GimmieSomeMatricesWithRandomizedSize(size);
                foreach ((Matrix, Matrix) example in generatedMatrices)
                {
                    FileReader.Write(example.Item1, example.Item2);
                }
            }
        }
    }
}
