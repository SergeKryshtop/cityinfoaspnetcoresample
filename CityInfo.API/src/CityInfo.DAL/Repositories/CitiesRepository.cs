using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CityInfo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DAL.Repositories
{
	public class CitiesRepository : RepositoryBase<City>
	{
		public CitiesRepository(CityInfoContext context) : base(context)
		{
		}

		public override IEnumerable<City> Get(Expression<Func<City, bool>> predicate = null)
		{
			if (predicate != null)
			{
				return DbSet.Include(t => t.PointsOfInterest).Where(predicate).ToList();
			}

			return DbSet.Include(t => t.PointsOfInterest).ToList();
		}

		public override City Get(int id)
		{
			var entry = DbSet.Include(t => t.PointsOfInterest).FirstOrDefault(e => e.Id == id);

			return entry;
		}
	}
}
