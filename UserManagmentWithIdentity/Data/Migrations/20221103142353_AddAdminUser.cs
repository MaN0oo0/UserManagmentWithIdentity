using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagmentWithIdentity.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Secuirty].[Users] ([Id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],[ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled],[AccessFailedCount],[FirstName],[LastName] ,[ProfilePicture]) VALUES  ('9016cc9b-5e78-4c4d-8724-4efb4a2be9d3','admin' ,'ADMIN' ,'admin@admin.com','ADMIN@ADMIN.COM',0,'AQAAAAEAACcQAAAAEOa6gAKTFSwhleiRZvmn4G+qAFrOb6UrCbMzc5Wq3+/O1HlGIRt1lZuVIEVLuIMz7w==','4S6Q42KL5KFXMIWU7YG4KIFBDS44HF7U','0f5018a0-8f7e-46d2-b9ca-ddb55fc8011a',NULL,0,0,NULL,1,0,'Mohamed','Orban' ,NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Secuirty].[Users] WHERE Id='9016cc9b-5e78-4c4d-8724-4efb4a2be9d3' ");
        }
    }
}
