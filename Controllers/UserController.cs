using FastAndFuriousApi.Models;
using FastAndFuriousApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastAndFuriousApi.Controllers
{
    [Route("[controller]")]
    [Authorize("Bearer")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> Login(UserLoginModel userLogin)
        {
            ResponseModel response = await _userService.Login(userLogin);
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers([FromQuery] int page, int qtd)
        {
            ResponseModel response = await _userService.GetUsers(page, qtd);
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
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