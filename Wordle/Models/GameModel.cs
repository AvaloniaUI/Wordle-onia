namespace Wordle.Models;

using Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GameModel : ObservableModel
{
    private readonly IGuessValidationService _guessValidationService;
    private readonly IWordProvider _wordProvider;
    private int _currentGuessIndex;
    private int _currentLetterIndex;
    private bool _gameOver;
    private bool _isCommittingGuess;

    public GameModel(IGuessValidationService guessValidationService, IWordProvider wordProvider)
    {
        ArgumentNullException.ThrowIfNull(guessValidationService);
        ArgumentNullException.ThrowIfNull(wordProvider);

        _guessValidationService = guessValidationService;
        _wordProvider = wordProvider;
        TargetWord = new WordModel(wordProvider.GetDailyWord());
        Guesses = Enumerable.Range(0, Constants.MaximumGuesses)
            .Select(_ => new WordModel(TargetWord.Letters.Count))
            .ToList();
    }

    public IReadOnlyCollection<WordModel> Guesses { get; }

    public WordModel TargetWord { get; }

    public async Task<GuessResult?> CommitGuess()
    {
        if (_gameOver)
        {
            return null;
        }

        if (_isCommittingGuess)
        {
            return null;
        }

        _isCommittingGuess = true;

        try
        {
            if (_currentLetterIndex != TargetWord.Letters.Count)
            {
                return new GuessResult
                {
                    Success = false,
                    Validated = false,
                    Message = "Not enough letters"
                };
            }

            WordModel currentGuess = Guesses.ElementAt(_currentGuessIndex);
            string currentWord = string.Join(null, currentGuess.Letters.Select(x => x.Character));
            bool isRecognizedWord = _wordProvider.IsRecognizedWord(currentWord);
            if (!isRecognizedWord)
            {
                return new GuessResult
                {
                    Success = false,
                    Validated = false,
                    Message = "Not in word list",
                    GuessedWord = currentGuess
                };
            }

            await _guessValidationService.ValidateGuessAsync(currentGuess, TargetWord);
            _currentGuessIndex++;
            _currentLetterIndex = 0;

            bool success = currentGuess.Letters.All(x => x.State == LetterState.RightLetterRightPlace);
            GuessResult guessResult = new()
            {
                Success = success,
                Validated = true,
                Message = success ? "Genius" : null,
                GuessedWord = currentGuess
            };

            if (guessResult.Success || _currentGuessIndex == Constants.MaximumGuesses)
            {
                _gameOver = true;
            }

            return guessResult;
        }
        finally
        {
            _isCommittingGuess = false;
        }
    }

    public bool DeleteLastCharacter()
    {
        if (_isCommittingGuess)
        {
            return false;
        }

        if (_gameOver)
        {
            return false;
        }

        if (_currentLetterIndex == 0)
        {
            return false;
        }

        _currentLetterIndex--;
        WordModel currentGuess = Guesses.ElementAt(_currentGuessIndex);
        LetterModel currentLetterGuess = currentGuess.Letters.ElementAt(_currentLetterIndex);
        currentLetterGuess.Character = null;

        return true;
    }

    public bool GuessLetter(char letter)
    {
        if (_isCommittingGuess)
        {
            return false;
        }

        if (_gameOver)
        {
            return false;
        }

        if (_currentLetterIndex == TargetWord.Letters.Count)
        {
            return false;
        }

        WordModel currentGuess = Guesses.ElementAt(_currentGuessIndex);
        LetterModel currentLetterGuess = currentGuess.Letters.ElementAt(_currentLetterIndex);
        currentLetterGuess.Character = letter;
        _currentLetterIndex++;

        return true;
    }
}