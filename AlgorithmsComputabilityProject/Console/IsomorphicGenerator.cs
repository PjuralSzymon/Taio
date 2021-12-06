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
        private readonly List<int[]> Permutations;
        private readonly Matrix M;

        public IsomorphicGenerator(Matrix _M)
        {
            M = _M;
            Permutations = new Permutation(M.VerticesNumber).Permutations;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (int[] array in Permutations)
            {
                Matrix newMatrix = new Matrix(M.Graph);
                for (int j = 0; j < array.Length; j++)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] != i)
                        {
                            newMatrix.SwapColumn(array[i], i);
                            newMatrix.SwapRow(array[i], i);
                        }
                    }
                }
                yield return new IsomorphicGeneratorEnumeratorData(newMatrix, array);
            }
        }
    }

}
