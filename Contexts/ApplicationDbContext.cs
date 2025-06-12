using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<MemberEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<MemberAddressEntity> Addresses { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectEntity>()
                .Property(p => p.Budget)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MemberEntity>()
                .HasOne(m => m.Address)
                .WithOne(a => a.Member)
                .HasForeignKey<MemberAddressEntity>(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
