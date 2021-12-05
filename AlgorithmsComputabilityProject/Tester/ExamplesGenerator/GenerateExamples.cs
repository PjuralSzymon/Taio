using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A, B - graphs. Types of graphs we have to generate AND save on disk:
// B isomorphic to A. Sizes: 6, 8, 15, 20.
// B isomorphic to subgraph in A. Sizes: (6, 4), (8, 7), (9, 5), (15, 11), (20, 15).
// Random graphs. Sizes: (4, _), (5, _), (6, _), (7, _), (8, _), (9, _), (10, _), (15, _), (20, _), (25, _), ..., (95, _).
// Also: Random graphs for speed and quality tests. We don't need to save them. Sizes:
// 4, 5, ..., 10. Each 5 times.
// 15, 20, ..., 95. Each 50 times.

namespace AlgorithmsComputabilityProject.Tester
{
    public static class GenerateExamples
    {
        public static List<(Matrix, Matrix)> GenerateIsomorphicExamplesWithEqualSizes()
        {
            List<(Matrix, Matrix)> generatedExamples = new List<(Matrix, Matrix)>();
            
            foreach (int size in Storage.ALL_ISO_MATRIX_SIZES)
            {
                Matrix A = GraphGenerator.GetRandomMatrix(size);
                Matrix B = Matrix.GetIsomorphism(A);
                generatedExamples.Add((A, B));
            }

            return generatedExamples;
        }

        public static List<(Matrix, Matrix)> GenerateIsomorphicExamplesWithDifferentSizes()
        {
            List<(Matrix, Matrix)> generatedExamples = new List<(Matrix, Matrix)>();

            foreach ((int, int) size in Storage.ALL_ISO_SUB_MATRIX_SIZES)
            {
                Matrix A = GraphGenerator.GetRandomMatrix(size.Item1);
                Matrix B = Matrix.GetIsomorphicSubgraph(A, size.Item2);
                generatedExamples.Add((A, B));
            }

            return generatedExamples;
        }

        public static List<(Matrix, Matrix)> GenerateRandomExamplesWithConcreteSizes()
        {
            List<(Matrix, Matrix)> matrices = new List<(Matrix, Matrix)>();
            foreach ((int, int) size in Storage.ALL_RANDOM_SUB_MATRIX_SIZES)
            {
                matrices.Add((GraphGenerator.GetRandomMatrix(size.Item1), GraphGenerator.GetRandomMatrix(size.Item2)));
            }
            return matrices;
        }

        public static List<(Matrix, Matrix)> GenerateRandomExamplesWithVaryingSizes(int maxSize, int numberOfMatrices = 3)
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

        public static List<(Matrix, Matrix)> GenerateRandomExamples(int sizeOfMatrices, int numberOfMatrices = 10)
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