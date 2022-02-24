using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; }
        public string Message { get; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request, make sure params compitable.",
                401 => "Authorized ! Make sure you has token to access",
                403 => "No Permission to update/delete data",
                404 => "Not Found ! Make sure you routing correct.",
                500 => "Server errors ! Sorry please try next request.",
                200 => "Ok",
                _ => null
            };
        }
    }
}
