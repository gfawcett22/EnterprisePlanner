using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBindings;


namespace WebApiHelpers
{
    public class UnprocessableEntityObjectResult : ObjectResult 
    {
        public UnprocessableEntityObjectResult(ModelStateDictionary modelState) : base(new SerializableError(modelState))
        {
            if(modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            StatusCode = 422;
        }
    }
}