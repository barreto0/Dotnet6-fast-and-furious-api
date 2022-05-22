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
                if (authorFromDb == null)
                {
                    Author author = new Author
                    {
                        Name = authorRequest.Name,
                        Movie = authorRequest.Movie,
                        CreatedAt = DateTime.Now
                    };
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return response.BuildOkResponse("Autor cadastrado com sucesso", author);
                }
                return response.BuildBadRequestResponse("Autor já cadastrado no sistema", new { });
            }
            catch (System.Exception e)
            {
                return response.BuildErrorResponse("Ops! Algo aconteceu durante o cadastro de Author, por favor tente novamente.", new { ErrorMessage = e.Message });

            }
        }
    }
}