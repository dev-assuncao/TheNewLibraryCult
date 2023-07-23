using LibraryCult.Identity.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static LibraryCult.Identity.API.Models.UserViewModel;

namespace LibraryCult.Identity.API.Controllers
{

    [Route("api/[Controller]")]
    public class IdentityController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWT _appSettings;

        public IdentityController(SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager,
                                  IOptions<JWT> settings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = settings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(UserRegisterViewModel userRegister)
        {

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                // chamar gerador de JWT
                return Ok();
            }

            foreach (var erro in result.Errors)
            {
                AddProcessingError(erro.Description);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email,
                                                                  userLogin.Password, false, false);

            if (result.Succeeded)
            {
                //gerar JWT
                return CustomResponse(GenerateJWT());
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("User is locked out by too many access wrong");
                return CustomResponse();
            }

            AddProcessingError("User or Password is incorrect.");
            return CustomResponse();
        }


        private string GenerateJWT()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationTime),
                SigningCredentials  = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            }) ;

            return tokenHandler.WriteToken(token);
        }
    }
}
