using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VirtualMind.Exam.Infraestructure.Entity;

#nullable disable

namespace VirtualMind.Exam.Infraestructure
{
    public partial class VirtualMindDBContext : DbContext
    {
        public VirtualMindDBContext()
        {
        }

        public VirtualMindDBContext(DbContextOptions<VirtualMindDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Purchase> Purchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=PE-IT000282\\NALDOSERVER;User=sa;Password=S0lst1c3@;Trusted_Connection=False;Initial Catalog=VirtualMindDB;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchase");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
