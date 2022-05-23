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

        [HttpGet]
        public async Task<ActionResult> ListPhrases()
        {
            ResponseModel response = await _phraseService.ListPhrases();
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
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

        [HttpGet]
        [Route("change-phrase-status/{phraseId}")]
        public async Task<ActionResult> ChangePhraseStatus([FromRoute] Guid phraseId)
        {
            ResponseModel response = await _phraseService.ChangePhraseStatus(phraseId);
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
        }

        [HttpGet]
        [Route("random")]
        public async Task<ActionResult> ListRandomPhrase()
        {
            ResponseModel response = await _phraseService.ListRandomPhrase();
            ObjectResult result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            return result;
        }
    }
}