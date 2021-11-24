using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class SpeedTester
    {
        // Methods return dictionary in following form:
        // "Algorithm_Name": [ t for minSize, t for minSize + 1, ..., t for maxSize]
        // ...
        // Where t is mean running time in milliseconds

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
            File.WriteAllText(Storage.GetPath("ExactAlgsSpeedTestsResults.txt"), json);

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

            RunTestsAndSaveMeanResults(timings, minSize, maxSize, "milliseconds");

            string json = JsonSerializer.Serialize(timings);
            File.WriteAllText(Storage.GetPath("ApproxAlgsSpeedTestsResults.txt"), json);

            return timings;
        }

        private static void RunTestsAndSaveMeanResults(Dictionary<string, List<double>> timings, int minSize, int maxSize, string measure = "normal")
        {
            Stopwatch stopwatch = new Stopwatch();

            for (int size = minSize; size <= maxSize; size++)
            {
                Console.WriteLine($"SPEED TESTS FOR SIZE {size}");

                List<double> partialTimings = new List<double>();
                List<(Matrix, Matrix)> matrices = KoksTester3000.GimmieSomeMatrices(size);

                foreach (string algorithm in timings.Keys)
                {
                    foreach ((Matrix, Matrix) example in matrices)
                    {
                        stopwatch.Start();
                        Storage.AllFunctions[algorithm](example.Item1, example.Item2, true);
                        stopwatch.Stop();
                        // TO DO 
                        //partialTimings.Add(stopwatch.Elastopwatch.Elapsed.Seconds * 1000 + stopwatch.Elapsed.Milliseconds);
                        stopwatch.Reset();
                    }
                    timings[algorithm].Add(partialTimings.Average());
                    partialTimings.Clear();
                }
            }
        }
    }
}
