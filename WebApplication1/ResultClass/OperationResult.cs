namespace WebApplication1.ResultClass;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string ErrorMessage { get; set; }

    public static OperationResult<T> CreateSuccess(T data) => new OperationResult<T>
    {
        Success = true,
        Data = data,
        ErrorMessage = null
    };

    public static OperationResult<T> CreateFailure(string error) => new OperationResult<T>
    {
        Success = false,
        Data = default,
        ErrorMessage = error
    };
}