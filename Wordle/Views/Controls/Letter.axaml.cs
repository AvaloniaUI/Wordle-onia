using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using Wordle.Models.Enums;

namespace Wordle.Views.Controls;

public class Letter : TemplatedControl
{
    public static readonly StyledProperty<string> CharacterProperty =
        AvaloniaProperty.Register<Letter, string>(nameof(Character));

    public static readonly StyledProperty<LetterState> StateProperty =
        AvaloniaProperty.Register<Letter, LetterState>(nameof(State));
    
    private bool _hasLetter;

    public string Character
    {
        get => GetValue(CharacterProperty);
        set => SetValue(CharacterProperty, value);
    }

    public LetterState State
    {
        get => GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == StateProperty)
        {
            // PlayRevealAnimation();
            UpdatePsuedoClasses(State);
        }

        if (change.Property == CharacterProperty)
        {
            if (change.OldValue.HasValue && change.OldValue.Value is null &&
                change.NewValue.HasValue && change.NewValue.Value is string newChar && char.IsLetter(newChar[0]))
            {
                _hasLetter = true;
            }

            if (change.NewValue.HasValue && change.NewValue.Value is null &&
                change.OldValue.HasValue && change.OldValue.Value is string oldChar && char.IsLetter(oldChar[0]))
            {
                _hasLetter = false;
            }

            UpdatePsuedoClasses(State);
        }
    }

    private void UpdatePsuedoClasses(LetterState newValue)
    {
        PseudoClasses.Set(":has-letter", _hasLetter);
        PseudoClasses.Set(":wrong-letter", newValue == LetterState.WrongLetter);
        PseudoClasses.Set(":right-letter", newValue == LetterState.RightLetterWrongPlace);
        PseudoClasses.Set(":right-letter-placement", newValue == LetterState.RightLetterRightPlace);

        if (newValue != LetterState.None)
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                PseudoClasses.Set(":flip", true);
                await Task.Delay(1000);
                PseudoClasses.Set(":flip", false);
            }, DispatcherPriority.ApplicationIdle);
        }
    }
}