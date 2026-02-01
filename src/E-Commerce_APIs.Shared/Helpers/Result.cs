namespace E_Commerce_APIs.Shared.Helpers;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string Message { get; }
    public int StatusCode { get; }
    public IDictionary<string, string[]>? Errors { get; }

    private Result(bool isSuccess, T? data, string message, int statusCode, IDictionary<string, string[]>? errors = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        StatusCode = statusCode;
        Errors = errors;
    }

    public static Result<T> Success(T data, string message = "Operation completed successfully", int statusCode = 200) 
        => new(true, data, message, statusCode);

    public static Result<T> Failure(string message, int statusCode = 400, IDictionary<string, string[]>? errors = null) 
        => new(false, default, message, statusCode, errors);

    public static Result<T> ValidationFailure(string message, IDictionary<string, string[]> errors, int statusCode = 400) 
        => new(false, default, message, statusCode, errors);
}

public class Result
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public int StatusCode { get; }
    public IDictionary<string, string[]>? Errors { get; }

    private Result(bool isSuccess, string message, int statusCode, IDictionary<string, string[]>? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        Errors = errors;
    }

    public static Result Success(string message = "Operation completed successfully", int statusCode = 200) 
        => new(true, message, statusCode);

    public static Result Failure(string message, int statusCode = 400, IDictionary<string, string[]>? errors = null) 
        => new(false, message, statusCode, errors);

    public static Result ValidationFailure(string message, IDictionary<string, string[]> errors, int statusCode = 400) 
        => new(false, message, statusCode, errors);
}
