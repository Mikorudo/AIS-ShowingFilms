using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace APPClasses
{
	public partial class AccessContext : DbContext
	{
		public AccessContext()
		{
		}

		public AccessContext(DbContextOptions<AccessContext> options)
			: base(options)
		{
		}

		public virtual DbSet<AccessLevels> AccessLevels { get; set; }
		public virtual DbSet<AppMenuItems> AppMenuItems { get; set; }
		public virtual DbSet<Users> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				string path = Settings.Settings.ServicePath;
				string password = Settings.Settings.ServicePassword;
				optionsBuilder.UseJet("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Jet OLEDB:Database Password=" + password + ";");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccessLevels>(entity =>
			{
				entity.HasKey(e => e.AccessId)
					.HasName("PrimaryKey");

				entity.Property(e => e.AccessId)
					.HasColumnName("accessID")
					.HasColumnType("counter");

				entity.Property(e => e.D)
					.HasColumnType("bit")
					.HasDefaultValueSql("Yes");

				entity.Property(e => e.E)
					.HasColumnType("bit")
					.HasDefaultValueSql("Yes");

				entity.Property(e => e.MenuItemId).HasColumnName("menuItemID");

				entity.Property(e => e.R)
					.HasColumnType("bit")
					.HasDefaultValueSql("Yes");

				entity.Property(e => e.UserId).HasColumnName("userID");

				entity.Property(e => e.W)
					.HasColumnType("bit")
					.HasDefaultValueSql("Yes");

				entity.HasOne(d => d.User)
					.WithMany(p => p.AccessLevels)
					.HasForeignKey(d => d.UserId)
					.HasConstraintName("UsersAccessLevels");
			});

			modelBuilder.Entity<AppMenuItems>(entity =>
			{
				entity.HasKey(e => e.ItemId)
					.HasName("PrimaryKey");

				entity.Property(e => e.ItemId)
					.HasColumnName("itemID")
					.HasColumnType("counter");

				entity.Property(e => e.Dllname)
					.IsRequired()
					.HasColumnName("DLLName")
					.HasMaxLength(255)
					.HasDefaultValueSql("Null");

				entity.Property(e => e.FunctionName)
					.IsRequired()
					.HasMaxLength(255)
					.HasDefaultValueSql("Null");

				entity.Property(e => e.ItemName)
					.IsRequired()
					.HasColumnName("itemName")
					.HasMaxLength(255);

				entity.Property(e => e.ParentItemId).HasColumnName("parentItemID");

				entity.Property(e => e.SequenceNumber).HasColumnName("sequenceNumber");
			});

			modelBuilder.Entity<Users>(entity =>
			{
				entity.HasKey(e => e.UserId)
					.HasName("PrimaryKey");

				entity.HasIndex(e => e.Login)
					.HasName("login")
					.IsUnique();

				entity.Property(e => e.UserId)
					.HasColumnName("userID")
					.HasColumnType("counter");

				entity.Property(e => e.Login)
					.IsRequired()
					.HasColumnName("login")
					.HasMaxLength(20);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasColumnName("password")
					.HasMaxLength(255);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
