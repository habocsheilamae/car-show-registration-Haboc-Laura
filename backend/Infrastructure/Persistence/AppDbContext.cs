namespace Infrastructure.Persistence;

using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Registration> Registrations => Set<Registration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.RegistrationNumber).IsRequired().HasMaxLength(20);
            entity.HasIndex(r => r.RegistrationNumber).IsUnique();

            entity.Property(r => r.RegistrationDate).IsRequired();

            entity.Property(r => r.ParticipantName).IsRequired().HasMaxLength(150);
            entity.Property(r => r.ContactNumber).IsRequired().HasMaxLength(11);
            entity.Property(r => r.Email).IsRequired().HasMaxLength(200);
            entity.Property(r => r.Address).IsRequired().HasMaxLength(300);
            entity.Property(r => r.CarMakeModel).IsRequired().HasMaxLength(100);

            entity.Property(r => r.PlateNumber).IsRequired().HasMaxLength(15);
            entity.HasIndex(r => r.PlateNumber).IsUnique();

            entity.Property(r => r.RegistrationType).HasConversion<string>().HasMaxLength(20);
            entity.Property(r => r.Status).HasConversion<string>().HasMaxLength(20);

            // SQLite has no native decimal; double lets SUM/ORDER BY work
            entity.Property(r => r.RegistrationFee).HasConversion<double>();
            entity.Property(r => r.AmountPaid).HasConversion<double>();

            // Computed in code, not a column
            entity.Ignore(r => r.PaymentStatus);
        });
    }
}