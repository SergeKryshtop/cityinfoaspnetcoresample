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

			assemblyInformation.AssemblyVersion =
					Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyVersionAttribute>().Version;

			assemblyInformation.Product = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
			assemblyInformation.Version =
				Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;



			return Ok(assemblyInformation);
		}
	}
}
