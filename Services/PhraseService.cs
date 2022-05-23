using FastAndFuriousApi.Data.IWantApp.Data;
using FastAndFuriousApi.Domain.Quote;
using FastAndFuriousApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FastAndFuriousApi.Services
{
    public class PhraseService
    {
        private readonly ApplicationDbContext db;
        ResponseModel response = new ResponseModel();
        public PhraseService(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<ResponseModel> ListPhrases()
        {
            try
            {
                List<Phrase> phrasesFromDb = db.Phrases.Where(p => p.Active == true).Include(p => p.Author).ToList();
                return response.BuildOkResponse("Busca realizada com sucesso", phrasesFromDb);
            }
            catch (System.Exception e)
            {
                return response.BuildErrorResponse("Ops! Algo aconteceu durante a listagem de Frase, por favor tente novamente.", new { ErrorMessage = e.Message });
            }
        }
        public async Task<ResponseModel> RegisterPhrase(PhraseModel phraseRequest)
        {
            try
            {
                Phrase phraseFromDb = await db.Phrases.Where(a => a.Text == phraseRequest.Text).FirstOrDefaultAsync();
                Author authorFromDb = await db.Authors.Where(a => a.Id == phraseRequest.AuthorId).FirstOrDefaultAsync();

                if (phraseFromDb != null)
                {
                    return response.BuildBadRequestResponse("Frase já cadastrada no sistema", new { });
                }
                if (authorFromDb == null)
                {
                    return response.BuildBadRequestResponse("Autor não encontrado na base de dados, solicite o cadastro no mesmo", new { });
                }
                Phrase phrase = new Phrase
                {
                    Text = phraseRequest.Text,
                    Author = authorFromDb,
                    CreatedAt = DateTime.Now
                };
                await db.Phrases.AddAsync(phrase);
                await db.SaveChangesAsync();
                return response.BuildOkResponse("Frase cadastrada com sucesso", phrase);

            }
            catch (System.Exception e)
            {
                return response.BuildErrorResponse("Ops! Algo aconteceu durante o cadastro de Frase, por favor tente novamente.", new { ErrorMessage = e.Message });

            }
        }

        public async Task<ResponseModel> ChangePhraseStatus(Guid phraseId)
        {
            try
            {
                Phrase phraseFromDb = await db.Phrases.Where(a => a.Id == phraseId).FirstOrDefaultAsync();
                if (phraseFromDb == null)
                {
                    return response.BuildBadRequestResponse("Frase não encontrada", new { });
                }
                phraseFromDb.Active = !phraseFromDb.Active;
                await db.SaveChangesAsync();
                return response.BuildOkResponse("Status de frase atualizado com sucesso", phraseFromDb);
            }
            catch (System.Exception e)
            {
                return response.BuildErrorResponse("Ops! Algo aconteceu durante a mudança de status da Frase, por favor tente novamente.", new { ErrorMessage = e.Message });
            }
        }
    }
}