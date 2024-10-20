using Newtonsoft.Json;

namespace CleanArchitecture.Utilities.Bases;

public class BaseError<T> where T : class
{
    public required string Code { get; set; }
    public required string Message { get; set; }
    public T? Data { get; set; }
    /// <summary>
    /// Exception stack trace used when env is not production 
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? StackTrace { get; set; }
}