using CleanArchitecture.Master.Domain.Entities;
using CleanArchitecture.Master.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Shared.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Database;

public class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {
    }

    #region Master Data DbSets

    public DbSet<Course> Courses { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Level> Levels { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatically configure all entities that inherit from AuditableEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                // Check if the properties already exist before adding
                if (!entityType.GetProperties().Any(p => p.Name == "CreatedAt"))
                {
                    entityType.AddProperty("CreatedAt", typeof(DateTime));
                    entityType.GetProperty("CreatedAt").SetDefaultValueSql("GETDATE()");
                }

                if (!entityType.GetProperties().Any(p => p.Name == "CreatedBy"))
                {
                    entityType.AddProperty("CreatedBy", typeof(string));
                    entityType.GetProperty("CreatedBy").SetMaxLength(500);
                    entityType.GetProperty("CreatedBy").SetDefaultValue("SYSTEM");
                }

                if (!entityType.GetProperties().Any(p => p.Name == "UpdatedAt"))
                {
                    entityType.AddProperty("UpdatedAt", typeof(DateTime));
                    entityType.GetProperty("UpdatedAt").SetDefaultValueSql("GETDATE()");
                }

                if (!entityType.GetProperties().Any(p => p.Name == "UpdatedBy"))
                {
                    entityType.AddProperty("UpdatedBy", typeof(string));
                    entityType.GetProperty("UpdatedBy").SetMaxLength(500);
                }
            }
        }

        #region Master Data Configurations

        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        modelBuilder.ApplyConfiguration(new LevelConfiguration());

        #endregion
    }
}