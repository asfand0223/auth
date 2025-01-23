using Auth.Configuration;
using Auth.Database;
using Auth.Interfaces;
using Auth.Repositories;
using Auth.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? allowedUrlsString = builder.Configuration.GetValue<string>("AllowedUrls");
string[] allowedUrls =
    allowedUrlsString?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowedUrls",
        builder =>
            builder.WithOrigins(allowedUrls).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    );
});

builder.Services.AddSignalR();

builder.Services.Configure<Config>(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthenticationValidationService, AuthenticationValidationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowedUrls");

app.UseRouting();
app.MapControllers();

app.Run();
