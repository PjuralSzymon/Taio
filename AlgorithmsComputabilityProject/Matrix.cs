using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public class Matrix
    {
        public int VerticesNumber { get; set; }
        public int EdgesNumber
        {
            get { return CountEdges(this); }
        }
        public int[][] Graph { get; set; }
        public int[] VertexOrder { get; private set; }

        public int this[int i, int j]
        {
            get { return Graph[i][j]; }
            set
            {
                if (Graph[i][j] == value)
                    return;
                Graph[i][j] = value;
            }
        }

        public Matrix(int[][] graph)
        {
            InitializeGraph(graph);
        }

        private void InitializeGraph(int[][] graph)
        {
            int rowCount = graph.Length;
            foreach (int[] row in graph)
            {
                if (row.Length != rowCount)
                {
                    throw new ArgumentException("ERROR: Matrix dimensions must be equal");
                }
            }
            int[] order = new int[rowCount];
            int[][] data = new int[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                data[i] = new int[rowCount];
                graph[i].CopyTo(data[i], 0);
                order[i] = i;
            }

            Graph = data;
            VerticesNumber = rowCount;
            VertexOrder = order;
            //EdgesNumber = CountOnes(this);
        }
        public static int CountEdges(Matrix M)
        {
            int counter = 0;
            for (int i = 0; i < M.VerticesNumber; i++)
            {
                for (int j = 0; j < M.VerticesNumber; j++)
                {
                    if (M[i, j] != 0)
                        counter += 1;
                }
            }
            return counter;
        }

        public Matrix GetSubMatrix(int startIndexX, int startIndexY, int size)
        {
            int[][] newGraph = new int[size][];
            InitializeArrays(newGraph, size);

            for (int i = startIndexX; i < startIndexX + size; i++)
            {
                for (int j = startIndexY; j < startIndexY + size; j++)
                {
                    newGraph[i - startIndexX][j - startIndexY] = Graph[i][j];
                }
            }

            return new Matrix(newGraph);
        }

        public static int CountCommonEdges(Matrix A, Matrix B)
        {
            if (A.VerticesNumber != B.VerticesNumber)
            {
                throw new ArgumentException("ERROR: Matrix A and B must have the same size");
            }

            int counter = 0;
            for (int i = 0; i < A.VerticesNumber; i++)
            {
                for (int j = 0; j < A.VerticesNumber; j++)
                {
                    if ((A[i, j] == B[i, j]) && (B[i, j] == 1)) counter++;
                }
            }

            return counter;
        }

        public static Matrix FindCommonMatrix(Matrix A, Matrix B)
        {
            for (int i = 0; i < A.VerticesNumber; i++)
            {
                for (int j = 0; j < A.VerticesNumber; j++)
                {
                    if ((A[i, j] == 1) && (B[i, j] == 0)) A[i, j] = 0;
                }
            }
            //A.EdgesNumber = CountOnes(A);
            return A;
        }


        // Helper functions
        public void SwapColumn(int columnA, int columnB)
        {
            int[] colB = Graph.Select(row => row[columnB]).ToArray();

            for (int i = 0; i < Graph.Length; i++)
            {
                Graph[i][columnB] = Graph[i][columnA];
                Graph[i][columnA] = colB[i];
            }
        }

        public void SwapRow(int rowA, int rowB)
        {
            int[] temp = Graph[rowB];
            Graph[rowB] = Graph[rowA];
            Graph[rowA] = temp;
        }

        private void SwapVertexOrder(int a, int b)
        {
            int tmp = VertexOrder[a];
            VertexOrder[a] = VertexOrder[b];
            VertexOrder[b] = tmp;
        }

        public void InsertEdgesToMatrixAt(int x, int y, Matrix M)
        {
            for (int i = x; i < VerticesNumber && i - x < M.VerticesNumber; i++)
            {
                for (int j = y; j < VerticesNumber && j - y < M.VerticesNumber; j++)
                {
                    if (M.Graph[i - x][j - y] == 1)
                    {
                        Graph[i][j] = M.Graph[i - x][j - y];
                    }
                }
            }
        }

        public static void InitializeArrays(int[][] graph, int size)
        {
            for (int i = 0; i < size; i++)
            {
                graph[i] = new int[size];
            }
        }

        public void TransformToSortedForm()
        {
            for (int i = 0; i < VerticesNumber; i++)
            {
                for (int j = 0; j < VerticesNumber; j++)
                {
                    if (GetVertexDegreeRow(i) + GetVertexDegreeCol(i) > GetVertexDegreeRow(j) + GetVertexDegreeCol(j))
                    {
                        SwapColumn(i, j);
                        SwapRow(i, j);
                        SwapVertexOrder(i, j);
                    }
                }
            }
        }

        public static Matrix GetIsomorphism(Matrix A)
        {
            Matrix isomorphism = new Matrix(A.Graph);
            int numberOfVerticesToSwap = A.VerticesNumber / 3;
            List<int> usedIndices = new List<int>();
            while (numberOfVerticesToSwap > 0)
            {
                int firstIndex = GetUniqueRandomNumber(usedIndices, A.VerticesNumber);
                usedIndices.Add(firstIndex);
                int secondIndex = GetUniqueRandomNumber(usedIndices, A.VerticesNumber);
                usedIndices.Add(secondIndex);
                isomorphism.SwapColumn(firstIndex, secondIndex);
                isomorphism.SwapRow(firstIndex, secondIndex);
                numberOfVerticesToSwap--;
            }
            return isomorphism;
        }

        public static Matrix GetIsomorphicSubgraph(Matrix A, int subgraphSize)
        {
            if (subgraphSize > A.VerticesNumber)
            {
                throw new InvalidOperationException("Subgraph can't be bigger than the supergraph.");
            }

            int[][] newGraph = new int[subgraphSize][];
            for (int i = 0; i < subgraphSize; i++)
            {
                newGraph[i] = new int[subgraphSize];
                Array.Copy(A.Graph[i], newGraph[i], subgraphSize);
            }

            Matrix subGraph = new Matrix(newGraph);
            return Matrix.GetIsomorphism(subGraph);
        }

        private static int GetUniqueRandomNumber(List<int> usedNumbers, int maximum)
        {
            Random random = new Random();
            int num = random.Next(0, maximum);
            while (usedNumbers.Contains(num))
            {
                num = random.Next(0, maximum);
            }
            return num;
        }

        private int GetVertexDegreeRow(int id)
        {
            int sum = 0;
            for (int x = 0; x < VerticesNumber; x++)
            {
                sum += Graph[id][x];
            }
            return sum;
        }

        private int GetVertexDegreeCol(int id)
        {
            int sum = 0;
            for (int x = 0; x < VerticesNumber; x++)
            {
                sum += Graph[x][id];
            }
            return sum;
        }


        // Printing
        public static void PrintGraphs(Matrix G1, Matrix G2)
        {
            if (G1.VerticesNumber < G2.VerticesNumber)
            {
                Matrix tmp = G2;
                G2 = G1;
                G1 = tmp;
            }
            string g1 = "  G1", g2 = "  G2";
            string edge1 = $"  Edges: {G1.EdgesNumber}";
            string edge2 = $"  Edges: {G2.EdgesNumber}";
            int separator = 8;
            Console.WriteLine(g1.PadRight(G1.VerticesNumber * 3) + "".PadLeft(separator) + g2);

            int maxVertices = G1.VerticesNumber > G2.VerticesNumber ? G1.VerticesNumber : G2.VerticesNumber;
            for (int i = 0; i < maxVertices; i++)
            {
                for (int j = 0; j < G1.VerticesNumber; j++)
                {
                    if (i < G1.VerticesNumber)
                    {
                        if (G1[i, j] == 0) PrintColorEdge(0);
                        else PrintColorEdge(2);
                    }
                    else Console.Write("   ");
                }
                Console.Write("".PadLeft(separator));

                for (int j = 0; j < G2.VerticesNumber; j++)
                {
                    if (i < G2.VerticesNumber)
                    {
                        if (G2[i, j] == 0) PrintColorEdge(0);
                        else PrintColorEdge(1);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void PrintResults(PrintModel results, string resultType)
        {
            int separator = 8;
            string suffix1 = " sorted", suffix2 = " original";
            if (resultType.Contains("Exact"))
                suffix1 = " permuted";
            else if (resultType.Contains("Approximate") && results.Sorting == true)
                suffix2 = suffix1;
            else
                suffix1 = suffix2;

            string g1 = "  G1" + suffix1, g2 = "  G2" + suffix2;
            string prefix = " ";
            int diff = 0, prefix2 = 0;
            if (g1.Length > results.LargerGraph.VerticesNumber * 3)
                diff = g1.Length - (results.LargerGraph.VerticesNumber * 3);
            int[] largeOrder = results.LargerGraphVertexOrder.Select(x => x += 1).ToArray();
            int[] smallOrder = results.SmallerGraphVertexOrder.Select(x => x += 1).ToArray();
            if (largeOrder[0] > 9) prefix = "";
            if (smallOrder[0] > 9) prefix2 = 1;

            Console.WriteLine();
            Console.WriteLine(g1.PadRight(results.LargerGraph.VerticesNumber * 3) + "".PadLeft(separator - diff) + g2);
            Console.Write(prefix + "(" + JoinVertexOrder(largeOrder) + ")");
            Console.WriteLine("".PadLeft(separator - prefix2) + "(" + JoinVertexOrder(smallOrder) + ")");
            Console.WriteLine("".PadLeft(results.LargerGraph.VerticesNumber * 3 + 1, '-') + "".PadLeft(separator) + "".PadLeft(results.SmallerGraph.VerticesNumber * 3, '-'));

            int maxVertices = results.LargerGraph.VerticesNumber;
            for (int i = 0; i < maxVertices; i++)
            {
                for (int j = 0; j < results.LargerGraph.VerticesNumber; j++)
                {
                    if (i < results.LargerGraph.VerticesNumber)
                        PrintColorEdge(results.LargerGraph[i, j]);
                    else Console.Write("   ");
                }
                Console.Write("".PadLeft(separator));

                for (int j = 0; j < results.SmallerGraph.VerticesNumber; j++)
                {
                    if (i < results.SmallerGraph.VerticesNumber)
                        PrintColorEdge(results.SmallerGraph[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("  " + resultType);
            Console.WriteLine("  EDGES: " + results.ResultGraph.EdgesNumber);
            for (int i = 0; i < results.ResultGraph.VerticesNumber; i++)
            {
                for (int j = 0; j < results.ResultGraph.VerticesNumber; j++)
                {
                    PrintColorEdge(results.ResultGraph[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void PrintColorEdge(int edgeType)
        {
            int edgeValue = 1;
            if (edgeType == 0) edgeValue = 0;

            switch (edgeType)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 1: //graph 1 edges
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 2: //graph 2 edges
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 3: //common edges
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.Write($"  {edgeValue}");
            Console.ResetColor();
        }

        private static string JoinVertexOrder(int[] order)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < order.Length; i++)
            {
                output.Append(order[i]);
                if(i < order.Length - 1)
                {
                    if (order[i] < 10 && order[i + 1] < 10) output.Append("  ");
                    else if (order[i] >= 10 && order[i + 1] < 10) output.Append("  ");
                    else output.Append(' ');
                }
            }
            return output.ToString();
        }
    }
}
