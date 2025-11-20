using CVBuilder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<Profile> Profiles {get; set; } = null!;
    public DbSet<ProfileLink> ProfileLinks {get; set; } = null!;
    public DbSet<ProfileSection> ProfileSections {get; set; } = null!;
    public DbSet<CV> CV {get; set; } = null!;
    public DbSet<Template> Templates {get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasMany(u => u.Profiles)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder.Entity<User>()
            .HasMany(u => u.CVs)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        builder.Entity<CV>()
            .HasOne(c => c.Profile)
            .WithMany()
            .HasForeignKey(c => c.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CV>()
            .HasOne(c => c.Template)
            .WithMany(t => t.CVs)
            .HasForeignKey(c => c.TemplateId);

        builder.Entity<Profile>()
            .HasMany(c => c.ProfileSections)
            .WithOne(t => t.Profile)
            .HasForeignKey(c => c.ProfileId);

        builder.Entity<Profile>()
            .HasMany(c => c.ProfileLink)
            .WithOne(t => t.Profile)
            .HasForeignKey(c => c.ProfileId);
    }
}

