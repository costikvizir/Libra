using Libra.Dal.Entities;
using Microsoft.EntityFrameworkCore;
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
		string connectionString = "Data Source=CEDINTL925\\MSSQLSERVERSC;Initial Catalog=LibraDb;Integrated Security=True;";
		//<add name="DefaultConnection" connectionString="Data Source=CEDINTL925\MSSQLSERVERSC;Initial Catalog=Student;Integrated Security=True" providerName="System.Data.SqlClient" />

		public DbSet<City> Cities { get; set; }
		public DbSet<ConnectionType> ConnectionType { get; set; }
		public DbSet<Issue> Issues { get; set; }
		public DbSet<IssueType> IssueTypes { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Pos> Pos { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserType> UserTypes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<City>()
				.HasMany(e => e.PosList)
				.WithOne(e => e.City)
				.HasForeignKey(e => e.CityId)
				.IsRequired();

			modelBuilder.Entity<ConnectionType>()
			   .HasMany(e => e.PosList)
			   .WithOne(e => e.ConnectionType)
			   .HasForeignKey(e => e.ConnectionTypeId)
			   .IsRequired();

			modelBuilder.Entity<Issue>()
			   .HasMany(e => e.Logs)
			   .WithOne(e => e.Issue)
			   .HasForeignKey(e => e.IssueId)
			   .IsRequired();

			modelBuilder.Entity<Pos>()
			   .HasMany(e => e.Issues)
			   .WithOne(e => e.Pos)
			   .HasForeignKey(e => e.PosId)
			   .IsRequired();

			modelBuilder.Entity<Status>()
			   .HasMany(e => e.Issues)
			   .WithOne(e => e.Status)
			   .HasForeignKey(e => e.StatusId)
			   .IsRequired();


			modelBuilder.Entity<IssueType>()
				.HasMany(e => e	.IssueTypes)
				.WithOne(e => e.IssueType)
				.HasForeignKey(e => e.TypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			modelBuilder.Entity<IssueType>()
				.HasMany(e => e.IssueSubTypes)
				.WithOne(e => e.IssueSubType)
				.HasForeignKey(e => e.SubTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			modelBuilder.Entity<IssueType>()
				.HasMany(e => e.IssuesProblems)
				.WithOne(e => e.IssueProblem)
				.HasForeignKey(e => e.ProblemId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			modelBuilder.Entity<UserType>()
				.HasMany(e => e.Issues)
				.WithOne(e => e.UserType)
				.HasForeignKey(e => e.AssignedId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}