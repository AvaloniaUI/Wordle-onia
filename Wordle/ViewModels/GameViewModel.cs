using Avalonia.Input;

namespace Wordle.ViewModels;

using CommunityToolkit.Mvvm.Messaging;
using Messages;
using Models;
using System.Collections.Generic;
using System.Linq;

public class GameViewModel : ObservableViewModel<GameModel>
{
    private readonly IMessenger _messenger;

    public GameViewModel(GameModel model, IMessenger messenger)
        : base(model)
    {
        _messenger = messenger;
        Guesses = model.Guesses.Select(x => new WordViewModel(x)).ToList();

        _messenger.Register<GameViewModel, KeyPressedMessage>(this, (_, m) => OnKeyPressed(m.Value));
    }

    public IReadOnlyCollection<WordViewModel> Guesses { get; }

    private async void CommitGuess()
    {
        var guess = await Model.CommitGuess();
        if (guess == null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(guess.Message))
        {
            _messenger.Send(new PushNotificationMessage(guess.Message));
        }

        if (guess.Success)
        {
            _messenger.Send(new GameWonMessage());
        }

        if (guess.Validated && guess.GuessedWord != null)
        {
            foreach (var guessedWordLetter in guess.GuessedWord.Letters)
            {
                _messenger.Send(
                    new LetterGuessedMessage(
                        guessedWordLetter.Character!.Value,
                        guessedWordLetter.State));
            }
        }
    }

    private void DeleteLastLetter()
    {
        Model.DeleteLastCharacter();
    }

    private void GuessLetter(char character)
    {
        Model.GuessLetter(character);
    }

    private void OnKeyPressed(Key key)
    {
        switch (key)
        {
            case Key.Enter:
                CommitGuess();
                break;
            case Key.Back:
                DeleteLastLetter();
                break;
            case Key.A:
            case Key.B:
            case Key.C:
            case Key.D:
            case Key.E:
            case Key.F:
            case Key.G:
            case Key.H:
            case Key.I:
            case Key.J:
            case Key.K:
            case Key.L:
            case Key.M:
            case Key.N:
            case Key.O:
            case Key.P:
            case Key.Q:
            case Key.R:
            case Key.S:
            case Key.T:
            case Key.U:
            case Key.V:
            case Key.W:
            case Key.X:
            case Key.Y:
            case Key.Z:
                GuessLetter(key.ToString().ToUpper().ToCharArray()[0]);
                break;
        }
    }
}