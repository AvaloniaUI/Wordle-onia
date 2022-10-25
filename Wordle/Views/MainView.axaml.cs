using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using System;
using Avalonia.VisualTree;
using Wordle.Messages;

namespace Wordle.Views
{
    public partial class MainView : UserControl
    {
        private readonly IMessenger _messenger;

        public MainView()
        {
            InitializeComponent();

            _messenger = AvaloniaLocator.Current.GetService<IMessenger>();
            
            Dispatcher.UIThread.Post(()=>Focus(), DispatcherPriority.Loaded);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            _messenger.Send(new KeyPressedMessage(e.Key));

            e.Handled = true;
        }
    }
}