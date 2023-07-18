using LibraryCult.Identity.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
                Email = userRegister.Email,
                PasswordHash = userRegister.Password,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user);

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
                                                                  userLogin.Password, false, true);

            if (result.Succeeded)
            {
                //gerar JWT
                return Ok();
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("User is locked out by too many access wrong");
                return CustomResponse();
            }

            AddProcessingError("User or Password is incorrect.");
            return CustomResponse();
        }


        //public string Generate JsonWebToken()
        //{

        //}
    }
}
