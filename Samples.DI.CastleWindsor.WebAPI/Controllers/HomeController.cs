using Microsoft.AspNetCore.Mvc;

namespace Samples.DI.CastleWindsor.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
            return new ContentResult()
            {
                Content = "<p>" +
                          "<ul>" +
                          $"<li><a href=\"{baseUrl}/LifetimeDemo\">Lifetime Demo</a></li>" +
                          $"<li><a href=\"{baseUrl}/FeaturesDemo\">Features Demo Demo</a></li>" +
                          "</ul>" +
                          "</p>",
                ContentType = "text/html"
            };
        }
    }
}
