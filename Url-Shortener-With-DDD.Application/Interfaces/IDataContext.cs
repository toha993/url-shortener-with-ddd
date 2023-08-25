using Microsoft.EntityFrameworkCore;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Application.Interfaces;

public interface IDataContext
{
   DbSet<Url> Urls { get; set; }
}