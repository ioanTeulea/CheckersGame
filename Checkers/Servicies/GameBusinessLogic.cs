using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Checkers.MVVM.Model;

namespace Checkers.Servicies
{
    public class GameBusinessLogic
    {
        public IBoard board;
        public ObservableCollection<GridPosition> gridPositions;

        public event Action<string> CurrentPlayerChanged;

        public string CurrentPlayerColor = "/Assets/Images/black";
        public bool IsMultipleJumpsSelected;
        public GameBusinessLogic(IBoard board, ObservableCollection<GridPosition> gridPositions,bool IsMultipleJumpsSelected)
        {
            this.board = board;
            this.gridPositions = gridPositions;
            this.IsMultipleJumpsSelected = IsMultipleJumpsSelected;

        }
        public void MovePiece(GridPosition gridPosition)
        {
                var destination = (gridPosition.Column, gridPosition.Row);

                if (board.SelectedPiece != null && IsValidMove(board.SelectedPiece, destination))
                {
                    List<GridPosition> jumpedPieces = null;

                    foreach (var entry in GetValidMoves(board.SelectedPiece))
                    {
                        if (entry.Key.Row == destination.Item2 && entry.Key.Column == destination.Item1)
                        {
                            jumpedPieces = entry.Value;
                            break;
                        }
                    }

                    if (jumpedPieces != null)
                    {
                        foreach (var jumpedPiecePosition in jumpedPieces)
                        {
                            var jumpedPiece = board.Pieces.FirstOrDefault(p => p.X == jumpedPiecePosition.Column && p.Y == jumpedPiecePosition.Row);
                            if (jumpedPiece != null)
                            {
                            board.RemovePiece(jumpedPiece);
                            
                        }
                        }
                    }


                board.MovePiece(board.SelectedPiece, destination);
                
                // Verifică dacă piesa a ajuns la ultima/întâia linie
                if (board.SelectedPiece.ImagePath.StartsWith("/Assets/Images/blackPieceS") && destination.Item2 == 0)
                    {
                        var pieceToUpdate = board.Pieces.FirstOrDefault(p => p.X == board.SelectedPiece.X && p.Y == board.SelectedPiece.Y);
                        if (pieceToUpdate != null)
                        {
                            pieceToUpdate.ImagePath = "/Assets/Images/blackPieceK.png";
                            pieceToUpdate.IsKing = true;
                        }
                    }

                    if (board.SelectedPiece.ImagePath.StartsWith("/Assets/Images/redPieceS") && destination.Item2 == 7)
                    {
                        var pieceToUpdate = board.Pieces.FirstOrDefault(p => p.X == board.SelectedPiece.X && p.Y == board.SelectedPiece.Y);
                        if (pieceToUpdate != null)
                        {
                            pieceToUpdate.ImagePath = "/Assets/Images/redPieceK.png";
                            pieceToUpdate.IsKing = true;
                        }
                    }
                    board.SelectedPiece = null;
                    gridPositions.Clear();
                CurrentPlayerColor = CurrentPlayerColor == "/Assets/Images/black"   
                        ? "/Assets/Images/red"
                        : "/Assets/Images/black";
                CurrentPlayerChanged?.Invoke(CurrentPlayerColor);
            }
            
        }

        public bool CanExecuteMovePiece(GridPosition gridPosition)
        {
            
            var destination = (gridPosition.Column, gridPosition.Row);
            return board.SelectedPiece != null && IsValidMove(board.SelectedPiece, destination);
        }

        private bool IsValidMove(Piece piece, (int, int) destination)
        {
            var validMoves = GetValidMoves(piece);
            // Convertim tuplul destination într-un GridPosition
            var destinationPosition = new GridPosition(destination.Item2, destination.Item1);

            foreach (var move in validMoves.Keys)
            {
                if (move.Row == destinationPosition.Row && move.Column == destinationPosition.Column)
                {
                    return true;
                }
            }

            return false;
        }

