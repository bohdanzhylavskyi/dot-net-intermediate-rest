using ASPNetCore;
using ASPNetCore.DTOs;
using ASPNetCore.Entities;
using ASPNetCore.Mapper;
using ASPNetCore.Repositories;
using ASPNetCore.Services;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development
});


builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSingleton<InMemoryRepository>();
builder.Services.AddScoped<CategoriesService>();
builder.Services.AddScoped<ItemsService>();

builder.Configuration.Sources.Clear();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

Console.WriteLine($"Is development: {app.Environment.IsDevelopment()}");

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
