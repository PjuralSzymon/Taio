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
            if(A.VerticesNumber<B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }
            Matrix BiggestSubGraph = null;
            int maxCommonEdges = 0;
            // permutions ...
            for(int x =0; x<=A.VerticesNumber-B.VerticesNumber; x++)
            {
                for(int y=0;y<=A.VerticesNumber-B.VerticesNumber;y++)
                {
                    Matrix subMatrix = A.GetSubMatrix(x, y,B.VerticesNumber);
                    subMatrix = Matrix.FindCommonMatrix(subMatrix, B);
                    if (subMatrix.EdgesNumber > maxCommonEdges)
                    {
                        maxCommonEdges = subMatrix.EdgesNumber;
                        BiggestSubGraph = subMatrix;
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
