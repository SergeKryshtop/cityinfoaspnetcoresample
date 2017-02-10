using AutoMapper;
using CityInfo.API.Models;
using CityInfo.DAL.Entities;
using CityInfo.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;


namespace CityInfo.API
{
	public class Startup
	{
		private readonly ILogger _logger;

		public static IConfigurationRoot Configuration;

		public Startup(IHostingEnvironment environment, ILoggerFactory loggerFactory)
		{
			
			var builder = new ConfigurationBuilder()
				.SetBasePath(environment.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();


			Configuration = builder.Build();

			_logger = loggerFactory.CreateLogger("");
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvc()
					.AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

			var connectionString = Configuration["ConnectionStrings:DBConnection"];

			
			_logger.LogInformation("Connection string: " + connectionString);

			services.AddScoped<IDataStorage, DataStorage>(_ => new DataStorage(connectionString, _logger));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			env.ConfigureNLog("nlog.config");

		
			
			loggerFactory.AddConsole().AddNLog().AddDebug();

			//if (env.IsDevelopment())
			//{
			//	app.UseDeveloperExceptionPage();
			//}
			//else
			//{
			//	app.UseExceptionHandler();
			//}

			app.UseDeveloperExceptionPage();

			app.UseMvc();

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World!");
			});

			ConfigureAutomapper();
		}

		private static void ConfigureAutomapper()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<City, CityDto>();
				cfg.CreateMap<City, CityWithoutPointsOfInterestDto>();
				cfg.CreateMap<CityForCreationDto, City>();
				cfg.CreateMap<CityForUpdateDto, City>();
				cfg.CreateMap<PointOfInterest, PointOfInterestDto>();
				cfg.CreateMap<PointOfInterestForCreationDto, PointOfInterest>();
				cfg.CreateMap<PointOfInterestForUpdateDto, PointOfInterest>();
			});
		}
	}
}
