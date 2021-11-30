using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsComputabilityProject.Tester
{
    public class IsomorphicGenerator : IEnumerable
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
                            Permutation.Swap(array, array[i], i);
                        }
                    }
                }
                yield return newMatrix;
            }
        }
    }

}
