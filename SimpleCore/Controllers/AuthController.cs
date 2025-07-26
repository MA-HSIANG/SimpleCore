using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCore.Common.Dtos;
using SimpleCore.Model;
using SimpleCore.Model.Dtos;
using SimpleCore.Model.ViewModel;
using SimpleCore.Service.Base;
using SimpleCore.Service.Interfaces;

namespace SimpleCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ApiRequest<LoginDTO> dto)
        {
            if (string.IsNullOrEmpty(dto?.Data?.LoginName) || string.IsNullOrEmpty(dto.Data.Password))
                return FailReponse("帳號或密碼請誤留空");

            var data = await _authService.Login(dto.Data);
            if(data.Id == 0)
            {
                return FailReponse();
            }
            return OkResponse("成功", data);
        }
    }
}
