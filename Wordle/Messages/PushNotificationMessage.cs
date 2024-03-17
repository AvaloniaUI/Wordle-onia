namespace Wordle.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class PushNotificationMessage(string message) : ValueChangedMessage<string>(message);

public class GameWonMessage() : ValueChangedMessage<bool>(true);