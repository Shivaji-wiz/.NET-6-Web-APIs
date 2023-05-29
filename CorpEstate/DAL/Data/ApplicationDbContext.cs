using CorpEstate.BLL.Model;
using Microsoft.EntityFrameworkCore;

namespace CorpEstate.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Role_Id = 1,
                    Role_Name = "Admin"
                },
                new Role()
                {
                    Role_Id = 2,
                    Role_Name = "buyer"
                },
                new Role()
                {
                    Role_Id = 3,
                    Role_Name = "Seller"
                });
        }


    }
}

