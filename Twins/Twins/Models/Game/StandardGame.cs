﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Twins.Models.Game
{
    public class StandardGame : AbstractGame
    {
        public StandardGame(int height, int width, Deck deck, TimeSpan timeLimit, TimeSpan turnLimit, Board.Cell[,] cells = null, int level = 0)
            : base(height, width, deck, timeLimit, turnLimit, cells, level)
        { }

        public override IEnumerable<Board.Cell> TryMatch()
        {
            Card reference = Board.FlippedCells.First().Card;
            bool isMatch = Board.FlippedCells.All(c => c.Card.Equals(reference));
            return HandleMatchResult(isMatch);
        }

        public override void EndTurn()
        {
            base.EndTurn();
            Board.ReferenceCard = null;
        }
    }
}
