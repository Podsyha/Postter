using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Postter.Common.Assert;
using Postter.Common.Attribute;
using Postter.Common.Auth;
using Postter.Common.Helpers;
using Postter.Common.Middlewares;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.Repository.AccountRepository;
using Postter.Infrastructure.Repository.CommentRepository;
using Postter.Infrastructure.Repository.LikeRepository;
using Postter.Infrastructure.Repository.PostRepository;
using Postter.Infrastructure.Repository.RoleRepository;
using Postter.UseCases.UseCaseAccount;
using Postter.UseCases.UseCaseComment;
using Postter.UseCases.UseCaseLike;
using Postter.UseCases.UseCasePost;
using Postter.UseCases.UseCaseRole;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1" });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT token into field"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] { }
        }
    });
});
builder.Services.AddAuthentication(cfg =>
    {
        cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,

        };
    });

// Connect to PostgreSQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString), ServiceLifetime.Transient);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

// Other services
// Common
builder.Services.AddScoped<IAssert, Assert>();
builder.Services.AddScoped<IRegistrationHelper, RegistrationHelper>();
builder.Services.AddScoped<ICustomAuthorizeAttribute, CustomAuthorizeAttribute>();
// Infrasctructure
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
// UseCase
builder.Services.AddTransient<IUseCaseAccount, UseCaseAccount>();
builder.Services.AddTransient<IUseCaseRole, UseCaseRole>();
builder.Services.AddTransient<IUseCasePost, UseCasePost>();
builder.Services.AddTransient<IUseCaseComment, UseCaseComment>();
builder.Services.AddTransient<IUseCaseLike, UseCaseLike>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Custom middleware
app.UseMiddleware<ExceptionMiddleware>();
app.Run();