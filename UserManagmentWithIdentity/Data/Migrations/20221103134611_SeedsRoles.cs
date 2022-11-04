using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace UserManagmentWithIdentity.Data.Migrations
{
    public partial class SeedsRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] {"Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values:new object[] {Guid.NewGuid().ToString(),"User","User".ToUpper(), Guid.NewGuid().ToString() },
                schema: "Secuirty"
             );

     migrationBuilder.InsertData(
      table: "Roles",
      columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
      values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() },
      schema: "Secuirty"
   );



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Secuirty].[Roles]");
        }
    }
}
