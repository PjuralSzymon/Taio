using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    class Permutation
    {
        public List<string> Permutations { get; set; }

        public Permutation(int size)
        {
            Permute(GenerateString(size), 0, size);
        }

        private string GenerateString(int size)
        {
            string result = "";
            for(int i=0;i<size;i++)
            {
                result += i.ToString();
            }
            return result;
        }


        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!
        // skopiowane z wypizdowa DO SPRAWDZENIA : !!!!

        private void Permute(string str, int l, int r)
        {
            if (l == r)
                Permutations.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    Permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
        }

        public string swap(string a, int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }
    }
}
