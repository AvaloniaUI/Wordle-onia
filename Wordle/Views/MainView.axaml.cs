using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Wordle.Messages;

namespace Wordle.Views;

internal partial class MainView : UserControl
{
    private readonly IMessenger _messenger;

    public MainView()
    {
        InitializeComponent();

        _messenger = App.ServiceProvider!.GetRequiredService<IMessenger>();
            
        Dispatcher.UIThread.Post(()=>Focus(), DispatcherPriority.Loaded);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
            
        _messenger.Send(new KeyPressedMessage(e.Key));

        e.Handled = true;
    }
}