using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DataSource
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("COURSES");
            builder.Property(p => p.Id).HasColumnName("COURSE_ID");
            builder.HasKey(p => p.Id);
            builder.OwnsOne(
                b => b.DataCourse,
                dc =>
                {
                    dc.Property(p => p.Name).HasColumnName("NAME").HasColumnType("nvarchar(100)");
                    dc.Property(p => p.Description).HasColumnName("DESCRIPTION").HasColumnType("nvarchar(max)");
                });
        }
    }
}
