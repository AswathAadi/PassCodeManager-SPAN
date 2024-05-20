using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PassCodeManager.Classified.Entities;

namespace PassCodeManager.Classified.DBcontext
{
    public class Context : IdentityDbContext
    {
        #region Db Connection Check.
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
            try
            {
                // Attempt to open the database connection
                bool isConnectionAvailable = Database.CanConnect();
                if (!isConnectionAvailable) { return; }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
                throw;
            }
        }
        #endregion

        public DbSet<TblPassCodes> PassCodes { get; set; }

        public DbSet<TblSecurityKeys> SecurityKeys { get; set; }
        public DbSet<TblUsers> Users { get; set; }
        public DbSet<TblLogs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TblPassCodes>(entity =>
            {
                entity.ToTable("tbl_passcodes");

                entity.HasIndex(e => e.Id)
                .HasDatabaseName("UQ_tbl_passcodes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PassCode)
                    .HasColumnName("passcode")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).IsRequired(true).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<TblSecurityKeys>(entity =>
            {
                entity.ToTable("tbl_security_keys");

                entity.HasIndex(e => e.Id)
                .HasDatabaseName("UQ_tbl_passcodes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PublicKey)
                    .HasColumnName("public_key")
                    .HasMaxLength(1024);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PrivateKey)
                    .HasColumnName("private_key")
                    .HasMaxLength(2048);

                entity.Property(e => e.CreatedOn).IsRequired(true).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(25);

                entity.Property(e => e.PassCodeId)
                    .HasColumnName("passcode_id")
                    .HasMaxLength(55)
                    .IsRequired(true);

                entity.HasMany(x => x.HasPassCodes)
                    .WithOne(y => y.SecurityKeys)
                    .HasPrincipalKey(x => x.PassCodeId)
                    .HasForeignKey(x => x.Id);
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.ToTable("tbl_users");

                entity.HasIndex(e => e.Id)
                .HasDatabaseName("UQ_tbl_users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.UserName)
                    .HasColumnName("name")
                    .HasMaxLength(60);

                entity.Property(e => e.VerifiedMediums)
                    .HasColumnName("verified_mediams")
                     .HasColumnType("int");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(70);


                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");


                entity.Property(e => e.IsLockedOut)
                    .HasColumnName("is_locked_out")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(75);

                entity.Property(e => e.RegistrationDate).IsRequired(true).HasColumnName("registration_on");
                entity.Property(e => e.LastLoginDate).HasColumnName("last_login_on");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<TblLogs>(entity =>
            {
                entity.ToTable("tbl_logs");

                entity.HasIndex(e => e.Id)
                .HasDatabaseName("UQ_tbl_logs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(2048);

                entity.Property(e => e.Exception)
                    .HasColumnName("exception")
                    .HasMaxLength(2048);

                entity.Property(e => e.EventTime)
                    .HasColumnName("event_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Event)
                    .HasColumnName("event")
                    .HasMaxLength(45);
            });

        }
    }
}
