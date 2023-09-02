using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UrlShortenerWithDDD.Application.Interfaces;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Infrastructure.Data.Database;

public class DataContext : DbContext, IDataContext
{
    
    public DataContext()
    {
        
    }
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    public DbSet<Url>? Urls { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Url>()
            .Property(u => u.Code).HasMaxLength(7);
        builder.Entity<Url>()
            .HasIndex(u => u.Code)
            .IsUnique();
    }
    
    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

}
