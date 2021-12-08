using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public class IsomorphicGeneratorEnumeratorData
    {
        public Matrix PermutedMatrix { get; private set; }
        public int[] Permutation { get; private set; }

        public IsomorphicGeneratorEnumeratorData(Matrix matrix, int[] permutation)
        {
            PermutedMatrix = matrix;
            Permutation = permutation;
        }
    }
}
