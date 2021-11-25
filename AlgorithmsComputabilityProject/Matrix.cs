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
            get
            {
                return CountOnes(this);
            }
        }
        public int[][] Graph { get; set; }

        public int this[int i, int j]
        {
            get
            { 
                return Graph[i][j];
            }
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

        public static int CountOnes(Matrix M)
        {
            int counter = 0;
            for(int i = 0; i < M.VerticesNumber; i++)
            {
                for (int j = 0; j < M.VerticesNumber; j++)
                {
                    if (M[i, j] != 0)
                        M[i, j] = 1;
                    counter += M.Graph[i][j];
                }
            }
            return counter;
        }

        public void Print(string graphType)
        {
            Console.WriteLine();
            Console.WriteLine("**** " + graphType.ToUpper() + " ****");
            Console.WriteLine(" Vertices: " + VerticesNumber);
            Console.WriteLine(" Edges: " + EdgesNumber);
            for (int i = 0; i < VerticesNumber; i++)
            {
                for (int j = 0; j < VerticesNumber; j++)
                {
                    Console.Write(Graph[i][j] + " ");
                }
                //Console.Write(" |" + GetVertexDegreeRow(i));
                Console.WriteLine();
            }
            Console.WriteLine(" ");
        }

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

        public void InsertOnesToMatrixAt(int x, int y, Matrix M)
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
            for(int x = 0; x < VerticesNumber; x++)
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
    }
}
