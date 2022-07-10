namespace Futebol.Application.Models;
public class ResponseModel<T> where T : class
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public T Data { get; set; }
}

public class ResponseModel
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
}
