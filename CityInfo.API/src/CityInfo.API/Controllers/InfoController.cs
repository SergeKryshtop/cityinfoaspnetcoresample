using System.Dynamic;
using Microsoft.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
	[Route("api/info")]
	public class InfoController : Controller
	{
		[HttpGet]
		public IActionResult GetAssembyInformaion()
		{
			dynamic assemblyInformation = new ExpandoObject();
			var assembly = typeof(InfoController).GetTypeInfo().Assembly;
			var app = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application;

			assemblyInformation.Product = app.ApplicationName;
			assemblyInformation.Version =
				app.ApplicationVersion;

			

			return Ok(assemblyInformation);
		}
	}
}
