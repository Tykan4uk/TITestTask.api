using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskApi.Migrations
{
    public partial class RoleAddingUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleName",
                table: "Role",
                column: "RoleName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_RoleName",
                table: "Role");
        }
    }
}
