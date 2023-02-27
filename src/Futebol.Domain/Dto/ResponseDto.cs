namespace Futebol.Domain.Dto;
public class ResponseDto<T> where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

public class ResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
