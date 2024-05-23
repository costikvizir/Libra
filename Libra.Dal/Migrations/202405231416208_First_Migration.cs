namespace Libra.Dal.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Telephone = c.String(),
                        Cellphone = c.String(),
                        Address = c.String(),
                        CityId = c.Int(nullable: false),
                        Model = c.String(),
                        Brand = c.String(),
                        ConnectionTypeId = c.Int(nullable: false),
                        MorningOpening = c.String(),
                        MorningClosing = c.String(),
                        AfternoonOpening = c.String(),
                        AfternoonClosing = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConnectionTypes", t => t.ConnectionTypeId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId)
                .Index(t => t.ConnectionTypeId);
            
            CreateTable(
                "dbo.ConnectionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        SubTypeId = c.Int(nullable: false),
                        ProblemId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        Memo = c.String(),
                        UserCreatedId = c.Int(nullable: false),
                        AssignedId = c.Int(nullable: false),
                        Description = c.String(),
                        AssignedDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Solution = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssueTypes", t => t.ProblemId)
                .ForeignKey("dbo.IssueTypes", t => t.SubTypeId)
                .ForeignKey("dbo.IssueTypes", t => t.TypeId)
                .ForeignKey("dbo.Users", t => t.UserCreatedId)
                .ForeignKey("dbo.UserTypes", t => t.AssignedId)
                .ForeignKey("dbo.Priorities", t => t.PriorityId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Pos", t => t.PosId)
                .Index(t => t.PosId)
                .Index(t => t.TypeId)
                .Index(t => t.SubTypeId)
                .Index(t => t.ProblemId)
                .Index(t => t.StatusId)
                .Index(t => t.PriorityId)
                .Index(t => t.UserCreatedId)
                .Index(t => t.AssignedId);
            
            CreateTable(
                "dbo.IssueTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueLevel = c.Int(nullable: false),
                        ParrentIssue = c.Int(nullable: false),
                        IssueNameId = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssueNames", t => t.IssueNameId)
                .Index(t => t.IssueNameId);
            
            CreateTable(
                "dbo.IssueNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Action = c.String(),
                        Notes = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .Index(t => t.IssueId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Telephone = c.String(),
                        UserTypeId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssuePriority = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PosWeekDays",
                c => new
                    {
                        PosId = c.Int(nullable: false),
                        WeekDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PosId, t.WeekDayId })
                .ForeignKey("dbo.WeekDays", t => t.WeekDayId, cascadeDelete: true)
                .ForeignKey("dbo.Pos", t => t.PosId, cascadeDelete: true)
                .Index(t => t.PosId)
                .Index(t => t.WeekDayId);
            
            CreateTable(
                "dbo.WeekDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pos", "CityId", "dbo.Cities");
            DropForeignKey("dbo.PosWeekDays", "PosId", "dbo.Pos");
            DropForeignKey("dbo.PosWeekDays", "WeekDayId", "dbo.WeekDays");
            DropForeignKey("dbo.Issues", "PosId", "dbo.Pos");
            DropForeignKey("dbo.Issues", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Issues", "PriorityId", "dbo.Priorities");
            DropForeignKey("dbo.Logs", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Issues", "AssignedId", "dbo.UserTypes");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Issues", "UserCreatedId", "dbo.Users");
            DropForeignKey("dbo.Issues", "TypeId", "dbo.IssueTypes");
            DropForeignKey("dbo.Issues", "SubTypeId", "dbo.IssueTypes");
            DropForeignKey("dbo.Issues", "ProblemId", "dbo.IssueTypes");
            DropForeignKey("dbo.IssueTypes", "IssueNameId", "dbo.IssueNames");
            DropForeignKey("dbo.Pos", "ConnectionTypeId", "dbo.ConnectionTypes");
            DropIndex("dbo.PosWeekDays", new[] { "WeekDayId" });
            DropIndex("dbo.PosWeekDays", new[] { "PosId" });
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Logs", new[] { "IssueId" });
            DropIndex("dbo.IssueTypes", new[] { "IssueNameId" });
            DropIndex("dbo.Issues", new[] { "AssignedId" });
            DropIndex("dbo.Issues", new[] { "UserCreatedId" });
            DropIndex("dbo.Issues", new[] { "PriorityId" });
            DropIndex("dbo.Issues", new[] { "StatusId" });
            DropIndex("dbo.Issues", new[] { "ProblemId" });
            DropIndex("dbo.Issues", new[] { "SubTypeId" });
            DropIndex("dbo.Issues", new[] { "TypeId" });
            DropIndex("dbo.Issues", new[] { "PosId" });
            DropIndex("dbo.Pos", new[] { "ConnectionTypeId" });
            DropIndex("dbo.Pos", new[] { "CityId" });
            DropTable("dbo.WeekDays");
            DropTable("dbo.PosWeekDays");
            DropTable("dbo.Status");
            DropTable("dbo.Priorities");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Logs");
            DropTable("dbo.IssueNames");
            DropTable("dbo.IssueTypes");
            DropTable("dbo.Issues");
            DropTable("dbo.ConnectionTypes");
            DropTable("dbo.Pos");
            DropTable("dbo.Cities");
        }
    }
}
