namespace ApplicationCore.Application.Commons;
public class Result<T>
{
    private T? _value;
    private string _error;
    private Result(T value, string error)
    {
        _value = value;
        _error = error;
    }
    public bool IsSuccess => _error == string.Empty;
    public bool IsError => _error != String.Empty;
    public T Value => IsError || _value == null ? throw new InvalidOperationException("There is no value for error!") : _value;
    public string Error => _error;

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, String.Empty);
    }

    public static Result<T?> Failure(string error = "Unknown error")
    {
        return new Result<T?>(default(T), error == String.Empty ? "Empty error message" : error);
    }
}