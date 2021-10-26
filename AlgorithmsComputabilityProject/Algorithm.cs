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
            foreach (Matrix M in new IsomorphicGenerator(A))
            {
                //M.Print();
                for (int x = 0; x <= M.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= M.VerticesNumber - B.VerticesNumber; y++)
                    {
                        //foreach (Matrix N in new IsomorphicGenerator(B))
                        //{
                        //    Matrix subMatrix = M.GetSubMatrix(x, y, N.VerticesNumber);
                        //    subMatrix = Matrix.FindCommonMatrix(subMatrix, N);
                        //    if (subMatrix.EdgesNumber > maxCommonEdges)
                        //    {
                        //        maxCommonEdges = subMatrix.EdgesNumber;
                        //        BiggestSubGraph = subMatrix;
                        //    }
                        //}
                        Matrix subMatrix = M.GetSubMatrix(x, y, B.VerticesNumber);
                        subMatrix = Matrix.FindCommonMatrix(subMatrix, B);
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
