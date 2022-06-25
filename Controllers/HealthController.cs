
using FastAndFuriousApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MalbyApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult HelloWorld()
        {
            ResponseModel response = new ResponseModel
            {
                StatusCode = 200,
                Content = "Healthy"
            };
            return new OkObjectResult(response);
        }

    }
}
