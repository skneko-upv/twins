using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Twins.Models
{
    public partial class Board
    {
        public int Height { get; }

        public int Width { get; }

        public Game Game { get; }

        /// <summary>
        /// The reference card shown to the player to help them achieve
        /// successful matches.
        /// </summary>
        public Card ReferenceCard {
            get => _referenceCard;
            set {
                _referenceCard = value;
                ReferenceCardChanged(value);
            } 
        }
        private Card _referenceCard;

        /// <summary>
        /// The cells temporarily flipped by the player in this time unit, e.g. the current turn.
        /// </summary>
        /// <remarks>
        /// This list does not include cells locked in a revealed position.
        /// </remarks>
        public ICollection<Cell> FlippedCells { get; private set; }

        /// <summary>
        /// Occurs when the reference card to display has changed.
        /// </summary>
        public event Action<Card> ReferenceCardChanged;

        /// <summary>
        /// Occurs when a cell is flipped by the player.
        /// </summary>
        public event Action<Cell> CellFlipped;

        /// <summary>
        /// Occurs when a cell is put back by the player.
        /// </summary>
        public event Action<Cell> CellUnflipped;

        /// <summary>
        /// Occurs when a cell is locked in a revealed position, or when this status is reversed.
        /// </summary>
        /// <remarks>
        /// A cell locked in a revealed position will remain flipped independently of the game
        /// logic, and the player will not be able to put it back.
        /// The boolean value passed to the event handler will be <c>true</c> if the cell has been
        /// locked into a revealed position, or <c>false</c> if it is no longer locked.
        /// </remarks>
        public event Action<Cell, bool> CellKeepRevealedStatusChanged;

        public Cell[,] Cells { get; private set; }

        readonly IBoardPopulationStrategy populationStrategy;

        /// <summary>
        /// Create a new board populated randomly.
        /// </summary>
        public Board(int height, int width, Game game, IBoardPopulationStrategy populationStrategy)
        {
            Height = height;
            Width = width;
            Game = game;

            this.populationStrategy = populationStrategy;
            Populate();
        }

        /// <summary>
        /// Create a new board from the given cell matrix.
        /// </summary>
        public Board(int height, int width, Game game, Deck deck, Cell[,] cells)
        {
            Height = height;
            Width = width;
            Game = game;
            Cells = cells;
        }

        public Cell this[int row, int column]
            => Cells[row, column];

        public void FlipCell(int row, int column)
        {
            var flipped = this[row, column];

            if (flipped.KeepRevealed)
            {
                throw new InvalidOperationException();
            }

            FlippedCells.Add(flipped);
            CellFlipped(flipped);
        }

        public void UnflipCell(int row, int column)
        {
            var flipped = this[row, column];

            if (flipped.KeepRevealed || !FlippedCells.Contains(flipped))
            {
                throw new InvalidOperationException();
            }

            FlippedCells.Remove(flipped);
            CellUnflipped(flipped);
        }

        public void UnflipAllCells()
        {
            foreach (Cell cell in FlippedCells)
            {
                if (!cell.KeepRevealed)
                {
                    CellUnflipped(cell);
                }
            }
            FlippedCells.Clear();
        }

        public void SetCellKeepRevealed(int row, int column, bool keepRevealed)
        {
            var cell = this[row, column];

            cell.KeepRevealed = keepRevealed;
            CellKeepRevealedStatusChanged(cell, keepRevealed);
        }
    }
}
