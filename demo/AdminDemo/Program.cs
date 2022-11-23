using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TinyFx;

var builder = AdminHost.CreateBuilder(args);

var app = builder.Build();

app.UseTinyFxEx();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseBootstrapBlazor();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
