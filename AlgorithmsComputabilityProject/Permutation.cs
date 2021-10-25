using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    class Permutation
    {
        public List<int[]> Permutations { get; set; }

        public Permutation(int size)
        {
            Permute(GenerateString(size), 0, size);
        }

        private int [] GenerateString(int size)
        {
            int[] result = new int[size];
            for(int i=0;i<size;i++)
            {
                result[i] = i;
            }
            return result;
        }


        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!! (tylko przerobiłem na int z stringu

        private void Permute(int[] data, int l, int r)
        {
            if (l == r)
                Permutations.Add(data);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    data = swap(data, l, i);
                    Permute(data, l + 1, r);
                    data = swap(data, l, i);
                }
            }
        }

        public static int[] swap(int[] data, int i, int j)
        {
            int tmp = data[i];
            data[i] = data[j];
            data[j] = tmp;
            return data;
        }
    }
}
