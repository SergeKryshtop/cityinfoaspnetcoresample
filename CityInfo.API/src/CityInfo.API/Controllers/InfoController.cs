using System.Dynamic;
using Microsoft.AspNetCore;
using System.Reflection;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
	[Route("api/info")]
	public class InfoController : ApiController
	{
		[HttpGet]
		public IActionResult GetAssembyInformaion()
		{
			dynamic assemblyInformation = new ExpandoObject();
			var assembly = typeof(Startup).GetTypeInfo().Assembly;

			assemblyInformation.AssemblyInformationalVersion =
					Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;

			assemblyInformation.Name = assembly.GetName().Name;
			assemblyInformation.AssemblyVersion = assembly.GetName().Version.ToString();

			assemblyInformation.AssemblyFileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()
					.Version;

			return Ok(assemblyInformation);
		}
	}
}
