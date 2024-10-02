using Futebol.Api;
using Futebol.Api.Endpoints;
using Futebol.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFutebolApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
}

app.UseCors("futebolApi");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapEndpoints();

await app.RunAsync();