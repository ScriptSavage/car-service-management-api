using Api.Middlewares;
using Application.Extensions;
using Application.Seeders;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddScoped<GlobalErrorHandlingMiddleware>();
builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

await using (var scope = app.Services.CreateAsyncScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
    
    await AdminSeeder.AddAdmin(userManager,builder.Configuration);
}


app.UseHttpsRedirection();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

