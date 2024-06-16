using Libra.Dal.Configurations;
using Libra.Dal.Entities;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Libra.Dal.Context
{
    public class LibraContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<ConnectionType> ConnectionType { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Pos> Pos { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PosWeekDay> PosWeekDay { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<IssueName> IssueNames { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        public LibraContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraContext, LibraDbInitializer>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ConnectionTypeConfiguration());
            modelBuilder.Configurations.Add(new IssueConfiguration());
            modelBuilder.Configurations.Add(new IssueTypeConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new PosConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
            modelBuilder.Configurations.Add(new UserTypeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PosWeekDayConfiguration());
            modelBuilder.Configurations.Add(new WeekDayConfiguration());
            modelBuilder.Configurations.Add(new IssueNameConfiguration());
            modelBuilder.Configurations.Add(new PriorityConfiguration());

            //modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}