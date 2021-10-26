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

        private List<int[]> permutations;
        private Matrix M;
        public IsomorphicGenerator(Matrix _M)
        {
            M = _M;
            permutations = (new Permutation(M.VerticesNumber)).Permutations;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (int[] array in permutations)
            {
                Matrix newMatrix = new Matrix(M.Graph);
                for(int i=0;i<array.Length;i++)
                {
                    if (array[i]!=i)
                    {
                        newMatrix.swapColumn(array[i], i);
                        newMatrix.swapRow(array[i], i);
                        Permutation.swap(array, array[i], i);
                    }
                }
                yield return newMatrix;
            }
        }
    }

}
