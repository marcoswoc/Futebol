using Futebol.Api;
using Futebol.Api.Endpoints;
using Futebol.Api.Extensions;
using Futebol.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

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