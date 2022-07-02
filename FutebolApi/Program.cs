using FutebolApi;
using FutebolApi.Data;
using FutebolApi.Data.Repositories;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Middleware;
using FutebolApi.Services;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder => { 
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var defaultConnection  = builder.Configuration.GetConnectionString("NpgSqlDefaultConnection");

var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? defaultConnection;

connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
var userPassSide = connectionUrl.Split("@")[0];
var hostSide = connectionUrl.Split("@")[1];

var user = userPassSide.Split(":")[0];
var password = userPassSide.Split(":")[1];
var host = hostSide.Split("/")[0];
var database = hostSide.Split("/")[1];

//var defaultConnectionString = $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
var defaultConnectionString = $"Host={host};Database={database};Username={user};Password={password};Trust Server Certificate=true";


builder.Services.AddDbContext<DataContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    opt.UseNpgsql(defaultConnectionString);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

var serviceProvider = builder.Services.BuildServiceProvider();
try
{
    var dbContext = serviceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Acesso protegido utilizando o accessToken obtido em \"api/user/login\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAutoMapper(x => { x.AllowNullCollections = true; }, typeof(MapperProfile));

builder.Services.AddScoped<IPlayerRepository, PlayerRepository<DataContext>>();
builder.Services.AddScoped<IRoundRepository, RoundRepository<DataContext>>();
builder.Services.AddScoped<IVoteRepository, VoteRepository<DataContext>>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IRoundService, RoundService>();
builder.Services.AddScoped<IVoteService, VoteService>();

builder.WebHost.UseUrls($"http://*:{Environment.GetEnvironmentVariable("PORT")}");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

await SeedData.Initialize(serviceProvider, builder.Configuration);

app.Run();
