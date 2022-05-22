using FastAndFuriousApi.Data.IWantApp.Data;
using FastAndFuriousApi.Domain.Quote;
using FastAndFuriousApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FastAndFuriousApi.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext db;
        ResponseModel response = new ResponseModel();
        public AuthorService(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<ResponseModel> RegisterAuthor(AuthorModel authorRequest)
        {
            try
            {
                Author authorFromDb = await db.Authors.Where(a => a.Name == authorRequest.Name).FirstOrDefaultAsync();
                if (authorFromDb != null)
                {
                    return response.BuildBadRequestResponse("Autor já cadastrado no sistema", new { });

                }
                Author author = new Author
                {
                    Name = authorRequest.Name,
                    Movie = authorRequest.Movie,
                    CreatedAt = DateTime.Now
                };
                await db.Authors.AddAsync(author);
                await db.SaveChangesAsync();
                return response.BuildOkResponse("Autor cadastrado com sucesso", author);
            }
            catch (System.Exception e)
            {
                return response.BuildErrorResponse("Ops! Algo aconteceu durante o cadastro de Author, por favor tente novamente.", new { ErrorMessage = e.Message });

            }
        }
    }
}