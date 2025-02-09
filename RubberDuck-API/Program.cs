using Microsoft.EntityFrameworkCore;
using RubberDuck_API.Data;
using RubberDuck_API.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DuckDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapGet()


app.Run();

