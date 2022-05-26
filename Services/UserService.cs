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

        public async Task<ResponseModel> GetUsers(int page, int qtd)
        {
            try
            {
                var usersFromDb = _userManager.Users.Skip((page - 1) * qtd).Take(qtd).ToList();
                var users = new List<UserResponseModel>();
                foreach (var user in usersFromDb)
                {
                    var claims = _userManager.GetClaimsAsync(user).Result;
                    var claimName = claims.FirstOrDefault(u => u.Type == "Name");
                    var claimNickname = claims.FirstOrDefault(u => u.Type == "Nickname");

                    string name = claimName != null ? claimName.Value : string.Empty;
                    string nickname = claimNickname != null ? claimNickname.Value : string.Empty;

                    users.Add(new UserResponseModel(user.Id, name, nickname, user.Email));
                }
                return response.BuildOkResponse("Listagem feita com sucesso", users);
            }
            catch (System.Exception e)
            {

                return response.BuildErrorResponse("Ops! Algo aconteceu durante a listagem de Usuários, por favor tente novamente.", new { ErrorMessage = e.Message });

            }
        }

        public async Task<ResponseModel> RegisterUser(UserModel userRequest)
        {
            try
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = userRequest.Email,
                    Email = userRequest.Email
                };
                var result = _userManager.CreateAsync(user, userRequest.Password).Result;

                if (!result.Succeeded)
                {
                    return response.BuildBadRequestResponse("Um ou mais erros de validação ocorreram", new { Errors = result.Errors });
                }

                List<Claim> userClaims = new List<Claim> {
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
            catch (System.Exception e)
            {

                return response.BuildErrorResponse("Ops! Algo aconteceu durante o cadastro de usuário, por favor tente novamente.", new { ErrorMessage = e.Message });

            }
        }
    }
}