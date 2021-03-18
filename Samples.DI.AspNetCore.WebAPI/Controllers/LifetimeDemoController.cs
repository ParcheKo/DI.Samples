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
    public class LifetimeDemoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Call 1
            var transientOperation = HttpContext.RequestServices.GetService(typeof(ITransientOperation));
            var scopedOperation = HttpContext.RequestServices.GetService(typeof(IScopedOperation));
            var singletonOperation = HttpContext.RequestServices.GetService(typeof(ISingletonOperation));
            // Call 2
            var transientOperation2 = HttpContext.RequestServices.GetService(typeof(ITransientOperation));
            var scopedOperation2 = HttpContext.RequestServices.GetService(typeof(IScopedOperation));
            var singletonOperation2 = HttpContext.RequestServices.GetService(typeof(ISingletonOperation));
            return new JsonResult(new
            {
                Call1 = new
                {
                    Info = "Call 1 to HttpContext.RequestServices.GetService()",
                    Transient = transientOperation?.ToString(),
                    Scoped = scopedOperation?.ToString(),
                    Singleton = singletonOperation?.ToString()
                },
                Call2 = new
                {
                    Info = "Call 2 to HttpContext.RequestServices.GetService()",
                    Transient = transientOperation2?.ToString(),
                    Scoped = scopedOperation2?.ToString(),
                    Singleton = singletonOperation2?.ToString()
                },
            });
        }
    }
}
