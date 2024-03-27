﻿using Libra.Dal.Configurations;
using Libra.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Context
{
	public class LibraContext : DbContext
	{
        //string connectionString = "Data Source=CEDINTL925\\MSSQLSERVERSC;Initial Catalog=LibraDb;Integrated Security=True;TrustServerCertificate=true;";
		//<add name="DefaultConnection" connectionString="Data Source=CEDINTL925\MSSQLSERVERSC;Initial Catalog=MVC_DB;Integrated Security=True" providerName="System.Data.SqlClient" />
		string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		//string connectionString = "Data Source=CEDINTL925\\MSSQLSERVERSC;Initial Catalog=LibraDb;Integrated Security=True;TrustServerCertificate=true;";
		//string mystring = "123";

		//public LibraContext(DbContextOptions<LibraContext> options) : base(options)
		//{
		//}

		//public LibraContext() : base("DefaultConnection")
		//{

		//}

		public DbSet<City> Cities { get; set; }
		public DbSet<ConnectionType> ConnectionType { get; set; }
		public DbSet<Issue> Issues { get; set; }
		public DbSet<IssueType> IssueTypes { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Pos> Pos { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserType> UserTypes { get; set; }
		public DbSet<PosWeekDay> PosWeekDays { get; set; }
		public DbSet<WeekDay> WeekDays { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(connectionString);
			//optionsBuilder.UseSqlServer("DefaultConnection");
			optionsBuilder.EnableSensitiveDataLogging();

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CityConfiguration());
			modelBuilder.ApplyConfiguration(new ConnectionTypeConfiguration());
			modelBuilder.ApplyConfiguration(new IssueConfiguration());
			modelBuilder.ApplyConfiguration(new IssueTypeConfiguration());
			modelBuilder.ApplyConfiguration(new LogConfiguration());
			modelBuilder.ApplyConfiguration(new PosConfiguration());
			modelBuilder.ApplyConfiguration(new StatusConfiguration());
			modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());


			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}