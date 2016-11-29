using CityInfo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DAL.Repositories
{
	public class DataStorage : IDataStorage
	{
		private CityInfoContext _context;

		private IRepository<City> _cities;
		private IRepository<PointOfInterest> _pointsOfInterest;

		public DataStorage(string connectionString)
		{
			var builder = new DbContextOptionsBuilder<CityInfoContext>();
			builder.UseSqlServer(connectionString);

			_context = new CityInfoContext(builder.Options);
			_context.EnsureSeedDataForContext();
		}

		public IRepository<City> Cities
		{
			get
			{
				if (_cities == null)
				{
					_cities = new CitiesRepository(_context);
				}

				return _cities;
			} 
		}

		public IRepository<PointOfInterest> PointsOfInterest
		{
			get
			{
				if (_pointsOfInterest == null)
				{
					_pointsOfInterest = new PointsOfInterestRepository(_context);
				}

				return _pointsOfInterest;
			}
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
