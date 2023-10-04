using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TinyFx;
using TinyFx.AspNet;

var builder = AspNetHost.CreateBuilder(args);

// Add services to the container.
builder.AddAspNetEx(AspNetType.Api);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAspNetEx();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/demo", async () =>
{
    var userId = Guid.NewGuid().ToString();
    Console.WriteLine(userId);
    await HttpContextEx.SignInUseCookie(userId);
})
.WithName("set");

app.Run();
