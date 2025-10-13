using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proudcts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proudcts", x => x.Id);
                    table.CheckConstraint("CHK_CODE_NOT_EMPTY", "Code <> ''");
                    table.CheckConstraint("CHK_PRICE_POSITIVE", "Price >= 0.0");
                    table.CheckConstraint("CHK_STOCK_POSITIVE", "Stock >= 0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proudcts_Code",
                table: "Proudcts",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proudcts_Name",
                table: "Proudcts",
                column: "Name",
                unique: true,
                filter: "Name <> ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proudcts");
        }
    }
}
