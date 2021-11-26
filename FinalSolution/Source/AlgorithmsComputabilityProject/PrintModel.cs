using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject
{
    public class PrintModel
    {
        public Matrix SmallerGraph { get; set; }
        public Matrix GreaterGraph { get; set; }
        public Matrix ResultGraph { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public PrintModel() { }

        public PrintModel(Matrix smaller)
        {
            SmallerGraph = smaller;
        }
    }
}
