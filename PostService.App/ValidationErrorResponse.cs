namespace PostService.App;

public class ValidationErrorResponse
{
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
