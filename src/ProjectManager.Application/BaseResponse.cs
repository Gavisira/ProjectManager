namespace ProjectManager.Application;

public class BaseResponse<T>(List<string> errors = null, T data = default, bool isSuccess = true)
{
    public List<string> Errors { get; set; } = errors;
    public T Data { get; set; } = data;
    public bool IsSuccess { get; set; } = isSuccess;

    public BaseResponse<T> Success(T data)
    {
        IsSuccess = true;
        Data = data;
        return this;
    }

    public BaseResponse<T> Fail(List<string> errors)
    {
        Errors ??= [];
        IsSuccess = false;
        Errors = errors;
        return this;
    }

    public BaseResponse<T> AddError(string error)
    {
        IsSuccess = false;
        Errors ??= [];
        Errors.Add(error);
        return this;
    }
}