var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.UseRouting();

app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint();
    if(endpoint != null)
        await context.Response.WriteAsync("Endpoint Name:"+ endpoint.DisplayName + "\n\n");
    await next(context);
});


app.UseEndpoints(endpoint =>
{
    endpoint.Map("/Home", async (context) =>
    {
        await context.Response.WriteAsync("You are in home page");
    });

    endpoint.MapGet("/product", async (context) =>
    {
        await context.Response.WriteAsync("you are in products page");
    });

    endpoint.MapPost("/users", async (context) =>
    {
        await context.Response.WriteAsync("this is new users is created");
    });
    endpoint.MapGet("/product/{id?}", async (context) =>
    {
        var id = Convert.ToInt32(context.Request.RouteValues["id"]);
        if(id != null)
        {
            await context.Response.WriteAsync("The product id is:" + id);
        }
        else
        {
            await context.Response.WriteAsync("you are in products page!");
        }
       
    });
    endpoint.MapGet("/book/{authorName}/{bookid}", async (context) =>
    {
        var AuthorName = Convert.ToString(context.Request.RouteValues["authorName"]);
        var BookId = Convert.ToInt32(context.Request.RouteValues["Bookid"]);

        await context.Response.WriteAsync($"This is the Book author by:{AuthorName} and bookid: {BookId}");
    });
});

app.Run(async (HttpContext context) =>
{
    //context.Response.StatusCode = 404;
    //await context.Response.WriteAsync("THe page you are looking for is not found");
    await context.Response.WriteAsync("welcome to Asp.Net Core!");
});


//app.MapGet("/", () => "Hello World!");

app.Run();
