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

            Matrix biggestSubGraph = null;
            int maxCommonEdges = 0;
            foreach (Matrix M in new IsomorphicGenerator(A))
            {
                //M.Print();
                for (int x = 0; x <= M.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= M.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix subMatrix = M.GetSubMatrix(x, y, B.VerticesNumber);
                        Matrix commonMatrix = Matrix.FindCommonMatrix(subMatrix, B);
                        if (commonMatrix.EdgesNumber > maxCommonEdges)
                        {
                            maxCommonEdges = commonMatrix.EdgesNumber;
                            biggestSubGraph = commonMatrix;
                        }
                    }
                }
            }
            return biggestSubGraph;
        }

        public static Matrix FindMinimalSuperGraph(Matrix A, Matrix B)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }

            Matrix SmallestSuperGraph = null;
            int minCommonEdges = int.MaxValue;
            foreach (Matrix M in new IsomorphicGenerator(A))
            {
                for (int x = 0; x <= M.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= M.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix newMatrix = new Matrix(M.Graph);
                        newMatrix.InsertOnesToMatrixAt(x, y, B);
                        if (newMatrix.EdgesNumber < minCommonEdges)
                        {
                            minCommonEdges = newMatrix.EdgesNumber;
                            SmallestSuperGraph = newMatrix;
                        }
                    }
                }
            }
            return SmallestSuperGraph;
        }

        //Approximate:

        public static Matrix FindMaximalSubGraphApproximate(Matrix A, Matrix B)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }
            A.TransformToSortedForm();
            B.TransformToSortedForm();
            Matrix biggestSubGraph = null;
            int maxCommonEdges = 0;
            for (int x = 0; x <= A.VerticesNumber - B.VerticesNumber; x++)
            {
                for (int y = 0; y <= A.VerticesNumber - B.VerticesNumber; y++)
                {
                    Matrix subMatrix = A.GetSubMatrix(x, y, B.VerticesNumber);
                    Matrix commonMatrix = Matrix.FindCommonMatrix(subMatrix, B);
                    if (commonMatrix.EdgesNumber > maxCommonEdges)
                    {
                        maxCommonEdges = commonMatrix.EdgesNumber;
                        biggestSubGraph = commonMatrix;
                    }
                }

            }
            return biggestSubGraph;
        }
    }
}
