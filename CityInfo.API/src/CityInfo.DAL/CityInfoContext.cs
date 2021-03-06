﻿using CityInfo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DAL
{
	public class CityInfoContext : DbContext
	{
		public CityInfoContext(DbContextOptions<CityInfoContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<PointOfInterest> PointsOfInterest { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<City>().ToTable("City");
			modelBuilder.Entity<PointOfInterest>().ToTable("PointOfInterest");
		}
	}
}
