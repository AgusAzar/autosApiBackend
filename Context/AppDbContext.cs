using Microsoft.EntityFrameworkCore;
using AutosApi.Models;
namespace AutosApi.Context{
    public class AppDbContext: DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){ }
        public DbSet<Auto> autos { get; set; }
        public DbSet<Marca> marcas { get; set; }
    }
}
