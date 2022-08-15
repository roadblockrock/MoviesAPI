using MoviesAPI.Models;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();

builder.Services.AddSqlServer<MoviesContext>(config.GetConnectionString("WebApiDatabase"));
builder.Services.AddScoped<IMovieInitializer, MovieInitializer>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IMovieRepo, MovieRepo>();

// seed db
provider = builder.Services.BuildServiceProvider();
var initializer = provider.GetRequiredService<IMovieInitializer>();
initializer.Seed();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
