using Microsoft.EntityFrameworkCore;
using System;

using ReposytoryPattern.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ReposytoryPattern
{
    public class AppDbContext : DbContext   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Car> Cars { get; set; }

        public override int SaveChanges()
        {
            AddInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddInfo();
            return await base.SaveChangesAsync();
        }

        private void AddInfo()
        {
            var ents = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Modified || x.State == EntityState.Added));

            foreach(var ent in ents)
            {
                if (ent.State == EntityState.Added)
                    ((BaseEntity)ent.Entity).Created = DateTime.UtcNow;

                ((BaseEntity)ent.Entity).Modified = DateTime.UtcNow;
                
            }
        }
    }
}
