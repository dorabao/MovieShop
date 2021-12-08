using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext:DbContext
{
    public MovieShopDbContext (DbContextOptions<MovieShopDbContext> options) : base(options)
    {
        
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Crew> Crews { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<MovieCrew> MovieCrews { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<UserRole>(ConfigureUserRole);
        modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        modelBuilder.Entity<Review>(ConfigureReview);
    }

    private void ConfigureReview(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");
        builder.HasKey(r => new { r.MovieId, r.UserId });
        builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)").HasDefaultValue(9.9m);
    }

    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCast");
        builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character});
        builder.Property(mc => mc.Character).HasMaxLength(450);
    }

    private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
    {
        builder.ToTable("MovieCrew");
        builder.HasKey(mc => new { mc.MovieId, mc.CrewId, mc.Department, mc.Job });
        builder.Property(mc => mc.Department).HasMaxLength(128);
        builder.Property(mc => mc.Job).HasMaxLength(128);
    }

    private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
    }

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenre");
        builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
    }

    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movie");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).HasMaxLength(256);
        builder.Property(m => m.Overview).HasMaxLength(4096);
        builder.Property(m => m.Tagline).HasMaxLength(512);
        builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
        builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
        builder.Property(m => m.PosterUrl).HasMaxLength(2084);
        builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
        builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
        builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
        builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
    }
}