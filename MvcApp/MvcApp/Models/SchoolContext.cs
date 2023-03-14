using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MvcApp.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        public SchoolContext(DbContextOptions<SchoolContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
