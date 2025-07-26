using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCore.Common.Cache;
using SimpleCore.Common.HttpContext;
using SimpleCore.Model;

namespace SimpleCore.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly ICaching _cache;
        private readonly ISysUser _sysUser;
        public TestController(ICaching cache, ISysUser sysUser)
        {
            _cache = cache;
            _sysUser = sysUser;
        }

        [HttpPost]
        public async Task<IActionResult> GetUserCache()
        {
            string cacheKey = $"{CacheConst.KeyUser}List";
            var userCaches = await _cache.GetListAsync<UserIninfoModel>(cacheKey);
            Console.WriteLine("當前登入使用者:" + _sysUser.Name);

            return OkResponse("成功", userCaches);

        }
    }
}
