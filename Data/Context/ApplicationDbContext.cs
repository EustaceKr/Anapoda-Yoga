using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().ToTable("Customers");
            base.OnModelCreating(builder);

            string Admin_id = Guid.NewGuid().ToString();
            string Admin_Role_id = Guid.NewGuid().ToString();
            string User_Role_id = Guid.NewGuid().ToString();

            //Seed Admin
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Admin_Role_id, Name = CustomUserRoles.Admin, NormalizedName = CustomUserRoles.Admin.ToUpper() });
            Customer admin = new Customer(Admin_id,"tade@tade.com","eustace","EUSTACE","St","Kr" );
            builder.Entity<Customer>().HasData(admin);
            PasswordHasher<Customer> ph = new PasswordHasher<Customer>();
            admin.PasswordHash = ph.HashPassword(admin, "MyPass123!");
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = Admin_Role_id, UserId = Admin_id });

            //Seed UserRole
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = User_Role_id, Name = CustomUserRoles.User, NormalizedName = CustomUserRoles.User.ToUpper() });
        }

        
    }
}
