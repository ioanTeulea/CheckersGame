using Checkers.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Checkers.Core;
using System.Windows;
using System.IO;
using System.Xml.Serialization;

namespace Checkers.MVVM.ViewModel
{
    public class GameViewModel : Core.ViewModel
    {
        public GameBusinessLogic bl;

        private readonly IBoard _board;
        public IBoard Gameboard { get; set; }

        private readonly ObservableCollection<GridPosition> _gridPositions;
        private ObservableCollection<GridPosition> _validMoveSquares;


        private string _currentPlayerColor;
        public string CurrentPlayerColor
        {
            get => _currentPlayerColor;
            set
            {
                _currentPlayerColor = value;
                OnPropertyChanged(nameof(CurrentPlayerColor));
            }
        }
        private bool _isMoveValid;
        public bool IsMoveValid
        {
            get => _isMoveValid;
            set
            {
                _isMoveValid = value;
                OnPropertyChanged(nameof(IsMoveValid));
            }
        }


        private bool _isMultipleJumpsSelected;
        public bool IsMultipleJumpsSelected
        {
            get { return _isMultipleJumpsSelected; }
            set
            {
                if (_isMultipleJumpsSelected != value)
                {
                    _isMultipleJumpsSelected = value;
                    OnPropertyChanged(nameof(IsMultipleJumpsSelected));
                    InitializeGameBusinessLogic();
                }
            }
        }

        public ObservableCollection<GridPosition> ValidMoveSquares
        {
            get => _validMoveSquares;
            set
            {
                _validMoveSquares = value;
                OnPropertyChanged(nameof(ValidMoveSquares));
            }
        }

        private INavigationService _navigationService;
        public INavigationService Navigation
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand PressPieceCommand { get; set; }
        public RelayCommand MovePieceCommand { get; set; }
        public RelayCommand ShowPopupCommand { get; set; }
        public RelayCommand ShowPopupWinner { get; set; }
        public RelayCommand NavigateHomeCommand { get; set; }
        public RelayCommand SaveGameCommand { get; set; }
        public RelayCommand LoadGameCommand { get; set; }
        public GameViewModel(INavigationService navService)
        {
            if (Gameboard == null)
            {
                _board = new Board(8, 8);
                Gameboard = _board;
                _gridPositions = new ObservableCollection<GridPosition>();
                ValidMoveSquares = _gridPositions;
                bl = new GameBusinessLogic(_board, _gridPositions, IsMultipleJumpsSelected);
            }
            bl.CurrentPlayerChanged += (newPlayer) =>
            {
                CurrentPlayerColor = newPlayer;
            };

            PressPieceCommand = new RelayCommand(
             parameter =>
             {
                 IsMoveValid = bl.PressPiece((Piece)parameter);
                 if (Gameboard.RedPieceCount == 0 || Gameboard.BlackPieceCount == 0)
                 {
                     ShowPopupWinner.Execute(null);
                 }
                 if (!IsMoveValid)
                 {

                     ShowPopupCommand.Execute(null);
                 }
             },
            parameter => parameter is Piece piece && bl.CanExecutePressPiece(piece)
            );

            ShowPopupCommand = new RelayCommand(
             parameter =>
            {
                MessageBox.Show("Nu există mutări valide disponibile.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);

            },
            parameter => !IsMoveValid
             );
            ShowPopupWinner = new RelayCommand(
            parameter =>
            {
                string winnerMessage = "";

                if (_board.RedPieceCount == 0)
                {
                    winnerMessage = "Jucătorul cu piesele negre a câștigat!";
                }
                else if (_board.BlackPieceCount == 0)
                {
                    winnerMessage = "Jucătorul cu piesele roșii a câștigat!";
                }
                else
                {
                    winnerMessage = "Eroare";
                }

                MessageBox.Show(winnerMessage, "Câștigător", MessageBoxButton.OK, MessageBoxImage.Information);
                Navigation.NavigateTo<HomeViewModel>();
                Gameboard.InitializeBoard();
                ValidMoveSquares.Clear();
                InitializeGameBusinessLogic();
            },
            parameter => _board.RedPieceCount == 0 || _board.BlackPieceCount == 0
            );


            MovePieceCommand = new RelayCommand(
             parameter => bl.MovePiece((GridPosition)parameter),
             parameter => parameter is GridPosition gridPosition && bl.CanExecuteMovePiece(gridPosition));

            Navigation = navService;
            NavigateHomeCommand = new RelayCommand(
            execute: obj =>
            {

                Navigation.NavigateTo<HomeViewModel>();
                Gameboard.InitializeBoard();
                ValidMoveSquares.Clear();
                InitializeGameBusinessLogic();
            },
            canExecute: obj => true
            );

            SaveGameCommand = new RelayCommand(
      execute: obj =>
      {
          GameSerialization gameSerialization = new GameSerialization(bl);

          SerializationActions serializationActions = new SerializationActions();
          serializationActions.SerializeGame(gameSerialization);
          MessageBox.Show("Jocul a fost salvat cu succes!", "Notificare", MessageBoxButton.OK, MessageBoxImage.Information);
      },
      canExecute: obj => true
  );

            LoadGameCommand = new RelayCommand(
        parameter =>
        {
            DeserializeGame();
        },
        parameter => true
    );




        }
        private void InitializeGameBusinessLogic()
        {
            bl = new GameBusinessLogic(_board, _gridPositions, IsMultipleJumpsSelected);
            bl.CurrentPlayerChanged += (newPlayer) =>
            {
                CurrentPlayerColor = newPlayer;
            };
        }



        private void DeserializeGame()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(GameSerialization));
            FileStream file = new FileStream("output.xml", FileMode.Open);

            try
            {
                GameSerialization loadedGame = (GameSerialization)xmlser.Deserialize(file);

                bl = new GameBusinessLogic(new Board(loadedGame.Board.Pieces, loadedGame.Board.SelectedPiece, loadedGame.Board.RedPieceCount, loadedGame.Board.BlackPieceCount,  8,8), new ObservableCollection<GridPosition>(loadedGame.GridPositions), loadedGame.IsMultipleJumpsSelected);

                // Actualizează alte proprietăți
                bl.CurrentPlayerColor = loadedGame.CurrentPlayerColor;

                file.Dispose();

            }
            catch (Exception ex)
            {
                // Tratează excepția
                MessageBox.Show("Eroare la încărcarea jocului: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
