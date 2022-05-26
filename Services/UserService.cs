using System.Security.Claims;
using FastAndFuriousApi.Models;
using Microsoft.AspNetCore.Identity;

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

            var userClaims = new List<Claim> {
                new Claim("Name", userRequest.Name),
                new Claim("Nickname", userRequest.Nickname)
            };
            var claimResult = _userManager.AddClaimsAsync(user, userClaims).Result;
            if (!result.Succeeded)
            {
                return response.BuildBadRequestResponse("Um ou mais erros de validação ocorreram", new { Errors = claimResult.Errors });
            }

            return response.BuildOkResponse("Usuário cadastrado com sucesso", user);
        }
    }
}