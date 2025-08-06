using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using service.roleapi.Models;

namespace service.roleapi.Data
{
    public class RoleDbContext : DbContext
    {
        public RoleDbContext(DbContextOptions<RoleDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public virtual DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
