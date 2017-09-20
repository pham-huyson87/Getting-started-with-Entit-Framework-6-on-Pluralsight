namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Text;

    public partial class addgetoldninjassp : DbMigration
    {
        public override void Up()
        {
            StringBuilder storedProcedureCode = new StringBuilder();

            storedProcedureCode.Append("CREATE PROCEDURE GetOldNinjas AS" + Environment.NewLine);
            storedProcedureCode.Append(@"SELECT * FROM Ninjas WHERE DateOfBirth <= '1/1/1980'");
            this.Sql(storedProcedureCode.ToString());
        }
        
        public override void Down()
        {
            this.Sql("DROP PROCEDURE GetOldNinjas");
        }
    }
}
