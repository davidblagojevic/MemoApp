using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MemoApp.Data.Entities
{
    public partial class MemoContext : DbContext
    {
        public MemoContext()
        {
        }

        public MemoContext(DbContextOptions<MemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Memo> Memo { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-CQ6US0Q\\SQLEXPRESS;Database=MemoApp;Trusted_Connection=True;User ID=sa;Password=Admin12");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 

            modelBuilder.Entity<Memo>(entity =>
            {
                entity.Property(e => e.AspNetUsersId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AspNetUsers)
                    .WithMany()
                    .HasForeignKey(d => d.AspNetUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Memo__AspNetUser__628FA481");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Memo)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Memo__StatusId__619B8048");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasOne(d => d.Memo)
                    .WithMany(p => p.Tag)
                    .HasForeignKey(d => d.MemoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tag__MemoId__656C112C");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.Property(e => e.AspNetUsersId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Culture)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateFormat)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TimeFormat)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZoneName).IsRequired();

                entity.HasOne(d => d.AspNetUsers)
                    .WithMany()
                    .HasForeignKey(d => d.AspNetUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Zone__AspNetUser__5CD6CB2B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
