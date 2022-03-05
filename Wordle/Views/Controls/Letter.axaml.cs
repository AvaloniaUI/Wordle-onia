using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Wordle.Models.Enums;

namespace Wordle.Views.Controls;

public class Letter : TemplatedControl
{
    public static readonly StyledProperty<string> CharacterProperty = AvaloniaProperty.Register<Letter, string>(nameof(Character));

    public static readonly StyledProperty<LetterState> StateProperty =
        AvaloniaProperty.Register<Letter, LetterState>(nameof(State));

    private Border? _border;
    private ScaleTransform? _scale;

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
            PlayRevealAnimation();
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _border = this.Find<Border>("PART_Border");
        _scale = this.Find<ScaleTransform>("PART_Scale");
        
        base.OnApplyTemplate(e);
    }
    
    private void UpdatePsuedoClasses(LetterState newValue)
    {
        PseudoClasses.Set(":wrong-letter", newValue == LetterState.WrongLetter);
        PseudoClasses.Set(":right-letter", newValue == LetterState.RightLetterWrongPlace);
        PseudoClasses.Set(":right-letter-placement", newValue == LetterState.RightLetterRightPlace);
    }

    private void PlayRevealAnimation()
    {
        if (State == LetterState.None)
        {
            //.SetResourceReference(ForegroundProperty, "TextForegroundKey");
            Background = Brushes.Transparent;
            _border.BorderThickness = new Thickness(2);
            return;
        }
        
        UpdatePsuedoClasses(State);
    }
}