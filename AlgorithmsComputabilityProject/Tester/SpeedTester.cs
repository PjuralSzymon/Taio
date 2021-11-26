using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class SpeedTester
    {
        // Methods return dictionary in following form:
        // "Algorithm_Name": [ t for minSize, t for minSize + 1, ..., t for maxSize]
        // ...
        // Where t is mean running time in milliseconds
        public static Dictionary<string, List<double>> RunSpeedTestsForExactAlgorithms(List<(Matrix, Matrix)> examples)
        {
            Console.WriteLine("SPEED TESTS FOR EXACT ALGORITHMS START");

            Dictionary<string, List<double>> timings = new Dictionary<string, List<double>>
            {
                { Storage.MAXIMUM_COMMON_SUBGRAPH_EXACT, new List<double>() },
                { Storage.MINIMAL_COMMON_SUPERGRAPH_EXACT, new List<double>() }
            };

            RunTestsAndSaveMeanResults(timings, examples, Storage.EXACT_MATRIX_SIZES);

            string json = JsonSerializer.Serialize(timings);
            File.WriteAllText(Storage.GetPathToOutput("ExactAlgsSpeedTestsResults.txt"), json, Encoding.UTF8);

            return timings;
        }

        public static Dictionary<string, List<double>> RunSpeedTestsForApproximateAlgorithms(List<(Matrix, Matrix)> examples)
        {
            Console.WriteLine("SPEED TESTS FOR APPROXIMATE ALGORITHMS START");

            Dictionary<string, List<double>> timings = new Dictionary<string, List<double>>
            {
                { Storage.MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, new List<double>() },
                { Storage.MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, new List<double>() }
            };

            RunTestsAndSaveMeanResults(timings, examples, Storage.APPROX_MATRIX_SIZES);

            string json = JsonSerializer.Serialize(timings);
            File.WriteAllText(Storage.GetPathToOutput("ApproxAlgsSpeedTestsResults.txt"), json, Encoding.UTF8);

            return timings;
        }

        private static void RunTestsAndSaveMeanResults(Dictionary<string, List<double>> timings, List<(Matrix, Matrix)> examples, int[] matrixSizes)
        {
            Stopwatch stopwatch = new Stopwatch();

            foreach (int size in matrixSizes)
            {
                Console.WriteLine($"SPEED TESTS FOR SIZE {size}");

                List<double> partialTimings = new List<double>();
                List<(Matrix, Matrix)> matrices = examples.Where(e => e.Item1.VerticesNumber == size).ToList();

                foreach (string algorithm in timings.Keys)
                {
                    foreach ((Matrix, Matrix) example in matrices)
                    {
                        Matrix firstMatrixCopy = new Matrix(example.Item1.Graph);
                        Matrix secondMatrixCopy = new Matrix(example.Item2.Graph);
                        stopwatch.Start();
                        Storage.AllFunctions[algorithm](firstMatrixCopy, secondMatrixCopy, true);
                        stopwatch.Stop();
                        partialTimings.Add(stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();
                    }
                    timings[algorithm].Add(partialTimings.Average());
                    partialTimings.Clear();
                }
            }
        }

        // -------------
        public static Dictionary<string, List<double>> RunSpeedTestsForExactAlgorithms(int minSize, int maxSize)
        {
            Console.WriteLine("SPEED TESTS FOR EXACT ALGORITHMS START");

            Dictionary<string, List<double>> timings = new Dictionary<string, List<double>>
            {
                { Storage.MAXIMUM_COMMON_SUBGRAPH_EXACT, new List<double>() },
                { Storage.MINIMAL_COMMON_SUPERGRAPH_EXACT, new List<double>() }
            };

            RunTestsAndSaveMeanResults(timings, minSize, maxSize);

            string json = JsonSerializer.Serialize(timings);
            File.WriteAllText(Storage.GetPathToOutput("ExactAlgsSpeedTestsResults.txt"), json, Encoding.UTF8);

            return timings;
        }

        public static Dictionary<string, List<double>> RunSpeedTestsForApproximateAlgorithms(int minSize, int maxSize)
        {
            Console.WriteLine("SPEED TESTS FOR APPROXIMATE ALGORITHMS START");

            Dictionary<string, List<double>> timings = new Dictionary<string, List<double>>
            {
                { Storage.MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, new List<double>() },
                { Storage.MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, new List<double>() }
            };

            RunTestsAndSaveMeanResults(timings, minSize, maxSize);

            string json = JsonSerializer.Serialize(timings);
            File.WriteAllText(Storage.GetPathToOutput("ApproxAlgsSpeedTestsResults.txt"), json, Encoding.UTF8);

            return timings;
        }

        private static void RunTestsAndSaveMeanResults(Dictionary<string, List<double>> timings, int minSize, int maxSize)
        {
            Stopwatch stopwatch = new Stopwatch();

            for (int size = minSize; size <= maxSize; size++)
            {
                Console.WriteLine($"SPEED TESTS FOR SIZE {size}");

                List<double> partialTimings = new List<double>();
                List<(Matrix, Matrix)> matrices = Tester.GimmieSomeMatrices(size);

                foreach (string algorithm in timings.Keys)
                {
                    foreach ((Matrix, Matrix) example in matrices)
                    {
                        Matrix firstMatrixCopy = new Matrix(example.Item1.Graph);
                        Matrix secondMatrixCopy = new Matrix(example.Item2.Graph);
                        stopwatch.Start();
                        Storage.AllFunctions[algorithm](firstMatrixCopy, secondMatrixCopy, true);
                        stopwatch.Stop();
                        //stopwatch.ElapsedMilliseconds();
                        stopwatch.Reset();
                    }
                    timings[algorithm].Add(partialTimings.Average());
                    partialTimings.Clear();
                }
            }
        }
    }
}
