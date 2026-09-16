using LibroFacil.Application.Interfaces;
using LibroFacil.Application.Services;
using LibroFacil.Infrastructure.Persistence;
using LibroFacil.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibroFacilDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILibroRepository, LibroRepositoryEf>();
builder.Services.AddScoped<LibroService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();