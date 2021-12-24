namespace ProjectTracker_API.Models.Abstracts
{
    public interface IServiceResponse
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }

    public interface IServiceResponse<T>
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
        T Data { get; set; }
    }
}