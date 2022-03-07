using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Wordle.Messages;
using Wordle.Models;
using Wordle.Services;

namespace Wordle.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private GameViewModel _game;

        [ObservableProperty] 
        private bool _showWonDialog;
        
        public MainViewModel()
        {
            var messenger = AvaloniaLocator.Current.GetService<IMessenger>();
            
            Dispatcher.UIThread.Post(async () =>
            {
                var wordProvider = AvaloniaLocator.Current.GetService<IWordProvider>();

                await wordProvider.LoadAsync();
                
                var gameModel = new GameModel(AvaloniaLocator.Current.GetService<IGuessValidationService>(), wordProvider);

                Game = new GameViewModel(gameModel, messenger);
            });
            
            Keyboard = new KeyboardViewModel(messenger);
            
            messenger.Register<MainViewModel, GameWonMessage>(this, (recipient, message) =>
            {
                ShowWonDialog = true;
            });
        }

        public KeyboardViewModel Keyboard { get; }
    }
}
