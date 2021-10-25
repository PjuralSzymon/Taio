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
        public int EdgesNumber { get; set; }
        public int[,] Graph { get; set; }

        public int this[int i, int j]
        {
            get { return Graph[i, j]; }
            set 
            {
                if (Graph[i, j] == value)
                    return;
                Graph[i, j] = value; 
            }
        }

        public Matrix(int[,] graph)
        {
            InitializeGraph(graph);
        }

        private void InitializeGraph(int[,] graph)
        {
            if (graph.GetLength(0) != graph.GetLength(1))
            {
                throw new ArgumentException("ERROR: Matrix dimensions must be equal");
            }

            int[,] data = new int[graph.GetLength(0), graph.GetLength(1)];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    data[i, j] = graph[i, j];
                }
            }

            Graph = data;
            VerticesNumber = Graph.GetLength(0);
            EdgesNumber = CountOnes(this);
        }

        public Matrix GetSubMatrix(int startIndexX,int startIndexY, int size)
        {
            int[,] newGraph = new int[size, size];
            for(int i= startIndexX; i< startIndexX + size;i++)
            {
                for(int j= startIndexY; j< startIndexY + size;j++)
                {
                    newGraph[i - startIndexX, j - startIndexY] = Graph[i, j];
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
            for(int i=0;i<A.VerticesNumber;i++)
            {
                for(int j=0;j<A.VerticesNumber;j++)
                {
                    if ((A[i, j] == B[i, j]) && (B[i,j] == 1)) counter++;
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
            A.EdgesNumber = Matrix.CountOnes(A);
            return A;
        }
        public static int CountOnes(Matrix M)
        {
            int counter = 0;
            for(int i=0; i<M.VerticesNumber; i++)
                for(int j=0; j<M.VerticesNumber; j++)
                {
                    if (M[i, j] != 0) 
                        M[i, j] = 1;
                    counter += M.Graph[i, j];
                }
            return counter;
        }

        public void Print()
        {
            Console.WriteLine("Matrix vertices: " + VerticesNumber);
            Console.WriteLine("Matrix edges: " + EdgesNumber);
            for (int i = 0; i < VerticesNumber; i++)
            {
                for (int j = 0; j < VerticesNumber; j++)
                {
                    Console.Write(Graph[i, j] + " ");
                }
                Console.Write("\n");
            }
            Console.WriteLine(" ");
        }

        public void swapColumn(int from, int to)
        {

        }
        public void swapRow(int from, int to)
        {

        }
    }
}
