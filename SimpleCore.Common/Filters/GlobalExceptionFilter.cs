using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            Log.Error(ex, "未處理例外錯誤");

            int statusCode;
            string message;

            if(ex is HttpRequestException httpEx && httpEx.StatusCode.HasValue)
            {
                statusCode = (int)httpEx.StatusCode.Value;
                message = httpEx.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => "未授權，請重新登入。",
                    HttpStatusCode.Forbidden => "禁止存取",
                    HttpStatusCode.InternalServerError => "系統錯誤，請重新登入。",
                    _ => "發生未知錯誤"
                };
            }
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = 401;
                message = "您沒有權限執行操作";
            }
            else
            {
                statusCode = 500;
                message = $"系統發生未預期錯誤，請稍後在試。{ex.Message}";
            }
            context.Result = new JsonResult(new
            {
                success = false,
                status = statusCode,
                message
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
