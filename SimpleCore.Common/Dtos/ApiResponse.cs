using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Dtos
{
    public class ApiResponse
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
        public ApiResponse(int statusCode, string message, bool success)
        {
            StatusCode = statusCode;
            Message = message;
            Success = success;
        }
        public static ApiResponse IsSuccess(string message, bool success = true)
        {
            return new ApiResponse(200, message, success);
        }
        public static ApiResponse Fail(int statusCode, string message, bool success = false)
        {
            return new ApiResponse(statusCode, message, success);
        }
    }

    public class ApiResponse<T>
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
        public T? Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(int statusCode, string message, bool success, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Success = success;
            Data = data;
        }

        public static ApiResponse<T> IsSuccess(string message = "Success", T? data = default)
        {
            return new ApiResponse<T>(200, message, true, data);
        }

        public static ApiResponse<T> Fail(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(statusCode, message, false);
        }
    }
}
