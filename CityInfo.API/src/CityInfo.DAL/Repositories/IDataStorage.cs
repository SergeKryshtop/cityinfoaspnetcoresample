using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.DAL.Entities;

namespace CityInfo.DAL.Repositories
{
	public interface IDataStorage
	{
		IRepository<City> Cities { get; }
		IRepository<PointOfInterest> PointsOfInterest { get; }

		void SaveChanges();
	}
}
