using Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Module> Modules { get; set; }

    }
}
