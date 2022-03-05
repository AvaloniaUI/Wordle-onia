namespace Wordle.Services.Impl;

using Models;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GuessValidationServiceImpl : IGuessValidationService
{
    public async Task ValidateGuessAsync(WordModel guess, WordModel target)
    {
        // TODO: This really need reworking totally, ive had to temp hack some new logic in here as i found a bug
        (LetterModel, LetterState)[] letterResults = new (LetterModel, LetterState)[guess.Letters.Count];

        List<LetterModel> matchedGuessLetters = new();
        List<LetterModel> matchedTargetLetters = new();

        // Get all correct letters first
        for (int i = 0; i < guess.Letters.Count; i++)
        {
            LetterModel letter = guess.Letters.ElementAt(i);
            LetterModel targetLetter = target.Letters.ElementAt(i);

            if (letter.Character == targetLetter.Character)
            {
                letterResults[i] = (letter, LetterState.RightLetterRightPlace);
                matchedGuessLetters.Add(letter);
                matchedTargetLetters.Add(targetLetter);
            }
        }

        // Now get partially correct
        for (int i = 0; i < guess.Letters.Count; i++)
        {
            LetterModel letter = guess.Letters.ElementAt(i);
            if (matchedGuessLetters.Contains(letter))
            {
                continue;
            }

            LetterModel? partialMatch = target.Letters.Except(matchedTargetLetters)
                .FirstOrDefault(x => x.Character == letter.Character);
            if (partialMatch != null)
            {
                letterResults[i] = (letter, LetterState.RightLetterWrongPlace);
                matchedGuessLetters.Add(letter);
                matchedTargetLetters.Add(partialMatch);
            }
            else
            {
                letterResults[i] = (letter, LetterState.WrongLetter);
            }
        }

        foreach ((LetterModel, LetterState) letterResult in letterResults)
        {
            letterResult.Item1.State = letterResult.Item2;
            await Task.Delay(Constants.TimeBetweenReveals);
        }

        //for (int i = 0; i < guess.Letters.Count; i++)
        //{
        //    LetterModel letter = guess.Letters.ElementAt(i);

        //    LetterState letterState = LetterState.WrongLetter;
        //    for (int j = 0; j < target.Letters.Count; j++)
        //    {
        //        LetterModel targetLetter = target.Letters.ElementAt(j);
        //        if (letter.Label == targetLetter.Label)
        //        {
        //            if (i == j)
        //            {
        //                letterState = LetterState.RightLetterRightPlace;
        //                break;
        //            }

        //            letterState = LetterState.RightLetterWrongPlace;
        //        }
        //    }

        //    letter.State = letterState;

        //    if (i < guess.Letters.Count - 1)
        //    {
        //        await Task.Delay(Constants.TimeBetweenReveals);
        //    }
        //}
    }
}