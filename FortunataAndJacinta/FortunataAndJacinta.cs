using Common;
using FileParser;
using MoreLinq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FortunataAndJacinta
{
    public class FortunataAndJacinta : BaseProblem
    {
        private const string BookFile = "Inputs/pg17013.txt";

        public FortunataAndJacinta(string inputFileName) : base(inputFileName)
        {
        }

        public override void Solve()
        {
            const string regexPattern = "[^a-zñáéíóúü]";
            var unicodeOrder = string.Join("", new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'á', 'é', 'í', 'ñ', 'ó', 'ú', 'ü' });
            var totalChars = unicodeOrder.Length;

            var rawBook = ParseBook();
            var words = Regex.Replace(rawBook.ToLowerInvariant(), regexPattern, " ")
                .Replace("\r", " ")
                .Replace("\n", "")
                .Replace("  ", " ")
                .Split(" ")
                .Where(w => !string.IsNullOrWhiteSpace(w) && w.Length > 2);

            var countingDictionary = new ConcurrentDictionary<string, int>();

            foreach (var word in words)
            {
                countingDictionary.AddOrUpdate(word, (_) => 1, (_, total) => ++total);
            }

            var longestRepeatedWord = countingDictionary.Where(entry => entry.Value > 1).MaxBy(entry => entry.Key.Length).FirstOrDefault().Key.Length;

            var orderedThingy = countingDictionary
                .OrderByDescending(entry => entry.Value)
                //.ThenByDescending(entry =>
                //{
                //    double n = 0;

                //    for (int i = 0; i < entry.Key.Length; ++i)
                //    {
                //        n += Math.Pow(totalChars + 5, longestRepeatedWord - i) * (totalChars - unicodeOrder.IndexOf(entry.Key[i]));
                //    }
                //    return n;
                //})
                .ThenByDescending(entry =>
                {
                    double n = 0;
                    var maxLengthToCompare = entry.Key.Length;

                    var otherWords = countingDictionary.Where(pair => entry.Key != pair.Key && entry.Value == pair.Value && entry.Key.StartsWith(pair.Key)).ToList();
                    if (otherWords.Count > 0)
                    {
                        var shorterWord = otherWords.OrderBy(p => p.Key.Length)?.FirstOrDefault();
                        maxLengthToCompare = shorterWord.Value.Key.Length;
                    }
                    for (int i = 0; i < maxLengthToCompare; ++i)
                    {
                        n += Math.Pow(totalChars + 5, longestRepeatedWord - i) * (totalChars - unicodeOrder.IndexOf(entry.Key[i]));
                    }
                    return n;
                })
                .ThenBy(entry => entry.Key.Length)
                .ToList();

            var orderedDictionary = orderedThingy.ToDictionary();

            var input = ParseInput();
            var solutions = new List<string>();

            foreach (var item in input)
            {
                if (int.TryParse(item, out int n))
                {
                    solutions.Add($"{orderedDictionary.ElementAt(n - 1).Key} {orderedDictionary.ElementAt(n - 1).Value}");
                }
                else
                {
                    var value = orderedDictionary[item];
                    solutions.Add($"{value} #{1 + orderedThingy.IndexOf(new KeyValuePair<string, int>(item, value))}");
                }
            }

            PrintOutput(solutions);
        }

        private string ParseBook()
        {
            using var sr = new StreamReader(BookFile);
            return sr.ReadToEnd();
        }

        private IEnumerable<string> ParseInput()
        {
            ParsedFile file = new ParsedFile(InputFilePath);

            var cases = file.NextLine().NextElement<int>();
            for (int i = 0; i < cases; ++i)
            {
                yield return file.NextLine().NextElement<string>();
            }
        }
    }
}
