using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class Storage
    {
        public static readonly int[] MATRIX_SIZES = new int[]
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
        public const string MAXIMUM_COMMON_SUBGRAPH_EXACT = "Maximum Common Subgraph Exact";
        public const string MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE = "Maximum Common Subgraph Approximate";
        public const string MINIMAL_COMMON_SUPERGRAPH_EXACT = "Minimal Common Supergraph Exact";
        public const string MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE = "Minimal Common Supergraph Approximate";

        public const string EXAMPLES_DEVELOPMENT_DIRECTORY = "../../../Tester/Examples/";
        public const string EXAMPLES_PRODUCTION_DIRECTORY = "Examples/";

        public const string OUTPUT_DEVELOPMENT_DIRECTORY = "../../../Tester/Output/";
        public const string OUTPUT_PRODUCTION_DIRECTORY = "Output/";

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
            return System.AppDomain.CurrentDomain.BaseDirectory + $"{OUTPUT_DEVELOPMENT_DIRECTORY}{fileName}";
            //return Directory.GetCurrentDirectory() + $"/../../../Tester/Output/{fileName}";
        }

        public static string GetPathToExamples(string fileName)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"{EXAMPLES_DEVELOPMENT_DIRECTORY}{fileName}";
            //return Directory.GetCurrentDirectory() + $"/../../../Tester/Examples/{fileName}";
        }

        public static string GetPathToOutputProd(string fileName)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"{OUTPUT_PRODUCTION_DIRECTORY}{fileName}";
            //return Directory.GetCurrentDirectory() + $"/../../../Tester/Output/{fileName}";
        }

        public static string GetPathToExamplesProd(string fileName)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"{EXAMPLES_PRODUCTION_DIRECTORY}{fileName}";
            //return Directory.GetCurrentDirectory() + $"/../../../Tester/Examples/{fileName}";
        }
    }
}
