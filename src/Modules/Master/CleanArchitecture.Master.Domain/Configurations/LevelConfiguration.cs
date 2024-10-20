using CleanArchitecture.Master.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Master.Domain.Configurations;

public class LevelConfiguration : IEntityTypeConfiguration<Level>
{
       public void Configure(EntityTypeBuilder<Level> builder)
       {
              builder.ToTable("Level", "Master");

              builder.HasKey(l => l.Id);

              builder.HasOne<Course>()
                     .WithMany()
                     .HasForeignKey(l => l.CourseId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.Property(l => l.Id)
                     .HasColumnName("Id");

              builder.Property(l => l.Name)
                     .HasColumnName("Name")
                     .HasMaxLength(255)
                     .IsRequired();

              builder.Property(l => l.IsActive)
                     .HasColumnName("IsActive")
                     .IsRequired();
       }
}