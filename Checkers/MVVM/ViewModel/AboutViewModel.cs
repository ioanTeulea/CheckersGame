using Checkers.Core;
using Checkers.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.MVVM.ViewModel
{
    public class AboutViewModel:Core.ViewModel
    {

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
        public RelayCommand NavigateHomeCommand { get; set; }

        public AboutViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateHomeCommand = new RelayCommand(
          execute: obj =>
          {

              Navigation.NavigateTo<HomeViewModel>();
          },
          canExecute: obj => true
      );
        }
    }
}
