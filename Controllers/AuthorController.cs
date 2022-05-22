using FastAndFuriousApi.Data.IWantApp.Data;
using FastAndFuriousApi.Models;
using FastAndFuriousApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastAndFuriousApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpPost]
        public async Task<ActionResult> RegisterAuthor(AuthorModel author)
        {
            ResponseModel response = await _authorService.RegisterAuthor(author);
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
        }
    }
}