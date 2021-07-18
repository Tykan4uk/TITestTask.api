using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskApi.Migrations
{
    public partial class RoleIntegerRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Role_RoleId",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Account_RoleId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Role_RoleName",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Role");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Role",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Account",
                type: "int",
                nullable: false);

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

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Role",
                type: "nvarchar(450)",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Account",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleName",
                table: "Role",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Role_RoleId",
                table: "Account",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
