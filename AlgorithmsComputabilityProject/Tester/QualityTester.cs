using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class QualityTester
    {
        // Method returns dictionary in following form:
        // "Algorithm_Name": [ d for minSize, d for minSize + 1, ..., d for maxSize]
        // ...
        // Where d is mean difference in amount of edges [ d = ||E(approximate)| - |E(exact)|| ]
        public static Dictionary<string, List<double>> ApproximateVersusExact(List<(Matrix, Matrix)> examples)
        {
            Console.WriteLine("QUALITY TESTS FOR APPROXIMATE vs EXACT START");

            Dictionary<string, List<double>> differences = new Dictionary<string, List<double>>
            {
                { "Maximum Common Subgraph", new List<double>() },
                { "Minimum Common Supergraph", new List<double>() }
            };

            RunTestsAndSaveDifferences(differences["Maximum Common Subgraph"],
                (Storage.MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, Storage.MAXIMUM_COMMON_SUBGRAPH_EXACT), examples, Storage.EXACT_MATRIX_SIZES);

            RunTestsAndSaveDifferences(differences["Minimum Common Supergraph"],
                (Storage.MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, Storage.MINIMAL_COMMON_SUPERGRAPH_EXACT), examples, Storage.EXACT_MATRIX_SIZES);

            string json = JsonSerializer.Serialize(differences);
            File.WriteAllText(Storage.GetPathToOutput("ApproxVsExactResults.txt"), json, Encoding.UTF8);

            return differences;
        }

        // Method returns dictionary in following form:
        // "Algorithm_Name": [ d for minSize, d for minSize + 1, ..., d for maxSize]
        // ...
        // Where d is mean difference in amount of edges [ d = ||E(approximate)| - |E(approximate_unsorted)|| ]
        public static Dictionary<string, List<double>> ApproximateSortedVersusUnsorted(List<(Matrix, Matrix)> examples)
        {
            Console.WriteLine("QUALITY TESTS FOR APPROXIMATE SORTED vs UNSORTED START");

            Dictionary<string, List<double>> differences = new Dictionary<string, List<double>>
            {
                { "Maximum Common Subgraph", new List<double>() },
                { "Minimum Common Supergraph", new List<double>() }
            };

            RunTestsAndSaveDifferencesForApprox(differences["Maximum Common Subgraph"],
                Storage.MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, examples, Storage.APPROX_MATRIX_SIZES);

            RunTestsAndSaveDifferencesForApprox(differences["Minimum Common Supergraph"],
                Storage.MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, examples, Storage.APPROX_MATRIX_SIZES);

            string json = JsonSerializer.Serialize(differences);
            File.WriteAllText(Storage.GetPathToOutput("ApproxVsApproxResults.txt"), json, Encoding.UTF8);

            return differences;
        }

        private static void RunTestsAndSaveDifferences(List<double> differences, (string, string) algs, List<(Matrix, Matrix)> examples, int[] matrixSizes)
        {
            foreach (int size in matrixSizes)
            {
                Console.WriteLine($"QUALITY TESTS FOR SIZE {size}, ALGORITHMS: {String.Join(", ", algs)}");

                List<double> partialDifferences = new List<double>();
                List<(Matrix, Matrix)> matrices = examples.Where(e => e.Item1.VerticesNumber == size).ToList();

                foreach ((Matrix, Matrix) example in matrices)
                {
                    Matrix firstMatrixCopy = new Matrix(example.Item1.Graph);
                    Matrix secondMatrixCopy = new Matrix(example.Item2.Graph);
                    Matrix result_approx = Storage.AllFunctions[algs.Item1](firstMatrixCopy, secondMatrixCopy, true);
                    Matrix result_exact = Storage.AllFunctions[algs.Item2](example.Item1, example.Item2, false);
                    if (result_approx == null || result_exact == null)
                        partialDifferences.Add(0);
                    else
                        partialDifferences.Add(Math.Abs(result_approx.EdgesNumber - result_exact.EdgesNumber));
                }

                differences.Add(partialDifferences.Average());
            }
        }

        private static void RunTestsAndSaveDifferencesForApprox(List<double> differences, string alg, List<(Matrix, Matrix)> examples, int[] matrixSizes)
        {
            foreach (int size in matrixSizes)
            {
                Console.WriteLine($"QUALITY TESTS FOR SIZE {size}, ALGORITHM: {alg}");

                List<double> partialDifferences = new List<double>();
                List<(Matrix, Matrix)> matrices = examples.Where(e => e.Item1.VerticesNumber == size).ToList();

                foreach ((Matrix, Matrix) example in matrices)
                {
                    Matrix firstMatrixCopy = new Matrix(example.Item1.Graph);
                    Matrix secondMatrixCopy = new Matrix(example.Item2.Graph);
                    Matrix result_approx_sorted = Storage.AllFunctions[alg](firstMatrixCopy, secondMatrixCopy, true);
                    Matrix result_approx_unsorted = Storage.AllFunctions[alg](example.Item1, example.Item2, false);
                    if (result_approx_sorted == null || result_approx_unsorted == null)
                        partialDifferences.Add(0);
                    else
                        partialDifferences.Add(Math.Abs(result_approx_sorted.EdgesNumber - result_approx_unsorted.EdgesNumber));
                }

                differences.Add(partialDifferences.Average());
            }
        }

        // ----------- RANDOMIZED METHODS -----------
        // Method returns dictionary in following form:
        // "Algorithm_Name": [ d for minSize, d for minSize + 1, ..., d for maxSize]
        // ...
        // Where d is mean difference in amount of edges [ d = ||E(approximate)| - |E(exact)|| ]
        public static Dictionary<string, List<double>> ApproximateVersusExact(int minSize, int maxSize)
        {
            Console.WriteLine("QUALITY TESTS FOR APPROXIMATE vs EXACT START");

            Dictionary<string, List<double>> differences = new Dictionary<string, List<double>>
            {
                { "Maximum Common Subgraph", new List<double>() },
                { "Minimum Common Supergraph", new List<double>() }
            };

            RunTestsAndSaveDifferences(differences["Maximum Common Subgraph"],
                (Storage.MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, Storage.MAXIMUM_COMMON_SUBGRAPH_EXACT), minSize, maxSize);

            RunTestsAndSaveDifferences(differences["Minimum Common Supergraph"],
                (Storage.MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, Storage.MINIMAL_COMMON_SUPERGRAPH_EXACT), minSize, maxSize);

            string json = JsonSerializer.Serialize(differences);
            File.WriteAllText(Storage.GetPathToOutput("ApproxVsExactResults.txt"), json, Encoding.UTF8);

            return differences;
        }

        // Method returns dictionary in following form:
        // "Algorithm_Name": [ d for minSize, d for minSize + 1, ..., d for maxSize]
        // ...
        // Where d is mean difference in amount of edges [ d = ||E(approximate)| - |E(approximate_unsorted)|| ]
        public static Dictionary<string, List<double>> ApproximateSortedVersusUnsorted(int minSize, int maxSize)
        {
            Console.WriteLine("QUALITY TESTS FOR APPROXIMATE SORTED vs UNSORTED START");

            Dictionary<string, List<double>> differences = new Dictionary<string, List<double>>
            {
                { "Maximum Common Subgraph", new List<double>() },
                { "Minimum Common Supergraph", new List<double>() }
            };

            RunTestsAndSaveDifferencesForApprox(differences["Maximum Common Subgraph"],
                Storage.MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, minSize, maxSize);

            RunTestsAndSaveDifferencesForApprox(differences["Minimum Common Supergraph"],
                Storage.MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, minSize, maxSize);

            string json = JsonSerializer.Serialize(differences);
            File.WriteAllText(Storage.GetPathToOutput("ApproxVsApproxResults.txt"), json, Encoding.UTF8);

            return differences;
        }

        private static void RunTestsAndSaveDifferences(List<double> differences, (string, string) algs, int minSize, int maxSize)
        {
            for (int size = minSize; size <= maxSize; size++)
            {
                Console.WriteLine($"QUALITY TESTS FOR SIZE {size}, ALGORITHMS: {String.Join(", ", algs)}");

                List<double> partialDifferences = new List<double>();
                List<(Matrix, Matrix)> matrices = KoksTester3000.GimmieSomeMatrices(size);

                foreach ((Matrix, Matrix) example in matrices)
                {
                    Matrix result1 = Storage.AllFunctions[algs.Item1](example.Item1, example.Item2, true);
                    Matrix result2 = Storage.AllFunctions[algs.Item2](example.Item1, example.Item2, true);
                    partialDifferences.Add(Math.Abs(result1.EdgesNumber - result2.EdgesNumber));
                }

                differences.Add(partialDifferences.Average());
            }
        }

        private static void RunTestsAndSaveDifferencesForApprox(List<double> differences, string alg, int minSize, int maxSize)
        {
            for (int size = minSize; size <= maxSize; size++)
            {
                Console.WriteLine($"QUALITY TESTS FOR SIZE {size}, ALGORITHM: {alg}");

                List<double> partialDifferences = new List<double>();
                List<(Matrix, Matrix)> matrices = KoksTester3000.GimmieSomeMatrices(size, 100);

                foreach ((Matrix, Matrix) example in matrices)
                {
                    Matrix result1 = Storage.AllFunctions[alg](example.Item1, example.Item2, false);
                    Matrix result2 = Storage.AllFunctions[alg](example.Item1, example.Item2, true);
                    partialDifferences.Add(Math.Abs(result1.EdgesNumber - result2.EdgesNumber));
                }

                differences.Add(partialDifferences.Average());
            }
        }
    }
}
