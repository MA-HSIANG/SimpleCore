using Microsoft.AspNetCore.Mvc;
using SimpleCore.Common.Cache;
using SimpleCore.Common.DB.DbContexts;
using SimpleCore.Model;
using SimpleCore.Model.ViewModel;
using SimpleCore.Service.Base;
using System.Net;

namespace SimpleCore.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICaching _cache;
        private readonly IBaseService<UserIninfoModel, UserIninfoViewModel> _userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IBaseService<UserIninfoModel, UserIninfoViewModel> userService,
            ICaching cache)
        {
            _logger = logger;
            _userService = userService;
            _cache = cache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        public IActionResult TestGetApiUnauthorized()
        {
            // 這會觸發 Exception Filter
            throw new HttpRequestException("Unauthorized access", null, HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        public async Task<IActionResult> TestPostUserCache()
        {
            var users = await _userService.GetAllAsync();
            string cacheKey = $"{CacheConst.KeyUser}List";
            await _cache.SetListAsync(cacheKey, users);

            return OkResponse();
        }
        [HttpPost]
        public async Task<IActionResult> TestGetCache()
        {
            string cacheKey = $"{CacheConst.KeyUser}List";
            var userCaches = await _cache.GetListAsync<UserIninfoModel>(cacheKey);

            return OkResponse("成功",userCaches);

        }

    }
}
