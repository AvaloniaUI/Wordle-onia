namespace Wordle.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using Enums;

public partial class LetterModel : ObservableModel
{
    [ObservableProperty]
    private char? _character;

    [ObservableProperty]
    private LetterState _state;

    public LetterModel()
    {
    }

    public LetterModel(char character)
    {
        _character = character;
    }
}