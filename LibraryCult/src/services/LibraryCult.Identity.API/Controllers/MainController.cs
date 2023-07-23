using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

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

                return Ok(
                    new
                    {
                        sucess = true,
                        data = result
                    });

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Message", Errors.ToArray() }
            }));
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
