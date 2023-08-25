using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UrlShortenerWithDDD.Application.Interfaces;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Infrastructure.Data.Database;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Url> Urls { get; set; }
}
