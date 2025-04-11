using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank_project.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToLikeListData_RemoveOrderName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "LikeListData");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "LikeListData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "LikeListData");

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "LikeListData",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
