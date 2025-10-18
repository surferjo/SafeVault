using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SafeVault.Data;
using SafeVault.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer; // ✅ Added
using Microsoft.IdentityModel.Tokens;                // ✅ Added
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure in-memory database (for testing and development)
builder.Services.AddDbContext<SafeVaultContext>(options =>
    options.UseInMemoryDatabase("SafeVaultDb"));

// Register repositories
builder.Services.AddScoped<UserRepository>();

// ✅ Configure authentication with JWT (fixed missing reference)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Basic JWT validation configuration (for dev/test)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey123!"))
        };
    });

builder.Services.AddAuthorization();

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

app.Run();

/// <summary>
/// Partial Program class for integration testing (required by WebApplicationFactory)
/// </summary>
public partial class Program { }
