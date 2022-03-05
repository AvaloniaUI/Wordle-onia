using Avalonia.Threading;

namespace Wordle.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Messages;
using System;
using System.Collections.ObjectModel;

public class NotificationsViewModel : ObservableObject
{
    private static readonly TimeSpan NotificationLifespan = TimeSpan.FromSeconds(1);

    public NotificationsViewModel(IMessenger messenger)
    {
        Notifications = new ObservableCollection<string>();

        messenger.Register<PushNotificationMessage>(this, (_, message) => PushNotification(message.Value));
    }

    public ObservableCollection<string> Notifications { get; }

    private void OnCardExpired(object? sender, EventArgs e)
    {
        if (sender is DispatcherTimer timer)
        {
            timer.Stop();
            timer.Tick -= OnCardExpired;
        }

        Notifications.RemoveAt(Notifications.Count - 1);
    }

    private void PushNotification(string message)
    {
        Notifications.Insert(0, message);
        DispatcherTimer dispatcherTimer = new()
        {
            Interval = NotificationLifespan
        };
        dispatcherTimer.Tick += OnCardExpired;
        dispatcherTimer.Start();
    }
}