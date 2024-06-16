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
            new UserType { Id = 1, Role = "Administrator" },
            new UserType { Id = 2, Role = "Technical Support" },
            new UserType { Id = 3, Role = "User" }
        };

        public static List<ConnectionType> ConnectionTypeSeed = new List<ConnectionType>
        {
            new ConnectionType { Id = 1, ConnectType = "Remote" },
            new ConnectionType { Id = 2, ConnectType = "Wireless" }
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
            new Status { Id = 1, IssueStatus = "New" },
            new Status { Id = 2, IssueStatus = "Assigned" },
            new Status { Id = 3, IssueStatus = "In progress" },
            new Status { Id = 4, IssueStatus = "Pending" }
        };

        public static List<IssueName> IssueNamesSeed = new List<IssueName>
        {
             // Software Issues
            new IssueName { Id = 1, Name = "Software Issues", IssueRank = 0, ParentId = null },
            new IssueName { Id = 2, Name = "System Crashes", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 3, Name = "Application Errors", IssueRank = 2, ParentId = 2 },
            new IssueName { Id = 4, Name = "Operating System Failures", IssueRank = 2, ParentId = 2 },
            new IssueName { Id = 5, Name = "Incompatibility Issues", IssueRank = 2, ParentId = 2 },
            new IssueName { Id = 6, Name = "Software Bugs", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 7, Name = "Transaction Errors", IssueRank = 2, ParentId = 6 },
            new IssueName { Id = 8, Name = "Incorrect Inventory Updates", IssueRank = 2, ParentId = 6 },
            new IssueName { Id = 9, Name = "Receipt Printing Errors", IssueRank = 2, ParentId = 6 },
            new IssueName { Id = 10, Name = "Network Connectivity", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 11, Name = "Wi-Fi Issues", IssueRank = 2, ParentId = 10 },
            new IssueName { Id = 12, Name = "Ethernet Connection Problems", IssueRank = 2, ParentId = 10 },
            new IssueName { Id = 13, Name = "Router/Modem Failures", IssueRank = 2, ParentId = 10 },
            new IssueName { Id = 14, Name = "Security Issues", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 15, Name = "Data Breaches", IssueRank = 2, ParentId = 14 },
            new IssueName { Id = 16, Name = "Malware Infections", IssueRank = 2, ParentId = 14 },
            new IssueName { Id = 17, Name = "Unauthorized Access", IssueRank = 2, ParentId = 14 },
            new IssueName { Id = 18, Name = "Integration Issues", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 19, Name = "Payment Gateway Failures", IssueRank = 2, ParentId = 18 },
            new IssueName { Id = 20, Name = "CRM Integration Problems", IssueRank = 2, ParentId = 18 },
            new IssueName { Id = 21, Name = "Inventory Management Sync Issues", IssueRank = 2, ParentId = 18 },
            new IssueName { Id = 22, Name = "User Interface Problems", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 23, Name = "Slow Performance", IssueRank = 2, ParentId = 22 },
            new IssueName { Id = 24, Name = "Unresponsive UI", IssueRank = 2, ParentId = 22 },
            new IssueName { Id = 25, Name = "Navigation Difficulties", IssueRank = 2, ParentId = 22 },
            new IssueName { Id = 26, Name = "Update Failures", IssueRank = 1, ParentId = 1 },
            new IssueName { Id = 27, Name = "Patch Install Errors", IssueRank = 2, ParentId = 26 },
            new IssueName { Id = 28, Name = "Version Compatibility Issues", IssueRank = 2, ParentId = 26 },
            new IssueName { Id = 29, Name = "Rollback Failures", IssueRank = 2, ParentId = 26 },

            // Hardware Issues
            new IssueName { Id = 30, Name = "Hardware Issues", IssueRank = 0, ParentId = null },
            new IssueName { Id = 31, Name = "Terminal Failures", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 32, Name = "Power Supply Issues", IssueRank = 2, ParentId = 31 },
            new IssueName { Id = 33, Name = "Screen Malfunctions", IssueRank = 2, ParentId = 31 },
            new IssueName { Id = 34, Name = "Touchscreen Problems", IssueRank = 2, ParentId = 31 },
            new IssueName { Id = 35, Name = "Peripheral Malfunctions", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 36, Name = "Barcode Scanner Failures", IssueRank = 2, ParentId = 35 },
            new IssueName { Id = 37, Name = "Receipt Printer Problems", IssueRank = 2, ParentId = 35 },
            new IssueName { Id = 38, Name = "Cash Drawer Issues", IssueRank = 2, ParentId = 35 },
            new IssueName { Id = 39, Name = "Connectivity Problems", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 40, Name = "Cable Damage", IssueRank = 2, ParentId = 39 },
            new IssueName { Id = 41, Name = "Port Failures", IssueRank = 2, ParentId = 39 },
            new IssueName { Id = 42, Name = "Bluetooth Pairing Issues", IssueRank = 2, ParentId = 39 },
            new IssueName { Id = 43, Name = "Card Reader Issues", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 44, Name = "Magnetic Stripe Reader Failures", IssueRank = 2, ParentId = 43 },
            new IssueName { Id = 45, Name = "Chip Reader Malfunctions", IssueRank = 2, ParentId = 43 },
            new IssueName { Id = 46, Name = "Contactless Payment Problems", IssueRank = 2, ParentId = 43 },
            new IssueName { Id = 47, Name = "Environmental Factors", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 48, Name = "Overheating", IssueRank = 2, ParentId = 47 },
            new IssueName { Id = 49, Name = "Dust and Dirt", IssueRank = 2, ParentId = 47 },
            new IssueName { Id = 50, Name = "Moisture Damage", IssueRank = 2, ParentId = 47 },
            new IssueName { Id = 51, Name = "Power Issues", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 52, Name = "Battery Failures", IssueRank = 2, ParentId = 51 },
            new IssueName { Id = 53, Name = "Power Surge Damage", IssueRank = 2, ParentId = 51 },
            new IssueName { Id = 54, Name = "Unstable Power Supply", IssueRank = 2, ParentId = 51 },
            new IssueName { Id = 55, Name = "Wear and Tear", IssueRank = 1, ParentId = 30 },
            new IssueName { Id = 56, Name = "Keyboard Malfunctions", IssueRank = 2, ParentId = 55 },
            new IssueName { Id = 57, Name = "Physical Damage", IssueRank = 2, ParentId = 55 },
            new IssueName { Id = 58, Name = "Component Aging", IssueRank = 2, ParentId = 55 }
        };

        public static List<Priority> PrioritiesSeed = new List<Priority>
        {
            new Priority { Id = 1, IssuePriority = "Low" },
            new Priority { Id = 2, IssuePriority = "Medium" },
            new Priority { Id = 3, IssuePriority = "High" },
            new Priority { Id = 4, IssuePriority = "None" }
        };
    }
}