namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Text;

    public partial class adddeleteninjaviaidsp : DbMigration
    {
        public override void Up()
        {
            StringBuilder storedProcedureCode = new StringBuilder();

            storedProcedureCode.Append("CREATE PROCEDURE DeleteNinjaViaId" + Environment.NewLine);
            storedProcedureCode.Append("@id int AS" + Environment.NewLine);
            storedProcedureCode.Append(@"DELETE FROM Ninjas WHERE id = @id");
            this.Sql(storedProcedureCode.ToString());
        }
        
        public override void Down()
        {
            this.Sql("DROP PROCEDURE DeleteNinjaViaId");
        }
    }
}
