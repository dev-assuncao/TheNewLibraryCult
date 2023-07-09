using Microsoft.AspNetCore.Mvc;
using static LibraryCult.Identity.API.Models.UserViewModel;

namespace LibraryCult.Identity.API.Controllers
{

    [Route("api/[Controller]")]
    public class IdentityController : MainController
    {
        //    [HttpGet]
        //    public async Task<ActionResult<>> Login()
        //    {
        //    }


        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterViewModel>> RegisterUser(UserRegisterViewModel register)
        {
            return await;
        }
    }
}
