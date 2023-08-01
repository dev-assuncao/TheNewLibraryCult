using LibraryCult.Identity.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                return CustomResponse(GenerateJWT(userRegister.Email));
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
                return CustomResponse(GenerateJWT(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("User is locked out by too many access wrong");
                return CustomResponse();
            }

            AddProcessingError("User or Password is incorrect.");
            return CustomResponse();
        }


        private async Task<UserResponseViewModel> GenerateJWT(string userEmail)
        {

            var user = await _userManager.FindByEmailAsync(userEmail);

            var claims = await _userManager.GetClaimsAsync(user);


            var identityClaims = await GetClaimsUser(user, claims);
      
            var token = EncodedToken(identityClaims);

            return GetUserResponseToken(token, user, claims);
        }

        private async Task<ClaimsIdentity> GetClaimsUser(IdentityUser user, ICollection<Claim> claims)
        {
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodedToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserResponseViewModel GetUserResponseToken (string token, IdentityUser identityUser, ICollection<Claim> claims)
        {
            return new UserResponseViewModel
            {
                AccessToken = token,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationTime).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    Claims = claims.Select(x => new UserClaimsViewModel { Type = x.Type, Value = x.Value })
                }
            };
        }


        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
