using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class Storage
    {
        // Sizes for tests.
        public static readonly int[] ALL_MATRIX_SIZES = new int[]
        {
            4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95
        };
        public static readonly int[] EXACT_MATRIX_SIZES = new int[]
        {
            4, 5, 6, 7, 8, 9, 10
        };
        public static readonly int[] APPROX_MATRIX_SIZES = new int[]
        {
            10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95
        };
        // Sizes to save on disk.
        public static readonly int[] ALL_ISO_MATRIX_SIZES = new int[]
        {
            6, 8, 15, 20
        };
        public static readonly int[] EXACT_ISO_MATRIX_SIZES = new int[]
        {
            6, 8
        };
        public static readonly int[] APPROX_ISO_MATRIX_SIZES = new int[]
        {
            15, 20
        };
        public static readonly (int, int)[] ALL_ISO_SUB_MATRIX_SIZES = new (int, int)[]
        {
            (6, 4), (8, 7), (9, 5), (15, 11), (20, 15)
        };
        public static readonly (int, int)[] EXACT_ISO_SUB_MATRIX_SIZES = new (int, int)[]
        {
            (6, 4), (8, 7), (9, 5)
        };
        public static readonly (int, int)[] APPROX_ISO_SUB_MATRIX_SIZES = new (int, int)[]
        {
            (15, 11), (20, 15)
        };
        public static readonly (int, int)[] ALL_RANDOM_SUB_MATRIX_SIZES = new (int, int)[]
        {
            (4, 3), (5, 5), (6, 4), (7, 7), (8, 5), (9, 7), (10, 6),
            (15, 12), (16, 11), (17, 17), (18, 17), (20, 18), (20, 20), (22, 14), (24, 19), (25, 21), (25, 25), (26, 15), (28, 20), (30, 24),
            (31, 31), (33, 24), (35, 29), (38, 32), (40, 31), (50, 40), (55, 51), (60, 55), (65, 46), (70, 56), (75, 61), (80, 80), (85, 69),
            (90, 74), (95, 79)
        };
        public static readonly (int, int)[] EXACT_RANDOM_SUB_MATRIX_SIZES = new (int, int)[]
        {
            (4, 3), (5, 5), (6, 4), (7, 7), (8, 5), (9, 7), (10, 6)
        };
        public static readonly (int, int)[] APPROX_RANDOM_SUB_MATRIX_SIZES = new (int, int)[]
        {
            (15, 12), (16, 11), (17, 17), (18, 17), (20, 18), (20, 20), (22, 14), (24, 19), (25, 21), (25, 25), (26, 15), (28, 20), (30, 24),
            (31, 31), (33, 24), (35, 29), (38, 32), (40, 31), (50, 40), (55, 51), (60, 55), (65, 46), (70, 56), (75, 61), (80, 80), (85, 69),
            (90, 74), (95, 79)
        };
        // -------------------------------
        public const string MAXIMUM_COMMON_SUBGRAPH_EXACT = "Maximum Common Subgraph Exact";
        public const string MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE = "Maximum Common Subgraph Approximate";
        public const string MINIMAL_COMMON_SUPERGRAPH_EXACT = "Minimal Common Supergraph Exact";
        public const string MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE = "Minimal Common Supergraph Approximate";

        public const string EXAMPLES_DIRECTORY = "../../../Tester/Examples/";
        public const string OUTPUT_DIRECTORY = "../../../Tester/Output/";

        public static readonly Dictionary<string, Func<Matrix, Matrix, bool, Matrix>> AllFunctions =
            new Dictionary<string, Func<Matrix, Matrix, bool, Matrix>>
        {
            { MAXIMUM_COMMON_SUBGRAPH_EXACT, Algorithm.FindMaximalSubGraph },
            { MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, Algorithm.FindMaximalSubGraphApproximate },
            { MINIMAL_COMMON_SUPERGRAPH_EXACT, Algorithm.FindMinimalSuperGraph },
            { MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, Algorithm.FindMinimalSuperGraphApproximate }
        };

        public static readonly Dictionary<string, Func<Matrix, Matrix, bool, Matrix>> ExactFunctions =
            new Dictionary<string, Func<Matrix, Matrix, bool, Matrix>>
        {
            { MAXIMUM_COMMON_SUBGRAPH_EXACT, Algorithm.FindMaximalSubGraph },
            { MINIMAL_COMMON_SUPERGRAPH_EXACT, Algorithm.FindMinimalSuperGraph }
        };

        public static readonly Dictionary<string, Func<Matrix, Matrix, bool, Matrix>> ApproximateFunctions =
            new Dictionary<string, Func<Matrix, Matrix, bool, Matrix>>
        {
            { MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE, Algorithm.FindMaximalSubGraphApproximate },
            { MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE, Algorithm.FindMinimalSuperGraphApproximate }
        };

        public static string GetPathToOutput(string fileName)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"{OUTPUT_DIRECTORY}{fileName}";
        }

        public static string GetPathToExamples(string fileName)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"{EXAMPLES_DIRECTORY}{fileName}";
        }
    }
}
