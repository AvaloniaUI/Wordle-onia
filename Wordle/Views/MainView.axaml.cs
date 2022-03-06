using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Runtime.InteropServices;
using Avalonia.Controls.Presenters;
using Avalonia.VisualTree;
using Wordle.Messages;
using Avalonia.Input;

namespace Wordle.Views
{
    internal class EntireScreenInputClient : ITextInputMethodClient
    {
        private InputElement? _parent;

        public EntireScreenInputClient(MainView parent)
        {
            _parent = parent;
        }

        public Rect CursorRectangle
        {
            get
            {
                return _parent.Bounds;
            }
        }

        public event EventHandler? CursorRectangleChanged;
        public IVisual TextViewVisual => _parent!;
        public event EventHandler? TextViewVisualChanged;
        public bool SupportsPreedit => false;
        public void SetPreeditText(string text) => throw new NotSupportedException();

        public bool SupportsSurroundingText => false;
        public TextInputMethodSurroundingText SurroundingText => throw new NotSupportedException();
        public event EventHandler? SurroundingTextChanged { add { } remove { } }
        public string? TextBeforeCursor => null;
        public string? TextAfterCursor => null;

        private void OnCaretBoundsChanged(object? sender, EventArgs e) => CursorRectangleChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public partial class MainView : UserControl
    {
        private readonly IMessenger _messenger;
        private EntireScreenInputClient _imClient;

        static MainView()
        {
            TextInputMethodClientRequestedEvent.AddClassHandler<MainView>((tb, e) =>
            {
                if (OperatingSystem.IsBrowser())
                {
                    e.Client = tb._imClient;
                }
            });
        }
        
        public MainView()
        {
            InitializeComponent();
            
              _imClient = new EntireScreenInputClient(this);

            _messenger = AvaloniaLocator.Current.GetService<IMessenger>();
            
            Dispatcher.UIThread.Post(()=>Focus(), DispatcherPriority.Loaded);
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            _messenger.Send(new KeyPressedMessage(e.Key));

            e.Handled = true;
        }
    }
}