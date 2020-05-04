using Common;
using FileParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    public class RockPaperScissors : BaseProblem
    {
        private enum Item { R, P, S }

        public RockPaperScissors(string inputFileName) : base(inputFileName)
        {
        }

        public override void Solve()
        {
            var solutions = Play().ToList();

            PrintOutput(solutions);
        }

        private IEnumerable<string> Play()
        {
            ParsedFile file = new ParsedFile(InputFilePath);

            var lines = file.NextLine().NextElement<int>();
            for (int i = 0; i < lines; ++i)
            {
                var line = file.NextLine();

                var player1 = (int)Enum.Parse(typeof(Item), line.NextElement<string>());
                var player2 = (int)Enum.Parse(typeof(Item), line.NextElement<string>());
                var players = new[] { player1, player2 };

                yield return Math.Abs(player2 - player1) switch
                {
                    0 => "-",
                    1 => ((Item)players.Max()).ToString(),
                    2 => ((Item)players.Min()).ToString(),
                    _ => throw new ArgumentException(@"¯\_(ツ)_/¯")
                };
            }
        }
    }
}
