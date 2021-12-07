using AlgorithmsComputabilityProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject.Tester
{
    public class MainTester
    {
        public static void RunTests()
        {
            //List<(Matrix, Matrix)> examples = ReadExamplesFromDisk();
            //List<(Matrix, Matrix)> examplesForExactAlgorithms = examples.Where(e => e.Item1.VerticesNumber <= 10).ToList();
            //List<(Matrix, Matrix)> examplesForApproxAlgorithms = examples.Where(e => e.Item1.VerticesNumber >= 10).ToList();

            //SpeedTester.RunSpeedTestsForExactAlgorithms(examplesForExactAlgorithms);
            //SpeedTester.RunSpeedTestsForApproximateAlgorithms(examplesForApproxAlgorithms);

            //QualityTester.ApproximateVersusExact(examplesForExactAlgorithms);
            //QualityTester.ApproximateSortedVersusUnsorted(examplesForApproxAlgorithms);

            //SpeedTester.RunSpeedTestsForExactAlgorithms(4, 10, 5);
            //SpeedTester.RunSpeedTestsForApproximateAlgorithms(10, 95, 50);
            //QualityTester.ApproximateVersusExact(4, 10, 5);
            //QualityTester.ApproximateSortedVersusUnsorted(10, 95, 50);
            //Console.ReadKey();
        }

        // Don't run unless you want to regenerate the examples folder!
        public static void GenerateExamplesFolder()
        {
            // ----- Generating and saving examples -----
            //List<(Matrix, Matrix)> sameIsoGraphs = GenerateExamples.GenerateIsomorphicExamplesWithEqualSizes();
            //List<(Matrix, Matrix)> differentIsoGraphs = GenerateExamples.GenerateIsomorphicExamplesWithDifferentSizes();
            List<(Matrix, Matrix)> randomGraphs = GenerateExamples.GenerateRandomExamplesWithConcreteSizes();
            //SaveExamplesOnDisk(sameIsoGraphs, "iso");
            //SaveExamplesOnDisk(differentIsoGraphs, "isosub", false);
            SaveExamplesOnDisk(randomGraphs, "noiso", false);
        }

        public static List<(Matrix, Matrix)> ReadExamplesFromDisk()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + Storage.EXAMPLES_DIRECTORY;
            string[] filenames = Directory.GetFiles(path);

            List<(Matrix, Matrix)> examples = new List<(Matrix, Matrix)>();
            foreach (string filename in filenames)
            {
                examples.Add(FileReader.Read(filename));
            }

            return examples;
        }

        // Don't call it unless you want to overwrite the examples!
        private static void SaveExamplesOnDisk(List<(Matrix, Matrix)> matricesToSave, string name = "random", bool deleteDirectoryContents = true)
        {
            if (deleteDirectoryContents)
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + Storage.EXAMPLES_DIRECTORY;
                DirectoryInfo directory = new DirectoryInfo(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }

            foreach ((Matrix, Matrix) example in matricesToSave)
            {
                FileReader.Write(example.Item1, example.Item2, name);
            }
        }

        // Don't call it unless you want to overwrite the examples!
        private static void SaveRandomExamplesOnDisk(string name = "random", bool deleteDirectoryContents = true)
        {
            if (deleteDirectoryContents)
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + Storage.EXAMPLES_DIRECTORY;
                DirectoryInfo directory = new DirectoryInfo(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }

            foreach (int size in Storage.ALL_MATRIX_SIZES)
            {
                List<(Matrix, Matrix)> generatedMatrices = GenerateExamples.GenerateRandomExamplesWithVaryingSizes(size);
                foreach ((Matrix, Matrix) example in generatedMatrices)
                {
                    FileReader.Write(example.Item1, example.Item2, name);
                }
            }
        }
    }
}
