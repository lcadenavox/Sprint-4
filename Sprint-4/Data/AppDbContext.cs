using Microsoft.EntityFrameworkCore;
using Sprint_4.Models;

namespace Sprint_4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Mecanico> Mecanicos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
    }
}