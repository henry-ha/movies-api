using System.Collections.Generic;

namespace API.Http
{
    public class ErrorJsonResponse : StandardJsonResponse
    {
        public enum ErrorMessages
        {
            BadRequest,
            NotFound
        };

        private static readonly IDictionary<ErrorMessages, string> _errorMessageMap = new Dictionary<ErrorMessages, string>()
        {
            {  ErrorMessages.BadRequest, "Bad Request" },
            {  ErrorMessages.NotFound, "Unable to find object." },
        };

        public ErrorJsonResponse(int statusCode, ErrorMessages message, dynamic data = null)
        {
            StatusCode = statusCode;
            Error = _errorMessageMap[message];
            Data = data;
        }

        public ErrorJsonResponse(int statusCode, string message, dynamic data = null)
        {
            StatusCode = statusCode;
            Error = message;
            Data = data;
        }

        public new int StatusCode { get; set; } = 400;
        public dynamic Error { get; set; } = _errorMessageMap[ErrorMessages.BadRequest];
    }
}
