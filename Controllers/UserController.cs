using FastAndFuriousApi.Models;
using FastAndFuriousApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastAndFuriousApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserModel userRequest)
        {
            ResponseModel response = await _userService.RegisterUser(userRequest);
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
        }
    }
}