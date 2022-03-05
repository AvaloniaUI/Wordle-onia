using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Splat;
using Wordle.Models;
using Wordle.Services;

namespace Wordle.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private GameViewModel _game;
        
        public MainViewModel()
        {
            var messenger = Locator.Current.GetService<IMessenger>();
            
            Dispatcher.UIThread.Post(async () =>
            {
                var wordProvider = Locator.Current.GetService<IWordProvider>();

                await wordProvider.LoadAsync();
                
                var gameModel = new GameModel(Locator.Current.GetService<IGuessValidationService>(), wordProvider);

                Game = new GameViewModel(gameModel, messenger);
            });
            
            Keyboard = new KeyboardViewModel(messenger);
        }

        public KeyboardViewModel Keyboard { get; }
    }
}
