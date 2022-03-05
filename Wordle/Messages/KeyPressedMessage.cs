namespace Wordle.Messages;

using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;

public class KeyPressedMessage : ValueChangedMessage<Key>
{
    public KeyPressedMessage(Key value)
        : base(value)
    {
    }
}