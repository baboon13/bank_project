using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank_project.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIdToLikeListData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeListData_UserData_UserId",
                table: "LikeListData");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LikeListData",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "LikeListData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductNo",
                table: "LikeListData",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LikeListData_ProductNo",
                table: "LikeListData",
                column: "ProductNo");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeListData_ProductData_ProductNo",
                table: "LikeListData",
                column: "ProductNo",
                principalTable: "ProductData",
                principalColumn: "No",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeListData_UserData_UserId",
                table: "LikeListData",
                column: "UserId",
                principalTable: "UserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeListData_ProductData_ProductNo",
                table: "LikeListData");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeListData_UserData_UserId",
                table: "LikeListData");

            migrationBuilder.DropIndex(
                name: "IX_LikeListData_ProductNo",
                table: "LikeListData");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "LikeListData");

            migrationBuilder.DropColumn(
                name: "ProductNo",
                table: "LikeListData");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LikeListData",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeListData_UserData_UserId",
                table: "LikeListData",
                column: "UserId",
                principalTable: "UserData",
                principalColumn: "Id");
        }
    }
}
