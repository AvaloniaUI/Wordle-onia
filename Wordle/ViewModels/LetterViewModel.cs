namespace Wordle.ViewModels;

using Models;
using Models.Enums;

public class LetterViewModel : ObservableViewModel<LetterModel>
{
    public LetterViewModel(LetterModel model)
        : base(model)
    {
    }

    public char? Character => Model.Character;

    public LetterState State => Model.State;
}