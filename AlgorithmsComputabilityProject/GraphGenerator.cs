using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public class GraphGenerator
    {
        public static Matrix getRandomMatrix(int size)
        {
            return new Matrix(getRandomGraph(size));
        }

        public static int[][] getRandomGraph(int size)
        {
            Random rnd = new Random();
            int[][] randGraph = new int[size][];
            Matrix.InitializeArrays(randGraph, size);
            for(int x=0;x<size;x++)
            {
                for(int y=0;y<size;y++)
                {
                    if(x!=y)
                    {
                        randGraph[x][y] = rnd.Next(0, 100) % 2;
                    }
                }
            }
            return randGraph;
        }
    }
}
