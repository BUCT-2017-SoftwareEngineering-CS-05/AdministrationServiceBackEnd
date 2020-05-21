using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdministrationServiceBackEnd.Models
{
    public partial class MuseumContext : DbContext
    {
        public MuseumContext()
        {
        }

        public MuseumContext(DbContextOptions<MuseumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Academic> Academic { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Collection> Collection { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<Exhibition> Exhibition { get; set; }
        public virtual DbSet<Maintable> Maintable { get; set; }
        public virtual DbSet<MuseumInformation> MuseumInformation { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=cdb-3lehih0k.cd.tencentcdb.com;port=10090;userid=CS1705museum;password=CS1705museum;database=museum", x => x.ServerVersion("5.7.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Academic>(entity =>
            {
                entity.HasKey(e => e.AdId)
                    .HasName("PRIMARY");

                entity.Property(e => e.AdId)
                    .HasColumnName("ad_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Username })
                    .HasName("PRIMARY");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Roles)
                    .HasColumnName("roles")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(e => e.Oid)
                    .HasName("PRIMARY");

                entity.Property(e => e.Oid)
                    .HasColumnName("oid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Midex)
                    .HasColumnName("midex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ointro)
                    .HasColumnName("ointro")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Oname)
                    .IsRequired()
                    .HasColumnName("oname")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Ophoto)
                    .HasColumnName("ophoto")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.Midex, e.Userid })
                    .HasName("PRIMARY");

                entity.Property(e => e.Midex)
                    .HasColumnName("midex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Envscore)
                    .HasColumnName("envscore")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Exhscore)
                    .HasColumnName("exhscore")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Msg)
                    .HasColumnName("msg")
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("'null'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Serscore)
                    .HasColumnName("serscore")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PRIMARY");

                entity.Property(e => e.Aid)
                    .HasColumnName("aid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Midex)
                    .HasColumnName("midex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId)
                    .HasColumnType("varchar(95)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("PRIMARY");

                entity.Property(e => e.Eid)
                    .HasColumnName("eid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Eintro)
                    .HasColumnName("eintro")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Ename)
                    .IsRequired()
                    .HasColumnName("ename")
                    .HasColumnType("varchar(450)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Midex)
                    .HasColumnName("midex")
                    .HasColumnType("int(150)");
            });

            modelBuilder.Entity<Maintable>(entity =>
            {
                entity.HasKey(e => e.Midex)
                    .HasName("PRIMARY");

                entity.ToTable("maintable");

                entity.Property(e => e.Midex)
                    .HasColumnName("midex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mbase)
                    .HasColumnName("mbase")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mname)
                    .IsRequired()
                    .HasColumnName("mname")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<MuseumInformation>(entity =>
            {
                entity.HasKey(e => e.Midex)
                    .HasName("PRIMARY");

                entity.ToTable("Museum_Information");

                entity.Property(e => e.Midex)
                    .HasColumnName("midex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Maddress)
                    .IsRequired()
                    .HasColumnName("maddress")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mbase)
                    .HasColumnName("mbase")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mname)
                    .IsRequired()
                    .HasColumnName("mname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mopentime)
                    .IsRequired()
                    .HasColumnName("mopentime")
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PictureAddress)
                    .HasColumnName("picture_address")
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Analyseresult)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Museum)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Publishtime)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Source)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Coright)
                    .HasColumnName("coright")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Nickname)
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Userpwd)
                    .HasColumnName("userpwd")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
