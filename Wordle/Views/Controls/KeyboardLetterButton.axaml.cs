using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Wordle.Models.Enums;

namespace Wordle.Views.Controls;

public class KeyboardLetterButton : Button
{
    public static readonly StyledProperty<LetterState> StateProperty =
        AvaloniaProperty.Register<KeyboardLetterButton, LetterState>(nameof(State), LetterState.None);

    public LetterState State
    {
        get => GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    private void UpdatePsuedoClasses(LetterState newValue)
    {
        PseudoClasses.Set(":wrong-letter", newValue == LetterState.WrongLetter);
        PseudoClasses.Set(":right-letter", newValue == LetterState.RightLetterWrongPlace);
        PseudoClasses.Set(":right-letter-placement", newValue == LetterState.RightLetterRightPlace);
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == StateProperty)
        {
            UpdatePsuedoClasses(change.NewValue.GetValueOrDefault<LetterState>());
        }
    }
}