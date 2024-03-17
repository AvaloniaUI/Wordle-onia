namespace Wordle.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;
using Models.Enums;

public class LetterGuessedMessage(char value, LetterState letterState) : ValueChangedMessage<char>(value)
{
    public LetterState LetterState { get; } = letterState;
}