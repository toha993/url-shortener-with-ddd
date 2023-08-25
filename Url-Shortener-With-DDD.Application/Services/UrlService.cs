using UrlShortenerWithDDD.Application.Interfaces;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Application.Services;


public interface IUrlService
{
    Url GetByCode(string code);
    void CreateShortLink(string url);

}
public class UrlService : IUrlService
{

    private readonly IDataContext _context;
    
    public Url GetByCode(string code)
    {
        throw new NotImplementedException();
    }

    public void CreateShortLink(string url)
    {
        throw new NotImplementedException();
    }
}
