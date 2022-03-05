namespace Wordle.ViewModels;

using Models;
using System.Collections.Generic;
using System.Linq;

public class WordViewModel : ObservableViewModel<WordModel>
{
    public WordViewModel(WordModel model)
        : base(model)
    {
        Letters = model.Letters.Select(x => new LetterViewModel(x)).ToList();
    }

    public IReadOnlyCollection<LetterViewModel> Letters { get; }
}