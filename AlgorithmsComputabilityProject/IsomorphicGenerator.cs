using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    class IsomorphicGenerator : IEnumerable
    {

        private List<string> permutations;
        private Matrix M;
        public IsomorphicGenerator(Matrix _M)
        {
            M = _M;
            permutations = (new Permutation(M.VerticesNumber)).Permutations;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (string array in permutations)
            {
                Matrix newMatrix = M;
                for(int i=0;i<array.Length;i++)
                {

                }
                // ...
                // Yield each day of the week.
                yield return newMatrix;
            }
        }
    }

}
