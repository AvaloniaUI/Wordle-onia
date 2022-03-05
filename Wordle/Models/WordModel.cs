namespace Wordle.Models;

using System.Collections.Generic;
using System.Linq;

public class WordModel : ObservableModel
{
    public WordModel(int wordLength)
    {
        Letters = Enumerable.Range(0, wordLength).Select(x => new LetterModel()).ToList();
    }

    public WordModel(string word)
    {
        Letters = word.ToCharArray().Select(x => new LetterModel(x)).ToList();
    }

    public IReadOnlyCollection<LetterModel> Letters { get; }
}