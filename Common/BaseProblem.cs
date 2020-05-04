using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public abstract class BaseProblem
    {
        public virtual string InputFilePath { get; }
        public virtual string OutputFilePath { get; }

        /// <summary>
        /// Providing class name is ProblemXX, it parses problem input from Inputs/XX.in
        /// </summary>
        protected BaseProblem(string inputFileName)
        {
            if (!Directory.Exists("Outputs"))
            {
                Directory.CreateDirectory("Outputs");
            }

            InputFilePath = Path.Combine("Inputs", inputFileName + ".txt");
            OutputFilePath = Path.Combine("Outputs", inputFileName + ".out");
        }

        public abstract void Solve();

        protected virtual void PrintOutput(ICollection<string> solutions)
        {
            using var sw = new StreamWriter(OutputFilePath);

            for (int i = 0; i < solutions.Count; ++i)
            {
                sw.WriteLine($"Case #{i + 1}: {solutions.ElementAt(i)}");
            }
        }
    }
}
