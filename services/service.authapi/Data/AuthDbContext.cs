using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using service.authapi.Models;

namespace service.authapi.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public virtual DbSet<MUser> Users { get; set; }
        public virtual DbSet<MPermissions> MPermissions{ get; set; }
        public virtual DbSet<RolePermissons> RolePermissons{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
