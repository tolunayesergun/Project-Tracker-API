
using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Constants
{
    public static class Responses<TEntity>
    {
        public static ServiceResponse<TEntity> SuccessResponse(TEntity data, string _message = Messages.SuccessMessage)
        {
            return new ServiceResponse<TEntity> { IsSuccess = true, Message = _message, Data = data };
        }

        public static ServiceResponse<TEntity> FailedResponse(string _message = Messages.SystemFail)
        {
            return new ServiceResponse<TEntity> { IsSuccess = false, Message = _message };
        }
    }

    public static class Responses
    {
        public static ServiceResponse SuccessResponse(string _message = Messages.SuccessMessage)
        {
            return new ServiceResponse { IsSuccess = true, Message = _message };
        }

        public static ServiceResponse FailedResponse(string _message = Messages.SystemFail)
        {
            return new ServiceResponse { IsSuccess = false, Message = _message };
        }
    }
}