using CityInfo.DAL.Entities;

namespace CityInfo.DAL.Repositories
{
	public class PointsOfInterestRepository : RepositoryBase<PointOfInterest>
	{
		public PointsOfInterestRepository(CityInfoContext context) : base(context)
		{
		}
	}
}
