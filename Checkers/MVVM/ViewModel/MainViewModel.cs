using Checkers.Core;
using Checkers.MVVM.View;
using Checkers.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
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
        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            Navigation.NavigateTo<HomeViewModel>();
          
        }
    }
}
