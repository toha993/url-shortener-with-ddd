using System.Text;
using Microsoft.EntityFrameworkCore;
using UrlShortenerWithDDD.Application.Interfaces;

namespace UrlShortenerWithDDD.Application.Helpers;


public interface IRandomCodeGenerator
{
    Task<string> GenerateCode(int len = 7);
}
public class RandomCodeGenerator : IRandomCodeGenerator
{
    private readonly IDataContext _context;
    
    public RandomCodeGenerator(IDataContext context)
    {
        _context = context;
    }
    public  async Task<string> GenerateCode(int len = 7)
    {
        var generatedCode = BuildCode();
        while (_context.Urls != null && await _context.Urls.FirstOrDefaultAsync((url) => url.Code == generatedCode) != null)
        {
            generatedCode = BuildCode();
        }

        return generatedCode;
    }

    private static string BuildCode(int len = 7)
    {
        StringBuilder builder = new();
        Random random = new();
        for (var i = 0; i < len; i++)
        {
            var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        
        return builder.ToString().ToLower();
    }
}