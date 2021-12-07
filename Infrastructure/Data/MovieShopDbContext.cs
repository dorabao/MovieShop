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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<Trailer>(ConfigureTrailer);
    }

    private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
    {
        builder.ToTable("Trailer");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        builder.Property(t => t.Name).HasMaxLength(2084);
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