using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataSource
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        public SchoolContext(DbContextOptions<SchoolContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}
