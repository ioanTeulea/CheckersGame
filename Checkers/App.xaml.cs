using Checkers.Core;
using Checkers.MVVM.Model;
using Checkers.MVVM.View;
using Checkers.MVVM.ViewModel;
using Checkers.Servicies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            }) ;
            services.AddSingleton<GameView>(provider =>
            {
                var gameView = new GameView();
                gameView.DataContext = provider.GetRequiredService<GameViewModel>();
                return gameView;
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<AboutViewModel>();
            services.AddSingleton<GameViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider =>
            {
                Func<Type, ViewModel> func = viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType);
                return func;
            });
            services.AddSingleton<Func<Type, bool, ViewModel>>(serviceProvider =>
            {
                Func<Type, bool, ViewModel> func = (viewModelType, isMultipleJumpsSelected) =>
                {
                    var viewModel = (ViewModel)serviceProvider.GetRequiredService(viewModelType);

                    if (viewModel is GameViewModel gameViewModel)
                    {
                        gameViewModel.IsMultipleJumpsSelected = isMultipleJumpsSelected;
                    }

                    return viewModel;
                };
                return func;
            });
            services.AddSingleton<IBoard>(provider => new Board(8, 8));
            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

    }
}
