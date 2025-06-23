namespace FinDoc.Application.Common;

public class HttpDataResponse
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public int StatusCode { get; set; } = 200;
}
