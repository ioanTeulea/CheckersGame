using Checkers.Core;
using Checkers.MVVM.Model;
using Checkers.Servicies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Checkers.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        public event Action<GameSerialization> GameDeserialized;
        private GameBusinessLogic bl;
        private bool _isMultipleJumpsSelected;
        public bool IsMultipleJumpsSelected
        {
            get => _isMultipleJumpsSelected;
            set
            {
                _isMultipleJumpsSelected = value;
                OnPropertyChanged();
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
        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenPopupCommand { get; set; }
        public RelayCommand NavigateAboutCommand { get; set; }
        public RelayCommand OkCommand { get; set; }
        public RelayCommand LoadGameCommand { get; set; }
        public HomeViewModel(INavigationService navService)
        {
            Navigation = navService;
            
            OpenPopupCommand = new RelayCommand(
            execute: obj =>
            {
                IsPopupOpen = true;
            },
            canExecute: obj => true
        );
            OkCommand = new RelayCommand(
           execute: obj =>
           {
               
               Navigation.NavigateToB<GameViewModel>(_isMultipleJumpsSelected);
               IsPopupOpen = false;
           },
           canExecute: obj => true
       );
            NavigateAboutCommand = new RelayCommand(
           execute: obj =>
           {
           
               Navigation.NavigateTo<AboutViewModel>();
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
        private void DeserializeGame()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(GameSerialization));
            FileStream file = new FileStream("gameData.xml", FileMode.Open);

            try
            {
                GameSerialization loadedGame = (GameSerialization)xmlser.Deserialize(file);


                Navigation.NavigateTo<GameViewModel>();
                file.Dispose();
                // Actualizeaza GameViewModel dupa deserializare
                Navigation.UpdateViewModel<GameViewModel>(vm =>
                {
                    vm.Gameboard = new Board(loadedGame.Board.Pieces, loadedGame.Board.SelectedPiece, loadedGame.Board.RedPieceCount, loadedGame.Board.BlackPieceCount, 8, 8);
                    vm.ValidMoveSquares = new ObservableCollection<GridPosition>(loadedGame.GridPositions);
                    vm.bl = new GameBusinessLogic(vm.Gameboard, vm.ValidMoveSquares, loadedGame.IsMultipleJumpsSelected);
                    vm.CurrentPlayerColor= loadedGame.CurrentPlayerColor;
                    vm.bl.CurrentPlayerColor = loadedGame.CurrentPlayerColor;
                });

            }
            catch (Exception ex)
            {
                // Trateaza exceptia
                MessageBox.Show("Eroare la încărcarea jocului: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

    }
}

