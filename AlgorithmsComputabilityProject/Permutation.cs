﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    class Permutation
    {
        public List<int[]> Permutations { get; set; } = new List<int[]>();

        public Permutation(int size)
        {
            Permute(GenerateInitialArray(size), 0, size - 1);
        }

        private static int[] GenerateInitialArray(int size)
        {
            int[] result = new int[size];
            for(int i = 0; i < size; i++)
            {
                result[i] = i;
            }
            return result;
        }


        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!! XDDDDDD
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!! (tylko przerobiłem na int z stringu

        private void Permute(int[] data, int start, int end)
        {
            if (start == end)
                Permutations.Add(data);
            else
            {
                for (int i = start; i <= end; i++)
                {
                    Swap(data, start, i);
                    Permute(data, start + 1, end);
                    Swap(data, start, i);
                }
            }
        }

        public static void Swap(int[] data, int indexA, int indexB)
        {
            int tmp = data[indexA];
            data[indexA] = data[indexB];
            data[indexB] = tmp;
        }
    }
}