        public bool PressPiece(Piece selectedPiece)
        {
            board.SelectedPiece = selectedPiece;

            // Actualizăm patratele valide
            gridPositions.Clear();
            foreach (GridPosition move in GetValidMoves(board.SelectedPiece).Keys)
            {
                gridPositions.Add(move);
            }

            return gridPositions.Any(); // Returnează true dacă există mutări valide, altfel false
        }

        public bool CanExecutePressPiece(Piece piece)
        {
     
               if (piece.ImagePath.StartsWith(CurrentPlayerColor))
                    return true;
            return false;
            
        }
        public Dictionary<GridPosition, List<GridPosition>> GetValidMoves(Piece selectedPiece)
        {
            var validMoves = new Dictionary<GridPosition, List<GridPosition>>();
            int direction = selectedPiece.IsKing ? 0 : (selectedPiece.ImagePath.StartsWith("/Assets/Images/black") ? -1 : 1);

            var positionsToCheck = selectedPiece.IsKing ?
                new List<(int, int)>
                {
                    (selectedPiece.X - 1, selectedPiece.Y - 1),
                    (selectedPiece.X - 1, selectedPiece.Y + 1),
                    (selectedPiece.X + 1, selectedPiece.Y - 1),
                    (selectedPiece.X + 1, selectedPiece.Y + 1)
                } :
                new List<(int, int)>
                {
                    (selectedPiece.X - 1, selectedPiece.Y + direction),
                    (selectedPiece.X + 1, selectedPiece.Y + direction)
                };
            foreach (var position in positionsToCheck)
            {
                if (IsPositionEmpty(position) && IsValidPosition(position))
                {
                    validMoves.Add(new GridPosition(position.Item2, position.Item1), new List<GridPosition>());
                }
                else
                if(IsValidPosition(position))
                {
                    if(IsMultipleJumpsSelected)
                    CheckDiagonalJump2(selectedPiece, position, validMoves);
                    else
                        CheckDiagonalJump(selectedPiece,position,validMoves);
                }
            }

            return validMoves;
        }
        private void CheckDiagonalJump(Piece selectedPiece, (int, int) position, Dictionary<GridPosition, List<GridPosition>> validMoves)
        {
            var jumpedPiecePosition = position;

            
            var nextDiagonalPosition = ((position.Item1 + (position.Item1 - selectedPiece.X)), position.Item2 + (position.Item2 - selectedPiece.Y));

            if (IsPositionEmpty(nextDiagonalPosition) && IsValidPosition(nextDiagonalPosition) && PiecesOfDifferentColors((selectedPiece.X, selectedPiece.Y), jumpedPiecePosition))
            {
                validMoves.Add(new GridPosition(nextDiagonalPosition.Item2, nextDiagonalPosition.Item1), new List<GridPosition> { new GridPosition(jumpedPiecePosition.Item2, jumpedPiecePosition.Item1) });
            }
        }

