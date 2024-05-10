using Checkers.Core;
using Checkers.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Checkers.Servicies
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
        void NavigateToB<TViewModel>(bool isMultipleJumpsSelected) where TViewModel : ViewModel;
        void UpdateViewModel<T>(Action<T> action) where T : ViewModel;
    }

    public class NavigationService : ObservableObject, INavigationService
    {
        private ViewModel _currentView;
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private readonly Func<Type, bool, ViewModel> _viewModelFactoryB;

        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView=value;
                OnPropertyChanged();
            }

        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory, Func<Type, bool, ViewModel> viewModelFactoryB)
        {
            _viewModelFactory = viewModelFactory;
            _viewModelFactoryB = viewModelFactoryB;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel=_viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;

        }
        public void UpdateViewModel<T>(Action<T> action) where T : ViewModel
        {
            if (CurrentView is T viewModel)
            {
                action(viewModel);
            }
        }

        public void NavigateToB<TViewModel>(bool isMultipleJumpsSelected) where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactoryB.Invoke(typeof(TViewModel), isMultipleJumpsSelected);
            CurrentView = viewModel;
        }
    }
}

