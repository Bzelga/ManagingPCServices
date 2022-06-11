using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ManagingPCServices.DBWorker
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<TypeAction> TypeActions { get; set; }
        public virtual DbSet<TypeParameter> TypeParameters { get; set; }
        public virtual DbSet<TypeValue> TypeValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=D:\\\\\\\\TestDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.ToTable("Parameter");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasColumnType("STRING");

                entity.Property(e => e.MaxValue)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("Max_value");

                entity.Property(e => e.MinValue)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("Min_value");

                entity.Property(e => e.TypeParameter).HasColumnName("Type_parameter");

                entity.Property(e => e.TypeValue).HasColumnName("Type_value");

                entity.HasOne(d => d.SourceNavigation)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.Source);

                entity.HasOne(d => d.TypeParameterNavigation)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.TypeParameter);

                entity.HasOne(d => d.TypeValueNavigation)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.TypeValue);
            });

            modelBuilder.Entity<Rule>(entity =>
            {
                entity.ToTable("Rule");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.Predicate)
                    .IsRequired()
                    .HasColumnType("STRING");

                entity.HasOne(d => d.ActionNavigation)
                    .WithMany(p => p.Rules)
                    .HasForeignKey(d => d.Action);
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.ToTable("Source");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("STRING");
            });

            modelBuilder.Entity<TypeAction>(entity =>
            {
                entity.ToTable("Type_action");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.TypeCommand)
                   .IsRequired()
                   .HasColumnType("INTEGER")
                   .HasColumnName("Type_command");

                entity.Property(e => e.NumberAction)
                    .IsRequired()
                    .HasColumnType("INTEGER")
                    .HasColumnName("Number_action");

                entity.Property(e => e.TextAction)
                    .IsRequired()
                    .HasColumnType("STRING")
                    .HasColumnName("Text_action");
            });

            modelBuilder.Entity<TypeParameter>(entity =>
            {
                entity.ToTable("Type_parameter");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("STRING");
            });

            modelBuilder.Entity<TypeValue>(entity =>
            {
                entity.ToTable("Type value");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("STRING");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
