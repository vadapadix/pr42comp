namespace PostService.App;

public class PostingValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public PostingValidationException(Dictionary<string, string[]> errors)
        : base("Помилка валідації")
    {
        Errors = errors;
    }
}
