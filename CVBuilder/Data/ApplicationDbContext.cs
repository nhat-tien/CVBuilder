using CVBuilder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<Profile> Profiles {get; set; } = null!;
    public DbSet<CV> CV {get; set; } = null!;
    public DbSet<CVSection> CVSections {get; set; } = null!;
    public DbSet<Template> Templates {get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


    }
}

