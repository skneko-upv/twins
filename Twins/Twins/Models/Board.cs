using System;
using System.Collections.Generic;
using Twins.Models.Game;
using Twins.Models.Strategies;

namespace Twins.Models
{
    public partial class Board
    {
        public int Height { get; }

        public int Width { get; }

        public IGame Game { get; set; }

        /// <summary>
        /// The reference card shown to the player to help them achieve
        /// successful matches.
        /// </summary>
        public Card ReferenceCard {
            get => _referenceCard;
            set {
                _referenceCard = value;
                ReferenceCardChanged?.Invoke(value);
            }
        }
        private Card _referenceCard;

        /// <summary>
        /// The reference card category shown to the player to help them
        /// achieve successful matches.
        /// </summary>
        public Category ReferenceCategory {
            get => _referenceCategory;
            set {
                _referenceCategory = value;
                ReferenceCategoryChanged?.Invoke(value);
            }
        }
        private Category _referenceCategory;

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
        /// Occurs when the reference card category to display has changed.
        /// </summary>
        public event Action<Category> ReferenceCategoryChanged;

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

        public IList<Cell> Cells { get; private set; }

        private readonly int[,] cellMap;

        /// <summary>
        /// Create a new board populated randomly.
        /// </summary>
        public Board(int height, int width, IGame game, IBoardPopulationStrategy populationStrategy)
            : this(height, width, game, populationStrategy?.Populate(height, width))
        { }

        /// <summary>
        /// Create a new board from the given cell matrix.
        /// </summary>
        public Board(int height, int width, IGame game, Cell[,] cells)
        {
            if (cells is null)
            {
                throw new ArgumentNullException(nameof(cells));
            }

            Height = height;
            Width = width;
            Game = game;
            FlippedCells = new List<Cell>(height * width);

            cellMap = new int[height, width];
            Cells = new List<Cell>(height * width);
            int i = 0;

            foreach (Cell cell in cells)
            {
                Cells.Add(cell);
                cellMap[cell.Row, cell.Column] = i;
                i++;
            }
        }

        public Cell this[int row, int column]
            => Cells[cellMap[row, column]];

        public void FlipCell(int row, int column)
        {
            Cell flipped = this[row, column];

            if (flipped.KeepRevealed)
            {
                throw new InvalidOperationException();
            }

            flipped.FlipCount++;

            FlippedCells.Add(flipped);
            CellFlipped(flipped);
        }

        public void UnflipCell(int row, int column)
        {
            Cell flipped = this[row, column];

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
            Cell cell = this[row, column];

            cell.KeepRevealed = keepRevealed;
            CellKeepRevealedStatusChanged(cell, keepRevealed);
        }
    }
}
