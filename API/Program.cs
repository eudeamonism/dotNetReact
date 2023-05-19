using Persistence;
using Microsoft.EntityFrameworkCore;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Application Services Extension
builder.Services.AddApplicationServices(builder.Configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Middleware
app.UseCors("CorsPolicy");

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
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}


app.Run();
