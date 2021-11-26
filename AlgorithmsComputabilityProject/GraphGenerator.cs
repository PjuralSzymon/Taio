using System;

namespace AlgorithmsComputabilityProject
{
    public static class GraphGenerator
    {
        public static Matrix GetRandomMatrix(int size)
        {
            return new Matrix(GetRandomGraph(size));
        }

        private static int[][] GetRandomGraph(int size)
        {
            Random rnd = new Random();
            int[][] randGraph = new int[size][];
            Matrix.InitializeArrays(randGraph, size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (x != y)
                    {
                        randGraph[x][y] = rnd.Next(0, 100) <= 70 ? 0 : 1;
                    }
                }
            }
            return randGraph;
        }
    }
}
