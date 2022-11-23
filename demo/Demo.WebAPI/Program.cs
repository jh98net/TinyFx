using Serilog;
using TinyFx;
using TinyFx.Logging;

var builder = AspNetHost.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTinyFxEx(AspNetType.Api);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseTinyFxEx();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
