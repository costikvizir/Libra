namespace Libra.Dal.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Issue_Name_Modifyied : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IssueNames", "IssueRank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IssueNames", "IssueRank");
        }
    }
}
