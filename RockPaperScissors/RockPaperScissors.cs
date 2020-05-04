using Common;
using FileParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RockPaperScissors
{
    public class RockPaperScissors : BaseProblem
    {
        public RockPaperScissors(string inputFileName) : base(inputFileName)
        {
        }

        public override void Solve()
        {
            var input = ParseInput().ToList();
            var solutions = new List<string>();

            foreach (var battle in input)
            {
                var result = battle.Winner();
                solutions.Add(result != Item.Draw ? result.ToString() : "-");
            }

            PrintOutput(solutions);
        }

        private IEnumerable<Battle> ParseInput()
        {
            ParsedFile file = new ParsedFile(InputFilePath);

            var lines = file.NextLine().NextElement<int>();
            for (int i = 0; i < lines; ++i)
            {
                var line = file.NextLine();
                yield return new Battle(
                    (int)Enum.Parse(typeof(Item), line.NextElement<string>()),
                    (int)Enum.Parse(typeof(Item), line.NextElement<string>()));
            }
        }
    }
}
