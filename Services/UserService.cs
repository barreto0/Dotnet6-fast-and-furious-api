using FastAndFuriousApi.Data.IWantApp.Data;
using FastAndFuriousApi.Domain.Quote;
using FastAndFuriousApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastAndFuriousApi.Services
{
    public class UserService
    {
        ResponseModel response = new ResponseModel();

        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseModel> RegisterUser(UserModel userRequest)
        {
            var user = new IdentityUser
            {
                UserName = userRequest.Email,
                Email = userRequest.Email
            };
            var result = _userManager.CreateAsync(user, userRequest.Password).Result;

            if (!result.Succeeded)
            {
                return response.BuildBadRequestResponse("Um ou mais erros de validação ocorreram", new { Errors = result.Errors });
            }
            return response.BuildOkResponse("Usuário cadastrado com sucesso", user);
        }
    }
}