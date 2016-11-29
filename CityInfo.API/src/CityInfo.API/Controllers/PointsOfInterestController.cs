using System.Linq;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CityInfo.DAL.Repositories;
using AutoMapper;
using CityInfo.DAL.Entities;
using System.Collections.Generic;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class PointsOfInterestController : Controller
	{
		private IDataStorage _dataStorage;

		public PointsOfInterestController(IDataStorage dataStorage)
		{
			_dataStorage = dataStorage;
		}
		
		[HttpGet("{cityId}/pointsofinterest")]
		public IActionResult GetPointsOfInterest(int cityId)
		{
			var city = _dataStorage.Cities.Get(cityId);

			if (city == null)
			{
				return NotFound();
			}

			return Ok(Mapper.Map<IEnumerable<PointOfInterestDto>>(city.PointsOfInterest));			
		}

		[HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
		public IActionResult GetPointOfInterest(int cityId, int id)
		{
			var city = _dataStorage.Cities.Get(cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterest = _dataStorage.PointsOfInterest.Get(id);

			if (pointOfInterest == null)
			{
				return NotFound();
			}

			return Ok(Mapper.Map<PointOfInterestDto>(pointOfInterest));
		}

		[HttpPost("{cityId}/pointsofinterest")]
		public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
		{
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			if (pointOfInterest.Name == pointOfInterest.Description)
			{
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = _dataStorage.Cities.Get(cityId);

			if (city == null)
			{
				return NotFound();
			}

			var finalPointOfInterest = Mapper.Map<PointOfInterest>(pointOfInterest);

			_dataStorage.PointsOfInterest.Create(finalPointOfInterest);
			city.PointsOfInterest.Add(finalPointOfInterest);
			_dataStorage.SaveChanges();

			var createdPointOfInterest = Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

			return CreatedAtRoute("GetPointOfInterest", new { cityId = city.Id, id = createdPointOfInterest.Id }, createdPointOfInterest);
		}

		[HttpPut("{cityId}/pointsofinterest/{id}")]
		public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
		{
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			if (pointOfInterest.Name == pointOfInterest.Description)
			{
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = _dataStorage.Cities.Get(cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = _dataStorage.PointsOfInterest.Get(id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			Mapper.Map(pointOfInterest, pointOfInterestFromStore);
			_dataStorage.PointsOfInterest.Update(pointOfInterestFromStore);
			_dataStorage.SaveChanges();

			return NoContent();
		}

		[HttpPatch("{cityId}/pointsofinterest/{id}")]
		public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
			[FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
		{
			if (patchDoc == null)
			{
				return BadRequest();
			}

			var city = _dataStorage.Cities.Get(cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = _dataStorage.PointsOfInterest.Get(id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestFromStore);

			patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
			{
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			TryValidateModel(pointOfInterestToPatch);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Mapper.Map(pointOfInterestToPatch, pointOfInterestFromStore);
			_dataStorage.PointsOfInterest.Update(pointOfInterestFromStore);
			_dataStorage.SaveChanges();

			return NoContent();
		}

		[HttpDelete("{cityId}/pointsofinterest/{id}")]
		public IActionResult DeletePointOfInterest(int cityId, int id)
		{
			var city = _dataStorage.Cities.Get(cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = _dataStorage.PointsOfInterest.Get(id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			_dataStorage.PointsOfInterest.Delete(id);
			_dataStorage.SaveChanges();

			return NoContent();
		}
	}
}
