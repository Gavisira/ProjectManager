using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectManager.Application;

public class BaseResponse<T>(List<string> errors = null, T data = default, bool isSuccess = true)
{

    public List<string> Errors { get; set; } = errors;
    public T Data { get; set; } = data;
    public bool IsSuccess { get; set; } = isSuccess;

    public T Success(T data)
    {
        IsSuccess = true;
        Data = data;
        return data;
    }

    public void Fail(List<string> errors)
    {
        Errors ??= [];
        IsSuccess = false;
        Errors = errors;
    }

    public void AddError(string error)
    {
        IsSuccess = false;
        Errors ??= [];
        Errors.Add(error);
    }
}