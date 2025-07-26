using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCore.Common.Dtos;

namespace SimpleCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController() { }

        /// <summary>
        /// 無返回值
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult OkResponse(string message = "成功")
        {
            return Ok(ApiResponse.IsSuccess(message));
        }
        /// <summary>
        /// 有返回值
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult OkResponse<T>(string message = "成功", T? data = default)
        {
            return Ok(ApiResponse<T>.IsSuccess(message,data));
        }
        /// <summary>
        /// 無返回值,非系統錯。失敗返回用(例如:帳號查詢失敗)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult FailReponse(string message = "失敗")
        {
            return Ok(ApiResponse.IsSuccess(message, false));
        }
    }
}
