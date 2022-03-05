namespace Wordle.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class PushNotificationMessage : ValueChangedMessage<string>
{
    public PushNotificationMessage(string message)
        : base(message)
    {
    }
}