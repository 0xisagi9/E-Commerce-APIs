namespace E_Commerce_APIs.Shared.Helpers;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T Data { get; }
    public string Message { get; }
    public string? Error { get; }

    private Result(bool isSuccess, T data, string? error, string message)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        Message = message;
    }

    public static Result<T> Success(T data, string message) => new(true, data, null, message);
    public static Result<T> Failure(string error, string message) => new(false, default!, error, message);
}
