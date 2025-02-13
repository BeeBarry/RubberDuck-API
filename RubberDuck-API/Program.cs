using Microsoft.EntityFrameworkCore;
using RubberDuck_API.Data;
using RubberDuck_API.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DuckDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpressApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DuckDbContext>();
    db.Database.EnsureCreated();
}

// request pipeline?..
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Detta gör att Swagger UI visas på root URL
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RubberDuck API V1");
        c.RoutePrefix = string.Empty;
    });

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowExpressApp");

//READ
app.MapGet("/ducks", async (DuckDbContext db) =>
{
    return await db.Ducks.ToListAsync();
});

// READ by ID
app.MapGet("/ducks/{id}", async ( DuckDbContext db,int id) =>
{
    var duck = await db.Ducks.FirstOrDefaultAsync(d => d.Id == id);

    if (duck == null)
        return Results.NotFound();

    return Results.Ok(duck);
});

// CREATE
app.MapPost("/ducks", async (Duck duck, DuckDbContext db) => 
{
    
      db.Ducks.Add(duck);
    await db.SaveChangesAsync();
        return Results.Created($"/ducks/{duck.Id}", duck);
            });

// UPDATE 
app.MapPut("/ducks/{id}", async (DuckDbContext db, int id, Duck updatedDuck) =>
{
    var duck = await db.Ducks.FirstOrDefaultAsync(d => d.Id == id);
    if (duck == null)
        return Results.NotFound();

    duck.Name = updatedDuck.Name;
    duck.Specialty = updatedDuck.Specialty;
    duck.Personality = updatedDuck.Personality;
    duck.Motto = updatedDuck.Motto;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

//DELETE
app.MapDelete("/ducks/{id}", async (DuckDbContext db, int id) =>
{
    var duck = await db.Ducks.FirstOrDefaultAsync(d => d.Id == id);
    if (duck == null)
        return Results.NotFound();

    db.Ducks.Remove(duck);
    await db.SaveChangesAsync();
    return Results.NoContent();
});





app.Run();

