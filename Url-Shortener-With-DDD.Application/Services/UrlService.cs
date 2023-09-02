using System.Xml;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using UrlShortenerWithDDD.Application.Helpers;
using UrlShortenerWithDDD.Application.Interfaces;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Application.Services;


public interface IUrlService
{
    Task<string?> GetByCode(string code);
    Task<string> CreateShortLink(string url);

}
public class UrlService : IUrlService
{

    private readonly IDataContext _context;
    private readonly IRandomCodeGenerator _randomCodeGenerator;
    private readonly IAppCache _cache;

    public UrlService(IDataContext context, IRandomCodeGenerator randomCodeGenerator, IAppCache cache)
    {
        _context = context;
        _randomCodeGenerator = randomCodeGenerator;
        _cache = cache;
    }


    public async Task<string?> GetByCode(string code)
    {
        return await _cache.GetOrAddAsync(code, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            return await GetLongUrl(code);
        });
    }

    private async Task<string?> GetLongUrl(string code)
    {
        var url = await _context.Urls!.FirstOrDefaultAsync(x => x.Code == code);
        return url is not null ? url.Link : string.Empty;
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
