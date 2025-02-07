using API.Data;
using API.Models.Interfaces;
using API.Models.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFilmStudioRepository, FilmStudioRepository>();
builder.Services.AddScoped<IRentalsRepository, RentalsRepository>();
builder.Services.AddScoped<IHelperServices, HelperServices>();
builder.Services.AddDbContext<AppDbContext>(options => 
{
   options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
   ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();

   app.UseSwaggerUI(options =>
   {
      options.SwaggerEndpoint("/openapi/v1.json", "v1");
   });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
