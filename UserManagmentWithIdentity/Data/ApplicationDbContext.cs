using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagmentWithIdentity.Models;

namespace UserManagmentWithIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users","Secuirty");
            builder.Entity<IdentityRole>().ToTable("Roles", "Secuirty");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Secuirty");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Secuirty");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Secuirty");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Secuirty");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Secuirty");

        }
    }
}
