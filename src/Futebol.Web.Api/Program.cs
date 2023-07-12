using Futebol.Web.Core.Extensions;
using Futebol.Web.Core.Middlewares;

try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddFutebolCore(builder.Configuration);
    await builder.Services.Initialize(builder.Configuration);
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
        builder.WebHost.UseUrls($"http://*:{Environment.GetEnvironmentVariable("PORT")}");

    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}