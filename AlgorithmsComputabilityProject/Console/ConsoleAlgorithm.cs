using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public static class ConsoleAlgorithm
    {
        /// <summary>
        /// Returns all information needed to print the results in a presentable way:
        /// The maximal common subgraph of two matrices A and B, the permutation of greater matrix for which
        /// the solution was found, the smaller of two matrices and indexes (x,y) of the greater matrix  
        /// at which the smaller matrix should be compared to the greater matrix.
        /// </summary>
        public static PrintModel FindMaximalSubGraphConsole(Matrix A, Matrix B)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }

            PrintModel results = new PrintModel();
            results.SmallerGraph = B;
            int maxCommonEdges = 0;
            foreach (IsomorphicGeneratorEnumeratorData data in new IsomorphicGenerator(A))
            {
                //M.Print();
                for (int x = 0; x <= data.PermutedMatrix.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= data.PermutedMatrix.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix subMatrix = data.PermutedMatrix.GetSubMatrix(x, y, B.VerticesNumber);
                        Matrix commonMatrix = Matrix.FindCommonMatrix(subMatrix, B);
                        if (commonMatrix.EdgesNumber > maxCommonEdges)
                        {
                            maxCommonEdges = commonMatrix.EdgesNumber;
                            results.ResultGraph = commonMatrix;
                            results.GreaterGraph = data.PermutedMatrix;
                            results.X = x;
                            results.Y = y;
                        }
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Returns all information needed to print the results in a presentable way:
        /// The minimal common supergraph of two matrices A and B, the permutation of greater matrix for which
        /// the solution was found, the smaller of two matrices and indexes (x,y) of the greater matrix  
        /// at which the smaller matrix should be compared to the greater matrix.
        /// </summary>
        public static PrintModel FindMinimalSuperGraphConsole(Matrix A, Matrix B)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }

            PrintModel results = new PrintModel();
            results.SmallerGraph = B;
            int minCommonEdges = int.MaxValue;
            foreach (IsomorphicGeneratorEnumeratorData data in new IsomorphicGenerator(A))
            {
                for (int x = 0; x <= data.PermutedMatrix.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= data.PermutedMatrix.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix newMatrix = new Matrix(data.PermutedMatrix.Graph);
                        newMatrix.InsertEdgesToMatrixAt(x, y, B);
                        if (newMatrix.EdgesNumber < minCommonEdges)
                        {
                            minCommonEdges = newMatrix.EdgesNumber;
                            results.ResultGraph = newMatrix;
                            results.GreaterGraph = data.PermutedMatrix;
                            results.X = x;
                            results.Y = y;
                        }
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Approximate version of maximal common subgraph algorithm returning all information
        /// needed for printing
        /// </summary>
        public static PrintModel FindMaximalSubGraphApproximateConsole(Matrix A, Matrix B, bool sort = true)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }
            Matrix sortedA = new Matrix(A.Graph);
            Matrix sortedB = new Matrix(B.Graph);
            if (sort)
            {
                sortedA.TransformToSortedForm();
                sortedB.TransformToSortedForm();
            }

            PrintModel results = new PrintModel();
            results.GreaterGraph = sortedA;
            results.SmallerGraph = sortedB;
            int maxCommonEdges = 0;
            for (int x = 0; x <= sortedA.VerticesNumber - sortedB.VerticesNumber; x++)
            {
                for (int y = 0; y <= sortedA.VerticesNumber - sortedB.VerticesNumber; y++)
                {
                    Matrix subMatrix = sortedA.GetSubMatrix(x, y, sortedB.VerticesNumber);
                    Matrix commonMatrix = Matrix.FindCommonMatrix(subMatrix, sortedB);
                    if (commonMatrix.EdgesNumber > maxCommonEdges)
                    {
                        maxCommonEdges = commonMatrix.EdgesNumber;
                        results.ResultGraph = commonMatrix;
                        results.X = x;
                        results.Y = y;
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Approximate version of maximal common subgraph algorithm returning all information
        /// needed for printing
        /// </summary>
        public static PrintModel FindMinimalSuperGraphApproximateConsole(Matrix A, Matrix B, bool sort = true)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }
            Matrix sortedA = new Matrix(A.Graph);
            Matrix sortedB = new Matrix(B.Graph);
            if (sort)
            {
                sortedA.TransformToSortedForm();
                sortedB.TransformToSortedForm();
            }

            PrintModel results = new PrintModel();
            results.GreaterGraph = sortedA;
            results.SmallerGraph = sortedB;
            int minCommonEdges = int.MaxValue;
            for (int x = 0; x <= sortedA.VerticesNumber - sortedB.VerticesNumber; x++)
            {
                for (int y = 0; y <= sortedA.VerticesNumber - sortedB.VerticesNumber; y++)
                {
                    Matrix newMatrix = new Matrix(sortedA.Graph);
                    newMatrix.InsertEdgesToMatrixAt(x, y, sortedB);
                    if (newMatrix.EdgesNumber < minCommonEdges)
                    {
                        minCommonEdges = newMatrix.EdgesNumber;
                        results.ResultGraph = newMatrix;
                        results.X = x;
                        results.Y = y;
                    }
                }
            }
            return results;
        }


        /// <summary>
        /// Returns 3 matrices: the matrix resulting from subgraph algorithms, smaller graph and the permutation of 
        /// the larger graph for which solution was found. All returned graphs have edges marked as follows:
        /// 1 - edges from first (smaller) graph
        /// 2 - edges from second (larger) graph
        /// 3 - edge present in both input graphs
        /// We assume V(G2) >= V(G1) == V(GResult)
        /// </summary>
        public static Tuple<Matrix, Matrix, Matrix> MarkCommonSubgraphEdges(PrintModel model)
        {
            Matrix G1copy = new Matrix(model.SmallerGraph.Graph);
            Matrix G2copy = new Matrix(model.GreaterGraph.Graph);
            Matrix ResultCopy = new Matrix(model.ResultGraph.Graph);

            for (int i = 0; i < G2copy.VerticesNumber; i++)
            {
                for (int j = 0; j < G2copy.VerticesNumber; j++)
                {
                    if (i >= model.X && i < model.X + G1copy.VerticesNumber && 
                        j >= model.Y && j < model.Y + G1copy.VerticesNumber)
                    {
                        if (G1copy[i - model.X, j - model.Y] == 1 && G2copy[i, j] == 1 &&
                            ResultCopy[i - model.X, j - model.Y] == 1)
                        {
                            G1copy[i - model.X, j - model.Y] = 3;
                            G2copy[i, j] = 3;
                            ResultCopy[i - model.X, j - model.Y] = 3;
                        }
                        else if (G1copy[i - model.X, j - model.Y] == 0 && G2copy[i, j] == 1 &&
                            ResultCopy[i - model.X, j - model.Y] == 1)
                        {
                            G2copy[i, j] = 2;
                            ResultCopy[i - model.X, j - model.Y] = 2;
                        }
                        else if (G1copy[i - model.X, j - model.Y] == 0 && G2copy[i, j] == 1 &&
                            ResultCopy[i - model.X, j - model.Y] == 0)
                        {
                            G2copy[i, j] = 2;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if (G2copy[i, j] == 1)
                            G2copy[i, j] = 2;
                    }
                }
            }
            return new Tuple<Matrix, Matrix, Matrix>(G1copy, G2copy, ResultCopy);
        }

        /// <summary>
        /// Returns 3 matrices: the matrix resulting from supergraph algorithms, smaller graph and the permutation of 
        /// the larger graph for which solution was found. All returned graphs have edges marked as follows:
        /// 1 - edges from first (smaller) graph
        /// 2 - edges from second (larger) graph
        /// 3 - edge present in both input graphs
        /// We assume V(G2) == V(GResult) >= V(G1)
        /// </summary>
        /// <returns></returns>
        public static Tuple<Matrix, Matrix, Matrix> MarkCommonSupergraphEdges(PrintModel model)
        {
            Matrix G1 = new Matrix(model.SmallerGraph.Graph);
            Matrix G2 = new Matrix(model.GreaterGraph.Graph);
            Matrix Result = new Matrix(model.ResultGraph.Graph);

            for (int i = 0; i < G2.VerticesNumber; i++)
            {
                for (int j = 0; j < G2.VerticesNumber; j++)
                {
                    if (i >= model.X && i < model.X + G1.VerticesNumber &&
                        j >= model.Y && j < model.Y + G1.VerticesNumber)
                    {
                        if (G1[i - model.X, j - model.Y] == 1 && G2[i, j] == 1 && Result[i, j] == 1)
                        {
                            G1[i - model.X, j - model.Y] = 3;
                            G2[i, j] = 3;
                            Result[i, j] = 3;
                        }
                        else if (G1[i - model.X, j - model.Y] == 0 && G2[i, j] == 1 &&  Result[i, j] == 1)
                        {
                            G2[i, j] = 2;
                            Result[i, j] = 2;
                        }
                        else if (G1[i - model.X, j - model.Y] == 0 && G2[i, j] == 1 && Result[i, j] == 0)
                        {
                            G2[i, j] = 2;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if (G2[i, j] == 1)
                        {
                            G2[i, j] = 2;
                            if (Result[i, j] == 1)
                                Result[i, j] = 2;
                        }
                    }
                }
            }
            return new Tuple<Matrix, Matrix, Matrix>(G1, G2, Result);
        }
    }
}
