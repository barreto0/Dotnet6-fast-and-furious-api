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

        [HttpGet]
        public async Task<ActionResult> ListAuthors()
        {
            ResponseModel response = await _authorService.ListAuthors();
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
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