        private List<(int, int)> PositionToCheck(Piece selectedPiece)
        {
            int direction = selectedPiece.IsKing ? 0 : (selectedPiece.ImagePath.StartsWith("/Assets/Images/black") ? -1 : 1);

            var positionsToCheck = selectedPiece.IsKing ?
                new List<(int, int)>
                {
                    (selectedPiece.X - 1, selectedPiece.Y - 1),
                    (selectedPiece.X - 1, selectedPiece.Y + 1),
                    (selectedPiece.X + 1, selectedPiece.Y - 1),
                    (selectedPiece.X + 1, selectedPiece.Y + 1)
                } :
                new List<(int, int)>
                {
                    (selectedPiece.X - 1, selectedPiece.Y + direction),
                    (selectedPiece.X + 1, selectedPiece.Y + direction)
                };
            return positionsToCheck;
        }
        private void CheckDiagonalJump2(Piece selectedPiece, (int, int) position, Dictionary<GridPosition, List<GridPosition>> validMoves)
        {
            var jumpedPiecePosition = position;
            var initialDiagonalPosition = ((position.Item1 + (position.Item1 - selectedPiece.X)), position.Item2 + (position.Item2 - selectedPiece.Y));

            if (IsPositionEmpty(initialDiagonalPosition) && IsValidPosition(initialDiagonalPosition))
            {
                if (PiecesOfDifferentColors((selectedPiece.X, selectedPiece.Y), jumpedPiecePosition))
                {
                    validMoves.Add(new GridPosition(initialDiagonalPosition.Item2, initialDiagonalPosition.Item1), new List<GridPosition> { new GridPosition(jumpedPiecePosition.Item2, jumpedPiecePosition.Item1) });

                    List<(int, int)> nextPosToCheck = new List<(int, int)>();
                    Piece p = new Piece(initialDiagonalPosition.Item1, initialDiagonalPosition.Item2, selectedPiece.IsKing, selectedPiece.ImagePath);
                    nextPosToCheck = PositionToCheck(p);
                    int maxIterations = 1000;
                    int iterations = 0;
                    while (nextPosToCheck.Any() && iterations < maxIterations)
                    {
                        var nextPos = nextPosToCheck.First();
                        nextPosToCheck.RemoveAt(0);
                        iterations++;
                        if (IsValidPosition(nextPos) && !IsPositionEmpty(nextPos))
                        {
                            jumpedPiecePosition = nextPos;
                            var nextDiagonalPosition = ((nextPos.Item1 + (nextPos.Item1 - p.X)), nextPos.Item2 + (nextPos.Item2 - p.Y));

                            if (IsPositionEmpty(nextDiagonalPosition) && IsValidPosition(nextDiagonalPosition) && PiecesOfDifferentColors((selectedPiece.X, selectedPiece.Y), jumpedPiecePosition))
                            {
                                if (validMoves.TryGetValue(GetKeyByCoordinates(validMoves, p.X, p.Y), out List<GridPosition> existingList))
                                {
                                    List<GridPosition> newList = existingList.ToList();
                                    newList.Add(new GridPosition(jumpedPiecePosition.Item2, jumpedPiecePosition.Item1));
                                    validMoves[new GridPosition(nextDiagonalPosition.Item2, nextDiagonalPosition.Item1)] = newList;

                                    // Actualizeaza lista de pozitii urmatoare pentru noile pozitii de sarit
                                    p = new Piece(nextDiagonalPosition.Item1, nextDiagonalPosition.Item2, selectedPiece.IsKing, selectedPiece.ImagePath);
                                    nextPosToCheck.AddRange(PositionToCheck(p));
                                }
                            }
                        }

                       
                    }
                }
            }
        }


        private GridPosition GetKeyByCoordinates(Dictionary<GridPosition, List<GridPosition>> dictionary, int column, int row)
            {
                foreach (var key in dictionary.Keys)
                {
                    if (key.Column == column && key.Row == row)
                    {
                        return key;
                    }
                }

                return null; // Returnăm null dacă cheia nu a fost găsită
            }


        private bool IsPositionEmpty((int, int) position)
        {
            return !board.Pieces.Any(p => p.X == position.Item1 && p.Y == position.Item2);
        }
        private bool IsValidPosition((int, int) position)
        {
            // Verifică dacă rândul și coloana sunt între 0 și 7
            return position.Item1 >= 0 && position.Item1 <= 7 &&
                   position.Item2 >= 0 && position.Item2 <= 7;
        }
        private bool PiecesOfDifferentColors((int, int) position1, (int, int) position2)
        {
            var piece1 = GetPieceAtPosition(position1);
            var piece2 = GetPieceAtPosition(position2);

            if (piece1 == null || piece2 == null)
            {
                // Una dintre poziții este goală, deci piesele sunt diferite
                return true;
            }

            return piece1.ImagePath.Contains("black") != piece2.ImagePath.Contains("black");
        }
        private Piece GetPieceAtPosition((int, int) position)
        {
            return board.Pieces.FirstOrDefault(p => p.X == position.Item1 && p.Y == position.Item2);
        }
    }
}
