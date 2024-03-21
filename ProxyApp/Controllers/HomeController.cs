using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProxyApp.Services;
using System.Net;

namespace ProxyApp.Controllers
{
    [EnableRateLimiting("fixed")]
    public class HomeController : Controller
    {
        private readonly IExternalApiService _apiService;

        public HomeController(IExternalApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("swapi")]
        public string CallAPI(string resource, int id)
        {
            var result = _apiService.GetResourceById(resource, id);
            return result;

        }

        [HttpGet("getresource")]
        public string GetResource(string resource)
        {
            var result = _apiService.GetResource(resource);
            return result;
        }
    }
}
