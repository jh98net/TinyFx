using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection.PortableExecutable;
using TinyFx;
using TinyFx.Logging;

var builder = AspNetHost.CreateBuilder(args);
// Add services to the container.
builder.AddAspNetEx(AspNetType.Api);
builder.Host.ConfigureServices(services =>
{
    services.AddSingleton(new TESTA { Name="aa"});
    services.AddSingleton(new TESTB { Name = "bb" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAspNetEx();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public class TESTA
{
    public string Name { get; set; }
}
public class TESTB
{
    public string Name { get; set; }
}