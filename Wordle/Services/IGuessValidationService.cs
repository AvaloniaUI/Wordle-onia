namespace Wordle.Services;

using Models;
using System.Threading.Tasks;

public interface IGuessValidationService
{
    Task ValidateGuessAsync(WordModel guess, WordModel target);
}