namespace CyberSecurity_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwoFactorAuth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TwoFactorEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TwoFactorEnabled");
        }
    }
}
