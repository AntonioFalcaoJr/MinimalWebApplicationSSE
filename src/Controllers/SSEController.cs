using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationSSE.Controllers
{
    [ApiController, Route("[controller]")]
    public class SSEController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            while (true)
            {
                await response.WriteAsync($"{DateTimeOffset.Now}\n");
                await response.Body.FlushAsync();
                await Task.Delay(500);
            }
        }
    }
}