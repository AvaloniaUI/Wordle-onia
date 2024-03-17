namespace Wordle.ViewModels;

using Models;
using Models.Enums;

public class LetterViewModel(LetterModel model) : ObservableViewModel<LetterModel>(model)
{
    public char? Character => Model.Character;

    public LetterState State => Model.State;
}