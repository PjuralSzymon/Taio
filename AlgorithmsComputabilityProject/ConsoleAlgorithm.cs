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
                            results.ResultGraph = commonMatrix;
                            results.GreaterGraph = M;
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
            foreach (Matrix M in new IsomorphicGenerator(A))
            {
                for (int x = 0; x <= M.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= M.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix newMatrix = new Matrix(M.Graph);
                        newMatrix.InsertEdgesToMatrixAt(x, y, B);
                        if (newMatrix.EdgesNumber < minCommonEdges)
                        {
                            minCommonEdges = newMatrix.EdgesNumber;
                            results.ResultGraph = newMatrix;
                            results.GreaterGraph = M;
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
            Matrix smallGraph = new Matrix(model.SmallerGraph.Graph);
            Matrix largeGraph = new Matrix(model.GreaterGraph.Graph);
            Matrix ResultCopy = new Matrix(model.ResultGraph.Graph);

            for (int i = 0; i < largeGraph.VerticesNumber; i++)
            {
                for (int j = 0; j < largeGraph.VerticesNumber; j++)
                {
                    if (i >= model.X && i < model.X + smallGraph.VerticesNumber && 
                        j >= model.Y && j < model.Y + smallGraph.VerticesNumber)
                    {
                        if (smallGraph[i - model.X, j - model.Y] == 1 && largeGraph[i, j] == 1 &&
                            ResultCopy[i - model.X, j - model.Y] == 1)
                        {
                            smallGraph[i - model.X, j - model.Y] = 3;
                            largeGraph[i, j] = 3;
                            ResultCopy[i - model.X, j - model.Y] = 3;
                        }
                        else if (smallGraph[i - model.X, j - model.Y] == 0 && largeGraph[i, j] == 1 &&
                            ResultCopy[i - model.X, j - model.Y] == 1)
                        {
                            largeGraph[i, j] = 2;
                            ResultCopy[i - model.X, j - model.Y] = 2;
                        }
                        else if (smallGraph[i - model.X, j - model.Y] == 0 && largeGraph[i, j] == 1 &&
                            ResultCopy[i - model.X, j - model.Y] == 0)
                        {
                            largeGraph[i, j] = 2;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if (largeGraph[i, j] == 1)
                            largeGraph[i, j] = 2;
                    }
                }
            }
            return new Tuple<Matrix, Matrix, Matrix>(largeGraph, smallGraph, ResultCopy);
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
            Matrix smallGraph = new Matrix(model.SmallerGraph.Graph);
            Matrix largeGraph = new Matrix(model.GreaterGraph.Graph);
            Matrix Result = new Matrix(model.ResultGraph.Graph);

            for (int i = 0; i < largeGraph.VerticesNumber; i++)
            {
                for (int j = 0; j < largeGraph.VerticesNumber; j++)
                {
                    if (i >= model.X && i < model.X + smallGraph.VerticesNumber &&
                        j >= model.Y && j < model.Y + smallGraph.VerticesNumber)
                    {
                        if (smallGraph[i - model.X, j - model.Y] == 1 && largeGraph[i, j] == 1 && Result[i, j] == 1)
                        {
                            smallGraph[i - model.X, j - model.Y] = 3;
                            largeGraph[i, j] = 3;
                            Result[i, j] = 3;
                        }
                        else if (smallGraph[i - model.X, j - model.Y] == 0 && largeGraph[i, j] == 1 &&  Result[i, j] == 1)
                        {
                            largeGraph[i, j] = 2;
                            Result[i, j] = 2;
                        }
                        else if (smallGraph[i - model.X, j - model.Y] == 0 && largeGraph[i, j] == 1 && Result[i, j] == 0)
                        {
                            largeGraph[i, j] = 2;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if (largeGraph[i, j] == 1)
                        {
                            largeGraph[i, j] = 2;
                            if (Result[i, j] == 1)
                                Result[i, j] = 2;
                        }
                    }
                }
            }
            return new Tuple<Matrix, Matrix, Matrix>(largeGraph, smallGraph, Result);
        }
    }
}
