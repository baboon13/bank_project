using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank_project.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToLikeListData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeListData_ProductData_ProductNo",
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

            migrationBuilder.AddColumn<string>(
                name: "No",
                table: "LikeListData",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LikeListData_No",
                table: "LikeListData",
                column: "No");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeListData_ProductData_No",
                table: "LikeListData",
                column: "No",
                principalTable: "ProductData",
                principalColumn: "No",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeListData_ProductData_No",
                table: "LikeListData");

            migrationBuilder.DropIndex(
                name: "IX_LikeListData_No",
                table: "LikeListData");

            migrationBuilder.DropColumn(
                name: "No",
                table: "LikeListData");

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
        }
    }
}
