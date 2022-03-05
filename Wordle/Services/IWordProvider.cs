namespace Wordle.Services;

using System.Threading.Tasks;

public interface IWordProvider
{
    public string GetDailyWord();

    public bool IsRecognizedWord(string word);

    public Task LoadAsync();
}