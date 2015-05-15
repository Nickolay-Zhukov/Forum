using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Services.ExceptionsAndErrors;

namespace Web.Filters
{
    public class ServicesExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static HttpResponseMessage BuildResponseMessage(HttpActionExecutedContext context, HttpStatusCode httpStatusCode, string reasonPhrase)
        {
            return new HttpResponseMessage(httpStatusCode)
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = reasonPhrase
            };
        }
        
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ActionArgumentException)
            {
                var httpStatusCode = HttpStatusCode.BadRequest;
                var reasonPhrase = string.Empty;

                var ex = context.Exception as ActionArgumentException;
                switch (ex.ErrorType)
                {
                    case DataCheckingErrors.EntityNotFound:
                        httpStatusCode = HttpStatusCode.NotFound;
                        reasonPhrase = "Entity ID Not Found";
                        break;
                    case DataCheckingErrors.IdsMismatch:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        reasonPhrase = "Bad request";
                        break;
                }
                context.Response = BuildResponseMessage(context, httpStatusCode, reasonPhrase);
            }

            if (context.Exception is MessageQuotedException)
            {
                context.Response = BuildResponseMessage(context, HttpStatusCode.Conflict, "Data relations conflict");
            }

            if (context.Exception is SameThemeExistsException)
            {
                context.Response = BuildResponseMessage(context, HttpStatusCode.Conflict, "Data already exist");
            }

            if (context.Exception is DbUpdateConcurrencyException)
            {
                context.Response = BuildResponseMessage(context, HttpStatusCode.Conflict, "Data access denided");
            }
        }
    }
}