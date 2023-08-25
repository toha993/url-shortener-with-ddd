using Microsoft.AspNetCore.Mvc;

namespace UrlShortenerWithDDD.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class UrlController : ControllerBase
{
	// create a getrequest method for the urlcontroller with geturlrequest as the name
	[HttpGet("GetUrlRequest")]
	
	// create a method that returns an ActionResult of GetUrlRequest
	public ActionResult GetUrlRequest()
	{
		// return a new instance of GetUrlRequest
		return Ok();
	}
	
	// create a postrequest method for the urlcontroller with posturlrequest as the name
	[HttpPost("PostUrlRequest")]
	
	// create a method that returns an ActionResult of PostUrlRequest
	public ActionResult PostUrlRequest()
	{
		// return a new instance of PostUrlRequest
		return Ok();
	}
	
}

