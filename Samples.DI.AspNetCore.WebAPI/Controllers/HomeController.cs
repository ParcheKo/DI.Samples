using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Samples.DI.Shared.Operation;

namespace Samples.DI.AspNetCore.WebAPI.Controllers
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
