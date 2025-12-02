using Microsoft.EntityFrameworkCore;
using TechnightApi.Models;

namespace TechnightApi.Data;

/// <summary>
/// Database context for the application
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// DbSet for Example entities
    /// </summary>
    public DbSet<Example> Examples { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Example entity
        modelBuilder.Entity<Example>(entity =>
        {
            entity.ToTable("example");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.EntryDate)
                .HasColumnName("entry_date")
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .IsRequired()
                .HasDefaultValue(true);

            // Add an index on EntryDate for better query performance
            entity.HasIndex(e => e.EntryDate)
                .HasDatabaseName("ix_example_entry_date");
        });

        // Seed some initial data
        // Note: Using static dates to avoid EF Core migration warnings about dynamic values
        modelBuilder.Entity<Example>().HasData(
            new Example
            {
                Id = 1,
                Name = "First Example",
                Title = "Introduction",
                EntryDate = new DateTime(2025, 12, 2, 12, 0, 0, DateTimeKind.Utc),
                Description = "This is the first example entry",
                IsActive = true
            },
            new Example
            {
                Id = 2,
                Name = "Second Example",
                Title = "Advanced Topics",
                EntryDate = new DateTime(2025, 12, 1, 12, 0, 0, DateTimeKind.Utc),
                Description = "This is the second example entry",
                IsActive = true
            }
        );
    }
}

