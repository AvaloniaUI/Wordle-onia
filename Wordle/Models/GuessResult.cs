namespace Wordle.Models;

public class GuessResult
{
    public WordModel? GuessedWord { get; set; }

    public string? Message { get; set; }

    public bool Success { get; set; }

    public bool Validated { get; set; }
}