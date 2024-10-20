using CleanArchitecture.Master.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Master.Domain.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
       public void Configure(EntityTypeBuilder<Subject> builder)
       {
              builder.ToTable("Subject", "Master");

              builder.HasKey(s => s.Id);

              builder.HasOne<Course>()
                     .WithMany()
                     .HasForeignKey(s => s.CourseId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.Property(s => s.Id)
                     .HasColumnName("Id");

              builder.Property(s => s.Name)
                     .HasColumnName("Name")
                     .HasMaxLength(255)
                     .IsRequired();

              builder.Property(s => s.IsActive)
                     .HasColumnName("IsActive")
                     .IsRequired();
       }
}