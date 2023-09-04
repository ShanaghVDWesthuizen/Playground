using System.Security;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using KingPriceUserManagementWebApp.Models;
using Group = KingPriceUserManagementWebApp.Models.Group;

namespace KingPriceUserManagementWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder. UseSqlServer("Server=SHANAGH_LT2022\\SQLEXPRESS;Database=UserManagementDB;Integrated Security=SSPI;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defined relationships and constraints
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });
        }
    }
}
