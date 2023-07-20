namespace CyberSecurity_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Users(username,password)Values('Ahmed','W4RMNbHfAkp/8ez5ySJ6bQ==')");
            Sql("INSERT INTO Users(username,password)Values('Ali','p8ydy2g0SNBZXlBfeOD9kg==')");
            Sql("INSERT INTO Users(username,password)Values('Mona','3BwqCOLbGu+lYQEAZUBc1Q==')");
        }
        
        public override void Down()
        {
        }
    }
}
