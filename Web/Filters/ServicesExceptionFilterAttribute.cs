using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Services.ExceptionsAndErrors;

namespace Web.Filters
{
    public class ServicesExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static HttpResponseMessage BuildResponseMessage(HttpActionExecutedContext context, HttpStatusCode httpStatusCode, string reasonPhrase = null)
        {
            var responseMessage = new HttpResponseMessage(httpStatusCode) { Content = new StringContent(context.Exception.Message) };
            if (reasonPhrase != null) responseMessage.ReasonPhrase = reasonPhrase;
            return responseMessage;
        }
        
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ActionArgumentException)
            {
                var httpStatusCode = HttpStatusCode.BadRequest;

                var ex = context.Exception as ActionArgumentException;
                switch (ex.ErrorType)
                {
                    case DataCheckingErrors.EmptyRequestBody:
                    case DataCheckingErrors.IdsMismatch:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        break;
                    case DataCheckingErrors.EntityNotFound:
                        httpStatusCode = HttpStatusCode.NotFound;
                        break;
                }
                context.Response = BuildResponseMessage(context, httpStatusCode);
            }

            if (context.Exception is SameThemeExistsException)
            {
                context.Response = BuildResponseMessage(context, HttpStatusCode.Conflict, "Data already exists");
            }

            if (context.Exception is AccessDeniedException)
            {
                context.Response = BuildResponseMessage(context, HttpStatusCode.Conflict, "Permission conflict");
            }

            if (context.Exception is MessageQuotedException)
            {
                context.Response = BuildResponseMessage(context, HttpStatusCode.Conflict, "Data relations conflict");
            }
        }
    }
}