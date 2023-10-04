using Serilog;
using System.Reflection.PortableExecutable;
using TinyFx;
using TinyFx.Logging;

var builder = AspNetHost.CreateBuilder(args);
// Add services to the container.
builder.AddAspNetEx(AspNetType.Api);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAspNetEx();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
