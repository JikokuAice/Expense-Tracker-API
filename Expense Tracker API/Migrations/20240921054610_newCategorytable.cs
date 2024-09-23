using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker_API.Migrations
{
    /// <inheritdoc />
    public partial class newCategorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseType",
                table: "Expense",
                newName: "CategoryType");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryExpense",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    expensesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExpense", x => new { x.categoryId, x.expensesId });
                    table.ForeignKey(
                        name: "FK_CategoryExpense_Expense_expensesId",
                        column: x => x.expensesId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryExpense_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExpense_expensesId",
                table: "CategoryExpense",
                column: "expensesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryExpense");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.RenameColumn(
                name: "CategoryType",
                table: "Expense",
                newName: "ExpenseType");
        }
    }
}
