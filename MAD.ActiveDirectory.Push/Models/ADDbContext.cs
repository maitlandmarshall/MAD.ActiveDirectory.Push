using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
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
                cfg.HasKey(y => new { y.Id, y.ExtractedDate });
            });

            modelBuilder.Entity<AdWritebackLog>(cfg =>
            {
                cfg.HasKey(y => new { y.Email, y.Date });

                cfg.Property(y => y.OldValues).IsRequired().HasConversion(
                    y => JsonConvert.SerializeObject(y),
                    y => JsonConvert.DeserializeObject<Dictionary<string, object>>(y));

                cfg.Property(y => y.NewValues).IsRequired().HasConversion(
                    y => JsonConvert.SerializeObject(y),
                    y => JsonConvert.DeserializeObject<Dictionary<string, object>>(y));
            });
        }
    }
}
