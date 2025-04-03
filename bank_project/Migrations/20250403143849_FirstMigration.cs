using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank_project.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductData",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProductName = table.Column<string>(name: "Product Name", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Feerate = table.Column<decimal>(name: "Fee rate", type: "decimal(5,4)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductData", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password1 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.UserID);
                    table.UniqueConstraint("AK_UserData_Account", x => x.Account);
                });

            migrationBuilder.CreateTable(
                name: "LikeListData",
                columns: table => new
                {
                    SN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeListData", x => x.SN);
                    table.ForeignKey(
                        name: "FK_LikeListData_UserData_Account",
                        column: x => x.Account,
                        principalTable: "UserData",
                        principalColumn: "Account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeListData_Account",
                table: "LikeListData",
                column: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_UserData_Account",
                table: "UserData",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserData_Email",
                table: "UserData",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeListData");

            migrationBuilder.DropTable(
                name: "ProductData");

            migrationBuilder.DropTable(
                name: "UserData");
        }
    }
}
