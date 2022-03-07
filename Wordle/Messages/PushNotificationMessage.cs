using System.Reactive;

namespace Wordle.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class PushNotificationMessage : ValueChangedMessage<string>
{
    public PushNotificationMessage(string message)
        : base(message)
    {
    }
}

public class GameWonMessage : ValueChangedMessage<Unit>
{
    public GameWonMessage() : base(Unit.Default)
    {
        
    }
}