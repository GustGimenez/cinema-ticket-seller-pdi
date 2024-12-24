namespace Commons.Exceptions;

public class DataNotFoundException : ApplicationException
{
    public DataNotFoundException(string message)
        : base(message)
    {
    }
}