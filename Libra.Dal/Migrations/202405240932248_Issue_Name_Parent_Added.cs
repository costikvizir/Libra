namespace Libra.Dal.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Issue_Name_Parent_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IssueNames", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IssueNames", "ParentId");
        }
    }
}
