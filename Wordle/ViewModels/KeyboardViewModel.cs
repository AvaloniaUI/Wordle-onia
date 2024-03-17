using Avalonia.Input;

namespace Wordle.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Messages;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;

public class KeyboardViewModel : ObservableObject
{
    private static readonly Key[] KeyboardRow1 =
    [
        Key.Q,
        Key.W,
        Key.E,
        Key.R,
        Key.T,
        Key.Y,
        Key.U,
        Key.I,
        Key.O,
        Key.P
    ];

    private static readonly Key[] KeyboardRow2 =
    [
        Key.A,
        Key.S,
        Key.D,
        Key.F,
        Key.G,
        Key.H,
        Key.J,
        Key.K,
        Key.L
    ];

    private static readonly Key[] KeyboardRow3 =
    [
        Key.Z,
        Key.X,
        Key.C,
        Key.V,
        Key.B,
        Key.N,
        Key.M
    ];

    public KeyboardViewModel(IMessenger messenger)
    {
        IReadOnlyCollection<KeyViewModelBase> rowOneKeys =
            KeyboardRow1.Select(x => new LetterKeyViewModel(x, messenger)).ToList();

        IReadOnlyCollection<KeyViewModelBase> rowTwoKeys =
            KeyboardRow2.Select(x => new LetterKeyViewModel(x, messenger)).ToList();

        var rowThreeKeys =
            KeyboardRow3.Select(x => new LetterKeyViewModel(x, messenger)).ToList<KeyViewModelBase>();
        rowThreeKeys.Insert(0, new EnterKeyViewModel(messenger));
        rowThreeKeys.Add(new DeleteKeyViewModel(messenger));

        Keys = new List<IReadOnlyCollection<KeyViewModelBase>>
        {
            rowOneKeys,
            rowTwoKeys,
            rowThreeKeys
        };

        messenger.Register<KeyboardViewModel, LetterGuessedMessage>(
            this,
            (_, m) => OnLetterGuessed(m.Value, m.LetterState));
    }

    public IReadOnlyCollection<IReadOnlyCollection<KeyViewModelBase>> Keys { get; }

    private void OnLetterGuessed(char character, LetterState letterState)
    {
        var match = Keys.SelectMany(x => x)
            .OfType<LetterKeyViewModel>()
            .FirstOrDefault(x => x.Label == character);

        match?.UpdateLetterResult(letterState);
    }
}

public partial class KeyViewModelBase : ObservableObject
{
    private readonly Key _key;
    private readonly IMessenger _messenger;

    [ObservableProperty]
    private LetterState _letterState;

    protected KeyViewModelBase(Key key, IMessenger messenger)
    {
        _key = key;
        _messenger = messenger;
    }
    
    public void KeyPressed()
    {
        _messenger.Send(new KeyPressedMessage(_key));
    }
}

public class EnterKeyViewModel(IMessenger messenger) : KeyViewModelBase(Key.Enter, messenger);

public class DeleteKeyViewModel(IMessenger messenger) : KeyViewModelBase(Key.Back, messenger);

public class LetterKeyViewModel(Key key, IMessenger messenger) : KeyViewModelBase(key, messenger)
{
    public char Label { get; } = key.ToString().ToUpper().ToCharArray()[0];

    public void UpdateLetterResult(LetterState letterState)
    {
        if (letterState > LetterState)
        {
            LetterState = letterState;
        }
    }
}