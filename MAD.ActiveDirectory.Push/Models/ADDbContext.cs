using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Models
{
    public class ADDbContext : DbContext
    {
        public ADDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(cfg =>
            {
                cfg.HasKey(y => y.Id);
            });
        }
    }
}
