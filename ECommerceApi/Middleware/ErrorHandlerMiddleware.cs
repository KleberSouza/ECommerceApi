
using ECommerceApi.Models.Entities;
using System.Net;
using System.Text.Json;

namespace ECommerceApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    var response = new { messagem = "Autenticação necessária. Por favor, faça login." };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
                else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    var response = new { messagem = "Você não tem permissão para acessar este recurso." };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ArgumentException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { messagem = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
