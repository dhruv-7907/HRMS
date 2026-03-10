using Application;
using Domain.Common;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Serilog;
//using Serilog.Events;
using System.Text;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region 🔥 Configure Serilog (Production Ready)

Log.Logger = new LoggerConfiguration()
     .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

try
{
    Log.Information("Starting Web API");

    #region CORS

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowOrigin",
            policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    });

    #endregion

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<GenerateJwtToken>();

    builder.Services.Configure<MailSettings>(
        builder.Configuration.GetSection("MailSettings"));

    // Clean Architecture Layers
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPersistance(builder.Configuration);

    #region JWT Authentication

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                ),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

    builder.Services.AddAuthorization();

    #endregion

    var app = builder.Build();

    #region Middleware Pipeline

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging(); // 🔥 Logs all HTTP requests

    app.UseHttpsRedirection();
    app.UseCors("AllowOrigin");

    app.UseMiddleware<GlobalExceptionMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    #endregion

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}