using Blazored.LocalStorage;
using Futebol.Web;
using Futebol.Web.JwtService;
using Futebol.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiBaseAddress = builder.Configuration["APIBaseAddress"];
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserServiceClient>();
builder.Services.AddScoped<ITokenService, TokenService>();
await builder.Build().RunAsync();
