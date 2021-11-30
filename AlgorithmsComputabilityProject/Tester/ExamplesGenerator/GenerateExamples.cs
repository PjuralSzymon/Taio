using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject.Tester.ExamplesGenerator
{
    public class GenerateExamples
    {
        // Don't call it unless you want to overwrite the examples!
        private static void SaveExamplesOnDisk()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + Storage.EXAMPLES_DEVELOPMENT_DIRECTORY;
            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (int size in Storage.MATRIX_SIZES)
            {
                List<(Matrix, Matrix)> generatedMatrices = Tester.GimmieSomeMatricesWithRandomizedSize(size);
                foreach ((Matrix, Matrix) example in generatedMatrices)
                {
                    FileReader.Write(example.Item1, example.Item2);
                }
            }
        }
    }
}
