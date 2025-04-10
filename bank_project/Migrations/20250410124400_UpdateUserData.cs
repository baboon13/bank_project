using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank_project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserData_Email",
                table: "UserData");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserData",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_UserData_Email",
                table: "UserData",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserData_Email",
                table: "UserData");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserData",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserData_Email",
                table: "UserData",
                column: "Email",
                unique: true);
        }
    }
}
