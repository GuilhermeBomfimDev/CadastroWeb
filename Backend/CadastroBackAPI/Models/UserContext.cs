using Microsoft.EntityFrameworkCore;

namespace CadastroBackAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;Database=Guilherme;User=root;Password=Bomfim1998;");
            }
        }

        public DbSet<UserDados> UserDados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDados>().ToTable("UsersStore", schema: "CakesForYou");
        }
    }
}
