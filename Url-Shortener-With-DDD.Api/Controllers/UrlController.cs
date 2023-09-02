using Microsoft.AspNetCore.Mvc;
using UrlShortenerWithDDD.Application.Services;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Api.Controllers;
[Route("[controller]")]
public class UrlController : Controller
{
	private readonly IUrlService _urlService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	
	public UrlController(IUrlService urlService, IHttpContextAccessor httpContext)
	{
		_urlService = urlService;
		_httpContextAccessor = httpContext;
	}
	
	[HttpGet("shorten")]
	
	public async Task<IResult> GetUrlRequest(UrlRequestModel req)
	{
		if (!Uri.TryCreate(req.LongUrl,UriKind.Absolute,out var uri))
		{
			return Results.BadRequest("The url is not correct");
		}

		var item = await _urlService.CreateShortLink(req.LongUrl);
		var updatedUrl = _httpContextAccessor.HttpContext?.Request.Scheme + "://" + _httpContextAccessor.HttpContext?.Request.Host + "/url/" + item;
		return Results.Ok(updatedUrl);
	}
	
	[HttpGet("{code}")]
	public async Task<IResult> PostUrlRequest(string code)
	{
		var foundUrl = await _urlService.GetByCode(code);

		if (foundUrl.Link != null) return Results.Redirect(foundUrl.Link);
		return Results.BadRequest("The url is not correct");
	}
	
}



