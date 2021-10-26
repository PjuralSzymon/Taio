using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public static class Algorithm
    {
        public static Matrix FindMaximalSubGraph(Matrix A, Matrix B)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }

            Matrix BiggestSubGraph = null;
            int maxCommonEdges = 0;
            foreach (Matrix M in new IsomorphicGenerator(B))
            {
                for (int x = 0; x <= A.VerticesNumber - M.VerticesNumber; x++)
                {
                    for (int y = 0; y <= A.VerticesNumber - M.VerticesNumber; y++)
                    {
                        Matrix subMatrix = A.GetSubMatrix(x, y, M.VerticesNumber);
                        subMatrix = Matrix.FindCommonMatrix(subMatrix, M);
                        if (subMatrix.EdgesNumber > maxCommonEdges)
                        {
                            maxCommonEdges = subMatrix.EdgesNumber;
                            BiggestSubGraph = subMatrix;
                        }
                    }
                }
            }
            return BiggestSubGraph;
        }

        public static Matrix FindMinimalSuperGraph(Matrix A, Matrix B)
        {
            return null;
        }
    }
}
