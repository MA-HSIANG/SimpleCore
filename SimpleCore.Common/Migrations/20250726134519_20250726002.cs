using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleCore.Common.Migrations
{
    /// <inheritdoc />
    public partial class _20250726002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleEntity",
                table: "UserRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserIninfoEntity",
                table: "UserIninfoEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenuModuleEntity",
                table: "RoleMenuModuleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleEntity",
                table: "ModuleEntity");

            migrationBuilder.RenameTable(
                name: "UserRoleEntity",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserIninfoEntity",
                newName: "UserIninfo");

            migrationBuilder.RenameTable(
                name: "RoleMenuModuleEntity",
                newName: "RoleMenuModule");

            migrationBuilder.RenameTable(
                name: "RoleEntity",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "ModuleEntity",
                newName: "Module");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserIninfo",
                table: "UserIninfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenuModule",
                table: "RoleMenuModule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Module",
                table: "Module",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserIninfo",
                table: "UserIninfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenuModule",
                table: "RoleMenuModule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Module",
                table: "Module");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoleEntity");

            migrationBuilder.RenameTable(
                name: "UserIninfo",
                newName: "UserIninfoEntity");

            migrationBuilder.RenameTable(
                name: "RoleMenuModule",
                newName: "RoleMenuModuleEntity");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "RoleEntity");

            migrationBuilder.RenameTable(
                name: "Module",
                newName: "ModuleEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleEntity",
                table: "UserRoleEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserIninfoEntity",
                table: "UserIninfoEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenuModuleEntity",
                table: "RoleMenuModuleEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleEntity",
                table: "ModuleEntity",
                column: "Id");
        }
    }
}
