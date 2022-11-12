using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using SD;
using SD.Models.Repositories;
using SD.Models.Repositories.Interfaces;
using SD.Repos.IRepository;
using SD.Services;
using SD.Services.Interfaces;
using SD_lib.DB;
using SD_lib.Tokens.Services;
using Microsoft.OpenApi.Models;
using SD_lib.Tokens.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SD.Validation.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.RequestHeaders.Add("Authorization");
    logging.RequestHeaders.Add("X-real-IP");
    logging.RequestHeaders.Add("X-Forwarded-For");
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    })
    .UseNLog(new NLogAspNetCoreOptions(){
        RemoveLoggerFactoryFilter = true
});

builder.Services.AddDbContext<SD_libContext>();

builder.Services.AddScoped<ITokenService, TokenService>();

//right solution?
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<IValidationServiceAccount, ValidationServiceAccount>();
builder.Services.AddSingleton<IValidationServiceCard, ValidationServiceCard>();
builder.Services.AddSingleton<IValidationServiceClient, ValidationServiceClient>();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme =
    JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new
                TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthSettings:SecretWord"])),
                   ValidateIssuer = false,
                   ValidateLifetime = true,
                   ValidateAudience = false,
                   ClockSkew = TimeSpan.Zero
               };
           });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Some service",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "DD",
            Url = new Uri("https://github.com/ddoo5")
        },
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "This is Authorization \n Example: 'Bearer 1234token'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

