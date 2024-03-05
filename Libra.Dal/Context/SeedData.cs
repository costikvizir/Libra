using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra.Dal.Context
{
	public static class SeedData
	{



		public static List<City> GetCities()
		{
			int id = 1;
			var cities = new List<string> { "Chișinău", "Bălți", "Tiraspol", "Bender", "Cahul", "Comrat", "Orhei", "Ungheni", "Soroca", "Călărași", };

			var citiesList = new List<City>();

			foreach (var city in cities)
			{
				citiesList.Add(new City
				{
					Id = id++,
					CityName = city
				});
			}
			//if (!Debugger.IsAttached)
			//{
			//	Debugger.Launch();
			//}

			//var cities = new List<City>
			//{
			//	new City { Id = 1, CityName = "Chișinău"},
			//	new City { Id = 2, CityName = "Bălți"},
			//	new City { Id = 3, CityName = "Tiraspol"},
			//	new City { Id = 4, CityName = "Bender"},
			//	new City { Id = 5, CityName = "Cahul"},
			//	new City { Id = 6, CityName = "Comrat"},
			//	new City { Id = 7, CityName = "Orhei"},
			//	new City { Id = 8, CityName = "Ungheni"},
			//	new City { Id = 9, CityName = "Soroca"},
			//	new City { Id = 10, CityName = "Călărași"}

			//};

			return citiesList;
		}


	 //public static List<UserType> GetRoles()
		//{
		//	var id = 1;
		//	var roles = new List<string> { "Administrator", "Technical Group", "User" };

		//	var rolesList = new List<UserType>();

		//	if (!Debugger.IsAttached)
		//	{
		//		Debugger.Launch();
		//	}

		//	foreach (var role in roles)
		//	{
		//		rolesList.Add(new UserType
		//		{
		//			Id = id++,
		//			Role = role
		//		});
		//	}

		//	return rolesList;
		//}
		public static List<UserType> UserTypesSeed = new List<UserType>
		{
			new UserType
			{
				Id = 1,
				Role = "Administrator"
			},
			new UserType
			{
				Id = 2,
				Role = "Technical Group"
			},
			new UserType
			{
				Id = 3,
				Role = "User"
			}
		};

		public static List<User> UsersSeed = new List<User>
		{

			new User
			{
				Id = 1,
				Email = "admin@gmail.com",
				Name = "admin",
				Login = "admin",
				Password = "admin",
				UserTypeId = 1,
				IsDeleted = false
			}
		};
	}
}
