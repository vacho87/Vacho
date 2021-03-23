using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ManageBTDB
{
    public partial class BTdbContext : DbContext
    {
        public BTdbContext()
        {
        }

        public BTdbContext(DbContextOptions<BTdbContext> options) : base(options)
        {
        }

        public virtual DbSet<BusinessTrip> BusinessTrips { get; set; }
        public virtual DbSet<BusinessTripPurpose> BusinessTripPurposes { get; set; }
        public virtual DbSet<BusinessTripState> BusinessTripStates { get; set; }
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
        public virtual DbSet<Employee> Staff { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=COMP11\\VACHOSRV; database=BTdb; integrated security=true;");
//            }
//        }

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
                    .HasForeignKey(d => d.EmployeeId);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.BusinessTrips)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BusinessTripPurpose>(entity =>
            {
                entity.HasIndex(e => e.Purpose)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ExpenditureItem)
                    .IsRequired();

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<BusinessTripState>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Calculation>(entity =>
            {
                entity.HasIndex(e => e.BusinessTripId)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DailyCosts).HasColumnType("smallmoney");

                entity.Property(e => e.DueToPay).HasColumnType("smallmoney");

                entity.Property(e => e.DwellingCosts).HasColumnType("smallmoney");

                entity.Property(e => e.PaySheetDate).HasColumnType("date");

                entity.Property(e => e.PrePay).HasColumnType("smallmoney");

                entity.Property(e => e.TransitCosts).HasColumnType("smallmoney");

                entity.HasOne(d => d.BusinessTrip)
                    .WithOne(p => p.Calculation)
                    .HasForeignKey<Calculation>(d => d.BusinessTripId);                    
            });

            modelBuilder.Entity<ChangedOrderInfo>(entity =>
            {
                entity.ToTable("ChangedOrderInfo");

                entity.HasIndex(e => e.OrderInfoId)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.ChangedOrderInfos)
                    .HasForeignKey(d => d.LocalityId);

                entity.HasOne(d => d.OrderInfo)
                    .WithOne(p => p.ChangedOrderInfo)
                    .HasForeignKey<ChangedOrderInfo>(d => d.OrderInfoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.ChangedOrderInfos)
                    .HasForeignKey(d => d.PurposeId);
            });

            modelBuilder.Entity<DailyRate>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<DwellingRate>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnType("smallmoney");

                entity.HasOne(d => d.LocalityType)
                    .WithMany()
                    .HasForeignKey(d => d.LocalityTypeId);
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.HasIndex(e => e.Name)
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
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<LocalityType>(entity =>
            {
                entity.HasIndex(e => e.Type)
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

                entity.HasIndex(e => e.BusinessTripId)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.BusinessTrip)
                    .WithOne(p => p.OrderInfo)
                    .HasForeignKey<OrderInfo>(d => d.BusinessTripId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.LocalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.HasIndex(e => e.RankName)
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
                entity.ToTable("StatementInfo");

                entity.HasIndex(e => e.BusinessTripId)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FeedingBegin).HasColumnType("date");

                entity.Property(e => e.FeedingEnd).HasColumnType("date");

                entity.Property(e => e.PrePay).HasColumnType("smallmoney");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.BusinessTrip)
                    .WithOne(p => p.StatementInfo)
                    .HasForeignKey<StatementInfo>(d => d.BusinessTripId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.StatementInfos)
                    .HasForeignKey(d => d.LocalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TransitRate>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName)
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
                    .HasForeignKey(d => d.RankId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
