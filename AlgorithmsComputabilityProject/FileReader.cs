using AlgorithmsComputabilityProject.Tester;
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
            int firstMatrixSize = Int32.Parse(lines[0]);
            int secondMatrixSize = Int32.Parse(lines[firstMatrixSize + 1]);

            int[][] graphA = new int[firstMatrixSize][];
            Matrix.InitializeArrays(graphA, firstMatrixSize);

            int[][] graphB = new int[secondMatrixSize][];
            Matrix.InitializeArrays(graphB, secondMatrixSize);

            for (int i = 1; i < firstMatrixSize + 1; i++)
            {
                string[] digits = lines[i].Split(' ');
                for(int j = 0; j < digits.Length; j++)
                {
                    graphA[i - 1][j] = Int32.Parse(digits[j]);
                }
            }

            for (int i = firstMatrixSize + 2; i < firstMatrixSize + secondMatrixSize + 2; i++)
            {
                string[] digits = lines[i].Split(' ');
                for (int j = 0; j < digits.Length; j++)
                {
                    graphB[i - (firstMatrixSize + 2)][j] = Int32.Parse(digits[j]);
                }
            }

            return (new Matrix(graphA), new Matrix(graphB));
        }

        public static void Write(Matrix A, Matrix B, string name = "noniso")
        {
            string content = $"{A.VerticesNumber}\n";
            foreach (int[] row in A.Graph)
            {
                content += String.Join(" ", row);
                content += '\n';
            }

            content += $"{B.VerticesNumber}\n";
            foreach (int[] row in B.Graph)
            {
                content += String.Join(" ", row);
                content += '\n';
            }

            string filename = $"{A.VerticesNumber}_{B.VerticesNumber}_{name}";
            string path = Storage.GetPathToExamples(filename);
            //int counter = 1;
            //path += $"{counter}.txt";
            path += ".txt";
            while (System.IO.File.Exists(path))
            {
                //counter++;
                path = path[0..^5];
                //path += $"{counter}.txt";
                path += ".txt";
            }
            System.IO.File.WriteAllText(path, content, Encoding.UTF8);
        }
    }
}
