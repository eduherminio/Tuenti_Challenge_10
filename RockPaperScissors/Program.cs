namespace RockPaperScissors
{
    public static class Program
    {
        public static void Main()
        {
            new RockPaperScissors("sampleInput").Solve();
            new RockPaperScissors("testInput").Solve();
            new RockPaperScissors("submitInput").Solve();
        }
    }
}
