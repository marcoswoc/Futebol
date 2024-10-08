using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Futebol.Web;
using Futebol.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Futebol.Shared.Handlers;
using Futebol.Web.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();

builder.Services.AddHttpClient("futebol", opt =>
{
    opt.BaseAddress = new Uri("https://localhost:7070");
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<ITeamHandler, TeamHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();

await builder.Build().RunAsync();
