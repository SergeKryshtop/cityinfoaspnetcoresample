using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CityInfo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DAL.Repositories
{
	public class RepositoryBase<T> : IRepository<T> where T : class, IEntity
	{
		protected CityInfoContext Context;
		protected DbSet<T> DbSet;

		public RepositoryBase(CityInfoContext context)
		{
			Context = context;
			DbSet = Context.Set<T>();
		}

		public virtual T Create(T entity)
		{
			Context.Set<T>().Add(entity);

			return entity;
		}

		public virtual void Delete(int id)
		{
			var entry = DbSet.First(e => e.Id == id);
			DbSet.Remove(entry);
		}

		public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null)
		{
			if (predicate != null)
			{
				return DbSet.Where(predicate).ToList();
			}

			return DbSet.ToList();
		}

		public virtual T Get(int id)
		{
			var entry = DbSet.FirstOrDefault(e => e.Id == id);

			return entry;
		}

		public virtual void Update(T entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
		}
	}
}
