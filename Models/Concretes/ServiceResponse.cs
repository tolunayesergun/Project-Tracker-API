using ProjectTracker_API.Models.Abstracts;

namespace ProjectTracker_API.Models.Concretes
{
    public class ServiceResponse : IServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}