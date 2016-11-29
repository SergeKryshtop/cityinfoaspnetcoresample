using System.Collections.Generic;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CityInfo.DAL.Repositories;
using AutoMapper;
using CityInfo.DAL.Entities;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class CitiesController : Controller
	{
		private IDataStorage _dataStorage;

		public CitiesController(IDataStorage dataStorage)
		{
			_dataStorage = dataStorage;
		}

		[HttpGet]
		public IActionResult GetCities(bool includePointsOfInterest = false)
		{
			var entities = _dataStorage.Cities.Get();

			if (includePointsOfInterest)
			{
				return Ok(Mapper.Map<IEnumerable<CityDto>>(entities));
			}

			return Ok(Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(entities));
		}

		[HttpGet("{id}", Name = "GetCity")]
		public IActionResult GetCity(int id, bool includePointsOfInterest = false)
		{
			var entities = _dataStorage.Cities.Get(id);

			if (entities == null)
			{
				return NotFound();
			}

			if (includePointsOfInterest)
			{
				return Ok(Mapper.Map<CityDto>(entities));
			}
			return Ok(Mapper.Map<CityWithoutPointsOfInterestDto>(entities));
		}

		[HttpPost]
		public IActionResult CreateCity([FromBody] CityForCreationDto city)
		{
			if (city == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var entity = Mapper.Map<City>(city);

			_dataStorage.Cities.Create(entity);
			_dataStorage.SaveChanges();

			var createdCity =  Mapper.Map<CityWithoutPointsOfInterestDto>(entity);
			
			return CreatedAtRoute("GetCity", new { id = createdCity.Id }, city); ;
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCity(int id, [FromBody]CityForUpdateDto city)
		{
			if (city == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var cityFromStore = _dataStorage.Cities.Get(id);

			if (cityFromStore == null)
			{
				return NotFound();
			}

			Mapper.Map(city, cityFromStore);
			_dataStorage.Cities.Update(cityFromStore);
			_dataStorage.SaveChanges();

			return NoContent();
		}

		[HttpPatch("{id}")]
		public IActionResult PartiallyUpdateCity(int id, [FromBody] JsonPatchDocument<CityForUpdateDto> patchDoc)
		{
			if (patchDoc == null)
			{
				return BadRequest();
			}

			var cityFromStore = _dataStorage.Cities.Get(id);

			if (cityFromStore == null)
			{
				return NotFound();
			}

			var cityToPatch = Mapper.Map<CityForUpdateDto>(cityFromStore);

			patchDoc.ApplyTo(cityToPatch, ModelState);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Mapper.Map(cityToPatch, cityFromStore);
			_dataStorage.Cities.Update(cityFromStore);
			_dataStorage.SaveChanges();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCity(int id)
		{
			var city = _dataStorage.Cities.Get(id);

			if (city == null)
			{
				return NotFound();
			}

			_dataStorage.Cities.Delete(id);
			_dataStorage.SaveChanges();

			return NoContent();
		}
	}
}
