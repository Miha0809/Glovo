using Microsoft.EntityFrameworkCore;

namespace Glovo.Services;

public class GlovoDbContext : DbContext
{
    public GlovoDbContext(DbContextOptions<GlovoDbContext> options) : base(options) { }

    // Clients
    public virtual DbSet<Client.Models.Client> Clients { get; set; }

    // Companies
    public virtual DbSet<Companies.Models.Company> Companies { get; set; }
    public virtual DbSet<Companies.Models.Product> Products { get; set; }

    // Couriers
    public virtual DbSet<Courier.Models.Courier> Couriers { get; set; }

    // Glovos
    public virtual DbSet<Glovo.Models.Glovo> Glovos { get; set; }

    // Moderator
    public virtual DbSet<Moderator.Models.Moderator> Moderators { get; set; }
    
    // Configuration
    public virtual DbSet<Configure.Models.Role> Roles { get; set; }
    public virtual DbSet<Configure.Models.Email> Emails { get; set; }
    public virtual DbSet<Configure.Models.RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Configure.Models.Email>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}
