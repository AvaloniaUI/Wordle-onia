using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Wordle.Views;

public partial class KeyboardView : UserControl
{
    public KeyboardView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}