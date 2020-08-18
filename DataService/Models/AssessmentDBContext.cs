using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataService.Models
{
    public partial class AssessmentDBContext : DbContext
    {
        public AssessmentDBContext()
        {
        }

        public AssessmentDBContext(DbContextOptions<AssessmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerType> CustomerType { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:ramkyazuredatabase.database.windows.net,1433;Initial Catalog=AssessmentDB;Persist Security Info=False;User ID=ramky;Password=neelu@6719;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerType_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerTypeValue)
                    .HasColumnName("CustomerType_Value")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__8CB28699BED77065");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.CustomerName).IsUnicode(false);

                entity.Property(e => e.CustomerType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TotalSalesAmount).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
