using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Postter.Common.Assert;
using Postter.Common.Auth;
using Postter.Common.Middlewares;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.Repository;
using Postter.UseCases.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
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

// Other services
// Common
builder.Services.AddTransient<IAssert, Assert>();
// Infrasctructure
builder.Services.AddTransient<IUserRepository, UserRepository>();
// UseCase
builder.Services.AddTransient<IUseCaseAccount, UseCaseAccount>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

// Custom middleware
app.UseMiddleware<ExceptionMiddleware>();
app.Run();