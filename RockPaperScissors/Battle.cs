using System;
using System.Linq;

namespace RockPaperScissors
{
    internal enum Item
    {
        Draw = 0,
        R = 1,
        P = 2,
        S = 3,
    }

    internal class Battle
    {
        public int Player1 { get; set; }
        public int Player2 { get; set; }

        public Battle(int player1, int player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public Item Winner()
        {
            return Math.Abs(Player2 - Player1) switch
            {
                0 => Item.Draw,
                1 => (Item)new[] { Player1, Player2 }.Max(),
                2 => (Item)new[] { Player1, Player2 }.Min(),
                _ => throw new ArgumentException("Invalid player")
            };
        }
    }
}
