using Common;
using FileParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLuckyOne
{
    public class TheLuckyOne : BaseProblem
    {
        public TheLuckyOne(string inputFileName) : base(inputFileName)
        {
        }

        public override void Solve()
        {
            var input = ParseInput().ToList();
            var solutions = new List<string>();

            foreach (var resultList in input)
            {
                var winners = new HashSet<int>();
                var losers = new HashSet<int>();

                static void UpdateSets(HashSet<int> winners, HashSet<int> losers, int winner, int loser)
                {
                    winners.Remove(loser);
                    losers.Add(loser);

                    if (!losers.Contains(winner))
                    {
                        winners.Add(winner);
                    }
                }

                foreach (var result in resultList)
                {
                    if (result.Player1Wins)
                    {
                        UpdateSets(winners, losers, result.Player1, result.Player2);
                    }
                    else
                    {
                        UpdateSets(winners, losers, result.Player2, result.Player1);
                    }
                }

                solutions.Add(winners.Single().ToString());
            }

            PrintOutput(solutions);
        }

        private IEnumerable<ICollection<Result>> ParseInput()
        {
            ParsedFile file = new ParsedFile(InputFilePath);

            var cases = file.NextLine().NextElement<int>();
            for (int i = 0; i < cases; ++i)
            {
                yield return ParseCase(file).ToList();
            }

            if (!file.Empty)
            {
                throw new ParsingException();
            }
        }

        private IEnumerable<Result> ParseCase(ParsedFile file)
        {
            var matches = file.NextLine().NextElement<int>();
            for (int j = 0; j < matches; ++j)
            {
                var line = file.NextLine();
                yield return new Result(line.NextElement<int>(), line.NextElement<int>(), Convert.ToBoolean(line.NextElement<int>()));
            }
        }
    }
}
