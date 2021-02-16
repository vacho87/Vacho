using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BTdbManagement
{
    public partial class BTdbContext : DbContext
    {
        public BTdbContext()
        {
        }

        public BTdbContext(DbContextOptions<BTdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BusinessTrip> BusinessTrips { get; set; }
        public virtual DbSet<BusinessTripPurpose> BusinessTripPurposes { get; set; }
        public virtual DbSet<Calculation> Calculations { get; set; }
        public virtual DbSet<ChangedOrderInfo> ChangedOrderInfos { get; set; }
        public virtual DbSet<DailyRate> DailyRates { get; set; }
        public virtual DbSet<DwellingRate> DwellingRates { get; set; }
        public virtual DbSet<Locality> Localities { get; set; }
        public virtual DbSet<LocalityType> LocalityTypes { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<StatementInfo> StatementInfos { get; set; }
        public virtual DbSet<TransitRate> TransitRates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        /* почему-то при создании контекста через Scaffold-DbContext имя класса-сущности и имя свойства-таблицы было автоматически установлено 
         * с первой прописной буквы, в БД таблица называется Staff, остальные имена были корректно интерпретированы */
        public virtual DbSet<Employee> Staff { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.; database=BTdb; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<BusinessTrip>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Info)
                    .HasMaxLength(300)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.BusinessTrips)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BusinessT__Emplo__3C69FB99");
            });

            modelBuilder.Entity<BusinessTripPurpose>(entity =>
            {
                entity.HasIndex(e => e.Purpose, "UQ__Business__6EA87AE8810B573D")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Calculation>(entity =>
            {
                entity.HasIndex(e => e.BusinessTripId, "UQ_Calculation_BusinessTripId")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessTripId).HasColumnName("BusinessTripID");

                entity.Property(e => e.DailyCosts).HasColumnType("smallmoney");

                entity.Property(e => e.DwellingCosts).HasColumnType("smallmoney");

                entity.Property(e => e.PaySheetDate).HasColumnType("date");

                entity.Property(e => e.TransitCosts).HasColumnType("smallmoney");

                entity.HasOne(d => d.BusinessTrip)
                    .WithOne(p => p.Calculation)
                    .HasForeignKey<Calculation>(d => d.BusinessTripId)
                    .HasConstraintName("FK__Calculati__Busin__48CFD27E");
            });

            modelBuilder.Entity<ChangedOrderInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ChangedOrderInfo");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Locality)
                    .WithMany()
                    .HasForeignKey(d => d.LocalityId)
                    .HasConstraintName("FK__ChangedOr__Local__4316F928");

                entity.HasOne(d => d.OrderInfo)
                    .WithMany()
                    .HasForeignKey(d => d.OrderInfoId)
                    .HasConstraintName("FK__ChangedOr__Order__4222D4EF");

                entity.HasOne(d => d.Purpose)
                    .WithMany()
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK__ChangedOr__Purpo__4AB81AF0");
            });

            modelBuilder.Entity<DailyRate>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<DwellingRate>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnType("smallmoney");

                entity.HasOne(d => d.LocalityType)
                    .WithMany()
                    .HasForeignKey(d => d.LocalityTypeId)
                    .HasConstraintName("FK__DwellingR__Local__35BCFE0A");
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Localiti__737584F67B2A8A53")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LocalityTypeId).HasColumnName("LocalityTypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.LocalityType)
                    .WithMany(p => p.Localities)
                    .HasForeignKey(d => d.LocalityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Localitie__Local__2E1BDC42");
            });

            modelBuilder.Entity<LocalityType>(entity =>
            {
                entity.HasIndex(e => e.Type, "UQ__Locality__F9B8A48B64FAF8E1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.ToTable("OrderInfo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.BusinessTrip)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.BusinessTripId)
                    .HasConstraintName("FK__OrderInfo__Busin__3F466844");

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.LocalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderInfo__Local__403A8C7D");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderInfo__Purpo__4BAC3F29");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.HasIndex(e => e.RankName, "UQ__Ranks__DF85EC574DA06BE0")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RankName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Rank");
            });

            modelBuilder.Entity<StatementInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StatementInfo");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.BusinessTrip)
                    .WithMany()
                    .HasForeignKey(d => d.BusinessTripId)
                    .HasConstraintName("FK__Statement__Busin__44FF419A");

                entity.HasOne(d => d.Locality)
                    .WithMany()
                    .HasForeignKey(d => d.LocalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Statement__Local__45F365D3");
            });

            modelBuilder.Entity<TransitRate>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UQ_UserName")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Pasword)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Patronymic).HasMaxLength(30);

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_RankID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
