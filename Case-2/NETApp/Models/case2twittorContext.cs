using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NETApp.Models
{
    public partial class case2twittorContext : DbContext
    {
        public case2twittorContext()
        {
        }

        public case2twittorContext(DbContextOptions<case2twittorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Twittor> Twittors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433; Initial Catalog=case2-twittor; User ID=twittor; Password=twittor");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.TwittorId, "IX_Comments_TwittorId");

                entity.HasIndex(e => e.UserId, "IX_Comments_UserId");

                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(d => d.Twittor)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TwittorId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Twittor>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Twittors_UserId");

                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Twittors)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IsLocked)
                    .IsRequired()
                    .HasColumnName("isLocked")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

                entity.HasIndex(e => e.UserId, "IX_UserRoles_UserId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
