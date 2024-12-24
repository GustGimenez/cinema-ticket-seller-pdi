namespace Commons.Integrations;

public class RequestData
{
    public string? Path { get; set; }
    public Dictionary<string, string>? Headers { get; set; }
    public object? Body { get; set; }
}