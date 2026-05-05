using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Core.DataSource
{
    public class SchoolContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        public SchoolContext(DbContextOptions<SchoolContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
        }
    }
}
