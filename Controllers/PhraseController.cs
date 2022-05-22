using FastAndFuriousApi.Data.IWantApp.Data;
using FastAndFuriousApi.Models;
using FastAndFuriousApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastAndFuriousApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class PhraseController : ControllerBase
    {
        private readonly PhraseService _phraseService;

        public PhraseController(PhraseService phraseService)
        {
            _phraseService = phraseService;
        }


        [HttpPost]
        public async Task<ActionResult> RegisterPhrase(PhraseModel phrase)
        {
            ResponseModel response = await _phraseService.RegisterPhrase(phrase);
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
        }
    }
}