using System.Xml;
using Microsoft.EntityFrameworkCore;
using UrlShortenerWithDDD.Application.Helpers;
using UrlShortenerWithDDD.Application.Interfaces;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Application.Services;


public interface IUrlService
{
    Task<Url> GetByCode(string code);
    Task<string> CreateShortLink(string url);

}
public class UrlService : IUrlService
{

    private readonly IDataContext _context;
    private readonly IRandomCodeGenerator _randomCodeGenerator;

    public UrlService(IDataContext context, IRandomCodeGenerator randomCodeGenerator)
    {
        _context = context;
        _randomCodeGenerator = randomCodeGenerator;
    }


    public async Task<Url> GetByCode(string code)
    {
        var longUrl = await _context.Urls!.FirstOrDefaultAsync(x => x.Code == code);
        if (longUrl == null)
        {
            throw new Exception("Url not found");
        }
        return longUrl;
    }

    public async Task<string> CreateShortLink(string urlString)
    {
        if (await _context.Urls.FirstOrDefaultAsync(url => url.Link == urlString) == null)
            return await GenerateShortLink(urlString);
        
        var foundUrl = await _context.Urls.FirstOrDefaultAsync(url => url.Link == urlString);
        return foundUrl.Code;
        

    }

    private async Task<string> GenerateShortLink(string url)
    {
        var createdShortLink = await _randomCodeGenerator.GenerateCode(7);
        var newUrl = new Url
        {
            Id = Guid.NewGuid().ToString(),
            Code = createdShortLink,
            Link = url,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            UpdatedShortUrl = createdShortLink
        };

        _context.Urls?.Add(newUrl);

        await _context.SaveChangesAsync();

        return createdShortLink;

        
    }
}
