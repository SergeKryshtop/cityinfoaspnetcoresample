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

			assemblyInformation.InformationVersion =
				assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

			return Ok(assemblyInformation);
		}
	}
}
