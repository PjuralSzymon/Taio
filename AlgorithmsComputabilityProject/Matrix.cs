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
            
            int[][] data = new int[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                data[i] = new int[rowCount];
                graph[i].CopyTo(data[i], 0);
            }

            Graph = data;
            VerticesNumber = rowCount;
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

            for(int i = startIndexX; i < startIndexX + size; i++)
            {
                for(int j = startIndexY; j < startIndexY + size; j++)
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
            for(int i = 0; i < A.VerticesNumber; i++)
            {
                for(int j = 0; j < A.VerticesNumber; j++)
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

        public void InsertEdgesToMatrixAt(int x, int y, Matrix M)
        {
            for(int i = x; i < VerticesNumber && i - x < M.VerticesNumber; i++)
            {
                for(int j = y; j < VerticesNumber && j - y < M.VerticesNumber; j++)
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
                for(int j = 0; j < VerticesNumber; j++)
                {
                    if (GetVertexDegreeRow(i) + GetVertexDegreeCol(i) > GetVertexDegreeRow(j) + GetVertexDegreeCol(j))
                    {
                        SwapColumn(i, j);
                        SwapRow(i, j);
                    }
                }
            }
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
            string g1 = "  G1", g2 = "  G2";
            int padLength = (G1.VerticesNumber * 3) - g1.Length;
            int separator = 10;
            Console.WriteLine(g1.PadRight(G1.VerticesNumber * 3) + "".PadLeft(separator) + g2);

            int maxVertices = G1.VerticesNumber > G2.VerticesNumber ? G1.VerticesNumber : G2.VerticesNumber;
            for (int i = 0; i < maxVertices; i++)
            {
                for (int j = 0; j < G1.VerticesNumber; j++)
                {
                    if (i < G1.VerticesNumber)
                    {
                        if (G1[i, j] == 0) PrintColorEdge(0);
                        else Console.Write($"  {1}");
                    }
                    else Console.Write("   ");
                }
                Console.Write("".PadLeft(separator));

                for (int j = 0; j < G2.VerticesNumber; j++)
                {
                    if (i < G2.VerticesNumber)
                    {
                        if (G2[i, j] == 0) PrintColorEdge(0);
                        else Console.Write($"  {1}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void PrintResults(Matrix G1, Matrix G2, Matrix result, string resultType)
        {
            string g1 = "  G1", g2 = "  G2";
            int separator = 10;
            Console.WriteLine();
            Console.WriteLine(g1.PadRight(G1.VerticesNumber * 3) + "".PadLeft(separator) + g2);

            int maxVertices = G1.VerticesNumber > G2.VerticesNumber ? G1.VerticesNumber : G2.VerticesNumber;
            for (int i = 0; i < maxVertices; i++)
            {
                for (int j = 0; j < G1.VerticesNumber; j++)
                {
                    if (i < G1.VerticesNumber)
                        PrintColorEdge(G1[i, j]);
                    else Console.Write("   ");
                }
                Console.Write("".PadLeft(separator));

                for (int j = 0; j < G2.VerticesNumber; j++)
                {
                    if (i < G2.VerticesNumber)
                        PrintColorEdge(G2[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("  " + resultType);

            for (int i = 0; i < result.VerticesNumber; i++)
            {
                for (int j = 0; j < result.VerticesNumber; j++)
                {
                    PrintColorEdge(result[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void PrintColorEdge(int edgeType)
        {
            int edgeValue = 1;
            if (edgeType == 0)  edgeValue = 0;

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
    }
}
