using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Movies.Application.Handlers;
using Movies.Application.Mappers;
using Movies.Core.Repositories;
using Movies.Core.Repositories.Base;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Repositories.Base;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieContext>(
    m=>m.UseSqlServer(builder.Configuration.GetConnectionString("MovieConnection")),ServiceLifetime.Singleton
    );
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movie API Review"
    });
});
builder.Services.AddAutoMapper(typeof(MovieMappingProfile).Assembly);
builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof(CreateMovieCommandHandler).Assembly));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IMovieRepository, MovieRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Review Api V1");
    });
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();
