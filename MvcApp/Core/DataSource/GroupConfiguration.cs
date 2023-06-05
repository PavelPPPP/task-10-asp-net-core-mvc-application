using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DataSource
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("GROUPS");
            builder.Property(p => p.Id).HasColumnName("GROUP_ID");
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.Name, n =>
            {
                n.Property(v => v.Value).HasColumnName("NAME");
                n.Property(v => v.Value).HasColumnType("nvarchar(30)");
                n.Property(v => v.Value).IsRequired();
            });
            builder.Property(p => p.CourseId).HasColumnName("COURSE_ID");
            builder.HasOne(g => g.Course)
                .WithMany(c => c.Groups)
                .HasForeignKey(g => g.CourseId);
        }
    }
}
