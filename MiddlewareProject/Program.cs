using MiddlewareProject.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

// custom middleware
builder.Services.AddTransient<MyMidddleware>();

var app = builder.Build();


//app.MapGet("/", () => "Hello World!");

// middleware-1 
app.Use(async (HttpContext context, RequestDelegate next) => 
{
    await context.Response.WriteAsync("Chain the next middleware - 1 \n\n");
    await next(context);
});

//Middleware-2
app.Use(async (context, next ) =>
{
    await context.Response.WriteAsync(" middleware-2 \n\n");
    await next(context);
});

// middleware-3
app.UseMiddleware<MyMidddleware>();

//middleware-4 Executing middleware conditionally.
app.UseWhen(context => context.Request.Query.ContainsKey("IsAuthorized") 
            && context.Request.Query["IsAuthorized"] == "true",
            app =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Middleware-4 \n\n");
                    await next(context);
                });
            });

// Middleware-5
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("First .netcore application- middleware 5 \n\n");
});

app.Run();