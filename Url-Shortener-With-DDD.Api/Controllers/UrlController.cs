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
			var updatedUrl = GetBaseUrl() + "/url/" + item;
			data = GetResponseBody("Success", updatedUrl);
		}
		else
		{
			data = GetResponseBody("Failed", null);
		}
		return GetJsonResult(data, isValid ? 200 : 404);
	}
	
	[HttpGet("{code}")]
	public async Task<IResult> PostUrlRequest(string code)
	{
		string? foundLongUrl = await _urlService.GetByCode(code);
		if (foundLongUrl.IsNullOrEmpty() == false)
		{
			return Results.Redirect(foundLongUrl!);
			/*return GetJsonResult(GetResponseBody("Success", foundLongUrl), 200);*/
		}
		return Results.Redirect("https://www.google.com");
		/*return GetJsonResult(GetResponseBody("Failed", "https://www.google.com"), 404);*/
		
	}
	
	private string GetBaseUrl()
	{
		var request = _httpContextAccessor.HttpContext?.Request;
		var host = request?.Host.ToUriComponent();
		return $"{request?.Scheme}://{host}";
	}
	
	private ResponseBody GetResponseBody(string message, string? data)
	{
		return new ResponseBody()
		{
			Message = message,
			Data = data,
		};
	}
	
	private JsonResult GetJsonResult(ResponseBody body, int statusCode)
	{
		return new JsonResult(body)
		{
			StatusCode = statusCode
		};
	}
	
}



