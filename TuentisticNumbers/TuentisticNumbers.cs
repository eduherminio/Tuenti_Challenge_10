using Common;
using FileParser;
using System.Collections.Generic;
using System.Linq;

namespace TuentisticNumbers
{
    public class TuentisticNumbers : BaseProblem
    {
        public TuentisticNumbers(string inputFileName) : base(inputFileName)
        {
        }

        public override void Solve()
        {
            var input = ParseInput().ToList();
            var solutions = new List<string>();

            foreach (var n in input)
            {
                if (n <= 19 || (n >= 30 && n <= 39) || n == 59)
                {
                    solutions.Add("IMPOSSIBLE");
                }
                else
                {
                    if (n < 29)
                    {
                        solutions.Add(1.ToString());
                    }
                    else
                    {
                        ulong maxElements = n % 20 <= 19
                            ? n / 20
                            : (n / 20) + 1;

                        solutions.Add(maxElements.ToString());
                    }
                }
            }

            PrintOutput(solutions);
        }

        private IEnumerable<ulong> ParseInput()
        {
            ParsedFile file = new ParsedFile(InputFilePath);

            var cases = file.NextLine().NextElement<int>();
            for (int i = 0; i < cases; ++i)
            {
                yield return ulong.Parse(file.NextLine().ToSingleString());
            }
        }
    }
}
