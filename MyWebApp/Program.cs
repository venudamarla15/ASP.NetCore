using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", (HttpContext context) =>
//    {

//        //string path = context.Request.Path;
//        //string method = context.Request.Method;
//        context.Response.Headers["ContentType"] = "text/html";
//        //var UserAgent = "";
//        //if(context.Request.Headers.ContainsKey("User-Agent"))
//        //{
//        //    UserAgent = context.Request.Headers["User-Agent"];

//        //}
//        //int code = context.Response.StatusCode;
//        return  "<h2>Hello world</h2>";
//    }
//);
app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;

    if (path == "/" || path == "/Home")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("You are in home page");
    }
    else if(path =="/contacts")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("You are in contacts Page");
    } 
    else if(method == "GET" && path =="/product")
        {
        context.Response.StatusCode = 200;
        if ((context.Request.Query.ContainsKey("id")) && (context.Request.Query.ContainsKey("name")))
        {
            string id = context.Request.Query["id"];
            string name = context.Request.Query["name"];

            await context.Response.WriteAsync("You are select product id:" + id + "and productname:" + name);
            return;

        }
        
        await context.Response.WriteAsync(" you are in product page");
    }else if(method == "POST" && path == "/product")
    {
        string id = "";
        string name = "";
        StreamReader reader = new StreamReader(context.Request.Body);
        string data = await reader.ReadToEndAsync();
        await context.Response.WriteAsync("Request body contains"+ data );
        Dictionary<string, StringValues> dict =  QueryHelpers.ParseQuery(data);
        if(dict.ContainsKey("id"))
        {
            id = dict["id"];
        }
        if(dict.ContainsKey("name"))
        {
            //string s = "";
            foreach (string key in dict.Values)
            {
                name = dict["name"];
            }
        }
        await context.Response.WriteAsync("id:"+ id + "name:" + name);

    }

    else
        {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Not found");
        
    }

});
app.Run();
