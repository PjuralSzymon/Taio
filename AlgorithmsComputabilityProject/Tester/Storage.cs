using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class Storage
    {
        public const string MAXIMUM_COMMON_SUBGRAPH_EXACT = "Maximum Common Subgraph Exact";
        public const string MAXIMUM_COMMON_SUBGRAPH_APPROXIMATE = "Maximum Common Subgraph Approximate";
        public const string MINIMAL_COMMON_SUPERGRAPH_EXACT = "Minimal Common Supergraph Exact";
        public const string MINIMAL_COMMON_SUPERGRAPH_APPROXIMATE = "Minimal Common Supergraph Approximate";

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

        public static string GetPath(string fileName)
        {
            return Directory.GetCurrentDirectory() + $"/../../../Tester/Output/{fileName}";
        }
    }
}
