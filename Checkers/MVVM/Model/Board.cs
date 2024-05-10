using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Checkers.Core;
namespace Checkers.MVVM.Model
{
    public interface IBoard
    {
        [XmlIgnore]
        ObservableCollection<Piece> Pieces { get; set; }
        [XmlIgnore]
        Piece SelectedPiece { get; set; }
        int RedPieceCount { get; set; }
        int BlackPieceCount { get; set; }
        void MovePiece(Piece piece, (int, int) destination);
        void RemovePiece(Piece piece);
        void InitializeBoard();
    }

    [Serializable]
    public class Board : Core.ObservableObject, IBoard
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        [XmlIgnore]
        private int _redPieceCount;
        public int RedPieceCount
        {
            get { return _redPieceCount; }
            set
            {
                if (_redPieceCount != value)
                {
                    _redPieceCount = value;
                    OnPropertyChanged(nameof(RedPieceCount));
                }
            }
        }

        [XmlIgnore]
        private int _blackPieceCount;
        public int BlackPieceCount
        {
            get { return _blackPieceCount; }
            set
            {
                if (_blackPieceCount != value)
                {
                    _blackPieceCount = value;
                    OnPropertyChanged(nameof(BlackPieceCount));
                }
            }
        }

        [XmlArray("Pieces")]
        [XmlArrayItem("Piece")]
        public ObservableCollection<Piece> Pieces { get; set; }

        [XmlElement("SelectedPiece")]
        public Piece SelectedPiece { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new ObservableCollection<Piece>();
            InitializeBoard();
        }
        public Board(List<Piece> pieces, Piece selectedPiece, int redPieceCount, int blackPieceCount, int rows, int columns)
        {
            Pieces = new ObservableCollection<Piece>(pieces);
            SelectedPiece = selectedPiece;
            RedPieceCount = redPieceCount;
            BlackPieceCount = blackPieceCount;
            Rows = rows;
            Columns = columns;
        }


        public void InitializeBoard()
        {
            Pieces.Clear();
            RedPieceCount = 0;
            BlackPieceCount = 0;

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if ((row + col) % 2 == 1 && row < 3)
                    {
                        Pieces.Add(new Piece (col,row,false,"/Assets/Images/redPieceS.png" ));
                        RedPieceCount++;
                    }
                    else if ((row + col) % 2 == 1 && row > 4)
                    {
                        Pieces.Add(new Piece (col, row,false, "/Assets/Images/blackPieceS.png" ));
                        BlackPieceCount++;
                    }
                }
            }
            OnPropertyChanged(nameof(Pieces));
           
        }
        public void MovePiece(Piece piece, (int, int) destination)
        {
            foreach (var p in Pieces)
            {
                if (p.X == piece.X && p.Y == piece.Y)
                {
                    p.X = destination.Item1;
                    p.Y = destination.Item2;

                  
                    OnPropertyChanged(nameof(Pieces));
                    break; 
                }
            }
        }
        public void RemovePiece(Piece piece)
        {
            Pieces.Remove(piece);
            if (piece.ImagePath.StartsWith("/Assets/Images/red"))
            {
                RedPieceCount--;
            }
            else if (piece.ImagePath.StartsWith("/Assets/Images/black"))
            {
                BlackPieceCount--;
            }

            OnPropertyChanged(nameof(Pieces));
        }
    }
}
