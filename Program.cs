using System.Text;
using Authenticate;
using FindMyPet.Business;
using FindMyPet.Repository;
using FindMyPet.Repository.Interfaces;
using FindMyPet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Repository
builder.Services.AddScoped<IAccoutRepository, AccoutRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Business
builder.Services.AddTransient<AccoutBusiness>();
builder.Services.AddTransient<ApplicationBusiness>();
builder.Services.AddTransient<MessageRequest>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// tempo de vida do serviço
builder.Services.AddTransient<TokenService>(); // sempre criar um novo
//builder.Services.AddScoped(); // enquanto a requisição durar
//builder.Services.AddSingleton(); // 1 por aplicação

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();