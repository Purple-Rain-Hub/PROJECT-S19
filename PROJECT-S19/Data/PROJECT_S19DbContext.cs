using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROJECT_S19.Models;

namespace PROJECT_S19.Data
{
    public class PROJECT_S19DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public PROJECT_S19DbContext(DbContextOptions<PROJECT_S19DbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User).WithMany(u => u.ApplicationUserRoles).HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role).WithMany(r => r.ApplicationUserRoles).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Ticket>().Property(p => p.PurchaseDate).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Event>().HasOne(r => r.Artist).WithMany(c => c.Events).HasForeignKey(r => r.ArtistId);
            modelBuilder.Entity<Ticket>().HasOne(r => r.Event).WithMany(c => c.Tickets).HasForeignKey(r => r.EventId);
            modelBuilder.Entity<Ticket>().HasOne(r => r.User).WithMany(c => c.Tickets).HasForeignKey(r => r.UserId);
        }
    }
} 
