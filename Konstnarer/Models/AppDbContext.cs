using Konstnarer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Konstnarer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PicComment> PicComments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProfileComment> ProfileComments { get; set; }
        public DbSet<ValidateUser> ValidateUsers { get; set; }
        public DbSet<ChangePassword> ChangePasswords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Role = "Admin",
                UserName = "Administratör",
                Password = "5124admin",
                Email = "Admin@konst.se",
                UserId = Guid.NewGuid()
            });
        }
    }
}
