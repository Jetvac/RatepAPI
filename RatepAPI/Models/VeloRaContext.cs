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

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientViewFa> ClientViewFas { get; set; } = null!;
        public virtual DbSet<CostJournal> CostJournals { get; set; } = null!;
        public virtual DbSet<CostJournalViewFa> CostJournalViewFas { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeViewFa> EmployeeViewFas { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<GenderViewFa> GenderViewFas { get; set; } = null!;
        public virtual DbSet<LegalPerson> LegalPeople { get; set; } = null!;
        public virtual DbSet<LegalPersonViewFa> LegalPersonViewFas { get; set; } = null!;
        public virtual DbSet<Manufactory> Manufactories { get; set; } = null!;
        public virtual DbSet<ManufactoryType> ManufactoryTypes { get; set; } = null!;
        public virtual DbSet<ManufactoryTypeViewFa> ManufactoryTypeViewFas { get; set; } = null!;
        public virtual DbSet<ManufactoryViewFa> ManufactoryViewFas { get; set; } = null!;
        public virtual DbSet<NaturalPerson> NaturalPeople { get; set; } = null!;
        public virtual DbSet<NaturalPersonViewFa> NaturalPersonViewFas { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderPosition> OrderPositions { get; set; } = null!;
        public virtual DbSet<OrderPositionViewFa> OrderPositionViewFas { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<OrderStatusViewFa> OrderStatusViewFas { get; set; } = null!;
        public virtual DbSet<OrderViewFa> OrderViewFas { get; set; } = null!;
        public virtual DbSet<PartAssemblyUnit> PartAssemblyUnits { get; set; } = null!;
        public virtual DbSet<PartAssemblyUnitViewFa> PartAssemblyUnitViewFas { get; set; } = null!;
        public virtual DbSet<PassportDataViewFa> PassportDataViewFas { get; set; } = null!;
        public virtual DbSet<PassportDatum> PassportData { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostViewFa> PostViewFas { get; set; } = null!;
        public virtual DbSet<RoadMap> RoadMaps { get; set; } = null!;
        public virtual DbSet<RoadMapStatus> RoadMapStatuses { get; set; } = null!;
        public virtual DbSet<RoadMapStatusViewFa> RoadMapStatusViewFas { get; set; } = null!;
        public virtual DbSet<RoadMapViewFa> RoadMapViewFas { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleViewFa> RoleViewFas { get; set; } = null!;
        public virtual DbSet<Structure> Structures { get; set; } = null!;
        public virtual DbSet<StructureViewFa> StructureViewFas { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserViewFa> UserViewFas { get; set; } = null!;
        public virtual DbSet<Vat> Vats { get; set; } = null!;
        public virtual DbSet<VatViewFa> VatViewFas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMail");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<ClientViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Client_VIEW_FA");

                entity.Property(e => e.ClientId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ClientID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMail");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<CostJournal>(entity =>
            {
                entity.HasKey(e => e.CostId)
                    .HasName("PK__CostJour__8285231EC274BD5D");

                entity.ToTable("CostJournal");

                entity.Property(e => e.CostId).HasColumnName("CostID");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<CostJournalViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CostJournal_VIEW_FA");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.CostId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CostID");

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.Property(e => e.Number).HasMaxLength(6);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Seria).HasMaxLength(4);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Accoun__5441852A");

                entity.HasOne(d => d.Manufactory)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ManufactoryId)
                    .HasConstraintName("FK_Employee_Manufactory");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__PostID__5165187F");

                entity.HasOne(d => d.PassportDatum)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => new { d.Seria, d.Number })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__5535A963");
            });

            modelBuilder.Entity<EmployeeViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Employee_VIEW_FA");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Number).HasMaxLength(6);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Seria).HasMaxLength(4);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<GenderViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Gender_VIEW_FA");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GenderID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<LegalPerson>(entity =>
            {
                entity.HasKey(e => new { e.LegalPersonId, e.ClientId })
                    .HasName("PK__LegalPer__E5F85F376E523D9B");

                entity.ToTable("LegalPerson");

                entity.Property(e => e.LegalPersonId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LegalPersonID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Bic)
                    .HasMaxLength(100)
                    .HasColumnName("BIC");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Tin)
                    .HasMaxLength(100)
                    .HasColumnName("TIN");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.LegalPeople)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LegalPers__Clien__59FA5E80");
            });

            modelBuilder.Entity<LegalPersonViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LegalPerson_VIEW_FA");

                entity.Property(e => e.Bic)
                    .HasMaxLength(100)
                    .HasColumnName("BIC");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.LegalPersonId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LegalPersonID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Tin)
                    .HasMaxLength(100)
                    .HasColumnName("TIN");
            });

            modelBuilder.Entity<Manufactory>(entity =>
            {
                entity.ToTable("Manufactory");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.Property(e => e.ManufactoryTypeId).HasColumnName("ManufactoryTypeID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.ManufactoryType)
                    .WithMany(p => p.Manufactories)
                    .HasForeignKey(d => d.ManufactoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manufacto__Manuf__5BE2A6F2");
            });

            modelBuilder.Entity<ManufactoryType>(entity =>
            {
                entity.ToTable("ManufactoryType");

                entity.Property(e => e.ManufactoryTypeId).HasColumnName("ManufactoryTypeID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ManufactoryTypeViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ManufactoryType_VIEW_FA");

                entity.Property(e => e.ManufactoryTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ManufactoryTypeID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ManufactoryViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Manufactory_VIEW_FA");

                entity.Property(e => e.ManufactoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ManufactoryID");

                entity.Property(e => e.ManufactoryTypeId).HasColumnName("ManufactoryTypeID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<NaturalPerson>(entity =>
            {
                entity.HasKey(e => new { e.NaturalPersonId, e.ClientId })
                    .HasName("PK__NaturalP__CA97D5C98BBD04C9");

                entity.ToTable("NaturalPerson");

                entity.Property(e => e.NaturalPersonId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NaturalPersonID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.SecondName).HasMaxLength(100);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.NaturalPeople)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NaturalPe__Clien__59063A47");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.NaturalPeople)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NaturalPe__Gende__571DF1D5");
            });

            modelBuilder.Entity<NaturalPersonViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NaturalPerson_VIEW_FA");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NaturalPersonId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NaturalPersonID");

                entity.Property(e => e.SecondName).HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.IssuedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.Vatid).HasColumnName("VATID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__ClientID__5812160E");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__EmployeeI__52593CB8");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__OrderStat__5070F446");

                entity.HasOne(d => d.Vat)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Vatid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__VATID__4F7CD00D");
            });

            modelBuilder.Entity<OrderPosition>(entity =>
            {
                entity.HasKey(e => e.PosId)
                    .HasName("PK__OrderPos__67572BB3064022FE");

                entity.ToTable("OrderPosition");

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.Articul).HasMaxLength(6);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.ArticulNavigation)
                    .WithMany(p => p.OrderPositions)
                    .HasForeignKey(d => d.Articul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPosi__Artic__5EBF139D");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPositions)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPosi__Order__4D94879B");
            });

            modelBuilder.Entity<OrderPositionViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OrderPosition_VIEW_FA");

                entity.Property(e => e.Articul).HasMaxLength(6);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PosId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PosID");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<OrderStatusViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OrderStatus_VIEW_FA");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.OrderStatusId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderStatusID");
            });

            modelBuilder.Entity<OrderViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order_VIEW_FA");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.IssuedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderID");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.Vatid).HasColumnName("VATID");
            });

            modelBuilder.Entity<PartAssemblyUnit>(entity =>
            {
                entity.HasKey(e => e.Articul)
                    .HasName("PK__PartAsse__4944DE202D36E74A");

                entity.ToTable("PartAssemblyUnit");

                entity.Property(e => e.Articul).HasMaxLength(6);

                entity.Property(e => e.CostId).HasColumnName("CostID");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Cost)
                    .WithMany(p => p.PartAssemblyUnits)
                    .HasForeignKey(d => d.CostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartAssem__CostI__6383C8BA");

                entity.HasOne(d => d.Manufactory)
                    .WithMany(p => p.PartAssemblyUnits)
                    .HasForeignKey(d => d.ManufactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartAssem__Manuf__5AEE82B9");
            });

            modelBuilder.Entity<PartAssemblyUnitViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PartAssemblyUnit_VIEW_FA");

                entity.Property(e => e.Articul).HasMaxLength(6);

                entity.Property(e => e.CostId).HasColumnName("CostID");

                entity.Property(e => e.ManufactoryId).HasColumnName("ManufactoryID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<PassportDataViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PassportData_VIEW_FA");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.BirthPlace).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Number).HasMaxLength(6);

                entity.Property(e => e.SecondName).HasMaxLength(100);

                entity.Property(e => e.Seria).HasMaxLength(4);
            });

            modelBuilder.Entity<PassportDatum>(entity =>
            {
                entity.HasKey(e => new { e.Seria, e.Number })
                    .HasName("PK__Passport__AE0883CBE6C30A45");

                entity.Property(e => e.Seria).HasMaxLength(4);

                entity.Property(e => e.Number).HasMaxLength(6);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.BirthPlace).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.SecondName).HasMaxLength(100);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.PassportData)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PassportD__Gende__5629CD9C");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Post__RoleID__534D60F1");
            });

            modelBuilder.Entity<PostViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Post_VIEW_FA");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PostId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PostID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<RoadMap>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.PosId, e.Pauid, e.Pauidcontains })
                    .HasName("PK__RoadMap__27FC60406B44ABBB");

                entity.ToTable("RoadMap");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.Pauid)
                    .HasMaxLength(6)
                    .HasColumnName("PAUID");

                entity.Property(e => e.Pauidcontains)
                    .HasMaxLength(6)
                    .HasColumnName("PAUIDContains");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.RoadMapStatusId).HasColumnName("RoadMapStatusID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.RoadMaps)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoadMap__OrderID__4E88ABD4");

                entity.HasOne(d => d.Pau)
                    .WithMany(p => p.RoadMapPaus)
                    .HasForeignKey(d => d.Pauid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoadMap__PAUID__5FB337D6");

                entity.HasOne(d => d.PauidcontainsNavigation)
                    .WithMany(p => p.RoadMapPauidcontainsNavigations)
                    .HasForeignKey(d => d.Pauidcontains)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoadMap__PAUIDCo__60A75C0F");

                entity.HasOne(d => d.Pos)
                    .WithMany(p => p.RoadMaps)
                    .HasForeignKey(d => d.PosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoadMap__PosID__619B8048");

                entity.HasOne(d => d.RoadMapStatus)
                    .WithMany(p => p.RoadMaps)
                    .HasForeignKey(d => d.RoadMapStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoadMap__RoadMap__628FA481");
            });

            modelBuilder.Entity<RoadMapStatus>(entity =>
            {
                entity.ToTable("RoadMapStatus");

                entity.Property(e => e.RoadMapStatusId).HasColumnName("RoadMapStatusID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<RoadMapStatusViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoadMapStatus_VIEW_FA");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RoadMapStatusId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RoadMapStatusID");
            });

            modelBuilder.Entity<RoadMapViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoadMap_VIEW_FA");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Pauid)
                    .HasMaxLength(6)
                    .HasColumnName("PAUID");

                entity.Property(e => e.Pauidcontains)
                    .HasMaxLength(6)
                    .HasColumnName("PAUIDContains");

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.RoadMapStatusId).HasColumnName("RoadMapStatusID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<RoleViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Role_VIEW_FA");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RoleID");
            });

            modelBuilder.Entity<Structure>(entity =>
            {
                entity.HasKey(e => new { e.Pauid, e.Pauidcontains })
                    .HasName("PK__Structur__194954C318810A51");

                entity.ToTable("Structure");

                entity.Property(e => e.Pauid)
                    .HasMaxLength(6)
                    .HasColumnName("PAUID");

                entity.Property(e => e.Pauidcontains)
                    .HasMaxLength(6)
                    .HasColumnName("PAUIDContains");

                entity.HasOne(d => d.Pau)
                    .WithMany(p => p.StructurePaus)
                    .HasForeignKey(d => d.Pauid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Structure__PAUID__5CD6CB2B");

                entity.HasOne(d => d.PauidcontainsNavigation)
                    .WithMany(p => p.StructurePauidcontainsNavigations)
                    .HasForeignKey(d => d.Pauidcontains)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Structure__PAUID__5DCAEF64");
            });

            modelBuilder.Entity<StructureViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Structure_VIEW_FA");

                entity.Property(e => e.Pauid)
                    .HasMaxLength(6)
                    .HasColumnName("PAUID");

                entity.Property(e => e.Pauidcontains)
                    .HasMaxLength(6)
                    .HasColumnName("PAUIDContains");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__User__349DA586CDBFAD48");

                entity.ToTable("User");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Login).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            modelBuilder.Entity<UserViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("User_VIEW_FA");

                entity.Property(e => e.AccountId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AccountID");

                entity.Property(e => e.Login).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            modelBuilder.Entity<Vat>(entity =>
            {
                entity.ToTable("VAT");

                entity.Property(e => e.Vatid).HasColumnName("VATID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<VatViewFa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VAT_VIEW_FA");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Vatid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VATID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
