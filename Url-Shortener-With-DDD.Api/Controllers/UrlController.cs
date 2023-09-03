using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UrlShortenerWithDDD.Application.Services;
using UrlShortenerWithDDD.Domain.Models;

namespace UrlShortenerWithDDD.Api.Controllers;
[Route("[controller]")]
public class UrlController : Controller
{
	private readonly IUrlService _urlService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private string? foundLongUrl;

	public UrlController(IUrlService urlService, IHttpContextAccessor httpContext)
	{
		_urlService = urlService;
		_httpContextAccessor = httpContext;
	}
	
	[HttpPost("shorten")]
	
	public async Task<IActionResult> GetUrlRequest([FromBody] UrlRequestModel req)
	{
		bool isValid = Uri.TryCreate(req.LongUrl,UriKind.Absolute,out var uri);
		var data = new ResponseBody();
		if (isValid)
		{
			var item = await _urlService.CreateShortLink(req.LongUrl!);
			var updatedUrl = _httpContextAccessor.HttpContext?.Request.Scheme + "://" + _httpContextAccessor.HttpContext?.Request.Host + "/url/" + item;
			data.Message = "Success";
			data.Data = updatedUrl;
		}
		else
		{
			data.Message = "Invalid Url";
			data.Data = null;
		}
		return new JsonResult(data)
		{
			StatusCode = isValid ?  200 : 404
		};
	}
	
	[HttpGet("{code}")]
	public async Task<IActionResult> PostUrlRequest(string code)
	{
		foundLongUrl = await _urlService.GetByCode(code);
		if (foundLongUrl.IsNullOrEmpty() == false)
		{
			return new JsonResult( new ResponseBody()
			{
				Message = "Success",
				Data = foundLongUrl
			})
			{
				StatusCode = 200
			};
		}	
		else
		{
			return new JsonResult( new ResponseBody()
			{
				Message = "Failed",
				Data = "https://www.google.com"
			})
			{
				StatusCode = 404
			};
			
		}
	}
	
}



