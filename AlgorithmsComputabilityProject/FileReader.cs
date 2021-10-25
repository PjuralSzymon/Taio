using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public static class FileReader
    {
        public static (Matrix, Matrix) Read(string pathToFile)
        {
            string[] lines = System.IO.File.ReadAllLines(pathToFile);
            int FirstMatrixSize = Int32.Parse(lines[0]);
            int SecondMatrixSize = Int32.Parse(lines[FirstMatrixSize + 1]);
            int[,] graph_1 = new int[FirstMatrixSize, FirstMatrixSize];
            int[,] graph_2 = new int[SecondMatrixSize, SecondMatrixSize];
            for (int i=1; i<FirstMatrixSize+1;i++)
            {
                string[] digits = lines[i].Split(' ');
                for(int j =0; j< digits.Length; j++)
                {
                    graph_1[i - 1, j] = Int32.Parse(digits[j]);
                }
            }
            for (int i = FirstMatrixSize+2; i < FirstMatrixSize + SecondMatrixSize + 2; i++)
            {
                string[] digits = lines[i].Split(' ');
                for (int j = 0; j < digits.Length; j++)
                {
                    graph_2[i - (FirstMatrixSize + 2), j] = Int32.Parse(digits[j]);
                }
            }
            return (new Matrix(graph_1), new Matrix(graph_2));
        }
    }
}
