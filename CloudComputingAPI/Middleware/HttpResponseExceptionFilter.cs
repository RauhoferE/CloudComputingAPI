using CloudComputingAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CloudComputingAPI.Middleware
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = StatusCodes.Status400BadRequest;
            var responseMesage = context.Exception.Message;

            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                statusCode = StatusCodes.Status404NotFound;
            }

            context.Result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    statusCode,
                    message = responseMesage
                }),
                ContentType = "application/json",
                StatusCode = statusCode
            };
        }
    }
}
