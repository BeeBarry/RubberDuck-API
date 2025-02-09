using Microsoft.EntityFrameworkCore;
using RubberDuck_API.Models;

namespace RubberDuck_API.Data;

public class DuckDbContext : DbContext
{
    public DuckDbContext(DbContextOptions<DuckDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Duck> Ducks { get; set; }
}
