using Checkers.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Checkers.Servicies
{
    [Serializable]
    [XmlRoot("Game")]
    public class GameSerialization
    {
        public BoardSerialization Board { get; set; }

        [XmlArray("GridPositions")]
        [XmlArrayItem("GridPosition")]
        public List<GridPosition> GridPositions { get; set; }

        public string CurrentPlayerColor { get; set; }

        public bool IsMultipleJumpsSelected { get; set; }

        public GameSerialization(GameBusinessLogic gameBusinessLogic)
        {
            Board = new BoardSerialization(gameBusinessLogic.board);
            GridPositions = new List<GridPosition>(gameBusinessLogic.gridPositions);
            CurrentPlayerColor = gameBusinessLogic.CurrentPlayerColor;
            IsMultipleJumpsSelected = gameBusinessLogic.IsMultipleJumpsSelected;
        }

        public GameSerialization()
        {
        }
    }

    public class BoardSerialization
    {
        public List<Piece> Pieces { get; set; }
        public Piece SelectedPiece { get; set; }
        public int RedPieceCount { get; set; }
        public int BlackPieceCount { get; set; }

        public BoardSerialization(IBoard board)
        {
            Pieces = new List<Piece>(board.Pieces);
            SelectedPiece = board.SelectedPiece;
            RedPieceCount = board.RedPieceCount;
            BlackPieceCount = board.BlackPieceCount;
        }

        public BoardSerialization()
        {
        }
    }
}