using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Wordle.Messages;
using Wordle.Models;
using Wordle.Services;

namespace Wordle.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private GameViewModel? _game;

    [ObservableProperty] 
    private bool _showWonDialog;

    public MainViewModel(
        IMessenger messenger,
        IWordProvider wordProvider,
        IGuessValidationService validationService)
    {
        _ = Dispatcher.UIThread.InvokeAsync(async () =>
        {
            await wordProvider.LoadAsync();

            var gameModel = new GameModel(validationService, wordProvider);

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