using CleanArchitecture.Master.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Master.Domain.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
       public void Configure(EntityTypeBuilder<Course> builder)
       {
              builder.ToTable("Course", "Master");

              builder.HasKey(c => c.Id);

              builder.Property(c => c.Id)
                     .HasColumnName("Id");

              builder.Property(c => c.Name)
                     .HasColumnName("Name")
                     .HasMaxLength(255)
                     .IsRequired();

              builder.Property(c => c.IsActive)
                     .HasColumnName("IsActive")
                     .IsRequired();
       }
}