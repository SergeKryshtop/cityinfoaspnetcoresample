using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CityInfo.DAL.Entities;

namespace CityInfo.DAL.Repositories
{
	public interface IRepository<T> where T : IEntity
	{
		T Get(int id);
		IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null);
		T Create(T entity);
		void Update(T entity);
		void Delete(int id);
	}
}
