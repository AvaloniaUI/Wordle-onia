namespace Wordle.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;
using Models.Enums;

public class LetterGuessedMessage : ValueChangedMessage<char>
{
    public LetterGuessedMessage(char value, LetterState letterState)
        : base(value)
    {
        LetterState = letterState;
    }

    public LetterState LetterState { get; }
}