using Microsoft.EntityFrameworkCore;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class ModelContext : DbContext
	{
		public ModelContext()
		{
		}

		public ModelContext(DbContextOptions<ModelContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Banks> Banks { get; set; }
		public virtual DbSet<Categories> Categories { get; set; }
		public virtual DbSet<Cinemas> Cinemas { get; set; }
		public virtual DbSet<Distributors> Distributors { get; set; }
		public virtual DbSet<FilmScreenings> FilmScreenings { get; set; }
		public virtual DbSet<Films> Films { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				string path = Settings.Settings.DBPath;
				optionsBuilder.UseJet("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Banks>(entity =>
			{
				entity.HasKey(e => e.BankId)
					.HasName("PrimaryKey");

				entity.ToTable("banks");

				entity.Property(e => e.BankId)
					.HasColumnName("bankID")
					.HasColumnType("counter");

				entity.Property(e => e.BankName)
					.HasColumnName("bankName")
					.HasMaxLength(255);
			});

			modelBuilder.Entity<Categories>(entity =>
			{
				entity.HasKey(e => e.CategoryId)
					.HasName("PrimaryKey");

				entity.ToTable("categories");

				entity.Property(e => e.CategoryId)
					.HasColumnName("categoryID")
					.HasColumnType("counter");

				entity.Property(e => e.CategoryName)
					.HasColumnName("categoryName")
					.HasMaxLength(255);
			});

			modelBuilder.Entity<Cinemas>(entity =>
			{
				entity.HasKey(e => e.CinemaId)
					.HasName("PrimaryKey");

				entity.ToTable("cinemas");

				entity.HasIndex(e => e.CinemaName)
					.HasName("cinemaName")
					.IsUnique();

				entity.Property(e => e.CinemaId)
					.HasColumnName("cinemaID")
					.HasColumnType("counter");

				entity.Property(e => e.BankAccount)
					.HasColumnName("bankAccount")
					.HasMaxLength(255);

				entity.Property(e => e.BankId).HasColumnName("bankID");

				entity.Property(e => e.CinemaAdress)
					.HasColumnName("cinemaAdress")
					.HasMaxLength(255);

				entity.Property(e => e.CinemaName).HasColumnName("cinemaName");

				entity.Property(e => e.DirectorName)
					.HasColumnName("directorName")
					.HasMaxLength(255);

				entity.Property(e => e.Inn)
					.HasColumnName("INN")
					.HasMaxLength(255);

				entity.Property(e => e.OwnerName)
					.HasColumnName("ownerName")
					.HasMaxLength(255);

				entity.Property(e => e.PhoneNumber)
					.HasColumnName("phoneNumber")
					.HasMaxLength(255);

				entity.Property(e => e.SeatsNumber)
					.HasColumnName("seatsNumber")
					.HasDefaultValueSql("0");

				entity.HasOne(d => d.Bank)
					.WithMany(p => p.Cinemas)
					.HasForeignKey(d => d.BankId)
					.HasConstraintName("bankscinemas");
			});

			modelBuilder.Entity<Distributors>(entity =>
			{
				entity.HasKey(e => e.DistributorId)
					.HasName("PrimaryKey");

				entity.ToTable("distributors");

				entity.Property(e => e.DistributorId)
					.HasColumnName("distributorID")
					.HasColumnType("counter");

				entity.Property(e => e.BankAccount)
					.IsRequired()
					.HasColumnName("bankAccount")
					.HasMaxLength(255);

				entity.Property(e => e.BankId).HasColumnName("bankID");

				entity.Property(e => e.DistributorName)
					.IsRequired()
					.HasColumnName("distributorName")
					.HasMaxLength(255);

				entity.Property(e => e.Inn)
					.IsRequired()
					.HasColumnName("INN")
					.HasMaxLength(255);

				entity.Property(e => e.LegalAddress)
					.IsRequired()
					.HasColumnName("legalAddress")
					.HasMaxLength(255);

				entity.HasOne(d => d.Bank)
					.WithMany(p => p.Distributors)
					.HasForeignKey(d => d.BankId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("banksdistributors");
			});

			modelBuilder.Entity<FilmScreenings>(entity =>
			{
				entity.HasKey(e => e.FilmScreeningId)
					.HasName("PrimaryKey");

				entity.ToTable("filmScreenings");

				entity.HasIndex(e => e.FilmScreeningId)
					.HasName("showingFilmID");

				entity.Property(e => e.FilmScreeningId)
					.HasColumnName("filmScreeningID")
					.HasColumnType("counter");

				entity.Property(e => e.CinemaId).HasColumnName("cinemaID");

				entity.Property(e => e.EndScreeningDate).HasColumnName("endScreeningDate");

				entity.Property(e => e.FilmId).HasColumnName("filmID");

				entity.Property(e => e.IsReturned)
					.HasColumnName("isReturned")
					.HasColumnType("bit")
					.HasDefaultValueSql("=No");

				entity.Property(e => e.LateReturnPenalty)
					.HasColumnName("lateReturnPenalty")
					.HasColumnType("currency");

				entity.Property(e => e.RentalPaymentAmount)
					.HasColumnName("rentalPaymentAmount")
					.HasColumnType("currency");

				entity.Property(e => e.StartScreeningDate).HasColumnName("startScreeningDate");

				entity.HasOne(d => d.Cinema)
					.WithMany(p => p.FilmScreenings)
					.HasForeignKey(d => d.CinemaId)
					.HasConstraintName("cinemasfilmScreenings");

				entity.HasOne(d => d.Film)
					.WithMany(p => p.FilmScreenings)
					.HasForeignKey(d => d.FilmId)
					.HasConstraintName("filmsfilmScreenings");
			});

			modelBuilder.Entity<Films>(entity =>
			{
				entity.HasKey(e => e.FilmId)
					.HasName("PrimaryKey");

				entity.ToTable("films");

				entity.HasIndex(e => e.FilmId)
					.HasName("filmID")
					.IsUnique();

				entity.Property(e => e.FilmId)
					.HasColumnName("filmID")
					.HasColumnType("counter");

				entity.Property(e => e.CategoryId).HasColumnName("categoryID");

				entity.Property(e => e.DistributorId).HasColumnName("distributorID");

				entity.Property(e => e.FilmCost)
					.HasColumnName("filmCost")
					.HasColumnType("currency")
					.HasDefaultValueSql("0");

				entity.Property(e => e.FilmName)
					.HasColumnName("filmName")
					.HasMaxLength(255);

				entity.Property(e => e.ProducerName)
					.HasColumnName("producerName")
					.HasMaxLength(255);

				entity.Property(e => e.ProductionСompaniesName)
					.HasColumnName("productionСompaniesName")
					.HasMaxLength(255);

				entity.Property(e => e.ReleaseDate).HasColumnName("releaseDate");

				entity.Property(e => e.ScriptwriterName)
					.HasColumnName("scriptwriterName")
					.HasMaxLength(255);

				entity.HasOne(d => d.Category)
					.WithMany(p => p.Films)
					.HasForeignKey(d => d.CategoryId)
					.HasConstraintName("categoriesfilms");

				entity.HasOne(d => d.Distributor)
					.WithMany(p => p.Films)
					.HasForeignKey(d => d.DistributorId)
					.HasConstraintName("distributorsfilms");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
