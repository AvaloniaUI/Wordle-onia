namespace Wordle.Messages;

using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;

public class KeyPressedMessage(Key value) : ValueChangedMessage<Key>(value);