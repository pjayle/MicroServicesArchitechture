using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using service.userapi.Models;

namespace service.userapi.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public virtual DbSet<MUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
