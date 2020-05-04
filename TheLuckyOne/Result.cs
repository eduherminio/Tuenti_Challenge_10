namespace TheLuckyOne
{
    internal class Result
    {
        public Result(int player1, int player2, bool player1Wins)
        {
            Player1 = player1;
            Player2 = player2;
            Player1Wins = player1Wins;
        }

        public int Player1 { get; set; }

        public int Player2 { get; set; }

        public bool Player1Wins { get; set; }
    }
}
