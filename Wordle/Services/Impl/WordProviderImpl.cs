namespace Wordle.Services.Impl;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class WordProviderImpl : IWordProvider
{
    private static readonly Uri AnswersListUri = new(
                                                     "https://gist.githubusercontent.com/cfreshman/a03ef2cba789d8cf00c08f767e0fad7b/raw/5d752e5f0702da315298a6bb5a771586d6ff445c/wordle-answers-alphabetical.txt");

    private static readonly Uri ValidGuessesListUri = new(
        "https://gist.githubusercontent.com/cfreshman/cdcdf777450c5b5301e439061d29694c/raw/de1df631b45492e0974f7affe266ec36fed736eb/wordle-allowed-guesses.txt");

    private IReadOnlyCollection<string>? _answersList;

    private IReadOnlyCollection<string>? _validGuessesList;

    public string GetDailyWord()
    {
        if (_answersList == null || _validGuessesList == null)
        {
            throw new WordsNotLoadedException();
        }

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        int hash = today.GetHashCode();
        Random random = new(hash);
        int index = random.Next(0, _answersList.Count);

        return _answersList.ElementAt(index);
    }

    public bool IsRecognizedWord(string word)
    {
        if (_answersList == null || _validGuessesList == null)
        {
            throw new WordsNotLoadedException();
        }

        string key = word.ToUpper();
        return _answersList.Contains(key) || _validGuessesList.Contains(key);
    }

    public async Task LoadAsync()
    {
        using HttpClient httpClient = new();

        string answersString = await httpClient.GetStringAsync(AnswersListUri);
        _answersList = answersString.Split('\n', '\r')
            .Select(x => x.Trim().ToUpper())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToHashSet();

        string validGuessesString = await httpClient.GetStringAsync(ValidGuessesListUri);
        _validGuessesList = validGuessesString.Split('\n', '\r')
            .Select(x => x.Trim().ToUpper())
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToHashSet();
    }

    private class WordsNotLoadedException : Exception
    {
        public WordsNotLoadedException()
            : base("Words list not loaded. Call .LoadAsync prior to trying to get word related information.")
        {
        }
    }
}