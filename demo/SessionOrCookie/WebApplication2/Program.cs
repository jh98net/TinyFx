using Microsoft.AspNetCore.Authorization;
using TinyFx;
using TinyFx.AspNet;

var builder = AspNetHost.CreateBuilder();

// Add services to the container.
builder.AddAspNetEx(AspNetType.Api);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAspNetEx();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/demo", [Authorize] async () =>
{
    var userId = HttpContextEx.User?.Identity?.Name;
    Console.WriteLine($"Identity: {userId}");
})
.WithName("get");

app.Run();