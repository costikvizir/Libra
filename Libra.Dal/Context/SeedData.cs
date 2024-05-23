using Libra.Dal.Entities;
using System.Collections.Generic;

namespace Libra.Dal.Context
{
    public static class SeedData
    {
        //Method to insert a set of cities into the database
        public static List<City> GetCities()
        {
            int id = 1;
            var cities = new List<string> { "Chișinău", "Bălți", "Tiraspol", "Bender", "Cahul", "Comrat", "Orhei", "Ungheni", "Soroca", "Călărași", "Ialoveni", "Edineț", "Hîncești", };

            var citiesList = new List<City>();

            foreach (var city in cities)
            {
                citiesList.Add(new City
                {
                    Id = id++,
                    CityName = city
                });
            }

            return citiesList;
        }

        //Method to insert weekdays into the database
        public static List<WeekDay> GetWeekDays()
        {
            int id = 1;
            var days = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", };

            var dayList = new List<WeekDay>();

            foreach (var city in days)
            {
                dayList.Add(new WeekDay
                {
                    Id = id++,
                    Day = city
                });
            }

            return dayList;
        }

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
                Role = "Technical Support"
            },
            new UserType
            {
                Id = 3,
                Role = "User"
            }
        };

        public static List<ConnectionType> ConnectionTypeSeed = new List<ConnectionType>
        {
            new ConnectionType
            {
                Id = 1,
                ConnectType = "Remote"
            },
            new ConnectionType
            {
                Id = 2,
                ConnectType = "Wireless"
            },
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

        public static List<Status> statusesSeed = new List<Status>
        {
            new Status
            {
                Id = 1,
                IssueStatus = "New"
            },
            new Status
            {
                Id = 2,
                IssueStatus = "Assigned"
            },
            new Status
            {
                Id = 3,
                IssueStatus = "In progress"
            },
            new Status
            {
                Id = 4,
                IssueStatus = "Pending"
            }
        };

        public static List<IssueName> IssueNamesSeed = new List<IssueName>
        {
            new IssueName{ Id = 1, Name = "None", IssueRank = 3 },
            new IssueName{ Id = 2, Name = "Software", IssueRank = 0 },
            new IssueName{ Id = 3, Name = "Hardware", IssueRank = 0 },
            new IssueName{ Id = 4, Name = "Driver Issue", IssueRank = 1 },
            new IssueName{ Id = 5, Name = "Database", IssueRank = 1 },
            new IssueName{ Id = 6, Name = "Slow Processing", IssueRank = 1 },
            new IssueName{ Id = 7, Name = "Security", IssueRank = 1 },
            new IssueName{ Id = 8, Name = "Data Loss", IssueRank = 1 },
            new IssueName{ Id = 9, Name = "Components", IssueRank = 1 },
            new IssueName{ Id = 10, Name = "Network", IssueRank = 2 },
            new IssueName{ Id = 11, Name = "Compatibility", IssueRank = 2 },
        };

        public static List<Priority> PrioritiesSeed = new List<Priority>
        {
            new Priority
            {
                Id = 1,
                IssuePriority = "Low"
            },
            new Priority
            {
                Id = 2,
                IssuePriority = "Medium"
            },
            new Priority
            {
                Id = 3,
                IssuePriority = "High"
            },
            new Priority
            {
                Id = 4,
                IssuePriority = "None"
            }
        };
    }
}