using Persistence;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//This provides authentication to any endpoint other than Login and Register
builder.Services.AddControllers(opt => {
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

//Application Services Extension
builder.Services.AddApplicationServices(builder.Configuration);

//Identity Services Extension
builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
//If we are in development mode, we hit the following middleware

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Middleware
app.UseCors("CorsPolicy");

//Authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//using: whenever whatever is inside this is used, it is disposed of here.
//We created a scope to gain access to services.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

//Database

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}


app.Run();
