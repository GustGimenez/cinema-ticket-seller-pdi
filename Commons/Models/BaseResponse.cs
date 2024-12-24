namespace Commons.Models;

public class BaseResponse<T>
{
    public int StatusCode { get; set; }
    public T Data { get; set; }
}