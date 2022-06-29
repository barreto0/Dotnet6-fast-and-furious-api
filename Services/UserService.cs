using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastAndFuriousApi.Domain;
using FastAndFuriousApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FastAndFuriousApi.Services
{
    public class UserService
    {
        ResponseModel response = new ResponseModel();

        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        public UserService(UserManager<IdentityUser> userManager, IConfiguration iconfig)
        {
            _userManager = userManager;
            _configuration = iconfig;
        }

        public async Task<ResponseModel> Login(UserLoginModel userLogin)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(userLogin.Email).Result;
                if (user == null)
                {
                    return response.BuildBadRequestResponse("Email ou senha incorretos", new { });
                }
                if (!_userManager.CheckPasswordAsync(user, userLogin.Password).Result)
                {
                    return response.BuildBadRequestResponse("Email ou senha incorretos", new { });
                }
                var claims = _userManager.GetClaimsAsync(user).Result;
                var claimName = claims.FirstOrDefault(u => u.Type == "Name");
                var claimNickname = claims.FirstOrDefault(u => u.Type == "Nickname");

                string name = claimName != null ? claimName.Value : string.Empty;
                string nickname = claimNickname != null ? claimNickname.Value : string.Empty;
                UserResponseModel defaultRetUser = new UserResponseModel(user.Id, name, nickname, user.Email);
                var token = GenerateToken(userLogin);
                return response.BuildOkResponse("Login feito com sucesso", new GetTokenModel(
                        new JwtSecurityTokenHandler().WriteToken(token),
                        defaultRetUser,
                        token.ValidTo
                    ));
            }
            catch (System.Exception e)
            {
                return response.BuildErrorResponse("Um erro ocorreu ao tentar efetuar login", new { ErrorMessage = e.Message });
                throw;
            }
        }

        public SecurityToken GenerateToken(UserLoginModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenAuthentication")["SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration.GetSection("TokenAuthentication")["Issuer"],
                Audience = _configuration.GetSection("TokenAuthentication")["Audience"],

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
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
                if (!claimResult.Succeeded)
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

        public async Task<ResponseModel> CreateAdmin()
        {
            try
            {
                UserModel userRequest = new UserModel(name: "Admin", nickname: "admin", email: "gabriel.barreto.dev@gmail.com", password: "@Teste123");
                IdentityUser user = new IdentityUser
                {
                    UserName = userRequest.Email,
                    Email = userRequest.Email
                };
                var result = _userManager.CreateAsync(user, userRequest.Password).Result;

                if (!result.Succeeded)
                {
                    return response.BuildBadRequestResponse("Um ou mais erros de validação ocorreram", new { });
                }

                List<Claim> userClaims = new List<Claim> {
                    new Claim("Name", userRequest.Name),
                    new Claim("Nickname", userRequest.Nickname)
                };
                var claimResult = _userManager.AddClaimsAsync(user, userClaims).Result;
                if (!result.Succeeded)
                {
                    return response.BuildBadRequestResponse("Um ou mais erros de validação ocorreram", new { });
                }

                return response.BuildOkResponse("Usuário cadastrado com sucesso", new { });
            }
            catch (System.Exception e)
            {

                return response.BuildErrorResponse("Ops! Algo aconteceu durante o cadastro de usuário, por favor tente novamente.", new { ErrorMessage = e.Message });

            }
        }
    }
}