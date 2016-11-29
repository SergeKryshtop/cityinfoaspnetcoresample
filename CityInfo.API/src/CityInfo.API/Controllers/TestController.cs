using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
	[Route("api/test")]
    public class TestController : Controller
    {
		[HttpGet]
	    public IActionResult Test()
	    {
		    return Ok("Hello world 2");
	    }
    }
}
