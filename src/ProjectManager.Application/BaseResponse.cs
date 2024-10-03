namespace ProjectManager.Application;

public class BaseResponse<T>
{
    public List<string> Errors { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
}