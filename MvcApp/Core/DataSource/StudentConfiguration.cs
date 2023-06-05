using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DataSource
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("STUDENTS");

            builder.Property(p => p.Id).HasColumnName("STUDENT_ID");
            builder.HasKey(p => p.Id);

            builder.OwnsOne(b => b.StudentName, sn =>
            {
                sn.OwnsOne(p => p.FirstName, fn =>
                {
                    fn.Property(v => v.Value).HasColumnName("FIRST_NAME");
                    fn.Property(v => v.Value).HasColumnType("nvarchar(20)");
                    fn.Property(v => v.Value).IsRequired();
                });

                sn.OwnsOne(p => p.LastName, ln =>
                {
                    ln.Property(v => v.Value).HasColumnName("LAST_NAME");
                    ln.Property(v => v.Value).HasColumnType("nvarchar(30)");
                    ln.Property(v => v.Value).IsRequired();
                });
            });

            builder.Property(p => p.GroupId).HasColumnName("GROUP_ID");
            builder.HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);
        }
    }
}
