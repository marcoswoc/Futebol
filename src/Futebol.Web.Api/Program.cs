using Futebol.Web.Core.Extensions;
using Futebol.Web.Core.Middlewares;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddFutebolCore(builder.Configuration);
    await builder.Services.Initialize(builder.Configuration);   

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.UseWebAssemblyDebugging();
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapRazorPages();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}