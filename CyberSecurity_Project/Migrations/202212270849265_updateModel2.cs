namespace CyberSecurity_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModel2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "loginAttempts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "loginAttempts", c => c.Int(nullable: false));
        }
    }
}
