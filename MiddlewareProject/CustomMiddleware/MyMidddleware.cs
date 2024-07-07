
namespace MiddlewareProject.CustomMiddleware
{
    public class MyMidddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custome middleware started \n\n");
            await next(context);
            await context.Response.WriteAsync("custom middleware finished! \n\n");
        }
    }
}
