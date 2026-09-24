using Microsoft.EntityFrameworkCore;
using VideoGamesApp.Application.Interfaces;
using VideoGamesApp.Application.UserCases;
using VideoGamesApp.Domain.Interfaces;
using VideoGamesApp.Infraestructure.Persistence;
using VideoGamesApp.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Registrar DbContext con la base de datos en memoria
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("VideoGamesDb"));

// 2. Registrar Repositorios (Inyección de Dependencias: Contrato -> Implementación)
builder.Services.AddScoped<IVideoGameRepository, VideoGameRepository>();

// 3. Registrar Casos de Uso (Inyección de Dependencias)
builder.Services.AddScoped<ICreateVideoGameUseCase, CreateVideoGameUseCase>();
builder.Services.AddScoped<IGetAllVideoGamesUseCase, GetAllVideoGamesUseCase>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
