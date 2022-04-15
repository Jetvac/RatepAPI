using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RatepAPI.Models
{
    public partial class VeloRaContext : DbContext
    {
        public static string ConnectString = "Server=.\\SQLEXPRESS;Database=VeloRa;Trusted_Connection=True;";
        public VeloRaContext()
        {
        }

        public VeloRaContext(DbContextOptions<VeloRaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CardWork> CardWorks { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Manufactory> Manufactories { get; set; } = null!;
        public virtual DbSet<ManufactoryType> ManufactoryTypes { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialCard> MaterialCards { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderPosition> OrderPositions { get; set; } = null!;
        public virtual DbSet<PartAssemblyUnit> PartAssemblyUnits { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Structure> Structures { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardWork>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Card_work");

                entity.Property(e => e.Pauid).HasColumnName("PAUID");

                entity.HasOne(d => d.CodeOperationNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodeOperation)
                    .HasConstraintName("FK__Card_work__CodeO__4AB81AF0");

                entity.HasOne(d => d.CodePostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodePost)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Card_work__CodeP__48CFD27E");

                entity.HasOne(d => d.Pau)
                    .WithMany()
                    .HasForeignKey(d => d.Pauid)
                    .HasConstraintName("FK__Card_work__PAUID__44FF419A");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId)
                    .ValueGeneratedNever()
                    .HasColumnName("ClientID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Client__AccountI__4CA06362");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => new { e.ContractId, e.ClientId, e.EmployeeId })
                    .HasName("PK__Contract__671005E60BD5BE6F");

                entity.ToTable("Contract");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contract__Client__5070F446");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contract__Employ__4F7CD00D");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Accoun__4BAC3F29");

                entity.HasOne(d => d.CodePostNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CodePost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__CodePo__49C3F6B7");

                entity.HasOne(d => d.Manufactory)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ManufactoryId)
                    .HasConstraintName("FK__Employee__Manufa__47DBAE45");
            });

            modelBuilder.Entity<Manufactory>(entity =>
            {
                entity.ToTable("Manufactory");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.Property(e => e.ManufactoryTypeId).HasColumnName("ManufactoryTypeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(28)
                    .IsUnicode(false);

                entity.HasOne(d => d.ManufactoryType)
                    .WithMany(p => p.Manufactories)
                    .HasForeignKey(d => d.ManufactoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manufacto__Manuf__4D94879B");
            });

            modelBuilder.Entity<ManufactoryType>(entity =>
            {
                entity.ToTable("ManufactoryType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.CodeMaterial)
                    .HasName("PK__Material__283288559BD25AE2");

                entity.ToTable("Material");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.NumUnitNavigation)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.NumUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Material__NumUni__403A8C7D");
            });

            modelBuilder.Entity<MaterialCard>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MaterialCard");

                entity.Property(e => e.NumMaterial).HasColumnName("Num_material");

                entity.Property(e => e.Pauid).HasColumnName("PAUID");

                entity.HasOne(d => d.NumMaterialNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.NumMaterial)
                    .HasConstraintName("FK__MaterialC__Num_m__412EB0B6");

                entity.HasOne(d => d.Pau)
                    .WithMany()
                    .HasForeignKey(d => d.Pauid)
                    .HasConstraintName("FK__MaterialC__PAUID__4222D4EF");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.CodeOperation)
                    .HasName("PK__Operatio__0623DD8FFAA06F03");

                entity.ToTable("Operation");

                entity.Property(e => e.CodeOperation).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => new { d.ContractId, d.ClientId, d.EmployeeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__5165187F");
            });

            modelBuilder.Entity<OrderPosition>(entity =>
            {
                entity.HasKey(e => e.PosId)
                    .HasName("PK__OrderPos__67572BB39B446C7A");

                entity.ToTable("OrderPosition");

                entity.Property(e => e.PosId)
                    .ValueGeneratedNever()
                    .HasColumnName("PosID");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Pauid).HasColumnName("PAUID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPositions)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPosi__Order__4E88ABD4");

                entity.HasOne(d => d.Pau)
                    .WithMany(p => p.OrderPositions)
                    .HasForeignKey(d => d.Pauid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPosi__PAUID__45F365D3");
            });

            modelBuilder.Entity<PartAssemblyUnit>(entity =>
            {
                entity.HasKey(e => e.Pauid)
                    .HasName("PK__Part-ass__2D91EB9F05C5BA84");

                entity.ToTable("Part-assembly unit");

                entity.Property(e => e.Pauid)
                    .ValueGeneratedNever()
                    .HasColumnName("PAUID");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.Property(e => e.NameProduct)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Manufactory)
                    .WithMany(p => p.PartAssemblyUnits)
                    .HasForeignKey(d => d.ManufactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Part-asse__Manuf__46E78A0C");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.CodePost)
                    .HasName("PK__Post__F7326BA5F6E73071");

                entity.ToTable("Post");

                entity.Property(e => e.NamePost)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Structure>(entity =>
            {
                entity.HasKey(e => new { e.NumProductWhat, e.NumProductWhere })
                    .HasName("PK__Structur__74135A1F800CD1D4");

                entity.ToTable("Structure");

                entity.Property(e => e.NumProductWhat).HasColumnName("Num_product_what");

                entity.Property(e => e.NumProductWhere).HasColumnName("Num_product_where");

                entity.HasOne(d => d.NumProductWhatNavigation)
                    .WithMany(p => p.StructureNumProductWhatNavigations)
                    .HasForeignKey(d => d.NumProductWhat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Structure__Num_p__4316F928");

                entity.HasOne(d => d.NumProductWhereNavigation)
                    .WithMany(p => p.StructureNumProductWhereNavigations)
                    .HasForeignKey(d => d.NumProductWhere)
                    .HasConstraintName("FK__Structure__Num_p__440B1D61");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.NumUnit)
                    .HasName("PK__Unit__46A6C7146CDDCF70");

                entity.ToTable("Unit");

                entity.Property(e => e.NumUnit).ValueGeneratedNever();

                entity.Property(e => e.NameUnit)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__Users__349DA5863F79CE86");

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Login).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
