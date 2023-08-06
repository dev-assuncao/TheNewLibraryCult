using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using LibraryCult.Identity.API.Models;
using static LibraryCult.Identity.API.Models.UserViewModel;

namespace LibraryCult.Identity.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(result);

            //return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            //{
            //    {"Message", Errors.ToArray() }
            //}));

            return BadRequest(new UserResponseViewModel
            {
                Status = false,
                Errors = Errors.ToArray(),
                StatusCode = 400,               
            });
        }
  
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors).ToList();

            foreach (var erro in errors)
            {
                AddProcessingError(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected void AddProcessingError(string message)
        {
            Errors.Add(message);
        }

        protected void ClearErrorMessages()
        {
            Errors.Clear();
        }

    }
}
