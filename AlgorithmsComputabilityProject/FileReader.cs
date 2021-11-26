﻿using AlgorithmsComputabilityProject.Tester;
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

            int[][] graphA = new int[FirstMatrixSize][];
            Matrix.InitializeArrays(graphA, FirstMatrixSize);

            int[][] graphB = new int[SecondMatrixSize][];
            Matrix.InitializeArrays(graphB, SecondMatrixSize);

            for (int i = 1; i < FirstMatrixSize + 1; i++)
            {
                string[] digits = lines[i].Split(' ');
                for(int j = 0; j < digits.Length; j++)
                {
                    graphA[i - 1][j] = Int32.Parse(digits[j]);
                }
            }

            for (int i = FirstMatrixSize + 2; i < FirstMatrixSize + SecondMatrixSize + 2; i++)
            {
                string[] digits = lines[i].Split(' ');
                for (int j = 0; j < digits.Length; j++)
                {
                    graphB[i - (FirstMatrixSize + 2)][j] = Int32.Parse(digits[j]);
                }
            }

            return (new Matrix(graphA), new Matrix(graphB));
        }

        public static void Write(Matrix A, Matrix B)
        {
            Matrix firstMatrix = A.VerticesNumber >= B.VerticesNumber ? A : B;
            Matrix secondMatrix = A.VerticesNumber >= B.VerticesNumber ? B : A;

            string content = $"{firstMatrix.VerticesNumber}\n";
            foreach (int[] row in firstMatrix.Graph)
            {
                content += String.Join(" ", row);
                content += '\n';
            }

            content += $"{secondMatrix.VerticesNumber}\n";
            foreach (int[] row in secondMatrix.Graph)
            {
                content += String.Join(" ", row);
                content += '\n';
            }

            string filename = $"{firstMatrix.VerticesNumber}_{secondMatrix.VerticesNumber}_matrices";
            string path = Storage.GetPathToExamples(filename);
            int counter = 1;
            path += $"{counter}.txt";
            while (System.IO.File.Exists(path))
            {
                counter++;
                path = path[0..^5];
                path += $"{counter}.txt";
            }
            System.IO.File.WriteAllText(path, content, Encoding.UTF8);
        }
    }
}
