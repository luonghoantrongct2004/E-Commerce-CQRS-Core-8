using E.Application.Enums;
using E.Application.Models;

namespace E.API.Contracts.Common
{
    public class ErrorResponseHandler : IErrorResponseHandler
    {
        public ErrorResponse HandleErrors(List<Error> errors)
        {
            var apiError = new ErrorResponse();
            if(errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);
                apiError.StatusCode = 404;
                apiError.StatusPhrase = "Not Found";
                apiError.Timestamp = DateTime.Now;
                apiError.Errors.Add(error.Message);
            }
            else
            {
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad request";
                apiError.Timestamp = DateTime.Now;
                errors.ForEach(e => apiError.Errors.Add(e.Message));
            }
            return apiError;
        }
    }
}
