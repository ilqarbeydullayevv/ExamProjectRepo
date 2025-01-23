using ExamProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExamProject.DAL.Context
{
    public class AppDbContext : IdentityDbContext<Appuser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
